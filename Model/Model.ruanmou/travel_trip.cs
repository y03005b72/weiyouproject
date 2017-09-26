using com.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ruanmou
{
    public class travel_trip : BaseModel
    {
        public travel_trip()
        {
            PrimaryKey = "trip_tripId";
            DataBaseName = DataBaseEnum.Travel;
        }
        public int trip_tripId { get; set; }
        public int guide_guideId { get; set; }
        public string trip_name { get; set; }
        public double trip_price { get; set; }
        public string trip_content { get; set; }
        public string trip_picutur { get; set; }
        public int trip_count { get; set; }
        public string trip_startaddress { get; set; }
        public string trip_endaddress { get; set; }
    }
}
