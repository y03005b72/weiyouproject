using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Model.ruanmou;
using DAL.ruanmou;
using com.DAL.Base;
using System.Web.Script.Serialization;
using com.Model.Base;
using System.Web.SessionState;
namespace web
{
    /// <summary>
    /// Handler1 的摘要说明
    /// </summary>
    public class Handler1 : IHttpHandler, IRequiresSessionState
    {
        public string sResult = "";
        JavaScriptSerializer jss = new JavaScriptSerializer();
        ReturnMessage rm = new ReturnMessage();
        public void ProcessRequest(HttpContext context)
        {
            string cmd = context.Request.Form["cmd"];
            switch (cmd)
            {
                case "login":
                    sResult = UserLogin(context);
                    break;
                case "reg":
                    sResult = UserReg(context);
                    break;
            }
            context.Response.Write(sResult);
        }
        public string UserReg(HttpContext context)
        {
            try
            {
                string username = context.Request.Form["username"];
                string pwd = context.Request.Form["pwd"];
                string sex = context.Request.Form["sex"];
                string tel = context.Request.Form["tel"];
                string qq = context.Request.Form["qq"];//context.Request.QueryString["id"],通过get传值
                travel_user user = new travel_user();
                user.user_username = username;                
                user.user_password = pwd;
                user.user_sex= sex;
                user.user_tel = tel;
                user.user_qq = qq;
                user.user_createdtime = DateTime.Now;
                if (travel_userDAL.m_travel_userDal.Add(user) > 0)
                {
                    rm.Success = true;
                    rm.Info = "注册成功";
                }
                else
                {
                    rm.Success = false;
                    rm.Info = "注册失败";
                }
            }
            catch (Exception)
            {

                rm.Success = false;
                rm.Info = "注册失败";
            }
            return jss.Serialize(rm);
        }
        public string UserLogin(HttpContext context)
        {
            try
            {
                string qq = context.Request.Form["qq"];//context.Request.QueryString["id"],通过get传值
                string pwd = context.Request.Form["pwd"];
                List<dbParam> list = new List<dbParam>()
            {
                new dbParam(){ParamName="@user_qq",ParamValue=qq},
                new dbParam(){ParamName="@user_password",ParamValue=pwd}
            };
                travel_user user = travel_userDAL.m_travel_userDal.GetModel("user_qq=@user_qq and user_password=@user_password", list);
                if (user != null)
                {
                    rm.Success = true;
                    rm.Info = "登录成功";
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