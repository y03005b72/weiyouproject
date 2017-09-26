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
    /// ShopAjax 的摘要说明
    /// </summary>
    public class ShopAjax : IHttpHandler
    {
        public string sResult = "";
        JavaScriptSerializer jss = new JavaScriptSerializer();
        ReturnMessage rm = new ReturnMessage();
        public void ProcessRequest(HttpContext context)
        {
            string cmd = context.Request.Form["cmd"];
            switch (cmd)
            {
                case "guideadd":
                    sResult = GuideAdd(context);
                    break;
                case "guidedel":
                    sResult = GuideDel(context);
                    break;
                case "guideupdate":
                    sResult = GuideUpdate(context);
                    break;
                case "tripadd":
                    sResult = TripAdd(context);
                    break;
                case "tripdel":
                    sResult = TripDel(context);
                    break;
                case "tripupdate":
                    sResult = TripUpdate(context);
                    break;
            }
            context.Response.Write(sResult);
        }

        public string GuideAdd(HttpContext context)
        {
            try
            {
                int shopid = int.Parse(context.Request.Form["shopid"]);
                string guidecardId = context.Request.Form["guidecardId"];
                string guidename = context.Request.Form["guidename"];
                string Idcard = context.Request.Form["Idcard"];
                string guidedp = context.Request.Form["guidedp"];
                string guideintroduce = context.Request.Form["guideintroduce"];//context.Request.QueryString["id"],通过get传值
                travel_guide guide = new travel_guide();
                guide.shopper_shopId = shopid;
                guide.guide_guidecardId = guidecardId;
                guide.guide_name = guidename;
                guide.guide_IdCard = Idcard;
                guide.guide_dp = guidedp;
                guide.guide_introduce = guideintroduce;
                if (travel_guideDAL.m_travel_guideDal.Add(guide) > 0)
                {
                    rm.Success = true;
                    rm.Info = "添加成功";
                }
                else
                {
                    rm.Success = false;
                    rm.Info = "添加失败";
                }
            }
            catch (Exception)
            {

                rm.Success = false;
                rm.Info = "添加失败";
            }
            return jss.Serialize(rm);
        }

        public string GuideDel(HttpContext context)
        {
            try
            {
                int guideDid = int.Parse(context.Request.Form["guideDid"]);
                travel_guide guide = new travel_guide();
                guide.guide_guideId = guideDid;
                List<dbParam> list = new List<dbParam>() { 
                new dbParam(){ParamName="@guide_guideId",ParamValue=guideDid}
            };
                

                if (travel_guideDAL.m_travel_guideDal.Delete("guide_guideId=@guide_guideId", list) > 0)
                {
                    rm.Success = true;
                    rm.Info = "删除成功";
                }
                else
                {
                    rm.Success = false;
                    rm.Info = "删除失败";
                }
            }
            catch (Exception)
            {

                rm.Success = false;
                rm.Info = "删除失败";
            }
            return jss.Serialize(rm);
        }
        public string GuideUpdate(HttpContext context)
        {
            try
            {
                int Uguideid = int.Parse(context.Request.Form["UguideId"]);
                string UguidecardId = context.Request.Form["UguidecardId"];
                string Uguidename = context.Request.Form["Uguidename"];
                string UIdcard = context.Request.Form["UIdcard"];
                string Uguidedp = context.Request.Form["Uguidedp"];
                string Uguideintroduce = context.Request.Form["Uguideintroduce"];
                //travel_guide guide = new travel_guide();
                //guide.guide_guideId = Uguideid;
                //guide.guide_guidecardId = UguidecardId;
                //guide.guide_name = Uguidename;
                //guide.guide_IdCard = UIdcard;
                //guide.guide_dp = Uguidedp;
                //guide.guide_introduce = Uguideintroduce;

                List<dbParam> list1 = new List<dbParam>() { 
                new dbParam(){ParamName="@guide_guideId",ParamValue=Uguideid}
            };
                travel_guide rn = travel_guideDAL.m_travel_guideDal.GetModel("guide_guideId=@guide_guideId", list1);
                if (rn != null)
                {
                    rn.guide_guidecardId = UguidecardId;
                    rn.guide_name = Uguidename;
                    rn.guide_IdCard = UIdcard;
                    rn.guide_dp = Uguidedp;
                    rn.guide_introduce = Uguideintroduce;
                    travel_guideDAL.m_travel_guideDal.Update(rn);
                    
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
        public string TripAdd(HttpContext context)
        {
            try
            {
                int guideid = int.Parse(context.Request.Form["guideid"]);
                string tripname = context.Request.Form["tripname"];
                string tripprice = context.Request.Form["tripprice"];
                string tripcontent = context.Request.Form["tripcontent"];
                string trippicture = context.Request.Form["trippicture"];
                travel_trip trip = new travel_trip();
                trip.guide_guideId = guideid;
                trip.trip_name = tripname;
                trip.trip_price = double.Parse(tripprice);
                trip.trip_content= tripcontent;
                trip.trip_picutur = trippicture;
                if (travel_tripDAL.m_travel_tripDal.Add(trip) > 0)
                {
                    rm.Success = true;
                    rm.Info = "添加成功";
                }
                else
                {
                    rm.Success = false;
                    rm.Info = "添加失败";
                }
            }
            catch (Exception)
            {

                rm.Success = false;
                rm.Info = "添加失败";
            }
            return jss.Serialize(rm);
        }

        public string TripDel(HttpContext context)
        {
            try
            {
                int tripDid = int.Parse(context.Request.Form["tripDid"]);
                travel_trip trip = new travel_trip();
                trip.trip_tripId = tripDid;
                List<dbParam> list = new List<dbParam>() { 
                new dbParam(){ParamName="@trip_tripId",ParamValue=tripDid}
            };


                if (travel_tripDAL.m_travel_tripDal.Delete("trip_tripId=@trip_tripId", list) > 0)
                {
                    rm.Success = true;
                    rm.Info = "删除成功";
                }
                else
                {
                    rm.Success = false;
                    rm.Info = "删除失败";
                }
            }
            catch (Exception)
            {

                rm.Success = false;
                rm.Info = "删除失败";
            }
            return jss.Serialize(rm);
        }
        public string TripUpdate(HttpContext context)
        {
            try
            {
                int UtripId = int.Parse(context.Request.Form["UtripId"]);
                string Utripname = context.Request.Form["Utripname"];
                string Utripprice = context.Request.Form["Utripprice"];
                string Utripcontent = context.Request.Form["Utripcontent"];
                string Utrippicture = context.Request.Form["Utrippicture"];
                List<dbParam> list1 = new List<dbParam>() { 
                new dbParam(){ParamName="@trip_tripId",ParamValue=UtripId}
            };
                travel_trip rn = travel_tripDAL.m_travel_tripDal.GetModel("trip_tripId=@trip_tripId", list1);
                if (rn != null)
                {
                    rn.trip_name = Utripname;
                    rn.trip_price = double.Parse(Utripprice);
                    rn.trip_content = Utripcontent;
                    rn.trip_picutur = Utrippicture;
                    travel_tripDAL.m_travel_tripDal.Update(rn);

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

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}