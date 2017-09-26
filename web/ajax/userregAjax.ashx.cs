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
    /// userregAjax 的摘要说明
    /// </summary>
    public class userregAjax : IHttpHandler
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
                case "updateuser":
                    sResult = UpdateUser(context);
                    break;
            }
            context.Response.Write(sResult);
        }
        public string UpdateUser(HttpContext context)
        {
            try
            {
                string username = context.Session["username"].ToString();
                string txtidCard = context.Request.Form["txtidCard"];
                string txtname = context.Request.Form["txtname"];
                string txtemail = context.Request.Form["txtemail"];
                string txtdp = context.Request.Form["txtdp"];
 
                List<dbParam> list1 = new List<dbParam>() { 
                new dbParam(){ParamName="@user_username",ParamValue=username}
            };
                travel_user rn = travel_userDAL.m_travel_userDal.GetModel("user_username=@user_username", list1);
                if (rn != null)
                {
                    rn.user_IdCard = txtidCard;
                    rn.user_realname = txtname;
                    rn.user_email = txtemail;
                    rn.user_dp = txtdp;
                    travel_userDAL.m_travel_userDal.Update(rn);
                    rm.Success = true;
                    rm.Info = "修改成功";
                }
                else
                {
                    rm.Success = false;
                    rm.Info = "修改失败";
                }

            }
            catch (Exception)
            {

                rm.Success = false;
                rm.Info = "修改失败";
            }
            return jss.Serialize(rm);
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
                user.user_sex = sex;
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
                string username = context.Request.Form["username"];//context.Request.QueryString["id"],通过get传值
                string password = context.Request.Form["password"];
                //context.Session["username"] = username;
                List<dbParam> list = new List<dbParam>()
            {
                new dbParam(){ParamName="@user_username",ParamValue=username},
                new dbParam(){ParamName="@user_password",ParamValue=password}
            };
                travel_user user = travel_userDAL.m_travel_userDal.GetModel("user_username=@user_username and user_password=@user_password", list);
                if (user != null)
                {
                    rm.Success = true;
                    rm.Info = "登录成功";
                    context.Response.Cookies["username"].Value = user.user_username;
                    context.Response.Cookies["username"].Expires = DateTime.Now.AddMinutes(60);
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