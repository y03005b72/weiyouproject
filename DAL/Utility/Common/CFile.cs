using System;
using System.IO ;
using System.Text;
using System.Collections.Generic;
using System.Web;

namespace com.Utility
{
	/// <summary>
	/// A simple file handlng class
	/// </summary>
	public class CFile
    {  
        #region �ļ�������
        public static  string Read(string sFile)
        {
            return Read(sFile, "gb2312");
        }
        public static  string Read(string sFile, string sCoding)
        {
            Encoding code = Encoding.GetEncoding(sCoding);
            // ��ȡ�ļ� 
            StreamReader sr = null;
            string strContent = "";
            try
            {
                sr = new StreamReader(sFile, code);
                strContent = sr.ReadToEnd(); // ��ȡ�ļ� 
            }
            catch  
            { 
                 
            }
            finally
            {
                if (sr != null)
                    sr.Close();
            }
            return strContent;
        }
        #endregion �ļ�������

        #region �ļ�д����
        public static void Write(List<FileEntity> listFile)
        {
            foreach (FileEntity file in listFile)
            {
                Write(file);
            }
        }
        public static void Write(FileEntity file)
        {
            CDirectory.Create(file.FilePath );
            Write(file.FilePath + "\\" + file.FileName, file.FileContent);
        }
        public static void Write(string sFullFilePath, string sContent)
        {
            Write(sFullFilePath, sContent, "gb2312");
        }
        public static void Write(string sFullFilePath, string sContent, string sCoding)
        { 
			// д�ļ� 
			StreamWriter sw=null; 
			Encoding code = Encoding.GetEncoding(sCoding);
            try
            {
                FileInfo f = new FileInfo(sFullFilePath); 
             
                if (f.Exists)
                { 
                    f.Attributes = FileAttributes.Normal;                 
                    //f.Delete();
                    Create(sFullFilePath);
                    sw = new StreamWriter(sFullFilePath, false, code);
                    sw.Write(sContent); 
                    sw.Flush();
                    sw.Close(); 
                }
                else
                {
                    sw = new StreamWriter(sFullFilePath, false, code);
                    sw.Write(sContent);
                    sw.Flush();
                    sw.Close();
                }            
            }
            catch (Exception Ex)
            {
                throw new Exception(sFullFilePath + Ex.Message);
            }
            finally
            {
                if (sw != null)
                    sw.Close();
            }
        }

        public static void Write(string sFullFilePath, byte[]  btData)
        {
            // д�ļ�  
            FileStream fs = null;
            try
            {
                FileInfo f = new FileInfo(sFullFilePath);
                if (f.Exists)
                {
                    f.Attributes = FileAttributes.Normal;
                    f.Delete(); 
                } 
                fs = new FileStream(sFullFilePath, FileMode.Create, FileAccess.Write);
                fs.Write(btData, 0, btData.Length);
                fs.Flush();
                fs.Close();
            }
            catch (Exception Ex)
            {
                throw new Exception(sFullFilePath + Ex.Message);
            }
            finally
            {
                if (fs != null)
                    fs.Close();
            }
        }

        public static void WriteAppendLine(string sFullFilePath, string sContent)
        {
            // д�ļ� 
            StreamWriter sw = null;
            try
            {
                FileInfo f = new FileInfo(sFullFilePath);
                if (!f.Exists)
                {
                    Create(sFullFilePath);
                }                
                sw = f.AppendText();                
                sw.WriteLine(sContent);
                
                sw.Flush();
                sw.Close();

            }
            catch (Exception Ex)
            {
                throw new Exception(sFullFilePath + Ex.Message);
            }
            finally
            {
                if (sw != null)
                    sw.Close();
            }
        }
        #endregion �ļ�д����

        #region FileInfo ��� 

        public static void SetLastModify(string sFile )
        {
            SetLastModify(sFile, DateTime.Parse("1990-1-1"));
        }
        public static void  SetLastModify(string sFile, DateTime lastModify)
        {
            FileInfo file = new FileInfo(sFile); 
            if (file.Exists)
            {
                file.Attributes = FileAttributes.Normal;
                file.LastWriteTime = lastModify;
            } 
        }
        #endregion FileInfo ���

        #region ɾ���ļ�
        public static void  Delete(string sPath)
        {
            try
            {
                FileInfo f = new FileInfo(sPath);
                if (f.Exists)
                {
                    f.Attributes = FileAttributes.Normal;
                    f.Delete();
                }                
            }
            catch  
            {
                 
            }
        }
        #endregion ɾ���ļ�

        #region ����·��
        public static void Create(string sPath)
        {
            FileStream fs = null;
            try
            {
                string absolutePath = Path.GetDirectoryName(sPath);
                if (!Directory.Exists(absolutePath))
                {
                    Directory.CreateDirectory(absolutePath);
                }
                fs = File.Create(sPath);
                fs.Close();
            }
            catch(Exception ex)
            { Console.WriteLine(ex.Message); }
            finally 
            {
                if (fs != null)
                    fs.Close();
            }
        }
        #endregion ����·��

        public static string  UploadFile(System.Web.UI.HtmlControls.HtmlInputFile file, string basePath)
        {
            if (file.PostedFile.ContentLength > 1024 * 1024)
            {
                jsHelper.Alert("�ϴ��ļ����ô���1M");
                return "";
            }
            if (!file.PostedFile.FileName.ToLower().EndsWith(".jpg"))
            {
                jsHelper.Alert("ֻ���ϴ�jpgͼƬ");
                return "";
            }
            string tempPath = string.Format("{0}/{1}/{2}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            string tempFile = string.Format("{0}.jpg", BitConverter.ToUInt32(Guid.NewGuid().ToByteArray(), 0).ToString());
            CDirectory.Create(Path.Combine(basePath, tempPath));
            file.PostedFile.SaveAs(Path.Combine(Path.Combine(basePath, tempPath), tempFile));
            string sReturnUrl = string.Format("http://bbsimg.39.net/{0}/{1}",  tempPath ,tempFile);
            return sReturnUrl;
        }

        public static string UploadImage(System.Web.UI.HtmlControls.HtmlInputFile file, string basePath)
        {
            jsHelper js = new jsHelper();
            if (file.PostedFile.ContentLength > 1024 * 1024)
            {
                jsHelper.Alert("�ϴ��ļ����ô���1M");
                return "";
            }
            if (!file.PostedFile.FileName.ToLower().EndsWith(".jpg"))
            {
                jsHelper.Alert("ֻ���ϴ�jpgͼƬ");
                return "";
            }
            string tempPath = string.Format("{0}/{1}/{2}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            tempPath = string.Format("{0}/{1}", basePath.TrimEnd(new char[] { '/' }), tempPath);
            string tempFile = string.Format("{0}.jpg", BitConverter.ToUInt32(Guid.NewGuid().ToByteArray(), 0).ToString());
            string fullPath = HttpContext.Current.Server.MapPath(tempPath);
            CDirectory.Create(fullPath);
            file.PostedFile.SaveAs(Path.Combine(fullPath, tempFile));
            string sReturnUrl = string.Format("{0}/{1}", tempPath, tempFile);
            return sReturnUrl;
        }

    }
}
