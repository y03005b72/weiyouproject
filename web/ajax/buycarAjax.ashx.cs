using com.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.SessionState;

namespace web.ajax
{
    /// <summary>
    /// buycarAjax 的摘要说明
    /// </summary>
    public class buycarAjax : IHttpHandler, IRequiresSessionState 
    {
        public string sResult = "";
        JavaScriptSerializer jss = new JavaScriptSerializer();
        ReturnMessage rm = new ReturnMessage();
        public void ProcessRequest(HttpContext context)
        {
            string cmd = context.Request.Form["cmd"];
            switch (cmd)
            {
                case "addbuycar":
                    sResult = AddBuycar(context);
                    break;
            }
            context.Response.Write(sResult);
        }
        
        public string AddBuycar(HttpContext context)
        {
            try
            {
                string listTitle = context.Request.Form["listTitle"];//context.Request.QueryString["id"],通过get传值
                string listPrice = context.Request.Form["listPrice"];
                string listPicture = context.Request.Form["listPicture"];
                string listTxt = context.Request.Form["listTxt"];
                context.Session["listTitle"] = listTitle;
                context.Session["listPrice"] = listPrice;
                context.Session["listPicture"] = listPicture;
                context.Session["listTxt"] = listTxt;
                if (context.Session["listTitle"] != null && context.Session["listPrice"] != null && context.Session["listPicture"] != null && context.Session["listTxt"] != null)
                {
                    rm.Success = true;
                    rm.Info = "加入购物车成功";
                }
                else
                {
                    rm.Success = false;
                    rm.Info = "加入购物车失败";
                }
            }
            catch (Exception)
            {

                rm.Success = false;
                rm.Info = "加入购物车失败";
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