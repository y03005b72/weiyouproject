using com.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ruanmou
{
    public class travel_user : BaseModel
    {
        public travel_user()
        {
            PrimaryKey = "user_id";
            DataBaseName = DataBaseEnum.Travel;
        }
        public int user_id { get; set; }
        public string user_username { get; set; }
        public string user_realname { get; set; }
        public string user_password { get; set; }
        public string user_sex { get; set; }
        public string user_email { get; set; }
        public string user_tel { get; set; }
        public string user_address { get; set; }
        public string user_IdCard { get; set; }
        public string user_qq { get; set; }
        public string user_dp { get; set; }
        public DateTime user_createdtime { get; set; }
    
    }
}
