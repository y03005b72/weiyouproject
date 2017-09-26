using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Collections;
using System.IO;

namespace com.Utility
{
    public class jsHelper : System.Web.UI.Page
    {
        #region �򿪴���
        public static void openFullWindow(Page thisPage, string sURL)
        {
            thisPage.Response.Write("<script language=javascript> var x =screen.availWidth -10;</script>");
            thisPage.Response.Write("<script language=javascript> var y =screen.availHeight - 50;</script>");
            thisPage.Response.Write("<script language=javascript> window.open('" + sURL + "','NEW','resizable=yes,status=yes,toolbar=no,menubar=no,location=no,height='+ y +',width='+ x +',top=0,left=0,fullscreen=0');</script>");
            //thisPage.Response.Write("<script language=javascript> window.open('Includes/WebMain.aspx','Main_WIN','resizable=yes,status=yes,toolbar=no,menubar=no,location=no,height=' + y + ',width=' + x + ',top=0,left=0,fullscreen=0');</script>");
            //height=100, width=400, toolbar=no, menubar=no, scrollbars=no, resizable=no, location=no, status=no
        }
        /// <summary>
        /// ���´���
        /// </summary>
        /// <param name="url"></param>
        /// <param name="self"></param>
        public static void OpenWindow(Page page, string url, string self)
        {
            page.ClientScript.RegisterStartupScript(typeof(System.String), "", "<script language='javascript'>window.open('" + url + "',target ='" + self + "');<" + "/script>");
        }
        /// <summary>
        /// ���´���
        /// </summary>
        /// <param name="url"></param>
        /// <param name="self"></param>
        public static void OpenWindow(Page thisPage, string sUrl, int iWidth, int iHeight)
        {
            //thisPage.Response.Write("<script language=javascript> var x =screen.availWidth -10;</script>");
            //thisPage.Response.Write("<script language=javascript> var y =screen.availHeight - 50;</script>");
            thisPage.Response.Write("<script language=javascript> var x1 =(screen.availWidth + 20 - " + iWidth + ")/2;</script>");
            thisPage.Response.Write("<script language=javascript> var y1 =(screen.availHeight - " + iHeight + ")/2;</script>");
            thisPage.Response.Write("<script language=javascript> var x =" + iWidth + ";</script>");
            thisPage.Response.Write("<script language=javascript> var y =" + iHeight + ";</script>");
            thisPage.Response.Write("<script language=javascript> window.open('" + sUrl + "','NEW','resizable=yes,status=yes,toolbar=no,menubar=no,location=no,height='+ y +',width='+ x +',top='+ y1 +',left='+ y1 +',fullscreen=0');</script>");
        }
        public static void ShowModalDialog(Page thisPage, string sUrl, int Width, int Height)
        {
            string jsText = string.Format("<script language='javascript' type='text/javascript'>window.showModalDialog('{0}','', 'dialogWidth={1}px;dialogHeight={2}px;top=0;left=0;toolbar=no;menubar=no;scrollbars=no;resizable=no;location=no;status=no'); </script>", sUrl, Width, Height);
            thisPage.ClientScript.RegisterStartupScript(typeof(String), "", jsText);
        }
        public static void OpenModalDialogWindow(Page thisPage, string sURL, int iHeight, int iWidth, bool bResize, bool bStatus)
        {
            string sFeatures = "dialogHeight: " + iHeight.ToString() + "px; dialogWidth: " + iWidth.ToString() + "px;  resizable: " + (bResize ? "yes" : "no") + "; status: " + (bStatus ? "yes" : "no") + ";";
            thisPage.ClientScript.RegisterStartupScript(typeof(System.String), "", "<script language='javascript'>window.showModalDialog('" + sURL + "','','" + sFeatures + "')</script>");
        }
        public static void OpenModalDialogWindow(Page thisPage, string sURL, int iHeight, int iWidth, bool bResize, bool bStatus, string sHiddenID)
        {
            string sFeatures = "dialogHeight: " + iHeight.ToString() + "px; dialogWidth: " + iWidth.ToString() + "px;  resizable: " + (bResize?"yes":"no") + "; status: " + (bStatus?"yes":"no") + ";";
            thisPage.ClientScript.RegisterStartupScript(typeof(System.String), "", "<script language='javascript'>document.getElementById('" + sHiddenID + "').value=window.showModalDialog('" + sURL + "','','" + sFeatures + "')</script>");
        }
        #endregion  �򿪴���

        #region �رմ���
        //public static void CloseModalDialogWindow(Page thisPage, string sRetureValue)
        //{
        //    thisPage.ClientScript.RegisterStartupScript(typeof(System.String), "", "<stript language='javascript'>window.returnValue='" + sRetureValue + "';self.close();</script>");
        //}
        #endregion �رմ���



        #region ��ʾ��Ϣ
        /// <summary> 
        /// �������˵���alert�Ի��� 
        /// </summary> 
        /// <param name="str_Message">��ʾ��Ϣ,���ӣ�"����Ϊ��!"</param> 
        /// <param name="page">Page��</param> 
        public static void Alert(Page page, string sMessage)
        {
            page.ClientScript.RegisterStartupScript(typeof(System.String), "", "<script language='javascript' type='text/javascript' defer='defer'>alert('" + sMessage + "');</script>");
        }

        /// <summary> 
        /// �������˵���alert�Ի��� 
        /// </summary> 
        /// <param name="str_Message">��ʾ��Ϣ,���ӣ�"����Ϊ��!"</param> 
        /// <param name="page">Page��</param> 
        public  static void Alert(string sMessage)
        {
            HttpContext _context = HttpContext.Current;
            _context.Response.Write("<script language='javascript' type='text/javascript' defer='defer'>");
            _context.Response.Write("window.alert(" + "'" + sMessage.Replace("'", "").Replace("\r", "").Replace("\n", "") + "'" + ")");
            _context.Response.Write("</script>");
        }

        public static void Alert(string sMessage, string Url)
        {
            HttpContext _context = HttpContext.Current;
            StringBuilder sb = new StringBuilder();
            sb.Append("<script language='javascript' type='text/javascript' defer='defer'>");
            sb.Append("window.alert(" + "'" + sMessage.Replace("'", "").Replace("\r", "").Replace("\n", "") + "'" + ");");
            sb.Append("window.location.href='" + Url + "';");
            sb.Append("</script>");
            _context.Response.Write(sb.ToString());
        }

        public static void ConfirmClose(string sMessage)
        {
            HttpContext _context = HttpContext.Current;
            StringBuilder sb = new StringBuilder();
            sb.Append("<script language='javascript' type='text/javascript'>");
            sb.Append(@"if(confirm('"+sMessage+"'))  ");
            sb.Append("self.opener=null;self.open('','_self'); self.close();");
            sb.Append("</script>");
            _context.Response.Write(sb.ToString());

        }
        /// <summary> 
        /// �������˵���alert�Ի��򣬲�ʹ�ؼ���ý��� 
        /// </summary> 
        /// <param name="str_Ctl_Name">��ý���ؼ�Idֵ,���磺txt_Name</param> 
        /// <param name="str_Message">��ʾ��Ϣ,���ӣ�"������������!"</param> 
        /// <param name="page">Page��</param> 
        public static void Alert(Page page, string sCtlName, string sMessage)
        {
            page.ClientScript.RegisterStartupScript(typeof(System.String), "", "<script language='javascript' type='text/javascript' defer='defer'>alert('" + sMessage + "');document.forms(0)." + sCtlName + ".focus(); document.forms(0)." + sCtlName + ".select();</script>");
        }
        /// <summary> 
        /// �������˵���confirm�Ի��� 
        /// </summary> 
        /// <param name="str_Message">��ʾ��Ϣ,���ӣ�"���Ƿ�ȷ��ɾ��!"</param> 
        /// <param name="btn">����Botton��ťIdֵ,���磺btn_Flow</param> 
        /// <param name="page">Page��</param> 
        public static void ConfirmResponse(Page page, string sMessage, string sUrl)
        {
            page.ClientScript.RegisterStartupScript(typeof(System.String), "", "<script language='javascript' type='text/javascript' defer='defer'> if (confirm('" + sMessage + "')==true){ window.location.href = '" + sUrl + "';}</script>");
        }
        /// <summary> 
        /// �������˵���confirm�Ի��� 
        /// </summary> 
        /// <param name="str_Message">��ʾ��Ϣ,���ӣ�"���Ƿ�ȷ��ɾ��!"</param> 
        /// <param name="btn">����Botton��ťIdֵ,���磺btn_Flow</param> 
        /// <param name="page">Page��</param> 
        public static void Confirm(Page page, string sMessage, string btn)
        {
            page.ClientScript.RegisterStartupScript(typeof(System.String), "", "<script language='javascript' type='text/javascript' defer='defer'> if (confirm('" + sMessage + "')==true){document.forms(0)." + btn + ".click();}</script>");
        }
        /// <summary> 
        ///  �������˵���confirm�Ի���,ѯ���û�׼��ת����Щ������������ȷ�����͡�ȡ����ʱ�Ĳ��� 
        /// </summary> 
        /// <param name="str_Message">��ʾ��Ϣ�����磺"�ɹ���������,����\"ȷ��\"��ť��д����,����\"ȡ��\"�޸�����"</param> 
        /// <param name="btn_Redirect_Flow">"ȷ��"��ťidֵ</param> 
        /// <param name="btn_Redirect_Self">"ȡ��"��ťidֵ</param> 
        /// <param name="page">Page��</param> 
        public static void Confirm(Page page, string sMessage, string btn_Redirect_Flow, string btn_Redirect_Self)
        {
            page.ClientScript.RegisterStartupScript(typeof(System.String), "", "<script language='javascript' type='text/javascript' defer='defer'> if (confirm('" + sMessage + "')==true){document.forms(0)." + btn_Redirect_Flow + ".click();}else{document.forms(0)." + btn_Redirect_Self + ".click();}</script>");
        }

        /// <summary> 
        /// ʹ�ؼ���ý��� 
        /// </summary> 
        /// <param name="str_Ctl_Name">��ý���ؼ�Idֵ,���磺txt_Name</param> 
        /// <param name="page">Page��</param> 
        public static void GetFocus(Page page, string sCtlName)
        {
            page.ClientScript.RegisterStartupScript(typeof(System.String), "", "<script language='javascript' type='text/javascript' defer='defer'>document.forms(0)." + sCtlName + ".focus(); document.forms(0)." + sCtlName + ".select();</script>");
        }
        public static void ShowException(System.Exception e)
        {
            if (e.Message.IndexOf("COLUMN REFERENCE Լ��") > 0) // �����ͻ 
                ShowMessage(5, "");
            else if (e.Message.IndexOf("PRIMARY KEY Լ��") > 0)  //������ͻ
                ShowMessage(6, "");

            else if (e.Message.IndexOf("�������") > 0)  //�����ж�
                ShowMessage(3, "");
            else
                ShowMessage(e.Message);

        }
        //��ʾ������Ϣ 
        public static void ShowMessage(string strMessage)
        {
            HttpContext _context = HttpContext.Current;
            _context.Response.Write("<script language='javascript' type='text/javascript'> ");
            _context.Response.Write("window.alert(" + "'" + strMessage.Replace("'", "").Replace("\r", "").Replace("\n", "") + "'" + ")");
            _context.Response.Write("</script>");
        }
        //���ݴ������ʾ��Ϣ 
        public static void ShowMessage(int iErrID, string Col_Name)
        {
            string strMessage = Errinfo(iErrID, Col_Name);
            HttpContext _context = HttpContext.Current;
            _context.Response.Write("<script language='javascript' type='text/javascript'> ");
            _context.Response.Write("window.alert(" + "'" + strMessage + "'" + ")");
            _context.Response.Write("</script>");
        }
        private static string Errinfo(int iErrID, string Col_Name)
        {
            switch (iErrID)
            {
                case 0:
                    return "�����ͻ: " + Col_Name + " �Ѿ�����";
                case 1:
                    return "�����ͻ: " + Col_Name + " ���ӱ��д���";
                case 2:
                    return "���ݿ�����ʧ��";
                case 3:
                    return "���ݿ��ѯ����ʧ��";

                case 4:
                    return "����������";
                case 5:
                    return "�����ͻ," + Col_Name + "����ʧ��";
                case 6:
                    return "�ظ�����," + Col_Name + "����ʧ��";
                case 994:
                    return Col_Name + "����ʧ��";
                case 995:
                    return Col_Name;
                case 996:
                    return "�������";
                case 997:
                    return Col_Name + "������";
                case 998:
                    return Col_Name + "����Ϊ��";
                case 999:
                    return "�Բ���,��û��Ȩ��ʹ��";
                case 1000:
                    return Col_Name + "�����ɹ�";
                default:

                    return "";
            }
        }
        #endregion ��ʾ��Ϣ
         
        #region �ض���
        /// <summary>
        /// �ض�λ����ҳ��
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="page">page</param>
        public static void redirect(Page page, string url)
        {
            page.ClientScript.RegisterStartupScript(typeof(System.String), "", "<script>window.location.href='" + url + "';</script>");
        }
        /// <summary>
        /// �������˵���alert�Ի���,���ض�λ����ҳ��
        /// </summary>
        /// <param name="page"></param>
        /// <param name="sMessage"></param>
        /// <param name="url"></param>
        public static void redirect(Page page, string sMessage, string url)
        {
            page.ClientScript.RegisterStartupScript(typeof(System.String), "", "<script>alert('" + sMessage + "');window.location.href='" + url + "';</script>");
        }
        #endregion �ض���  

        #region ���js�ű�
        public static void WriteJS(string strJS)
        {
           HttpContext _context = HttpContext.Current;
            _context.Response.Write("<script language='javascript' type='text/javascript'> ");
            _context.Response.Write( strJS );
            _context.Response.Write("</script>");
        }
        #endregion  ���js�ű�
    }
}
