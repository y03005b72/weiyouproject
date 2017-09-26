using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace com.Utility
{
    public class cookieHelper
    {
        /// <summary>
        /// ��ȡ�û�cookieֵ
        /// </summary>
        public static string GetCookie(string name)
        {
            HttpRequest request = System.Web.HttpContext.Current.Request;
            HttpCookie cookie = request.Cookies[name];
            return (cookie != null ? cookie.Value : "");
        }

        public static void SetCookie(string name, string value)
        {
            SetCookie(name, value, 500000);
        }

        /// <summary>
        /// �����û�cookie��,ֵ����Чʱ�䣬����Ϊ��λ
        /// </summary>
        public static void SetCookie(string name, string value, int iMinutes)
        {
            HttpResponse response = System.Web.HttpContext.Current.Response;
            HttpCookie mycookie = new HttpCookie(name);
            mycookie.Value = value;
            mycookie.Expires = DateTime.Now.AddMinutes(iMinutes);
            //string strDomain = System.Configuration.ConfigurationManager.AppSettings["domain"];
            //if (string.IsNullOrEmpty(strDomain))
            //{
            //    mycookie.Domain = ".39.net";
            //}
            //else
            //{
            //    mycookie.Domain = strDomain;
            //}
            response.Cookies.Add(mycookie);
        }

        public static string GetCookieByHead(string sInput)
        {
            string strCookie = CRegex.GetText(sInput, @"Set-Cookie:(?<Cookie>[\s\S]+?)\n", "Cookie");
            List<string> list = CRegex.GetList(strCookie, @"(?<cookie>[\w\d\.]+=[\w\d\._-]+);", "cookie");
            string sReturnCookie = "";
            foreach (string s in list)
            {
                sReturnCookie += s + "; ";
            }
            return sReturnCookie;
        }
        public static List<CookieObj> GetCookieList(string sInput, List<CookieObj> listInput)
        {
            string strCookie = CRegex.GetText(sInput, @"Set-Cookie:(?<Cookie>[\s\S]+?)\n", "Cookie");
            strCookie = CRegex.Replace(strCookie, @"expires=([^;]+)GMT;", "", 0);
            strCookie = strCookie.Replace("path=/", "");
            List<string> list = CRegex.GetList(strCookie, @"(?<cookie>[\w\d\&\.=]+[\w\d\._-]+);", "cookie");
            string cookieName, cookieValue;
            CookieObj mCookie = null;
            List<CookieObj> listCookie = new List<CookieObj>();
            foreach (string s in list)
            {
                if (s.IndexOf('=') < 1)
                    continue;

                cookieName = s.Substring(0, s.IndexOf('='));
                if (cookieName == "domain")
                    continue;
                if (s.Length < s.IndexOf('=') + 1)
                    cookieValue = "";
                else
                    cookieValue = s.Substring(s.IndexOf('=') + 1);
                mCookie = new CookieObj();
                mCookie.cookieName = cookieName;
                mCookie.cookieValue = cookieValue;
                listCookie.Add(mCookie);
            }
            bool blExists;
            CookieObj mInput;
            foreach (CookieObj model in listCookie)
            {
                blExists = false;
                for (int i = 0; i < listInput.Count; i++)
                {
                    mInput = listInput[i];
                    if (mInput.cookieName == model.cookieName)
                    {
                        blExists = true;
                        mInput.cookieValue = model.cookieValue;
                    }
                }
                if (!blExists)
                {
                    listInput.Add(model);
                }
            }
            return listInput;
        }
        public static void SetCookie(List<CookieObj> listInput, string CookieName, string CookieValue)
        {
            foreach (CookieObj mCookie in listInput)
            {
                if (mCookie.cookieName == CookieName)
                {
                    mCookie.cookieValue = CookieValue;
                    return;
                }
            }
            CookieObj obj = new CookieObj();
            obj.cookieName = CookieName;
            obj.cookieValue = CookieValue;
            listInput.Add(obj);
        }
        public static void Remove(List<CookieObj> listInput, string CookieName)
        {
            for (int i = 0; i < listInput.Count; i++)
            {
                if (listInput[i].cookieName == CookieName)
                {
                    listInput.RemoveAt(i);
                }
            }
        }

        //cookie���ܽ���
        private static Byte[] KEY_64
        {
            get
            {
                return new byte[] { 42, 16, 93, 156, 78, 4, 218, 32 };
            }
        }
        private static Byte[] IV_64
        {
            get
            {
                return new byte[] { 55, 103, 246, 79, 36, 99, 167, 3 };
            }
        }
        /// <summary>
        /// ��cookie����
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string EncryptCookie(string name)//��׼��DES����  �ؼ��֡����ݼ���
        {
            //#region DES�����㷨
            if (name != "")
            {
                DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new
                    CryptoStream(ms, cryptoProvider.CreateEncryptor(KEY_64, IV_64), CryptoStreamMode.Write);
                StreamWriter sw = new StreamWriter(cs);
                sw.Write(name);
                sw.Flush();
                cs.FlushFinalBlock();
                ms.Flush();

                //��ת��Ϊһ���ַ���
                return Convert.ToBase64String(ms.GetBuffer(), 0, Int32.Parse(ms.Length.ToString()));
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// ��cookie���н���
        /// </summary>
        /// <param name="temp"></param>
        /// <returns></returns>
        public static string DecryptCookie(string temp)//��׼��DES����
        {
            //#region DES �����㷨
            if (temp != "")
            {
                DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
                //���ַ���ת��Ϊ�ֽ���
                Byte[] buffer = Convert.FromBase64String(temp);
                MemoryStream ms = new MemoryStream(buffer);
                CryptoStream cs = new
                    CryptoStream(ms, cryptoProvider.CreateDecryptor(KEY_64, IV_64), CryptoStreamMode.Read);
                StreamReader sr = new StreamReader(cs);
                return sr.ReadToEnd();
            }
            else
            {
                return "";
            }
        }


    }

}
