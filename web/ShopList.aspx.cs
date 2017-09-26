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
    public partial class ShopList : System.Web.UI.Page
    {
        private int _page;

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
            s = PagerDAL.GetPagerHtml(page, 10, travel_shopperDAL.m_travel_shopperDal.GetCount("1=1"));
            return s;
        }
        public string GetShopList()
        {
            StringBuilder sb = new StringBuilder();
            List<travel_shopper> list = travel_shopperDAL.m_travel_shopperDal.GetList("1=1", 10, page, true, "*");
            foreach (var r in list)
            {
                string txt = CRegex.FilterHTML(r.shopper_content).Length > 120 ? CRegex.FilterHTML(r.shopper_content).Substring(0, 120) + "..." : CRegex.FilterHTML(r.shopper_content);
                sb.Append(string.Format(@"<a href=""TripList.aspx?id={0}""><div class=""shopname"" style=""font-size: 20px;font-weight:800;"">{1}</div>
                <div class=""shopdp""><img src=""{2}""></div>
                <div class=""shopcontent"" style=""text-indent:28px;"">{3}</div></a>",r.shopper_shopId, r.shopper_shopname, r.shopper_dp, r.shopper_content));
            }
            return sb.ToString();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (System.Web.HttpContext.Current.Request.Cookies["username"] == null)
            {
                Response.Redirect("UserLogin.aspx");
            }
        }
    }
}