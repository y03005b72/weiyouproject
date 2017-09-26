using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.ruanmou
{
    public class PagerDAL
    {
        public static string GetPagerHtml(int pageindex, int pagesize, int count)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"<div id=""mypager""><ul>");
            sb.Append(@"<li><a href=""javascript:;"" title=""1"">首页</a></li>");
            if (pageindex == 1)
            {
                sb.Append(@"<li class=""disable"">上一页</li>");
            }
            else
            {
                sb.Append(string.Format(@"<li><a href=""javascript:;"" title=""{0}"">上一页</a></li>", pageindex - 1));
            }
            int pagercount = 1;
            if (count % pagesize == 0)
            {
                pagercount = count / pagesize;
            }
            else
            {
                double b = Convert.ToDouble(count) / Convert.ToDouble(pagesize);
                pagercount = Convert.ToInt32(Math.Ceiling(b));
            }
            if (pageindex == pagercount)
            {
                sb.Append(@"<li class=""disable"">下一页</li>");
            }
            else
            {
                sb.Append(string.Format(@"<li><a href=""javascript:;"" title=""{0}"">下一页</a></li>", pageindex + 1));
            }
            sb.Append(string.Format(@"<li><a href=""javascript:;"" title=""{0}"">尾页</a></li>", pagercount));
            sb.Append(string.Format(@"<li class=""disable"">{0}/{1}</li>", pageindex, pagercount));
            sb.Append("</ul></div>");
            return sb.ToString();
        }
    }
}
