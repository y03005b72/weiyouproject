using com.DAL.Base;
using com.Model.Base;
using DAL.ruanmou;
using Model.ruanmou;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace web.ajax
{
    /// <summary>
    /// UserSearch 的摘要说明
    /// </summary>
    public class UserSearch : IHttpHandler
    {

        ReturnMessage rm = new ReturnMessage();
        JavaScriptSerializer jss = new JavaScriptSerializer();
        public void ProcessRequest(HttpContext context)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                string title = context.Request.Form["title"];
                List<dbParam> list1 = new List<dbParam>()
            {
                new dbParam(){ParamName="@trip_name",ParamValue=title}
            };
                List<travel_trip> list = travel_tripDAL.m_travel_tripDal.GetList("trip_name like '%'+@trip_name+'%'", 5, 1, true, "trip_name", "trip_count", list1);
                if (list.Count > 0)
                {
                    foreach (var r in list)
                    {
                        sb.Append(string.Format("<li>{0}</li>", r.trip_name));
                    }
                }
                rm.Info = sb.ToString();
                rm.Success = true;
            }
            catch (Exception)
            {
                rm.Success = false;
            }
            context.Response.Write(jss.Serialize(rm));
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