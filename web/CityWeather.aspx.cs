using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace web
{
    public partial class CityWeather : System.Web.UI.Page
    {
        public StringBuilder sb = new StringBuilder();
        public string GetWeather()
        {
            string cityname = Session["city"].ToString();
            cn.com.webxml.www.WeatherWebService ws = new cn.com.webxml.www.WeatherWebService();
            string[] aWeather = ws.getWeatherbyCityName(cityname);
            sb.Append("<table>");
            sb.Append(string.Format("<tr><td>省市：</td><td>{0}-{1}</td></tr>", aWeather[0], aWeather[1]));
            sb.Append(string.Format("<tr><td>日期时间：</td><td>{0}</td></tr>", aWeather[4]));
            sb.Append(string.Format("<tr><td>温度：</td><td>{0}</td></tr>", aWeather[5]));
            sb.Append(string.Format("<tr><td>天气：</td><td>{0}</td></tr>", aWeather[6]));
            if (aWeather[8] == aWeather[9])
            {
                sb.Append(string.Format(@"<tr><td></td><td><img src=""weather/a_{0}""></td></tr>", aWeather[8]));
            }
            else
            {
                sb.Append(string.Format(@"<tr><td></td><td><img src=""weather/a_{0}"">~<img src=""weather/a_{1}""></td></tr>", aWeather[8], aWeather[9]));
            }
            sb.Append(string.Format("<tr><td>天气实况：</td><td>{0}</td></tr>", aWeather[10]));
            sb.Append("</table>");
            return sb.ToString();
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}