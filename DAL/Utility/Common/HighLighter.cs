using System.Collections.Generic;

namespace com.Utility
{
    public class HighLighter
    {
        /// <summary>
        /// ����ĳ����������Ĺؼ���
        /// </summary>
        /// <param name="keyWords">Ҫ������ʾ�Ĺؼ������鼯��</param>
        /// <param name="Content">ԭ������</param>
        /// <param name="cssClass">�������ֵ�CSS��ʽ����</param>
        /// <returns>������������ı�</returns>
        public static string HighLight(string[] keyWords, string Content, string cssClass)
        {
            foreach (string keyword in keyWords)
            {
                Content = Content.Replace(keyword, string.Format("<span class=\"{0}\">{1}</span>", cssClass, keyword));
            }
            return Content;
        }

        /// <summary>
        /// ����ĳ����������Ĺؼ���
        /// </summary>
        /// <param name="keyWords">Ҫ������ʾ�Ĺؼ������鼯��</param>
        /// <param name="Content">ԭ������</param>
        /// <param name="cssClass">�������ֵ�CSS��ʽ����</param>
        /// <returns>������������ı�</returns>
        public static string HighLight(List<string> keyWords, string Content, string cssClass)
        {
            foreach (string keyword in keyWords)
            {
                Content = Content.Replace(keyword, string.Format("<span class=\"{0}\">{1}</span>", cssClass, keyword));
            }
            return Content;
        }
    }
}