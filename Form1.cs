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
            cam = new CameraScan(cbbCam, 0, pbCam);
            fb = new FirebaseUtil();

        }
        CameraScan camQR;
        CameraScan cam;
        FirebaseUtil fb;

        //false là ok để quét
        //true là dừng lại để xử lý
        bool _stopScan = false;

        //Đếm số lần scan sai 
        int _timesScanPlate = 1;
        int _waitGuard = -1;

        //1 là line vào
        //2 là line ra
        int _lineInOut = 1;
        const int _line = 1;
        
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
            Result result = barcodeReader.Decode((Bitmap)pbCamQr.Image.Clone());
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
        void userIn(UserDTO user)
        {
            fb.pushUserIn(user);
            if (user.status == 0)
            {
                messNoti("Wait for guard ... !", true);
                _waitGuard = 0;
                waitAccep(x, user);
                return;
            }
            messNoti("OK ! You pass", true);
            //
            reset();
        }
        void userOut(UserDTO user)
        {
            if(!_isParking) { messNoti("You're not parking here", false); return; }
            
            if(user.status == 0)
            {
                messNoti("Wait for guard ... !", true);
                _waitGuard = 0;
                fb.setCodeErrAndlineOutInAndIdlistErr(user);
                fb.pushUserErr(user);
                waitAccep(x, user);
                return;
            }
            
            switch (fb.pushUserOut(user,_payMoney,lbStatus))
            {
                case 1: messNoti("Number plates are not the same as when you entered", false); lbTimesScan.Text = _timesScanPlate + ""; _timesScanPlate++;  _stopScan = false; break;
                case 2: messNoti("OK ! Have a good day !", false); reset(); return;
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
        private void timer1_Tick(object sender, EventArgs e)
        {

            if (_waitGuard != -1)
            {

                switch (_waitGuard)
                {
                    case 0: return;
                    case 1: messNoti("Guard not accept !", false);  reset(); break;
                    case 2: messNoti("Guard accept ! You pass", false); reset(); break;
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
            

            _stopScan = true;
            _lineInOut=rbCamIn.Checked ? 1 : 0;
            if (_qr.Length==1) { messNoti("Update your QR !", false); _stopScan = false; return; }
            //diffirent session user
            if (_user != null && !_qr[0].Equals(_user.id) ) reset();
            //assigned license plate
            _plate = _detector.ReadPlate((Bitmap)pbCam.Image.Clone());
            pbPlate.Image = _plate.bitmap;
            lbTxtLicense.Text = "Reading...";
            //check user valid
            if (!fb.checkUser(_qr[0],_qr[3])) { messNoti("You are not student of DTU !",false); _stopScan = false; reset(); return; }
            //Check out of money
            if (fb.checkPoor(_qr[0],_payMoney))
            {
                switch (_lineInOut)
                {
                    case 0:
                        messNoti("You're out of money !", false); _stopScan = false;
                        reset();
                        return;
                    case 1: //warning to phone
                        Console.WriteLine("warning to phone");
                        break;
                }
            } 
            //Set lable information
            lbId.Text = _qr[1];
            lbName.Text = _qr[2];
            
            //check parking
            _isParking = fb.checkParking(_qr[0], BLOCK);
            if (_lineInOut==1 && _isParking) { messNoti("You are parking !", false); _stopScan = false; reset(); return; }

            //check valid license plate
            messNoti("Detecting license plate !",true);
            if ((!_plate.hasPlate || _plate.text.IndexOf('_') != -1)&& _timesScanPlate<=3) { lbTimesScan.Text = _timesScanPlate+""; _timesScanPlate++; messNoti("Not detect license plate !!", false); _stopScan = false; return; }

            //check again license plate
            if (_plate.text.IndexOf('_') != -1 || !_plate.isValid)
            {
                _textLicense = "NOD"; _codeErr = 0;
            }
            else _textLicense = _plate.text;

            lbTxtLicense.Text = _textLicense;

            //assigned motorbike owner
            _user = new UserDTO {
                id = _qr[0],
                idT = _qr[1],
                name = _qr[2],
                block = BLOCK,
                codeErr = _codeErr,
                dateSend = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                line = _line,
                status = _textLicense.Equals("NOD") ? 0 : 2,
                txtPlate = _textLicense,
                lineOutIn = _lineInOut
            };


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

        public void addImage(string id,Image plate,bool status)
        {
            string fd;
            fd = status ? "imgOK" : "imgErr";

            var ms = new MemoryStream();
            var task = new FirebaseStorage("dtuparking.appspot.com")
            .Child("Plate")
            .Child(fd)
            .Child(id+".png")
            .PutAsync(GetImgUrl.ToStream(plate,ImageFormat.Png));
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
