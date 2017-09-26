using com.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ruanmou
{
    public class travel_assess : BaseModel
    {
        public travel_assess()
        {
            PrimaryKey = "trip_tripId";
            DataBaseName = DataBaseEnum.Travel;
        }
        public int trip_tripId { get; set; }
        public int user_id { get; set; }
        public string assess_content { get; set; }
        public int assess_level { get; set; }
        public DateTime assess_date { get; set; }
    }
}
