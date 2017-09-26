using com.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ruanmou
{
    public class travel_admin : BaseModel
    {
        public travel_admin()
        {
            PrimaryKey = "admin_id";
            DataBaseName = DataBaseEnum.Travel;
        }
        public int admin_id { get; set; }
        public string admin_password { get; set; }
        public string admin_dp { get; set; }
    
    }
}
