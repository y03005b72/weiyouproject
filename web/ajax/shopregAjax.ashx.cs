using com.DAL.Base;
using com.Model.Base;
using DAL.ruanmou;
using Model.ruanmou;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace web.ajax
{
    /// <summary>
    /// shopregAjax 的摘要说明
    /// </summary>
    public class shopregAjax : IHttpHandler
    {

        public string sResult = "";
        JavaScriptSerializer jss = new JavaScriptSerializer();
        ReturnMessage rm = new ReturnMessage();
        public void ProcessRequest(HttpContext context)
        {
            string cmd = context.Request.Form["cmd"];
            switch (cmd)
            {
                case "shoplogin":
                    sResult = ShopperLogin(context);
                    break;
            }
            context.Response.Write(sResult);
        }
        public string ShopperLogin(HttpContext context)
        {
            try
            {
                string shoppername = context.Request.Form["shoppername"];//context.Request.QueryString["id"],通过get传值
                string password = context.Request.Form["password"];
                List<dbParam> list = new List<dbParam>()
            {
                new dbParam(){ParamName="@shopper_shopId",ParamValue=shoppername},
                new dbParam(){ParamName="@shopper_password",ParamValue=password}
            };
                travel_shopper shopper = travel_shopperDAL.m_travel_shopperDal.GetModel("shopper_shopId=@shopper_shopId and shopper_password=@shopper_password", list);
                if (shopper != null)
                {
                    rm.Success = true;
                    rm.Info = "登录成功";
                    context.Response.Cookies["shoppername"].Value = shopper.shopper_shopId.ToString();
                    context.Response.Cookies["shoppername"].Expires = DateTime.Now.AddMinutes(60);
                }
                else
                {
                    rm.Success = false;
                    rm.Info = "登录失败";
                }
            }
            catch (Exception)
            {

                rm.Success = false;
                rm.Info = "登录错误";
            }
            return jss.Serialize(rm);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}