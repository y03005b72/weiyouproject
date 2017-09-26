using com.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ruanmou
{
    public class travel_guide : BaseModel
    {
        public travel_guide()
        {
            PrimaryKey = "guide_guideId";
            DataBaseName = DataBaseEnum.Travel;
        }
        public int shopper_shopId { get; set; }
        public int guide_guideId { get; set; }
        public string guide_dp { get; set; }
        public string guide_IdCard { get; set; }
        public string guide_guidecardId { get; set; }
        public string guide_name { get; set; }
        public string guide_introduce { get; set; }
    }
}
