using com.DAL.Base;
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
    public partial class TripLiskItem : System.Web.UI.Page
    {
        private int _id;
        private int _page;
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (System.Web.HttpContext.Current.Request.Cookies["username"] == null)
            {
                Response.Redirect("UserLogin.aspx");
            }
        }

        public string GetTriplist()
        {
            StringBuilder sb = new StringBuilder();
            travel_trip rn = travel_tripDAL.m_travel_tripDal.GetModel("trip_tripId=@trip_tripId", new List<dbParam>(){
            new dbParam(){ParamName="trip_tripId",ParamValue=Id}
            });
            Session["city"] = rn.trip_startaddress;
            List<travel_assess> list = travel_assessDAL.m_travel_assessDal.GetList("1=1", 10, page, true, "*");
            if (rn != null)
            {
                sb.Append(string.Format(@"<div class=""item_content"">
        <div class=""item_title""><span style=""font-size: 20px;font-weight:800;line-height:50px;"" id=""listTitle"">{0}</span></div>
        <div class=""item_picture""><img src=""{1}"" id=""listPicture""></div>", rn.trip_name,rn.trip_picutur));
                sb.Append(string.Format(@"<div class=""item_table""><table>
<tr><td style=""font-size: 20px;font-weight:500;text-align:right"">起点：</td><td style=""font-size: 20px;font-weight:500;""><a href=""CityWeather.aspx"">{0}</a></td></tr>
<tr><td style=""font-size: 20px;font-weight:500;text-align:right"">终点：</td><td style=""font-size: 20px;font-weight:500;"">{1}</td></tr>
<tr><td style=""font-size: 20px;font-weight:500;text-align:right"">购买人数：</td><td style=""font-size: 20px;font-weight:500;"">{2}</td></tr></table></div>", rn.trip_startaddress, rn.trip_endaddress, rn.trip_count));
                sb.Append(string.Format(@"<div class=""item_context"" style=""text-indent:36px;font-size:18px;""  id=""listTxt"">{0}</div>
        <div class=""item_price"" style=""font-size: 20px;line-height:50px;text-align:center;"">￥<span id=""listPrice"">{1}</span><span style=""font-size: 16px;margin-left: 3px;"">起</span></div>
        <div class=""item_buycar""><input type=""button"" style=""width:150px;height:50px;color:#ffffff;background-color:#35B558;"" id=""btnAddbuycar"" onclick=""AddBuyCar()"" value=""加入购物车""/></div>
        <div class=""item_buynow""><a href=""UserPay.aspx""><input type=""button"" style=""width:150px;height:50px;color:#ffffff;background-color:#35B558;"" id=""btnbuynow"" value=""立即购买""/></a></div>
    </div>", rn.trip_content,rn.trip_price));       
                foreach (var r in list)
                {
                    travel_user rm = travel_userDAL.m_travel_userDal.GetModel("user_id=@user_id", new List<dbParam>(){
            new dbParam(){ParamName="user_id",ParamValue=r.user_id}
            });
                    sb.Append(string.Format(@"<div class=""assess_zone"">
        <div class=""assess_dp""><img src=""{0}""></div>
        <div class=""assess_user"">{1}</div>
        <div class=""assess_sex"">{2}</div>
<div class=""assess_level"">评级：{3}</div>
        <div class=""assess_time"">{4}</div>
        <div class=""assess_content"">{5}</div>     
    </div>", rm.user_dp, rm.user_username, rm.user_sex,r.assess_level,r.assess_date,r.assess_content));
                }
            }
            return sb.ToString();
        }
    }
}