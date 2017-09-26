using System;
using System.Collections.Generic;
using System.Text;

namespace com.Utility
{
    public class Md5Class
    {
        public static string GetMD5(string myString, int code)
        {
            if (code == 16) //16位MD5加密（取32位加密的9~25字符） 
            {
                return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(myString, "MD5").ToLower().Substring(8, 16);
            }
            else//32位加密 
            {
                return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(myString, "MD5").ToLower();
            }
        }   
    }
}
