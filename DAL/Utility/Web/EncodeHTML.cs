using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.IO;
using System.Threading;
using System.Runtime.CompilerServices;

namespace com.Utility
{
    public class EncodeHTML
    {
        private static int counter = 0;
        private static readonly int max = 2000;

        /// <summary>
        /// ��ȡҪ������ַ�����
        /// </summary>
        public static readonly string[] EncodeChars = new string[] { "0","1", "2", "3", "4", "5", "6", "7", "8", "9",
            "һ", "��", "��", "��", "��", "��","��","��","��","ʮ","��","��" ,"��"};
        
        private static readonly string[] EncodeString = new string[] { "{##ling##}", "{##yi##}", "{##er##}", "{##san##}",
            "{##si##}","{##wu##}","{##liu##}","{##qi##}","{##ba##}","{##jiu##}","{@@yi@@}","{@@er@@}",
            "{@@san@@}","{@@si@@}","{@@wu@@}","{@@liu@@}","{@@qi@@}","{@@ba@@}","{@@jiu@@}","{@@shi@@}",
            "{@#dh#@}","{@#jh#@}","{@#dunhao#@}"};

        public static readonly Regex TextRegex = new Regex(@"(^|>)(?<Value>[^<>]+)(<|$)", RegexOptions.Compiled);

        public static readonly Regex CharsRegex = new Regex(@"(^|>)[^<>]*[\d����һ�����������߰˾�ʮ][^<>]*(<|$)", RegexOptions.Compiled);
        
        static ImageManage _ImageManage;

        static EncodeHTML()
        {
            _ImageManage = new ImageManage();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static string Encode(string text)
        {
            if (!CharsRegex.IsMatch(text))
                return text;

            if (counter == 0)
            {
                _ImageManage.GeneratePath();
            }

            MatchCollection mc = TextRegex.Matches(text);

            string temp = string.Empty;
            foreach (Match m in mc)
            {
                temp = m.Value;
                for (int i = 0; i < EncodeChars.Length; i++)
                {
                    temp = temp.Replace(EncodeChars[i], EncodeString[i]);
                }
                text = text.Replace(m.Value, temp);
            }
            for (int i = 0; i < EncodeChars.Length; i++)
            {
                text = text.Replace(EncodeString[i], _ImageManage.GetImgTag(EncodeChars[i]));
            }

            Interlocked.Increment(ref counter);
            System.Diagnostics.Trace.Write(counter);
            if (counter > max)
            {
                counter = 0;
            }
            
            return text;
        }
    }

    class ImageManage
    {
        private Dictionary<string, string> _dicImgTag = new Dictionary<string,string>();
        private static Dictionary<string, string> _dicOriginalPath = null;

        #region ����
        private static Dictionary<string, string> dicOriginalPath
        {
            get
            {
                if (_dicOriginalPath == null)
                {
                    _dicOriginalPath = new Dictionary<string, string>();
                    //����
                    _dicOriginalPath.Add("0", "/enc/orient/0.gif");
                    _dicOriginalPath.Add("1", "/enc/orient/1.gif");
                    _dicOriginalPath.Add("2", "/enc/orient/2.gif");
                    _dicOriginalPath.Add("3", "/enc/orient/3.gif");
                    _dicOriginalPath.Add("4", "/enc/orient/4.gif");
                    _dicOriginalPath.Add("5", "/enc/orient/5.gif");
                    _dicOriginalPath.Add("6", "/enc/orient/6.gif");
                    _dicOriginalPath.Add("7", "/enc/orient/7.gif");
                    _dicOriginalPath.Add("8", "/enc/orient/8.gif");
                    _dicOriginalPath.Add("9", "/enc/orient/9.gif");
                    //��������
                    _dicOriginalPath.Add("һ", "/enc/orient/1c.gif");
                    _dicOriginalPath.Add("��", "/enc/orient/2c.gif");
                    _dicOriginalPath.Add("��", "/enc/orient/3c.gif");
                    _dicOriginalPath.Add("��", "/enc/orient/4c.gif");
                    _dicOriginalPath.Add("��", "/enc/orient/5c.gif");
                    _dicOriginalPath.Add("��", "/enc/orient/6c.gif");
                    _dicOriginalPath.Add("��", "/enc/orient/7c.gif");
                    _dicOriginalPath.Add("��", "/enc/orient/8c.gif");
                    _dicOriginalPath.Add("��", "/enc/orient/9c.gif");
                    _dicOriginalPath.Add("ʮ", "/enc/orient/10c.gif");
                    //���
                    _dicOriginalPath.Add("��", "/enc/orient/dh.gif");
                    _dicOriginalPath.Add("��", "/enc/orient/jh.gif");
                    _dicOriginalPath.Add("��", "/enc/orient/dunhao.gif");
                    //_dicOriginalPath.Add(",", "/enc/orient/dh.gif");
                    //_dicOriginalPath.Add(".", "/enc/orient/jh.gif");
                }
                return _dicOriginalPath;
            }
        }
        #endregion ����

        public Dictionary<string, string> GeneratePath()
        {
            if (string.IsNullOrEmpty(WebPath.ImagePath))
                throw new Exception("�����ļ����Ҳ���ImagePath");
            lock (_dicImgTag)
            {
                _dicImgTag.Clear();
                string s = string.Empty;
                string copyto = string.Empty;
                string Imagedir = string.Empty;
                MD5 m = MD5CryptoServiceProvider.Create();
                foreach (string encodechar in EncodeHTML.EncodeChars)
                {
                    s = DateTime.Now.Ticks.ToString();
                    s = s.Insert(DateTime.Now.Millisecond % s.Length, encodechar);
                    s = GetString(m.ComputeHash(Encoding.UTF8.GetBytes(s)));
                    //Imagedir = ImgHelp.GetImageDirectory();
                    copyto = string.Format("{0}/enc{1}{2}.gif", WebPath.ImagePath, Imagedir, s);
                    CDirectory.Create(copyto.Substring(0, copyto.LastIndexOfAny(new char[] { '\\', '/' })));
                    File.Copy(string.Format("{0}{1}", WebPath.ImagePath, dicOriginalPath[encodechar]), copyto);

                    _dicImgTag.Add(encodechar, string.Format("<img src=\"http://img.39.net/enc/{0}{1}.gif\"/>", Imagedir, s));
                }
                m.Clear();
            }

            return _dicImgTag;
        }

        /// <summary>
        /// ���ݴ�����ַ���ȡͼƬ��·��
        /// </summary>
        /// <param name="encodechar">Ҫ������ַ�</param>
        /// <returns></returns>
        public string GetImgTag(string encodechar)
        {
            if (string.IsNullOrEmpty(WebPath.ImagePath))
                throw new Exception("�����ļ����Ҳ���ImagePath");
            lock (_dicImgTag)
            {
                if (_dicImgTag == null)
                    throw new Exception("��δ��ʼ��ͼƬ·��");

                return _dicImgTag[encodechar];
            }
        }

        private static string GetString(byte[] bs)
        {
            string[] s = new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q",
                "r","s","t","u","v","w","x","y","z","0","1","2","3","4","5","6","7","8","9"};
            string temp = string.Empty;
            foreach (byte sb in bs)
            {
                temp += s[((int)sb) % 35];
            }
            return temp;
        }
    }
}
