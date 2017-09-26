//------------------------------------------------------------------------------
// <copyright company="Telligent Systems">
//     Copyright (c) Telligent Systems Corporation.  All rights reserved.
// </copyright> 
//------------------------------------------------------------------------------

using System;
using System.IO;
using System.Reflection;
using System.Web;
using System.Text.RegularExpressions;

namespace com.Utility.HttpModule
{ 
    /// <summary>
    /// This HttpModule encapsulates all the forums related events that occur 
    /// during ASP.NET application start-up, errors, and end request.
    /// </summary>
    // ***********************************************************************/
    public class AskHttpModule : IHttpModule
    {
        #region Member variables and inherited properties / methods
        /// <summary>
        /// Initializes the HttpModule and performs the wireup of all application
        /// events.
        /// </summary>
        /// <param name="application">Application the module is being run for</param>
        public void Init(HttpApplication application)
        { 
            application.BeginRequest += new EventHandler(this.Application_BeginRequest); 
        } 
        public void Dispose() { }
        #endregion

        #region Application BeginRequest
        private void Application_BeginRequest(Object source, EventArgs e)
        {
            HttpApplication application = (HttpApplication)source;
            ReWriteUrl(application.Context); 
        }
        #endregion Application BeginRequest
         
		#region ReWriteUrl
        /// <summary>
        /// ʵ��url��д
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private bool ReWriteUrl(HttpContext context)
        {
            //string path = context.Request.Path.ToLower(); 
            //Match mc = Regex.Match(path, @"(?<fcode>[\w\d]+)/topic-(?<tid>[\d]+).html", RegexOptions.IgnoreCase);
            //string fcode, tid, state, page;

            //// �鿴����
            //if (mc.Success)
            //{
            //    fcode = mc.Groups["fcode"].Value;
            //    tid = mc.Groups["tid"].Value;
            //    context.RewritePath(string.Format("/aspx/topic.aspx?fid={0}&tid={1}", fcode, tid));
            //    return true;
            //}

            //// ������ҳ
            //mc = Regex.Match(path, @"/question/(?<tid>[\d]+).html", RegexOptions.IgnoreCase);
            //if (mc.Success)
            //{
            //    tid = mc.Groups["tid"].Value;
            //    context.RewritePath(string.Format("/aspx/topic.aspx?tid={0}", tid));
            //}

            //// ���
            //mc = Regex.Match(path, @"(?<fcode>[\w\d]+)/forum-(?<state>[\d]{1,2})-(?<page>[\d]+).html", RegexOptions.IgnoreCase);
            //if (mc.Success)
            //{
            //    fcode = mc.Groups["fcode"].Value;
            //    state = mc.Groups["state"].Value;
            //    page = mc.Groups["page"].Value;
            //    context.RewritePath(string.Format("/aspx/forum.aspx?fcode={0}&state={1}&pn={2}", fcode, state, page));
            //    return true;
            //}

            //// �µİ��ҳ
            //mc = Regex.Match(path, @"/browse/(?<fnum>[\d]+)-(?<state>[\d]{1,2})-(?<page>[\d]+).html", RegexOptions.IgnoreCase);
            //if (mc.Success)
            //{
            //    fcode = mc.Groups["fnum"].Value;
            //    state = mc.Groups["state"].Value;
            //    page = mc.Groups["page"].Value;
            //    context.RewritePath(string.Format("/aspx/forum.aspx?fnum={0}&state={1}&pn={2}", fcode, state, page));
            //    return true;
            //} 

            //// RSSҳ
            //mc = Regex.Match(path, @"/rss/(?<fnum>[\d]+).xml", RegexOptions.IgnoreCase);
            //if (mc.Success)
            //{
            //    fcode = mc.Groups["fnum"].Value; 
            //    context.RewritePath(string.Format("/aspx/rss.aspx?fnum={0}", fcode));
            //    return true;
            //}

            //if (path == "/rss.xml")
            //{
            //    context.RewritePath("/aspx/rss.aspx?fnum=0");
            //    return true;
            //}

            //mc = Regex.Match(path, @"/rss.xml", RegexOptions.IgnoreCase);
            //if (mc.Success)
            //{
            //    fcode = mc.Groups["fnum"].Value;
            //    context.RewritePath(string.Format("/aspx/rss.aspx?fnum={0}", fcode));
            //    return true;
            //}

            //// ��ҳ
            //if (path.ToLower() == "/" || path.ToLower() == "/index.html")
            //{
            //    context.RewritePath("/aspx/index.aspx");
            //    return true;
            //}

            //mc = Regex.Match(path, @"/doctor-(?<tid>[\d]+)-(?<page>[\d]+).html", RegexOptions.IgnoreCase);
            //// �鿴ҽ������
            //if (mc.Success)
            //{
            //    tid = mc.Groups["tid"].Value;
            //    page = mc.Groups["page"].Value;
            //    context.RewritePath(string.Format("/aspx/doctor.aspx?doctorid={0}&pn={1}", tid, page));
            //    return true;
            //}

            //// ����Ȧ�б�ҳ
            //mc = Regex.Match(path, @"/(?<fcode>[\w\d]+)/friend-(?<page>[\d]+).html", RegexOptions.IgnoreCase);
            //if (mc.Success)
            //{
            //    fcode = mc.Groups["fcode"].Value;
            //    page = mc.Groups["page"].Value;
            //    context.RewritePath(string.Format("/aspx/friend.aspx?fcode={0}&pn={1}", fcode, page));
            //    return true;
            //}

            //// �²���Ȧ�б�
            //mc = Regex.Match(path, @"/browse/friend/(?<fnum>[\d]+)-(?<page>[\d]+).html", RegexOptions.IgnoreCase);
            //if (mc.Success)
            //{
            //    fcode = mc.Groups["fnum"].Value;
            //    page = mc.Groups["page"].Value;
            //    context.RewritePath(string.Format("/aspx/friend.aspx?fnum={0}&pn={1}", fcode, page));
            //    return true;
            //}

            //mc = Regex.Match(path, @"/userinfo-(?<mid>[\d]+)-(?<page>[\d]+)-(?<postpage>[\d]+).html", RegexOptions.IgnoreCase);
            //// �û���ϸҳ
            //if (mc.Success)
            //{
            //    tid = mc.Groups["mid"].Value;
            //    page = mc.Groups["page"].Value;
            //    string postPage = mc.Groups["postpage"].Value;
            //    context.RewritePath(string.Format("/aspx/userinfo.aspx?mid={0}&pn={1}&ppn={2}", tid, page,postPage));
            //    return true;
            //}
            
            //// �û���ϸҳ(����)
            //mc = Regex.Match(path, @"/useredit-(?<mid>[\d]+)-(?<page>[\d]+)-(?<postpage>[\d]+).html", RegexOptions.IgnoreCase);
            //if (mc.Success)
            //{
            //    tid = mc.Groups["mid"].Value;
            //    page = mc.Groups["page"].Value;
            //    string postPage = mc.Groups["postpage"].Value;
            //    context.RewritePath(string.Format("/aspx/useredit.aspx?mid={0}&pn={1}&ppn={2}", tid, page, postPage));
            //    return true;
            //}
            //mc = Regex.Match(path, @"(?<fcode>[\w\d]+)/more.html", RegexOptions.IgnoreCase);
            //// ������
            //if (mc.Success)
            //{
            //    fcode = mc.Groups["fcode"].Value;
            //    context.RewritePath(string.Format("/aspx/more.aspx?fcode={0}", fcode));
            //    return true;
            //}

            //// �¸�����
            //mc = Regex.Match(path, @"/browse/more/(?<fnum>[\d]+).html", RegexOptions.IgnoreCase);
            //if (mc.Success)
            //{
            //    fcode = mc.Groups["fnum"].Value;
            //    context.RewritePath(string.Format("/aspx/more.aspx?fnum={0}", fcode));
            //    return true;
            //}

            //// �ղذ��
            //mc = Regex.Match(path, @"/(?<fcode>[\w\d]+)/addfavorite.html", RegexOptions.IgnoreCase);
            //if (mc.Success)
            //{
            //    fcode = mc.Groups["fcode"].Value;
            //    context.RewritePath(string.Format(@"/aspx/addfavorite.aspx?fcode={0}", fcode));
            //    return true;
            //}

            //// �û�����ҳ��
            //mc = Regex.Match(path, @"/personal.html", RegexOptions.IgnoreCase);
            //if (mc.Success)
            //{ 
            //    context.RewritePath(@"/aspx/personal.aspx");
            //    return true;
            //}

            //// ������Ƕ��ҳ
            //mc = Regex.Match(path, @"/iframeforum-(?<fid>[\d]+).html", RegexOptions.IgnoreCase);
            //if (mc.Success)
            //{
            //    fcode = mc.Groups["fid"].Value ;
            //    context.RewritePath(string.Format(@"/aspx/iframeforum.aspx?fid={0}", fcode));
            //    return true;
            //}

            ////��Ʒ�б�
            //mc = Regex.Match(path, @"/award/index.html", RegexOptions.IgnoreCase);
            //if (mc.Success)
            //{
            //    context.RewritePath(@"/award/awardlist.aspx?cid=0&pn=1");
            //    return true;
            //}
            //mc = Regex.Match(path, @"/award/(?<cid>[\d]+)/(?<page>[\d]+).html", RegexOptions.IgnoreCase);
            //if (mc.Success)
            //{
            //    fcode =  mc.Groups["cid"].Value ;
            //    page =  mc.Groups["page"].Value ;
            //    context.RewritePath(string.Format(@"/award/awardlist.aspx?cid={0}&pn={1}", fcode, page));
            //    return true;
            //}
            ////��Ʒҳ
            //mc = Regex.Match(path, @"/award/award/(?<tid>[\d]+).html", RegexOptions.IgnoreCase);
            //if (mc.Success)
            //{
            //    tid = mc.Groups["tid"].Value;
            //    context.RewritePath(string.Format(@"/award/award.aspx?aid={0}", tid));
            //    return true;
            //}
            ////ȷ��ҳ
            //mc = Regex.Match(path, @"/award/info/(?<tid>[\d]+).html", RegexOptions.IgnoreCase);
            //if (mc.Success)
            //{
            //    tid = mc.Groups["tid"].Value;
            //    context.RewritePath(string.Format(@"/award/info.aspx?aid={0}", tid));
            //    return true;
            //}

            //// tagҳ
            //mc = Regex.Match(path, @"/tag/(?<fnum>[\d]+)-(?<state>[\d]{1,2})-(?<page>[\d]+).html", RegexOptions.IgnoreCase);
            //if (mc.Success)
            //{
            //    fcode = mc.Groups["fnum"].Value;
            //    state = mc.Groups["state"].Value;
            //    page = mc.Groups["page"].Value;
            //    context.RewritePath(string.Format("/aspx/tag.aspx?fnum={0}&state={1}&pn={2}", fcode, state, page));
            //    return true;
            //} 

            //#region �ɰ�ask
            //mc = Regex.Match(path, @"/Topics/(?<fcode>[\w\d]+)/(?<tid>[\d]+).xml", RegexOptions.IgnoreCase);
            //if (mc.Success)
            //{
            //    fcode = mc.Groups["fcode"].Value ;
            //    tid = mc.Groups["tid"].Value ;
            //    context.RewritePath(string.Format("/aspx/topic.aspx?fid={0}&tid={1}", fcode, tid));
            //    return true;
            //}
            //mc = Regex.Match(path + "?"  + context.Request.QueryString, @"/bbs/((index)|(main)).aspx\?forumID=(?<fid>[\d]+)", RegexOptions.IgnoreCase);
            //if (mc.Success)
            //{
            //    fcode = mc.Groups["fid"].Value;
            //    context.Response.Redirect(string.Format("/post.ashx?action=go&fid={0}", fcode));
            //    return false;
            //}
            //#endregion �ɰ�ask
            return false;
        }     
        #endregion ReWriteUrl
    }
}
