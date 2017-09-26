using System.Text.RegularExpressions;

namespace com.Utility
{
	/// <summary>
	/// UBB ��ժҪ˵����
	/// </summary>
	public class UBB
	{
		public UBB()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		#region ������̬����

        /// <summary>
        /// UBB���봦����
        /// </summary>
        /// <param name="sDetail">�����ַ���</param>
        /// <returns></returns>
        public static string UbbToHtml(string sDetail)
        {
            return UbbToHtml(sDetail, true);
        }

		/// <summary>
		/// UBB���봦����
		/// </summary>
		/// <param name="sDetail">�����ַ���</param>
        /// <param name="blLink">�Ƿ�ȥ��������</param>
		/// <returns>����ַ���</returns> 
		public static string UbbToHtml(string sDetail,bool blLink)
		{
			Regex r;
			Match m;
			sDetail = sDetail.Replace("\r\n", "<br />");
            sDetail = ClearScript(sDetail,blLink);

			#region ��[b][/b]���

			r = new Regex(@"(\[b\])([ \S\t]*?)(\[\/b\])", RegexOptions.IgnoreCase);
			for (m = r.Match(sDetail); m.Success; m = m.NextMatch())
			{
				sDetail = sDetail.Replace(m.Groups[0].ToString(), "<B>" + m.Groups[2].ToString() + "</B>");
			}

			#endregion

			#region ��[i][/i]���

			r = new Regex(@"(\[i\])([ \S\t]*?)(\[\/i\])", RegexOptions.IgnoreCase);
			for (m = r.Match(sDetail); m.Success; m = m.NextMatch())
			{
				sDetail = sDetail.Replace(m.Groups[0].ToString(), "<I>" + m.Groups[2].ToString() + "</I>");
			}

			#endregion

			#region ��[u][/u]���

			r = new Regex(@"(\[U\])([ \S\t]*?)(\[\/U\])", RegexOptions.IgnoreCase);
			for (m = r.Match(sDetail); m.Success; m = m.NextMatch())
			{
				sDetail = sDetail.Replace(m.Groups[0].ToString(), "<U>" + m.Groups[2].ToString() + "</U>");
			}

			#endregion

			#region ��[p][/p]���

			//��[p][/p]���
			r = new Regex(@"((\r\n)*\[p\])(.*?)((\r\n)*\[\/p\])", RegexOptions.IgnoreCase | RegexOptions.Singleline);
			for (m = r.Match(sDetail); m.Success; m = m.NextMatch())
			{
				sDetail = sDetail.Replace(m.Groups[0].ToString(), "<P class='pstyle'>" + m.Groups[3].ToString() + "</P>");
			}

			#endregion

			#region ��[sup][/sup]���

			//��[sup][/sup]���
			r = new Regex(@"(\[sup\])([ \S\t]*?)(\[\/sup\])", RegexOptions.IgnoreCase);
			for (m = r.Match(sDetail); m.Success; m = m.NextMatch())
			{
				sDetail = sDetail.Replace(m.Groups[0].ToString(), "<SUP>" + m.Groups[2].ToString() + "</SUP>");
			}

			#endregion

			#region ��[sub][/sub]���

			//��[sub][/sub]���
			r = new Regex(@"(\[sub\])([ \S\t]*?)(\[\/sub\])", RegexOptions.IgnoreCase);
			for (m = r.Match(sDetail); m.Success; m = m.NextMatch())
			{
				sDetail = sDetail.Replace(m.Groups[0].ToString(), "<SUB>" + m.Groups[2].ToString() + "</SUB>");
			}

			#endregion

			#region ��[url][/url]���

			//��[url][/url]���
			r = new Regex(@"(\[url\])([ \S\t]*?)(\[\/url\])", RegexOptions.IgnoreCase);
			for (m = r.Match(sDetail); m.Success; m = m.NextMatch())
			{
				sDetail = sDetail.Replace(m.Groups[0].ToString(),
				                          "<a href='" + m.Groups[2].ToString() + "' target='_blank'><font color='blue'>"
				                          + m.Groups[2].ToString() + "</font></a>");
			}

			#endregion

			#region ��[url=xxx][/url]���

			//��[url=xxx][/url]���
			r = new Regex(@"(\[url=([\S\t]*?)\])([ \S\t]*?)(\[\/url\])", RegexOptions.IgnoreCase);
			for (m = r.Match(sDetail); m.Success; m = m.NextMatch())
			{
				sDetail = sDetail.Replace(m.Groups[0].ToString(),
				                          "<a href='" + m.Groups[2].ToString() + "' target='_blank'><font color='blue'>"
				                          + m.Groups[3].ToString() + "</font></a>");
			}

			#endregion

			#region ��[email][/email]���

			//��[email][/email]���
			r = new Regex(@"(\[email\])([ \S\t]*?)(\[\/email\])", RegexOptions.IgnoreCase);
			for (m = r.Match(sDetail); m.Success; m = m.NextMatch())
			{
				sDetail = sDetail.Replace(m.Groups[0].ToString(),
				                          "<A href=mailto:" + m.Groups[2].ToString() + " target='_blank'>" +
				                          m.Groups[2].ToString() + "</A>");
			}

			#endregion

			#region ��[email=xxx][/email]���

			//��[email=xxx][/email]���
			r = new Regex(@"(\[email=([ \S\t]+)\])([ \S\t]*?)(\[\/email\])", RegexOptions.IgnoreCase);
			for (m = r.Match(sDetail); m.Success; m = m.NextMatch())
			{
				sDetail = sDetail.Replace(m.Groups[0].ToString(),
				                          "<A href='mailto:" + m.Groups[2].ToString() + "' target='_blank'>" +
				                          m.Groups[3].ToString() + "</A>");
			}

			#endregion

			#region ��[size=x][/size]���

			//��[size=x][/size]���
			r = new Regex(@"(\[size=([1-7])\])([ \S\t]*?)(\[\/size\])", RegexOptions.IgnoreCase);
			for (m = r.Match(sDetail); m.Success; m = m.NextMatch())
			{
				sDetail = sDetail.Replace(m.Groups[0].ToString(),
				                          "<FONT SIZE=" + m.Groups[2].ToString() + ">" +
				                          m.Groups[3].ToString() + "</FONT>");
			}

			#endregion

			#region ��[color=x][/color]���

			//��[color=x][/color]���
			r = new Regex(@"(\[color=([\S\t]*?)\])([ \S\t]*?)(\[\/color\])", RegexOptions.IgnoreCase);
			for (m = r.Match(sDetail); m.Success; m = m.NextMatch())
			{
				sDetail = sDetail.Replace(m.Groups[0].ToString(),
				                          "<FONT COLOR=" + m.Groups[2].ToString() + ">" +
				                          m.Groups[3].ToString() + "</FONT>");
			}

			#endregion

			#region ��[font=x][/font]���

			//��[font=x][/font]���
			r = new Regex(@"(\[font=([\S\t]*?)\])([ \S\t]*?)(\[\/font\])", RegexOptions.IgnoreCase);
			for (m = r.Match(sDetail); m.Success; m = m.NextMatch())
			{
				sDetail = sDetail.Replace(m.Groups[0].ToString(),
				                          "<FONT FACE=" + m.Groups[2].ToString() + ">" +
				                          m.Groups[3].ToString() + "</FONT>");
			}

			#endregion

			#region ����ͼƬ����

			//����ͼƬ����
			r = new Regex("\\[picture\\](\\d+?)\\[\\/picture\\]", RegexOptions.IgnoreCase);
			for (m = r.Match(sDetail); m.Success; m = m.NextMatch())
			{
				sDetail = sDetail.Replace(m.Groups[0].ToString(),
				                          "<a href='ShowImage.aspx?Type=ALL&Action=forumImage&ImageID="
				                          + m.Groups[1].ToString() +
				                          "' target='_blank'><IMG border=0 Title='������´��ڲ鿴' src='ShowImage.aspx?Action=forumImage&ImageID=" +
				                          m.Groups[1].ToString() +
				                          "'></a>");
			}

			#endregion

			#region ����[align=x][/align]

			//����[align=x][/align]
			r = new Regex(@"(\[align=([\S]+)\])([ \S\t]*?)(\[\/align\])", RegexOptions.IgnoreCase);
			for (m = r.Match(sDetail); m.Success; m = m.NextMatch())
			{
				sDetail = sDetail.Replace(m.Groups[0].ToString(),
				                          "<P align=" + m.Groups[2].ToString() + ">" +
				                          m.Groups[3].ToString() + "</P>");
			}

			#endregion

			#region ��[H=x][/H]���

			//��[H=x][/H]���
			r = new Regex(@"(\[H=([1-6])\])([ \S\t]*?)(\[\/H\])", RegexOptions.IgnoreCase);
			for (m = r.Match(sDetail); m.Success; m = m.NextMatch())
			{
				sDetail = sDetail.Replace(m.Groups[0].ToString(),
				                          "<H" + m.Groups[2].ToString() + ">" +
				                          m.Groups[3].ToString() + "</H" + m.Groups[2].ToString() + ">");
			}

			#endregion

			#region ����[list=x][*][/list]

			//����[list=x][*][/list]
			r =
				new Regex(@"(\[list(=(A|a|I|i| ))?\]([ \S\t]*)\r\n)((\[\*\]([ \S\t]*\r\n))*?)(\[\/list\])", RegexOptions.IgnoreCase);
			for (m = r.Match(sDetail); m.Success; m = m.NextMatch())
			{
				string strLI = m.Groups[5].ToString();
				Regex rLI = new Regex(@"\[\*\]([ \S\t]*\r\n?)", RegexOptions.IgnoreCase);
				Match mLI;
				for (mLI = rLI.Match(strLI); mLI.Success; mLI = mLI.NextMatch())
				{
					strLI = strLI.Replace(mLI.Groups[0].ToString(), "<LI>" + mLI.Groups[1]);
				}
				sDetail = sDetail.Replace(m.Groups[0].ToString(),
				                          "<UL TYPE='" + m.Groups[3].ToString() + "'><B>" + m.Groups[4].ToString() + "</B>" +
				                          strLI + "</UL>");
			}

			#endregion

			#region ��[SHADOW=x][/SHADOW]���

			//��[SHADOW=x][/SHADOW]���
			r = new Regex(@"(\[SHADOW=)(\d*?),(#*\w*?),(\d*?)\]([\S\t]*?)(\[\/SHADOW\])", RegexOptions.IgnoreCase);
			for (m = r.Match(sDetail); m.Success; m = m.NextMatch())
			{
				sDetail = sDetail.Replace(m.Groups[0].ToString(),
				                          "<TABLE WIDTH=" + m.Groups[2].ToString() + "STYLE=FILTER:SHADOW(COLOR=" +
				                          m.Groups[3].ToString() + ", STRENGTH=" + m.Groups[4].ToString() + ")>" +
				                          m.Groups[5].ToString() + "</TABLE>");
			}

			#endregion

			#region ��[glow=x][/glow]���

			//��[glow=x][/glow]���
			r = new Regex(@"(\[glow=)(\d*?),(#*\w*?),(\d*?)\]([\S\t]*?)(\[\/glow\])", RegexOptions.IgnoreCase);
			for (m = r.Match(sDetail); m.Success; m = m.NextMatch())
			{
				sDetail = sDetail.Replace(m.Groups[0].ToString(),
				                          "<TABLE WIDTH=" + m.Groups[2].ToString() + "  STYLE=FILTER:GLOW(COLOR=" +
				                          m.Groups[3].ToString() + ", STRENGTH=" + m.Groups[4].ToString() + ")>" +
				                          m.Groups[5].ToString() + "</TABLE>");
			}

			#endregion

			#region ��[center][/center]���

			r = new Regex(@"(\[center\])([ \S\t]*?)(\[\/center\])", RegexOptions.IgnoreCase);
			for (m = r.Match(sDetail); m.Success; m = m.NextMatch())
			{
				sDetail = sDetail.Replace(m.Groups[0].ToString(), "<CENTER>" + m.Groups[2].ToString() + "</CENTER>");
			}

			#endregion

			#region ��[IMG][/IMG]���

			r = new Regex(@"(\[IMG\])(http|https|ftp):\/\/([ \S\t]*?)(\[\/IMG\])", RegexOptions.IgnoreCase);
			for (m = r.Match(sDetail); m.Success; m = m.NextMatch())
			{
				//				sDetail = sDetail.Replace(m.Groups[0].ToString(),"<br><a onfocus=this.blur() href=" + m.Groups[2].ToString() + "://" + m.Groups[3].ToString() + " target=_blank><IMG SRC=" + m.Groups[2].ToString() + "://" + m.Groups[3].ToString() + " border=0 alt=�������´������ͼƬ onload=javascript:if(screen.width-333<this.width)this.width=screen.width-333></a>");
				sDetail =
					sDetail.Replace(m.Groups[0].ToString(),
					                "<a onfocus=this.blur() href=" + m.Groups[2].ToString() + "://" + m.Groups[3].ToString() +
					                " target=_blank><IMG SRC=" + m.Groups[2].ToString() + "://" + m.Groups[3].ToString() +
					               // " border=0 alt=�������´������ͼƬ onload=javascript:if(screen.width-333<this.width)this.width=screen.width-333></a>");
					             " border=0 alt=�������´������ͼƬ onload=javascript:if(screen.width-700<this.width)this.width=screen.width-700></a>");
        
			}

			#endregion

			#region ��[em]���

			r = new Regex(@"(\[em([\S\t]*?)\])", RegexOptions.IgnoreCase);
			for (m = r.Match(sDetail); m.Success; m = m.NextMatch())
			{
				sDetail =
					sDetail.Replace(m.Groups[0].ToString(), "<img src=/pic/em" + m.Groups[2].ToString() + ".gif border=0 align=middle>");
			}

			#endregion

			#region ��[flash=x][/flash]���

			//			//��[mp=x][/mp]���
			r = new Regex(@"(\[flash=)(\d*?),(\d*?)\]([\S\t]*?)(\[\/flash\])", RegexOptions.IgnoreCase);
			for (m = r.Match(sDetail); m.Success; m = m.NextMatch())
			{
				//sDetail = sDetail.Replace(m.Groups[0].ToString(),
				//	"<a href=" + m.Groups[4].ToString() + " TARGET=_blank><IMG SRC=pic/swf.gif border=0 alt=������´������͸�FLASH����!> [ȫ������]</a><br><br><OBJECT codeBase=http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,29,0 classid=clsid:D27CDB6E-AE6D-11cf-96B8-444553540000 width=" + m.Groups[2].ToString() + " height=" + m.Groups[3].ToString() + "><PARAM NAME=movie VALUE=" + m.Groups[4].ToString() + "><PARAM NAME=quality VALUE=high><param name=menu value=false><embed src=" + m.Groups[4].ToString() + " quality=high menu=false pluginspage=http://www.macromedia.com/go/getflashplayer type=application/x-shockwave-flash width=" + m.Groups[2].ToString() + " height=" + m.Groups[3].ToString() + ">" + m.Groups[4].ToString() + "</embed></OBJECT>");
				sDetail = sDetail.Replace(m.Groups[0].ToString(),
				                          "<a href=" + m.Groups[4].ToString() +
				                          " TARGET=_blank><IMG SRC=pic/swf.gif border=0 alt=������´������͸�FLASH����!> [ȫ������]</a>");
			}

			#endregion

			#region ��[rm=x][/rm]���

			//��[rm=x][/rm]���
			r = new Regex(@"(\[rm=)(\d*?),(\d*?)\]([\S\t]*?)(\[\/rm\])", RegexOptions.IgnoreCase);
			for (m = r.Match(sDetail); m.Success; m = m.NextMatch())
			{
				sDetail = sDetail.Replace(m.Groups[0].ToString(),
				                          "<OBJECT classid=clsid:CFCDAA03-8BE4-11cf-B84B-0020AFBBCCFA class=OBJECT id=RAOCX width=" +
				                          m.Groups[2].ToString() + " height=" + m.Groups[3].ToString() + "><PARAM NAME=SRC VALUE=" +
				                          m.Groups[4].ToString() +
				                          "><PARAM NAME=CONSOLE VALUE=Clip1><PARAM NAME=CONTROLS VALUE=imagewindow><PARAM NAME=AUTOSTART VALUE=flase></OBJECT><br><OBJECT classid=CLSID:CFCDAA03-8BE4-11CF-B84B-0020AFBBCCFA height=32 id=video2 width=" +
				                          m.Groups[2].ToString() + "><PARAM NAME=SRC VALUE=" + m.Groups[4].ToString() +
				                          "><PARAM NAME=AUTOSTART VALUE=-1><PARAM NAME=CONTROLS VALUE=controlpanel><PARAM NAME=CONSOLE VALUE=Clip1></OBJECT>");
				//sDetail = sDetail.Replace(m.Groups[0].ToString(),
				//	"<a href=" + m.Groups[4].ToString() + " TARGET=_blank> [ȫ������]</a>");
			}

			#endregion

			#region ��[mp=x][/mp]���

			//��[mp=x][/mp]���
			r = new Regex(@"(\[mp=)(\d*?),(\d*?)\]([\S\t]*?)(\[\/mp\])", RegexOptions.IgnoreCase);
			for (m = r.Match(sDetail); m.Success; m = m.NextMatch())
			{
				sDetail = sDetail.Replace(m.Groups[0].ToString(),
				                          "<object align=middle classid=CLSID:22d6f312-b0f6-11d0-94ab-0080c74c7e95 class=OBJECT id=MediaPlayer width=" +
				                          m.Groups[2].ToString() + " height=" + m.Groups[3].ToString() +
				                          " ><PARAM NAME=AUTOSTART VALUE=false><param name=ShowStatusBar value=-1><param name=Filename value=" +
				                          m.Groups[4].ToString() +
				                          "><embed type=application/x-oleobject codebase=http://activex.microsoft.com/activex/controls/mplayer/en/nsmp2inf.cab#Version=5,1,52,701 flename=mp src=" +
				                          m.Groups[4].ToString() + "  width=" + m.Groups[2].ToString() + " height=" +
				                          m.Groups[3].ToString() + "></embed></object>");
				//sDetail = sDetail.Replace(m.Groups[0].ToString(),
				//"<a href=" + m.Groups[4].ToString() + " TARGET=_blank> [ȫ������]</a>");
			}

			#endregion

			#region ��[qt=x][/qt]���

			//��[qt=x][/qt]���
			r = new Regex(@"(\[qt=)(\d*?),(\d*?)\]([\S\t]*?)(\[\/qt\])", RegexOptions.IgnoreCase);
			for (m = r.Match(sDetail); m.Success; m = m.NextMatch())
			{
				sDetail = sDetail.Replace(m.Groups[0].ToString(),
				                          "<embed src=" + m.Groups[4].ToString() + " width=" + m.Groups[2].ToString() + " height=" +
				                          m.Groups[3].ToString() +
				                          " autoplay=true loop=false controller=true playeveryframe=false cache=false scale=TOFIT bgcolor=#000000 kioskmode=false targetcache=false pluginspage=http://www.apple.com/quicktime/>");
			}

			#endregion

			#region ��[QUOTE][/QUOTE]���

			r = new Regex(@"(\[QUOTE\])([ \S\t]*?)(\[\/QUOTE\])", RegexOptions.IgnoreCase);
			for (m = r.Match(sDetail); m.Success; m = m.NextMatch())
			{
				sDetail =
					sDetail.Replace(m.Groups[0].ToString(),
					                "<table cellpadding=0 cellspacing=0 border=1 WIDTH=94% bordercolor=#000000 bgcolor=#F2F8FF align=center  style=FONT-SIZE: 9pt><tr><td  ><table width=100% cellpadding=5 cellspacing=1 border=0><TR><TD >" +
					                m.Groups[2].ToString() + "</table></table><br>");
			}

			#endregion

			#region ��[move][/move]���

			r = new Regex(@"(\[move\])([ \S\t]*?)(\[\/move\])", RegexOptions.IgnoreCase);
			for (m = r.Match(sDetail); m.Success; m = m.NextMatch())
			{
				sDetail =
					sDetail.Replace(m.Groups[0].ToString(), "<MARQUEE scrollamount=3>" + m.Groups[2].ToString() + "</MARQUEE>");
			}

			#endregion

			#region ��[FLY][/FLY]���
			r = new Regex(@"(\[FLY\])([ \S\t]*?)(\[\/FLY\])", RegexOptions.IgnoreCase);
			for (m = r.Match(sDetail); m.Success; m = m.NextMatch())
			{
				sDetail = sDetail.Replace(m.Groups[0].ToString(),"<MARQUEE width=80% behavior=alternate scrollamount=3>" + m.Groups[2].ToString() + "</MARQUEE>");
			}
			#endregion

			#region ��[image][/image]���
			r = new Regex(@"(\[image\])([ \S\t]*?)(\[\/image\])", RegexOptions.IgnoreCase);
			for (m = r.Match(sDetail); m.Success; m = m.NextMatch())
			{
				sDetail = sDetail.Replace(m.Groups[0].ToString(), "<img src='" + m.Groups[2].ToString() + "' border=0 align=middle><br>");
			}
			#endregion

            #region ����[uploadimage][/uploadimage]���
            //r = new Regex(@"\[uploadimage\]\d+,(?<url>\d+/[\d_]+.jpg)\[/uploadimage\]", RegexOptions.IgnoreCase);
            //for (m = r.Match(sDetail); m.Success; m = m.NextMatch())
            //{
            //    sDetail = sDetail.Replace(m.Value, string.Format(@"<img src=""http://bbs.39.net/files/uploadfiles/{0}"" /><br />", m.Groups["url"].Value));
            //}            
            r = new Regex(@"\[uploadimage\]\d+,(?<url>\d+/[\d_]+.[\w]*)\[/uploadimage\]", RegexOptions.IgnoreCase);
            for (m = r.Match(sDetail); m.Success; m = m.NextMatch())
            {
                sDetail = sDetail.Replace(m.Value, string.Format(@"<img src=""http://bbs.39.net/files/uploadfiles/{0}"" /><br />", m.Groups["url"].Value));
            }

            r = new Regex(@"\[uploadimage\]\d+,(?<url>\d+\\[\d_]+.[\w]*)\[/uploadimage\]", RegexOptions.IgnoreCase);
            for (m = r.Match(sDetail); m.Success; m = m.NextMatch())
            {
                sDetail = sDetail.Replace(m.Value, string.Format(@"<img src=""http://bbs.39.net/files/uploadfiles/{0}"" /><br />", m.Groups["url"].Value));
            }    
            #endregion ����[uploadimage][/uploadimage]���

            return sDetail;
		}

		public static string ClearScript(string sDetail,bool blLink)
		{
            sDetail = CRegex.Replace(sDetail, @"<script([\s\S]+?)((</script>)|(/>))", "", 0);
            sDetail = CRegex.Replace(sDetail, @"<iframe([\s\S]+?)((</iframe>)|(/>))", "", 0);
            if (blLink)
            {
                sDetail = CRegex.Replace(sDetail, @"<a([\s\S]+?)((</a>)|(/>))", "", 0);
            }
			return sDetail;
		}
		#endregion
	}
}