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
    public partial class ShopOpera : System.Web.UI.Page
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
            s = PagerDAL.GetPagerHtml(page, 10, travel_guideDAL.m_travel_guideDal.GetCount("1=1"));
            return s;
        }
        public string GetGuideList()
        {
            StringBuilder sb = new StringBuilder();
            List<travel_guide> list = travel_guideDAL.m_travel_guideDal.GetList("1=1", 10, page, true, "*");
            foreach (var r in list)
            {
                string txt = CRegex.FilterHTML(r.guide_introduce).Length > 120 ? CRegex.FilterHTML(r.guide_introduce).Substring(0, 120) + "..." : CRegex.FilterHTML(r.guide_introduce);
                sb.Append(string.Format(@"<div class=""guidedp""><img src=""{0}""></div>
                <div class=""guidecardId"">导游证编号：{1}</div>
                <div class=""guidename"">姓名：{2}</div>
                <div class=""guideintroduce"">导游简介：{3}</div>", r.guide_dp, r.guide_guidecardId, r.guide_name, r.guide_introduce));
            }
            return sb.ToString();
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}