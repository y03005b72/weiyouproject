using System;
using System.Security.Principal;
using System.Web.Security;
using System.Web;
using System.IO;
using System.Text;
using System.Configuration;

namespace com.Utility
{
	/// <summary>
	/// ����ϵͳʵ�ֱ���֤��
	/// </summary>
	/// <remarks>
	/// ������ͬһһ�������²�ͬ������֮��ʵ��һ����֤���ദ���ʡ�
	/// ��Ҫ�� web.config �ļ��С��� <system.web /> ������� <machineKey /> �ڡ�
	/// Ȼ��ֻҪ��֤������������Ӧ�ó���� validationKey��decryptionKey��validation��������ͳһ���ɡ�
	/// </remarks>
	public class FormsAuth
	{
		private static int _expiration = 120;

		/// <summary>
		/// ��ȡ������������ַ����������Э�鷽����������ַ�Ͷ˿ںţ���
		/// </summary>
		/// <value>������������ַ��</value>
		public static string HostUrl
		{
			get
			{
				Uri uri = HttpContext.Current.Request.Url;
				return String.Concat(uri.Scheme, "://", uri.Authority);
			}
		}

		/// <summary>
		/// ��ȡ������Ӧ�ó�������·����
		/// </summary>
		/// <value>������Ӧ�ó�������·����</value>
		public static string ApplicationPath
		{
			get
			{
				string root = HttpContext.Current.Request.ApplicationPath;
				if (root!="/") root +="/";
				return root;
			}
		}

		/// <summary>
		/// ��ȡ��֤��־����
		/// </summary>
		/// <value>��֤��־����</value>
		public static string FormName
		{
			get { return FormsAuthentication.FormsCookieName; }
		}

		/// <summary>
		/// ��ȡ�������û����֤�����ʱ�䡣
		/// </summary>
		/// <value>�û����֤�����ʱ�䡣</value>
		public static int Expiration
		{
			get { return _expiration; }
			set { _expiration = value; }
		}

		/// <summary>
		/// ��ȡ��ǰ����֤�û���
		/// </summary>
		/// <returns>�û���ݱ�ʶ��</returns>
		public static string CurrentUserName
		{
			get{return HttpContext.Current.User.Identity.Name;}
		}

		/// <summary>
		/// ��ȡ��ǰ�����ס�
		/// </summary>
		/// <returns>��ǰ������</returns>
		public static string AccountID
		{
			get
			{
				string userID = HttpContext.Current.User.Identity.Name;
				if(userID.IndexOf("@")!=-1)
					return userID.Split('@')[1];
				else
                    return "39.net";
			}
		}
		/// <summary>
		/// �����û����֤�顣
		/// </summary>
		/// <param name="name">�û���ݱ�ʶ��</param>
		/// <param name="roles">�û���ɫ��</param>
		/// <param name="isPersistent">֤���Ƿ�����Ч��</param>
		public static void SetAuthTicket(string name, string[] roles, bool isPersistent)
		{
			DateTime expires = (isPersistent) ? DateTime.MaxValue : DateTime.Now.AddMinutes(_expiration);

			string rolesString = String.Join(",", roles);
			FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, name, DateTime.Now, expires, isPersistent, rolesString);
			
			string sEncode = FormsAuthentication.Encrypt(ticket);
			HttpCookie cookie = new HttpCookie(FormName, sEncode);
			cookie.Domain = SLDomain;
			//cookie.Expires = expires;
			HttpContext.Current.Response.Cookies.Set(cookie);
		}

		/// <summary>
		/// ����û����֤�顣
		/// </summary>
		public static void SignOut()
		{
			if(SLDomain!="")
			{
				HttpCookie cookie = new HttpCookie(FormName);
				cookie.Domain = SLDomain;
				cookie.Expires=DateTime.Now;
				HttpContext.Current.Response.Cookies.Set(cookie);
			}
			else	FormsAuthentication.SignOut();
		}

		// ��ȡ��ǰ���ʵĶ���������
		public static string SLDomain
		{
			get
			{
				string domain = "";
				Uri url = HttpContext.Current.Request.Url;
				if (url.HostNameType == UriHostNameType.Dns)
				{
					string[] nodes = url.Host.ToLower().Split('.');
					if (nodes.Length > 2)
					{
						string lastNode = nodes[nodes.Length - 1];
						domain = String.Format("{0}.{1}", nodes[nodes.Length - 2], lastNode);
						if (lastNode != "com" && lastNode != "net" && lastNode != "org" && lastNode != "co")
						{
							domain = String.Format("{0}.{1}", nodes[nodes.Length - 3], domain);
						}
					}
				}
				return domain;
			}
		}

		/// <summary>
		/// �����û�����ҳ���Զ�ת��
		/// </summary>
		public static void Redirect()
		{
			HttpContext.Current.Response.Redirect(GetRedirectUrl(), true);
		}

		/// <summary>
		/// ��ȡ�����ض��򵽵�¼ҳ��ԭʼ������ض���URL��
		/// </summary>
		/// <returns>�����ض��򵽵�¼ҳ��ԭʼ������ض���URL��</returns>
		public static string GetRedirectUrl()
		{
			string host = HttpContext.Current.Request["ReturnHost"];
			string url = FormsAuthentication.GetRedirectUrl("*", false);
			if (host != null)
			{
				url = host + url;
			}
			return url;
		}

		/// <summary>
		/// �滻�û���ÿ������ʱ���ݹ����Ľ�ɫ��
		/// </summary>
		/// <remarks>
		/// Ӧ�� Global.asax �� Application_OnAuthenticationRequest �����е��ø÷������Ӷ�
		/// ����Ҫ��֤��Form�����û����ɫ�Ĺ�����
		/// </remarks>
		public static void ImpersonateUser()
		{
			HttpContext currentContext = HttpContext.Current;
			if (currentContext.User != null)
			{
				if (currentContext.User.Identity is FormsIdentity && currentContext.User.Identity.IsAuthenticated)
				{
					FormsIdentity id = currentContext.User.Identity as FormsIdentity;
					FormsAuthenticationTicket ticket = id.Ticket;
					string[] roles = ticket.UserData.Split(',');
					currentContext.User = new GenericPrincipal(id, roles);
				}
			}
		}

		/// <summary>
		/// �������û������Ľ�ɫ��
		/// </summary>
		/// <param name="roles">�û��Ľ�ɫ��</param>
		/// <remarks>
		/// Ӧ�� Global.asax �� Session_OnStart �����е��ø÷������Ӷ�
		/// �ı��û��Ľ�ɫ��Ϣ��
		/// </remarks>
		public static void SetUserRoles(string[] roles)
		{
			HttpContext currentContext = HttpContext.Current;
			if (currentContext.User != null)
			{
				if (currentContext.User.Identity is FormsIdentity && currentContext.User.Identity.IsAuthenticated)
				{
					FormsIdentity id = currentContext.User.Identity as FormsIdentity;
					
					SetAuthTicket(id.Name, roles, false);
					
					FormsAuthenticationTicket ticket = id.Ticket;
					currentContext.User = new GenericPrincipal(id, roles);
				}
			}
		}

		/// <summary>
		/// ����һ��ֵ��ָʾ��ǰ�û��Ƿ���ָ���Ľ�ɫ�С�
		/// </summary>
		/// <param name="role">�û���ɫ��</param>
		/// <returns>�û�����ָ���Ľ�ɫ�򷵻� true�����򷵻� false��</returns>
		public static bool IsInRole(string role)
		{
			return HttpContext.Current.User.IsInRole(role);
		}

		public static void SetCookies(
			string domain , 
			string realName,
			string roles
			)
		{
			HttpCookie hc = new HttpCookie("LoginAccountID",AccountID);
			hc.Domain = domain;
			HttpContext.Current.Response.AppendCookie(hc);
			
			hc=new HttpCookie(AccountID+"_RealName",realName);
			hc.Domain=domain;
			HttpContext.Current.Response.AppendCookie(hc);

			hc=new HttpCookie(AccountID+"_Roles",roles);
			hc.Domain=domain;
			HttpContext.Current.Response.AppendCookie(hc);
		}

		public static string RealName
		{
			get{ HttpCookie hc=HttpContext.Current.Request.Cookies[AccountID+"_RealName"]; return hc==null?string.Empty:hc.Value;}
			set{ HttpContext.Current.Request.Cookies[AccountID+"_RealName"].Value = value;}
		}
		public static string Roles
		{
			get{ HttpCookie hc=HttpContext.Current.Request.Cookies[AccountID+"_Roles"]; return hc==null?string.Empty:hc.Value;}
			set{ HttpContext.Current.Request.Cookies[AccountID+"_Roles"].Value = value;}
		}
	}
}
