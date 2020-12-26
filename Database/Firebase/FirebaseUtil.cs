using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using GATE_SCAN2.Models.DTO;
using shortid;
using shortid.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GATE_SCAN2.Database.Firebase
{
    public class FirebaseUtil
    {
         IFirebaseConfig config;
         IFirebaseClient cl;
        public FirebaseUtil()
        {
            config = new FirebaseConfig
            {
                AuthSecret = "60NwcUMNV5rvqZSD6adULFT2tkfJgRT4cbw4Q0hs",
                BasePath = "https://dtuparking.firebaseio.com/"
            };
            cl = new FirebaseClient(config);
        }
        public IFirebaseClient getCL()
        {
            return cl;
        }
        public string getVersion()
        {
            try
            {

            }
            catch (Exception)
            {
                
            }
            FirebaseResponse res = cl.Get(@"Version");
            if (res.Body.Equals("null")) return "";
            return res.ResultAs<string>();
        }

        public async Task<bool> checkUser(string id,string secretNum)
        {
            FirebaseResponse res = await cl.GetAsync(@"User/information/parkingMan/" + id+ "/secretNum");
            if (res.Body.Equals("null")) return false;
            string sc = res.ResultAs<string>();
            Console.WriteLine(sc);
            return secretNum.Trim().Equals(sc.Trim());
        }
        public bool checkPoor(string id,double quota)
        {
            int position = getPositionUser(id);
            //Nếu là không phải sinh viên thì k add, trả về 1 OK
            if (position != 3) return false;
            double money = getMoney(id);
            if (money < quota) return true;
            return false; //OK
        }
        public bool checkParking(string id,int block)
        {
            FirebaseResponse res = cl.Get(@"APIParking/Parking/InfoList/"+block+"/" + id+ "/parking");
            if (res.Body.Equals("null")) return false;
            bool check = res.ResultAs<bool>();
            return check;
        }
        public int getPositionUser(string id)
        {
            FirebaseResponse res = cl.Get(@"User/information/parkingMan/" + id + "/position");
            if (res.Body.Equals("null")) return -1;
            return res.ResultAs<int>();
        }
        public double getMoney(string id)
        {
            FirebaseResponse res = cl.Get(@"User/information/parkingMan/" + id + "/money");
            if (res.Body.Equals("null")) return -1;
            return res.ResultAs<double>();
        }
        public async Task<int> getPosition(string id)
        {
            FirebaseResponse res = await cl.GetAsync(@"User/information/parkingMan/" + id + "/position");
            if (res.Body.Equals("null")) return -1;
            return res.ResultAs<int>();
        }

        public bool payMoney(string id,double payMoney)
        {
            int position = getPositionUser(id);
            //Nếu là không phải sinh viên thì k add, trả về 1 OK
            if (position != 3) return true;
            double money = getMoney(id);
            if (money < payMoney) return false;
            money -= payMoney;
            FirebaseResponse res = cl.Set(@"User/information/parkingMan/" + id + "/money", money+"");
            if (res.Body.Equals("null")) return false; // Loi thanh toan
            return true; //OK
        }
       
        public bool addHistory(string id,string plate,double money,int block)
        {
            var options = new GenerationOptions
            {
                UseNumbers = true,
                Length = 9,
                UseSpecialCharacters = false
            };
            //Lấy ngày gửi về
            FirebaseResponse res = cl.Get(@"APIParking/Parking/InfoList/" + block + "/" + id + "/dateSend");
            if (res.Body.Equals("null")) return false;
            string dateS = res.ResultAs<string>();

            HistoryDTO his = new HistoryDTO {
                idPay = ShortId.Generate(options),
                dateSend = dateS,
                method = 0,
                place = block,
                plateLicense = plate,
                payMoney = money + ""
        };
            cl.Push(@"History/parkingMan/moneyOut/" + id,his);
            return true;
        }
        public async Task<int> pushUserIn(UserDTO user)
        {
            if (checkParking(user.id,user.block)) return 2; //dang do
            user.position = await getPosition(user.id) + "";
            FirebaseResponse res = cl.Update(@"APIParking/Parking/InfoList/" + user.block + "/" + user.id,user);
            FirebaseResponse resId = cl.Set(@"APIParking/Parking/IdList/" + user.block + "/" + user.id,user.id);
            if (res.Body.Equals("null")) return 0; // Loi add
            if (resId.Body.Equals("null")) return 0; // Loi add
            return 1; //OK
        }
        public bool removeUserIn(string id,int block)
        {
            FirebaseResponse res = cl.Delete(@"APIParking/Parking/IdList/" + block + "/" + id);
            FirebaseResponse resId = cl.Delete(@"APIParking/Parking/InfoList/" + block + "/" + id);
            if (res.Body.Equals("null")) return false; // Loi dlt
            if (resId.Body.Equals("null")) return false; // Loi dlt
            return true;
        }
        public async Task<bool> isInOK(string id,int block)
        {
            FirebaseResponse res = await cl.GetAsync(@"APIParking/Parking/InfoList/" + block + "/" + id + "/isInOK");
            if (res.Body.Equals("null")) return false;
            return res.ResultAs<bool>();
        }
        public string getPlate(string id,int block)
        {
            FirebaseResponse res = cl.Get(@"APIParking/Parking/InfoList/" + block + "/" + id + "/txtPlate");
            if (res.Body.Equals("null")) return "";
            return res.ResultAs<string>();
        }
       
        public async Task<bool> checkSamePlate(string id,int block,string newPlate)
        {
            FirebaseResponse res = await cl.GetAsync(@"APIParking/Parking/InfoList/" + block + "/" + id + "/txtPlate");
            if (res.Body.Equals("null")) return false;

            if (!newPlate.Equals(res.ResultAs<string>())) return false;

            return true;
        }
        
        public int pushUserOut(UserDTO user, double money,Label lbMess)
        {
           
            //Chưa so sánh biển số

            //User OUT
            //Trừ tiền (nếu là student)
            //add history user
            //add chart
            //xóa đang đỗ APIParking

            string plate = getPlate(user.id, user.block);
            if (string.IsNullOrEmpty(plate) || !plate.Equals(user.txtPlate)) return 1;

           // lbMess.Text = "Paying...";
            //if (checkPoor(user.id,money)) return 3;//Lỗi tanh toán

            //Bất đồng bộ xử lý 2 cái này để giảm thời gian UI
            Thread t = new Thread(() => {
                payMoney(user.id,money);
                addHistory(user.id, user.txtPlate, int.Parse(user.position)==3 ? money:0, user.block);
                removeUserIn(user.id, user.block);
            });
            t.Start();
           
            return 2;
        }
        public void pushUserErr(UserDTO user)
        {
            FirebaseResponse resId = cl.Set(@"APIParking/Parking/IdListErrOut/" + user.block + "/" + user.id, user.id);
        }
        public void reSetStatus(UserDTO user)
        {
            user.status = 2;
            FirebaseResponse resId = cl.Set("APIParking/Parking/InfoList/" + user.block + "/" + user.id + "/status", user.status);
        }
        public void setCodeErrAndlineOutInAndIdlistErr(UserDTO user)
        {
            user.lineOutIn = 0;
            user.status = 0;
            user.parking = true;
            FirebaseResponse resId = cl.Update("APIParking/Parking/InfoList/" + user.block + "/" + user.id, user);
        }

        public void removeUserErr(UserDTO user)
        {
            FirebaseResponse res = cl.Delete(@"APIParking/Parking/IdListErrOut/" + user.block + "/" + user.id);
        }
    }
}
