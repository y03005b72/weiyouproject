using com.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ruanmou
{
    public class travel_shopper : BaseModel
    {
        public travel_shopper()
        {
            PrimaryKey = "shopper_shopId";
            DataBaseName = DataBaseEnum.Travel;
        }
        public int shopper_shopId { get; set; }
        public string shopper_password { get; set; }
        public string shopper_shopname { get; set; }
        public string shopper_tel { get; set; }
        public string shopper_dp { get; set; }
        public string shopper_content { get; set; }
    }
}
