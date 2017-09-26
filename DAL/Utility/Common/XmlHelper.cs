using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Xml;

namespace com.Utility
{
    public class XmlHelper
    {
        #region ��ȡInnerText
        public static string SelectNodeText(string xml, string xPath)
        {
            if (string.IsNullOrEmpty(xml) || string.IsNullOrEmpty(xPath))
                return string.Empty;

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            return SelectNodeText(doc, xPath);
        }
        public static string SelectNodeText(XmlDocument doc, string xPath)
        {
            if (doc == null || string.IsNullOrEmpty(xPath))
                return string.Empty;

            XmlNode node = doc.SelectSingleNode(xPath);
            return node == null ? string.Empty : node.InnerText;
        }
        public static string SelectNodeText(XmlNode node, string xPath)
        {
            if (node == null || string.IsNullOrEmpty(xPath))
                return string.Empty;

            XmlNode mNode = node.SelectSingleNode(xPath);
            return mNode == null ? string.Empty : mNode.InnerText;
        }
        #endregion ��ȡInnerText

        #region ��ȡAttribute
        public static string SelectAttribute(string xml, string xPath)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);

            return SelectAttribute(doc, xPath);
        }
        public static string SelectAttribute(XmlDocument doc, string xPath)
        {
            if (doc != null)
            {
                XmlNode node = doc.SelectSingleNode(xPath);
                if (node != null)
                    return node.Value;
            }
            return string.Empty;
        }
        public static string SelectAttribute(XmlNode node, string xPath)
        {
            if (node == null)
                return string.Empty;
            XmlNode mNode = node.SelectSingleNode(xPath); ;
            if (mNode != null)
                return mNode.Value;
            return string.Empty;
        }
        #endregion ��ȡAttribute
    }
}
