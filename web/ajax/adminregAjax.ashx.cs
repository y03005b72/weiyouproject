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
    /// adminregAjax 的摘要说明
    /// </summary>
    public class adminregAjax : IHttpHandler
    {

        public string sResult = "";
        JavaScriptSerializer jss = new JavaScriptSerializer();
        ReturnMessage rm = new ReturnMessage();
        public void ProcessRequest(HttpContext context)
        {
            string cmd = context.Request.Form["cmd"];
            switch (cmd)
            {
                case "adminlogin":
                    sResult = AdminLogin(context);
                    break;
            }
            context.Response.Write(sResult);
        }
        public string AdminLogin(HttpContext context)
        {
            try
            {
                string adminname = context.Request.Form["adminname"];//context.Request.QueryString["id"],通过get传值
                string password = context.Request.Form["password"];
                List<dbParam> list = new List<dbParam>()
            {
                new dbParam(){ParamName="@admin_id",ParamValue=adminname},
                new dbParam(){ParamName="@admin_password",ParamValue=password}
            };
                travel_admin admin = travel_adminDAL.m_travel_adminDal.GetModel("admin_id=@admin_id and admin_password=@admin_password", list);
                if (admin != null)
                {
                    rm.Success = true;
                    rm.Info = "登录成功";
                    context.Response.Cookies["adminname"].Value = admin.admin_id.ToString();
                    context.Response.Cookies["adminname"].Expires = DateTime.Now.AddMinutes(60);
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