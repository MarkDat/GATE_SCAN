using Firebase.Storage;
using FireSharp.Response;
using GATE_SCAN2.Common;
using GATE_SCAN2.Database.Firebase;
using GATE_SCAN2.Models.DTO;
using IPSS;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing;

namespace GATE_SCAN2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            camQR = new CameraScan(cbbQr, 0, pbCamQr);
            cam = new CameraScan(cbbCam, 2, pbCam); //Chỗ này điền 2
            fb = new FirebaseUtil();

            _sScan.SoundLocation = Application.StartupPath + @"\asset\song\scan.wav";
            _sErr.SoundLocation = Application.StartupPath + @"\asset\song\err.wav";
            _sOK.SoundLocation = Application.StartupPath + @"\asset\song\OKOK.wav";
            lbTimesScan.Text = _timesScanPlate + "";
            pbPlate.Image = Image.FromFile(Application.StartupPath + @"\asset\img\default\logo\logo.png");
            lbTxtLicense.Parent = panel1;
            lbId.Parent = panel1;
            lbName.Parent = panel1;
            lbStatus.Parent = panel1;
            lbTimesScan.Parent = panel1;
            label7.Parent = panel1;
            label6.Parent = panel1;
            label1.Parent = panel1;
        }
        CameraScan camQR;
        CameraScan cam;
        FirebaseUtil fb;

        SoundPlayer _sScan = new SoundPlayer();
        SoundPlayer _sOK = new SoundPlayer();
        SoundPlayer _sErr = new SoundPlayer();
        SoundPlayer _sAcp = new SoundPlayer();
        SoundPlayer _sNotAcp = new SoundPlayer();
        //false là ok để quét
        //true là dừng lại để xử lý
        bool _stopScan = false;

        //Đếm số lần scan sai 
        int _timesScanPlate = 1;
        int _waitGuard = -1;

        //1 là line vào
        //2 là line ra
        int _lineInOut = 1;
        int _line = 1;
        
        //Quet
        IPSSbike _detector = new IPSSbike();
        BikePlate _plate;

        const int BLOCK = 1;
        const int LINE = 1;
        const double _payMoney = 1000;

        EventStreamResponse x;

        #region process
        public string[] getQR()
        {
            Console.WriteLine("Get QR");
            BarcodeReader barcodeReader = new BarcodeReader();
            Result result=null;
            try
            {
                result = barcodeReader.Decode((Bitmap)pbCamQr.Image.Clone());
            }
            catch (Exception)
            {
                return null;
            }
           
            if (result == null) return null;
            string[] qr = result.ToString().Split('|');
            //qr[0]:id
            //qr[1]:idS
            //qr[2]:Name
            //qr[3]:secretNum
            try
            {
                Console.WriteLine("ID: " + qr[0]);
                Console.WriteLine("IDS: " + qr[1]);
                Console.WriteLine("Name: " + qr[2]);
                Console.WriteLine("Secret Num: " + qr[3]);
            }
            catch (Exception)
            {

                return new string[1];
            }
            
            return qr;

        }
        public void messNoti(string text,bool valid)
        {
            if (valid == false) _sErr.PlaySync();
            lbStatus.Invoke(new Action(() => {
                lbStatus.Text = text;
                
            }));

        }
        void sendToGuard(UserDTO user)
        {
                fb.setCodeErrAndlineOutInAndIdlistErr(user);
                fb.pushUserErr(user);
                waitAccep(x, user);
        }
        async void userIn(UserDTO user)
        {
            
             await fb.pushUserIn(user);
            if (user.status == 0)
            {
                messNoti("Wait for guard ... !", false);
                await addImage(user.id,pbPlate.Image,false,true);
                _waitGuard = 0;
                waitAccep(x, user);
                return;
            }
           await addImage(user.id, pbPlate.Image, true,true);
            _sOK.PlaySync();
            messNoti("OK ! You pass."+_outMoney, true);
            //
            reset();
        }
        async void userOut(UserDTO user)
        {
            if(!_isParking) { messNoti("You're not parking here", false);_stopScan = false; return; }
            
            if(user.status == 0)
            {
                messNoti("Wait for guard ... !", true);
              await addImage(user.id, pbPlate.Image, false, false);
                _waitGuard = 0;
                fb.setCodeErrAndlineOutInAndIdlistErr(user);
                fb.pushUserErr(user);
                waitAccep(x, user);
                return;
            }

            
           await addImage(user.id, pbPlate.Image, true, false);

            user.position = await fb.getPosition(user.id) + "";
            switch (fb.pushUserOut(user,_payMoney,lbStatus))
            {
                case 1: messNoti("Number plates are not the same as when you entered", false);lbTimesScan.Text = _timesScanPlate + ""; _timesScanPlate++;  _stopScan = false; break;
                case 2: messNoti("OK ! Have a good day !", true); _sOK.PlaySync(); reset(); return;
                case 3: messNoti("You run out of money!", false); reset(); break;
            }
            
        }
        #endregion

        private void Form1_Load(object sender, EventArgs e)
        {
            fb.getVersion();
            _detector.CropResultImage = true;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            camQR.stop();
            cam.stop();
        }

        string[] _qr=null;
        UserDTO _user = null;
        string _textLicense = "";
        int _codeErr=2;
        bool _isParking = false;
        string _outMoney = "";
        private async void timer1_Tick(object sender, EventArgs e)
        {

            if (_waitGuard != -1)
            {

                switch (_waitGuard)
                {
                    case 0: return;
                    case 1: messNoti("Guard not accept !", false);  reset(); break;
                    case 2: messNoti("Guard accept ! You pass", true); _sOK.PlaySync(); reset(); break;
                }
            }
            if (_stopScan) return;
            

            //PROCESS
            _qr = getQR();
            //qr[0]:id
            //qr[1]:idS
            //qr[2]:Name
            //qr[3]:secretNum  
            if (_qr== null) return;

            _sScan.PlaySync();
            _plate = _detector.ReadPlate((Bitmap)pbCam.Image.Clone());

            _stopScan = true;
            _lineInOut=rbCamIn.Checked ? 1 : 0;
            if (_qr.Length==1) { messNoti("Update your QR !", false); _stopScan = false; return; }
            //diffirent session user
            if (_user != null && !_qr[0].Equals(_user.id) ) reset();
            //assigned license plate
           
            pbPlate.Image = _plate.bitmap;
            lbTxtLicense.Text = "Reading...";
            //check user valid
            if (!await fb.checkUser(_qr[0], _qr[3])) { messNoti("Re-check your QR !",false); _stopScan = false; reset(); return; }
            //Check out of money
            if (fb.checkPoor(_qr[0],_payMoney))
            {
                switch (_lineInOut)
                {
                    case 0:
                        messNoti("Your wallet has 0 VND left ! Please come back !", false); _stopScan = false;
                        reset();
                        return;
                    case 1: //warning to phone
                        Console.WriteLine("warning to phone");
                        _outMoney = "Your account has 0 VND left";
                        break;
                }
            } 
            //Set lable information
            lbId.Text = _qr[1];
            lbName.Text = _qr[2];
            
            //check parking
            _isParking = fb.checkParking(_qr[0], BLOCK);
            if (_lineInOut==1 && _isParking) { messNoti("You are parking !", false); _stopScan = false; reset(); return; }
            if(_lineInOut==0 && !_isParking) { messNoti("You are not parking here!", false); _stopScan = false; reset(); return; }

            bool isInOK = true;
            //check valid license plate
            messNoti("Detecting license plate !",true);
            if ((!_plate.hasPlate || _plate.text.IndexOf('_') != -1)&& _timesScanPlate<=3) {
                lbTimesScan.Text = _timesScanPlate+""; _timesScanPlate++; 
                messNoti("Not detect license plate !!", false); lbTxtLicense.Text="NOD"; _stopScan = false; return; }

            //check again license plate
            if (_plate.text.IndexOf('_') != -1 || !_plate.isValid)
            {
                _textLicense = "NOD"; _codeErr = 0;
                if (_lineInOut == 1) isInOK = false;
            }
            else _textLicense = _plate.text;

            lbTxtLicense.Text = _textLicense;

            Console.WriteLine("CODEERRR "+ _codeErr);

            bool checkStatus = lbTxtLicense.Text.Equals("NOD") ? false:true;
           
            if (!lbTxtLicense.Text.Equals("NOD") && _lineInOut == 0)
            {
                bool checkPlate = await fb.checkSamePlate(_qr[0], BLOCK, lbTxtLicense.Text);
                if (_timesScanPlate <= 3)
                {
                    if (checkPlate == false)
                    {
                        messNoti("Number plates are not the same as when you entered!!ZZ", false);
                        lbTimesScan.Text = _timesScanPlate + "";
                        _timesScanPlate++;
                        _stopScan = false;
                        return;
                    }
                }
                else
                {
                    if (checkPlate == false)
                    {
                        Console.WriteLine("Lon hon 3");
                        checkStatus = false;
                    }
                }

               
            }

          

            if(_codeErr!=2 && _lineInOut == 1)
            {
                isInOK = false;
            }
            if (_lineInOut == 0)
            {
                isInOK = await fb.isInOK(_qr[0],BLOCK);
            }

            _stopScan = true;
            //assigned motorbike owner
            _user = new UserDTO {
                id = _qr[0],
                idT = _qr[1],
                name = _qr[2],
                block = BLOCK,
                codeErr = _codeErr,
                dateSend = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                line = _line,
                status = !checkStatus ? 0 : 2,
                txtPlate = _textLicense,
                lineOutIn = _lineInOut,
                isInOK =isInOK
            };

            Console.WriteLine("NÔTKOKOKOKOKOKO " + _user.status);

            //Choose form to process
            if (rbCamIn.Checked)
            {
                messNoti("Waiting...", true);

                userIn(_user);

            }
            else
            {
                messNoti("Waiting...", true);
                userOut(_user);
            }

        }

        public async Task<bool> addImage(string id,Image plate,bool status,bool isIn)
        {
            string fd;
            string inOrOut;
            fd = status ? "imgOK" : "imgErr";
            inOrOut = isIn ? "in":"out";
            var task = await new FirebaseStorage("dtuparking.appspot.com")
            .Child("Plate")
            .Child(fd)
            .Child(id+"-"+inOrOut+".jpg")
            .PutAsync(GetImgUrl.ToStream(plate,ImageFormat.Jpeg));
            return true;
        }
        public void reset()
        {
            _qr = null;
            _user = null;
            _textLicense = "";
            _codeErr = 2;
            _waitGuard = -1;
            _timesScanPlate = 1;
            _stopScan = false;
            _isParking = false;
            lbTimesScan.Text = _timesScanPlate + "";
            _outMoney = "";
        }
        public async void waitAccep(EventStreamResponse x, UserDTO user)
        {
            x = await fb.getCL().OnAsync("APIParking/Parking/InfoList/" + user.block + "/" + user.id + "/status", changed: (s, args, d) =>
            {
                switch (args.Data)
                {
                    case "2":
                        _waitGuard = 2;

                        x.Dispose(); return;
                    case "1":
                        _waitGuard = 1;
                        x.Dispose();
                        return;
                    default: return;
                }
               
            });
        }
    }
}
