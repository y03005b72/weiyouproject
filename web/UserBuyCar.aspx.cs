using com.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace web
{
    public partial class UserBuyCar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public string GetBuycarList()
        {
            string listTitle = Session["listTitle"].ToString();
            string listPrice = Session["listPrice"].ToString();
            string listPicture = Session["listPicture"].ToString();
            string listTxt = Session["listTxt"].ToString();
            StringBuilder sb = new StringBuilder();
            string txt = CRegex.FilterHTML(listTxt).Length > 120 ? CRegex.FilterHTML(listTxt).Substring(0, 120) + "..." : CRegex.FilterHTML(listTxt);
                string tit = CRegex.FilterHTML(listTitle).Length > 25 ? CRegex.FilterHTML(listTitle).Substring(0, 25) + "..." : CRegex.FilterHTML(listTitle);
            sb.Append(string.Format(@"<div class=""tripcard""><div class=""trippicture""><img src=""{0}""></div>
                <div class=""triptitle""><span style=""font-size: 20px;font-weight:800;"">{1}</span></div>
                <div class=""tripcontent"" style=""text-indent:28px;"">{2}</div>
                <div class=""tripprice"">￥{3}<span style=""font-size: 16px;margin-left: 3px;"">起</span></div><a class=""tripcount"" href=""UserPay.aspx"">立即支付</a></div>", listPicture, tit, txt, listPrice));
            return sb.ToString();
        }
    }
}