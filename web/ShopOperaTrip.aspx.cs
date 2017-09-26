using com.Utility;
using DAL.ruanmou;
using Model.ruanmou;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace web
{
    public partial class ShopOperaTrip : System.Web.UI.Page
    {
        private int _page;
        private int _id;

        public int Id
        {
            get
            {
                try
                {
                    _id = Request.QueryString["id"] == null ? 0 : Convert.ToInt32(Request.QueryString["id"].ToString());
                }
                catch (Exception ex)
                {
                    _id = 0;
                }
                return _id;
            }
            set { _id = value; }
        }
        public int page
        {
            get
            {
                try
                {
                    _page = Request.QueryString["page"] == null ? 1 : Convert.ToInt32(Request.QueryString["page"].ToString());
                }
                catch
                {
                    _page = 1;
                }
                return _page;
            }
            set { _page = value; }
        }

        public string GetPagerHtml()
        {
            string s = "";
            s = PagerDAL.GetPagerHtml(page, 10, travel_tripDAL.m_travel_tripDal.GetCount("1=1"));
            return s;
        }
        public string GetTripList()
        {
            StringBuilder sb = new StringBuilder();
            List<travel_trip> list = travel_tripDAL.m_travel_tripDal.GetList("1=1", 10, page, true, "*");
            foreach (var r in list)
            {
                string txt = CRegex.FilterHTML(r.trip_content).Length > 120 ? CRegex.FilterHTML(r.trip_content).Substring(0, 120) + "..." : CRegex.FilterHTML(r.trip_content);
                string tit = CRegex.FilterHTML(r.trip_name).Length > 25 ? CRegex.FilterHTML(r.trip_name).Substring(0, 25) + "..." : CRegex.FilterHTML(r.trip_name);
                sb.Append(string.Format(@"<a href=""TripLiskItem.aspx?id={0}""><div class=""trippicture""><img src=""{1}""></div>
                <div class=""triptitle""><span style=""font-size: 20px;font-weight:800;"">{2}</span></div>
                <div class=""tripcontent"" style=""text-indent:28px;"">{3}</div>
                <div class=""tripprice"">￥{4}<span style=""font-size: 16px;margin-left: 3px;"">起</span></div>
                <div class=""tripcount"">参与人数：{5}</div></a>", r.trip_tripId, r.trip_picutur, tit, txt, r.trip_price, r.trip_count));
            }
            return sb.ToString();
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}