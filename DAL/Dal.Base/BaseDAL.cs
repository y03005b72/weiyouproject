using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Text;
using com.Model.Base;
using com.Utility;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace com.DAL.Base
{
    /// <summary>
    /// Copyright (C) 2004-2008 LiTianPing 
    /// ���ݷ��ʻ�����(����SQLServer)
    /// �û������޸������Լ���Ŀ����Ҫ��
    /// </summary> 
    public class BaseDAL<T> where T : BaseModel, new()
    {
        private string _PrimaryKey = "";
        private string _connName = "";
        private string _TableName = "";
        private T _t;

        #region ����
        /// <summary>
        /// ���캯��
        /// </summary>
        public BaseDAL() { }

        public DataBaseEnum dbName
        {
            get { return t.DataBaseName; }
            set { t.DataBaseName = value; }
        }

        /// <summary>
        /// Model������Ե�ʵ��
        /// </summary>
        private T t
        {
            get
            {
                if (_t == null)
                    _t = new T();
                return _t;
            }
        }

        /// <summary>
        /// Ҫ���ӵ����ݿ���
        /// </summary>
        //public string connName
        //{
        //    get
        //    {
        //        return t.DataBaseName.ToString();
        //    }
        //}
        public string connName
        {
            get
            {
                if (t.IsExternalConn)
                {
                    if (string.IsNullOrEmpty(_connName))
                    {
                        _connName = t.connName;
                    }
                    return _connName;
                }
                return t.DataBaseName.ToString();
            }
            set { _connName = value; }
        }

        /// <summary>
        /// �����ֶ���
        /// </summary>
        public string PrimaryKey
        {
            get
            {
                if (_PrimaryKey == "")
                    _PrimaryKey = t.PrimaryKey;
                return _PrimaryKey;
            }
            set { _PrimaryKey = value; }
        }

        /// <summary>
        /// ���ݱ���
        /// </summary>
        public virtual string TableName
        {
            get
            {
                if (_TableName == "")
                {
                    _TableName = "[" + typeof(T).Name + "]";
                }
                return _TableName;
            }
            set { _TableName = value; }
        }

        /// <summary>
        /// ���IDֵ
        /// </summary>
        public int MaxID
        {
            get
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(string.Format("select max({0}) from {1}", PrimaryKey, TableName));
                Database db = DatabaseFactory.CreateDatabase(connName);
                using (DbCommand cmd = db.GetSqlStringCommand(strSql.ToString()))
                {
                    object obj = db.ExecuteScalar(cmd);
                    if (obj == null)
                    {
                        return 0;
                    }

                    #region Added BY  �α�

                    if (obj.ToString().Trim() == "")
                        return 0;

                    #endregion

                    return int.Parse(obj.ToString());
                }
            }
        }

        #endregion ����

        #region BaseMethod

        /// <summary>
        /// �жϼ�¼�Ƿ����
        /// </summary>
        /// <param name="id">��¼ID</param>
        public virtual bool Exists(int id)
        {
            return Exists(string.Format("{0}=@{0}", PrimaryKey),
                          new List<dbParam> { new dbParam { ParamName = PrimaryKey, ParamDbType = DbType.Int32, ParamValue = id } });
        }

        public virtual bool Exists(long id)
        {
            return Exists(string.Format("{0}=@{0}", PrimaryKey),
                          new List<dbParam> { new dbParam { ParamName = PrimaryKey, ParamDbType = DbType.Int64, ParamValue = id } });
        }

        /// <summary>
        /// �жϼ�¼�Ƿ����
        /// </summary>
        /// <param name="strWhere">Where�Ӿ�</param>
        public virtual bool Exists(string strWhere)
        {
            return Exists(strWhere, null);
        }

        public virtual bool Exists(string strWhere, List<dbParam> listPm)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + TableName);
            if (strWhere != "")
                strSql.Append(" where " + strWhere);
            Database db = DatabaseFactory.CreateDatabase(connName);
            using (DbCommand cmd = db.GetSqlStringCommand(strSql.ToString()))
            {
                if (listPm != null)
                {
                    foreach (dbParam pm in listPm)
                    {
                        db.AddInParameter(cmd, pm.ParamName, pm.ParamDbType, pm.ParamValue);
                    }
                }
                object obj = db.ExecuteScalar(cmd);
                int cmdresult;
                if ((Equals(obj, null)) || (Equals(obj, DBNull.Value)))
                {
                    cmdresult = 0;
                }
                else
                {
                    cmdresult = int.Parse(obj.ToString());
                }
                if (cmdresult == 0)
                {
                    return false;
                }
                return true;
            }
        }

        /// <summary>
        /// ɾ����¼
        /// </summary>
        /// <param name="id">��¼ID</param>
        public virtual int Delete(int id)
        {
            return Delete(string.Format("{0}={1}", PrimaryKey, id));
        }

        public virtual int Delete(long id)
        {
            return Delete(string.Format("{0}={1}", PrimaryKey, id));
        }

        /// <summary>
        /// ɾ����¼
        /// </summary>
        /// <param name="strWhere">Where�Ӿ�</param>
        public virtual int Delete(string strWhere)
        {
            return Delete(strWhere, null);
        }

        public virtual int Delete(string strWhere, List<dbParam> listPm)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete " + TableName);
            if (strWhere != "")
                strSql.Append(" where " + strWhere);
            Database db = DatabaseFactory.CreateDatabase(connName);
            using (DbCommand cmd = db.GetSqlStringCommand(strSql.ToString()))
            {
                if (listPm != null)
                {
                    foreach (dbParam pm in listPm)
                    {
                        db.AddInParameter(cmd, pm.ParamName, pm.ParamDbType, pm.ParamValue);
                    }
                }
                return db.ExecuteNonQuery(cmd);
            }
        }

        /// <summary>
        /// ��ü�¼��
        /// </summary>
        /// <param name="strWhere">Where�Ӿ�</param>
        public virtual int GetCount(string strWhere)
        {
            return GetCount(strWhere, null);
        }

        public virtual int GetCount(string strWhere, List<dbParam> listPm)
        {
            string strSQL = "select count(0) from " + TableName;
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSQL += " where " + strWhere;
            }
            Database db = DatabaseFactory.CreateDatabase(connName);
            using (DbCommand cmd = db.GetSqlStringCommand(strSQL))
            {
                if (listPm != null)
                {
                    foreach (dbParam pm in listPm)
                    {
                        db.AddInParameter(cmd, pm.ParamName, pm.ParamDbType, pm.ParamValue);
                    }
                }
                return int.Parse(db.ExecuteScalar(cmd).ToString());
            }
        }
        #endregion BaseMethod

        #region Add

        /// <summary>
        /// ��Ӽ�¼�����ص�ǰ�����������¼��ID
        /// </summary>
        /// <param name="model">ʵ���ĳʵ���ʵ��</param>
        public virtual int Add(T model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strParameter = new StringBuilder();
            strSql.Append(string.Format("insert into {0}(", TableName));
            PropertyInfo[] pis = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public);
            List<dbParam> listParam = new List<dbParam>();
            for (int i = 0; i < pis.Length; i++)
            {
                if (t.IsAutoID)
                {
                    if (t.PrimaryKey == pis[i].Name)
                        continue;
                }
                strSql.Append(pis[i].Name + ","); //����SQL���ǰ�벿�� 
                strParameter.Append("@" + pis[i].Name + ","); //�������SQL���
                listParam.Add(new dbParam
                                  {
                                      ParamName = pis[i].Name,
                                      ParamDbType = TypeConvert.GetDbType(pis[i].PropertyType),
                                      ParamValue = pis[i].GetValue(model, null)
                                  });
            }
            strSql = strSql.Replace(",", ")", strSql.Length - 1, 1);
            strParameter = strParameter.Replace(",", ")", strParameter.Length - 1, 1);
            strSql.Append(" values (");
            strSql.Append(strParameter + ";");
            if (t.IsAutoID)
            {
                strSql.Append("select SCOPE_IDENTITY()");
            }
            Database db = DatabaseFactory.CreateDatabase(connName);
            using (DbCommand cmd = db.GetSqlStringCommand(strSql.ToString()))
            {
                foreach (dbParam dbpm in listParam)
                {
                    db.AddInParameter(cmd, dbpm.ParamName, dbpm.ParamDbType, dbpm.ParamValue);
                }
                object obj = db.ExecuteScalar(cmd);
                return TypeConvert.ToInt32(obj);
            }
        }

        #endregion Add

        #region Update

        ///// <summary>
        ///// ���¼�¼
        ///// </summary>
        ///// <param name="model">��Ҫ���µ����ݿ��ʵ����</param> 
        //public virtual bool Update(T model)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("update  " + TableName + " set ");
        //    PropertyInfo[] pis =
        //        typeof (T).GetProperties(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public);
        //    List<dbParam> listParam = new List<dbParam>();
        //    for (int i = 0; i < pis.Length; i++)
        //    {
        //        if (pis[i].Name != PrimaryKey)
        //        {
        //            strSql.Append(pis[i].Name + "=" + "@" + pis[i].Name + ",");
        //        }
        //        listParam.Add(new dbParam
        //                          {
        //                              ParamDbType = TypeConvert.GetDbType(pis[i].PropertyType),
        //                              ParamName = pis[i].Name,
        //                              ParamValue = pis[i].GetValue(model, null)
        //                          });
        //    }
        //    strSql = strSql.Replace(",", " ", strSql.Length - 1, 1);
        //    strSql.Append(" where " + PrimaryKey + "=@" + PrimaryKey);
        //    Database db = DatabaseFactory.CreateDatabase(connName);
        //    using (DbCommand cmd = db.GetSqlStringCommand(strSql.ToString()))
        //    {
        //        foreach (dbParam dbpm in listParam)
        //        {
        //            db.AddInParameter(cmd, dbpm.ParamName, dbpm.ParamDbType, dbpm.ParamValue);
        //        }
        //        return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        //    }
        //}

        /// <summary>
        /// ���¼�¼
        /// </summary>
        /// <param name="model">��Ҫ���µ����ݿ��ʵ����</param>
        public virtual bool Update(T model)
        {
            return Update(model, null);
        }
        /// <summary>
        /// ���¼�¼
        /// </summary>
        /// <param name="model">��Ҫ���µ����ݿ��ʵ����</param>
        /// <param name="sColList">��Ҫ���µ��ֶ�</param>
        public virtual bool Update(T model, string sColList)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update  " + TableName + " set ");
            PropertyInfo[] pis =
                typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public);
            List<dbParam> listParam = new List<dbParam>();
            if (string.IsNullOrEmpty(sColList))
            {
                for (int i = 0; i < pis.Length; i++)
                {
                    if (pis[i].Name != PrimaryKey)
                    {
                        strSql.Append(pis[i].Name + "=" + "@" + pis[i].Name + ",");
                    }
                    listParam.Add(new dbParam
                    {
                        ParamDbType = TypeConvert.GetDbType(pis[i].PropertyType),
                        ParamName = pis[i].Name,
                        ParamValue = pis[i].GetValue(model, null)
                    });
                }
            }
            else
            {
                string[] strArr = sColList.Split(',');
                foreach (PropertyInfo pi in pis)
                {
                    for (int i = 0; i < strArr.Length; i++)
                    {
                        if (pi.Name == PrimaryKey || pi.Name != strArr[i]) continue;
                        strSql.Append(pi.Name + "=" + "@" + pi.Name + ",");

                    }

                    listParam.Add(new dbParam
                    {
                        ParamDbType = TypeConvert.GetDbType(pi.PropertyType),
                        ParamName = pi.Name,
                        ParamValue = pi.GetValue(model, null)
                    });
                }
            }
            strSql = strSql.Replace(",", " ", strSql.Length - 1, 1);
            strSql.Append(" where " + PrimaryKey + "=@" + PrimaryKey);
            Database db = DatabaseFactory.CreateDatabase(connName);
            using (DbCommand cmd = db.GetSqlStringCommand(strSql.ToString()))
            {
                foreach (dbParam dbpm in listParam)
                {
                    db.AddInParameter(cmd, dbpm.ParamName, dbpm.ParamDbType, dbpm.ParamValue);
                }
                return db.ExecuteNonQuery(cmd) > 0 ? true : false;
            }
        }
        #endregion Update

        #region GetModel

        /// <summary>
        /// ���һ��Model����ʵ��
        /// </summary>
        /// <param name="id">����ID��ֵ</param>
        public virtual T GetModel(int id)
        {
            return GetModel(string.Format("{0}={1}", PrimaryKey, id));
        }

        public virtual T GetModel(long id)
        {
            return GetModel(string.Format("{0}={1}", PrimaryKey, id));
        }

        /// <summary>
        /// ���һ��Model����ʵ��
        /// </summary>
        /// <param name="ID">����ID��ֵ</param>
        /// <param name="sColList">�Զ��ŷָ��Ĳ�ѯ�ֶ�����</param>
        public virtual T GetModel(int ID, string sColList)
        {
            string strWhere = PrimaryKey + "=" + ID.ToString();
            return GetModel(strWhere, sColList, 0);
        }

        /// <summary>
        /// ���һ��Model����ʵ��
        /// </summary>
        /// <param name="strWhere">Where�Ӿ�</param>
        public virtual T GetModel(string strWhere)
        {
            return GetModel(strWhere, null);
        }
        /// <summary>
        /// �ȿ�����sqlע�봦��Ҳ���Բ��ָ�����ֶ�
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="listPm"></param>
        /// <param name="s">ָ�����ֶ�</param>
        /// <returns></returns>
        public virtual T GetModel(string strWhere, List<dbParam> listPm, string s)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(string.Format("select top 1 {0} from {1}", s, TableName));
            if (!string.IsNullOrEmpty(strWhere))
                strSql.Append("  where " + strWhere);
            T model = default(T);
            Database db = DatabaseFactory.CreateDatabase(connName);
            using (DbCommand cmd = db.GetSqlStringCommand(strSql.ToString()))
            {
                if (listPm != null)
                {
                    foreach (dbParam pm in listPm)
                    {
                        db.AddInParameter(cmd, pm.ParamName, pm.ParamDbType, pm.ParamValue);
                    }
                }
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    if (dr.Read())
                        model = GetModel(dr);
                }
            }
            return model;
        }

        public virtual T GetModel(string strWhere, List<dbParam> listPm)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(string.Format("select top 1 {0} from {1}", "*", TableName));
            if (!string.IsNullOrEmpty(strWhere))
                strSql.Append("  where " + strWhere);
            T model = default(T);
            Database db = DatabaseFactory.CreateDatabase(connName);
            using (DbCommand cmd = db.GetSqlStringCommand(strSql.ToString()))
            {
                if (listPm != null)
                {
                    foreach (dbParam pm in listPm)
                    {
                        db.AddInParameter(cmd, pm.ParamName, pm.ParamDbType, pm.ParamValue);
                    }
                }
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    if (dr.Read())
                        model = GetModel(dr);
                }
            }
            return model;
        }

        /// <summary>
        /// ���һ��Model����ʵ��
        /// </summary>
        /// <param name="strWhere">Where�Ӿ�</param>
        /// <param name="sColList">�Զ��ŷָ��Ĳ�ѯ�ֶ�����</param>
        /// <param name="iIndex">��ʶ��ʵ�������ݿ����ǵڼ���</param>
        public virtual T GetModel(string strWhere, string sColList, int iIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(string.Format("select top {0} {1} from {2}", iIndex + 1, sColList, TableName));
            if (strWhere != "")
                strSql.Append(string.Format("  where {0}", strWhere));
            T model = default(T);
            Database db = DatabaseFactory.CreateDatabase(connName);
            DbCommand cmd = db.GetSqlStringCommand(strSql.ToString());
            using (IDataReader dr = db.ExecuteReader(cmd))
            {
                //while (iIndex > 0)
                //{
                //    if (dr.Read())
                //        iIndex--;
                //    else
                //        return null;
                //}
                if (dr.Read())
                    model = GetModel(dr);
            }
            return model;
        }

        /// <summary>
        /// ���һ��Model����ʵ��
        /// </summary>
        public virtual T GetModel(IDataReader dr)
        {
            T model = new T();
            PropertyInfo[] pis =
                typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public);
            int iIndex;
            foreach (PropertyInfo pi in pis)
            {
                try
                {
                    iIndex = dr.GetOrdinal(pi.Name);
                }
                catch (IndexOutOfRangeException)
                {
                    continue;
                }
                if (dr.IsDBNull(iIndex))
                    continue;
                dr.GetValue(iIndex);
                pi.SetValue(model, dr.GetValue(iIndex), null);
            }
            return GetModel(model);
        }

        public virtual T GetModel(T model)
        {
            return model;
        }

        /// <summary>
        /// ���һ��Model����ʵ��
        /// </summary>
        /// <param name="drv">��������ͼ</param>
        /// <returns>Model����ʵ��</returns>
        public virtual T GetModel(DataRowView drv)
        {
            T model = new T();
            PropertyInfo[] pis = model.GetType().GetProperties(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public);
            foreach (PropertyInfo pi in pis)
            {
                try
                {
                    if (drv[pi.Name].ToString() != "")
                    {
                        pi.SetValue(model, drv[pi.Name], null);
                    }
                }
                catch (ArgumentException)
                {
                    continue;
                }
            }
            return model;
        }

        #endregion GetModel

        #region GetMosaicModel

        public virtual T GetMosaicModel(string strSql)
        {
            T model = default(T);
            Database db = DatabaseFactory.CreateDatabase(connName);
            using (DbCommand cmd = db.GetSqlStringCommand(strSql))
            {
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    if (dr.Read())
                        model = GetModel(dr);
                }
            }
            return model;
        }

        #endregion

        #region  GetList

        /// <summary>
        /// ������ݼ�
        /// </summary>
        /// <returns></returns>
        public virtual List<T> GetList()
        {
            return GetList(null, 200000, 1, true, "*", PrimaryKey, TableName);
        }

        /// <summary>
        /// ������ݼ�
        /// </summary>
        /// <param name="strWhere">Where�Ӿ�</param>
        public virtual List<T> GetList(string strWhere)
        {
            return GetList(strWhere, 200000, 1, true, "*", PrimaryKey, TableName);
        }

        /// <summary>
        /// ������ݼ�
        /// </summary>
        /// <param name="strWhere">Where�Ӿ�</param>
        /// <param name="PageSize">ÿҳ��ʾ��¼��</param>
        /// <param name="PageIndex">��ǰҳ��</param>
        public virtual List<T> GetList(string strWhere, int PageSize, int PageIndex)
        {
            return GetList(strWhere, PageSize, PageIndex, true, "*", PrimaryKey, TableName);
        }

        /// <summary>
        /// ������ݼ�
        /// </summary>
        /// <param name="strWhere">Where�Ӿ�</param>
        /// <param name="PageSize">ÿҳ��ʾ��¼��</param>
        /// <param name="PageIndex">��ǰҳ��</param>
        /// <param name="bOrderType">�������(true-����flase-����)</param>
        public virtual List<T> GetList(string strWhere, int PageSize, int PageIndex, bool bOrderType)
        {
            return GetList(strWhere, PageSize, PageIndex, bOrderType, "*", PrimaryKey, TableName);
        }

        /// <summary>
        /// ������ݼ�
        /// </summary>
        /// <param name="strWhere">Where�Ӿ�</param>
        /// <param name="PageSize">ÿҳ��ʾ��¼��</param>
        /// <param name="PageIndex">��ǰҳ��</param>
        /// <param name="bOrderType">�������(true-����flase-����)</param>
        /// <param name="colList">�Զ��ŷָ��Ĳ�ѯ�ֶ�����</param>
        public virtual List<T> GetList(string strWhere, int PageSize, int PageIndex, bool bOrderType, string colList)
        {
            return GetList(strWhere, PageSize, PageIndex, bOrderType, colList, PrimaryKey, TableName);
        }

        /// <summary>
        /// ������ݼ�
        /// </summary>
        /// <param name="strWhere">Where�Ӿ�</param>
        /// <param name="PageSize">ÿҳ��ʾ��¼��</param>
        /// <param name="PageIndex">��ǰҳ��</param>
        /// <param name="bOrderType">�������(true-����flase-����)</param>
        /// <param name="colList">�Զ��ŷָ��Ĳ�ѯ�ֶ�����</param>
        /// <param name="fldOrder">�����ֶ�</param>
        public virtual List<T> GetList(string strWhere, int PageSize, int PageIndex, bool bOrderType, string colList,
                                       string fldOrder)
        {
            return GetList(strWhere, PageSize, PageIndex, bOrderType, colList, fldOrder, TableName);
        }


        public virtual List<T> GetList(string strWhere, int PageSize, int PageIndex, bool bOrderType, string colList,
                                    string fldOrder, List<dbParam> dbParam)
        {
            return GetList(strWhere, PageSize, PageIndex, bOrderType, colList, fldOrder, TableName, dbParam);
        }

        protected virtual List<T> GetList(string strWhere, int PageSize, int PageIndex, bool bOrderType, string colList,
                                        string fldOrder, string sTableName, List<dbParam> dbList)
        {
            using (
                IDataReader dr = GetDataReaderByPage(strWhere, PageSize, PageIndex, bOrderType, colList, fldOrder,
                                                     TableName, dbList))
            {
                return GetList(dr);
            }
        }

        /// <summary>
        /// ������ݼ�
        /// </summary>
        /// <param name="strWhere">Where�Ӿ�</param>
        /// <param name="PageSize">ÿҳ��ʾ��¼��</param>
        /// <param name="PageIndex">��ǰҳ��</param>
        /// <param name="bOrderType">�������(true-����flase-����)</param>
        /// <param name="colList">�Զ��ŷָ��Ĳ�ѯ�ֶ�����</param>
        /// <param name="fldOrder">�����ֶ�</param>
        /// <param name="sTableName">����</param>
        protected virtual List<T> GetList(string strWhere, int PageSize, int PageIndex, bool bOrderType, string colList,
                                          string fldOrder, string sTableName)
        {
            using (
                IDataReader dr = GetDataReaderByPage(strWhere, PageSize, PageIndex, bOrderType, colList, fldOrder,
                                                     TableName))
            {
                return GetList(dr);
            }
        }

        /// <summary>
        /// ���List����
        /// </summary>
        /// <param name="dr">��DataReader���ʵ��ת��List</param>
        public virtual List<T> GetList(IDataReader dr)
        {
            List<T> list = new List<T>();
            while (dr.Read())
            {
                list.Add(GetModel(dr));
            }
            return list;
        }

        /// <summary>
        /// ������ݼ�
        /// </summary>
        /// <param name="dv">������ͼ</param>
        /// <returns>���ݼ�</returns>
        public virtual List<T> GetList(DataView dv)
        {
            List<T> list = new List<T>();
            foreach (DataRowView drv in dv)
            {
                list.Add(GetModel(drv));
            }
            return list;
        }

        /// <summary>
        /// ������ݼ�
        /// </summary>
        /// <param name="dt">���ݱ�</param>
        /// <returns>���ݼ�</returns>
        public virtual List<T> GetList(DataTable dt)
        {
            List<T> list = new List<T>();
            foreach (DataRowView drv in dt.DefaultView)
            {
                list.Add(GetModel(drv));
            }
            return list;
        }

        #endregion  GetList

        #region GetMosaicList
        public virtual List<T> GetMosaicList(string strSql)
        {
            Database db = DatabaseFactory.CreateDatabase(connName);
            using (DbCommand cmd = db.GetSqlStringCommand(strSql))
            {
                using (IDataReader dr = db.ExecuteReader(cmd))
                {
                    return GetList(dr);
                }
            }
        }
        #endregion

        #region  GetReader

        /// <summary>
        /// ������ݼ�
        /// </summary>
        /// <returns></returns>
        public IDataReader GetReader()
        {
            return GetReader(null);
        }

        /// <summary>
        /// ������ݼ�
        /// </summary>
        /// <param name="strWhere">Where�Ӿ�</param>
        public virtual IDataReader GetReader(string strWhere)
        {
            return GetReader(strWhere, 2000, 1, true, "*", PrimaryKey, TableName);
        }

        /// <summary>
        /// ������ݼ�
        /// </summary>
        /// <param name="strWhere">Where�Ӿ�</param>
        /// <param name="PageSize">ÿҳ��ʾ��¼��</param>
        /// <param name="PageIndex">��ǰҳ��</param>
        public virtual IDataReader GetReader(string strWhere, int PageSize, int PageIndex)
        {
            return GetReader(strWhere, PageSize, PageIndex, true, "*", PrimaryKey, TableName);
        }

        /// <summary>
        /// ������ݼ�
        /// </summary>
        /// <param name="strWhere">Where�Ӿ�</param>
        /// <param name="PageSize">ÿҳ��ʾ��¼��</param>
        /// <param name="PageIndex">��ǰҳ��</param>
        /// <param name="bOrderType">�������(true-����flase-����)</param>
        public virtual IDataReader GetReader(string strWhere, int PageSize, int PageIndex, bool bOrderType)
        {
            return GetReader(strWhere, PageSize, PageIndex, bOrderType, "*", PrimaryKey, TableName);
        }

        /// <summary>
        /// ������ݼ�
        /// </summary>
        /// <param name="strWhere">Where�Ӿ�</param>
        /// <param name="PageSize">ÿҳ��ʾ��¼��</param>
        /// <param name="PageIndex">��ǰҳ��</param>
        /// <param name="bOrderType">�������(true-����flase-����)</param>
        /// <param name="colList">�Զ��ŷָ��Ĳ�ѯ�ֶ�����</param>
        public virtual IDataReader GetReader(string strWhere, int PageSize, int PageIndex, bool bOrderType,
                                             string colList)
        {
            return GetReader(strWhere, PageSize, PageIndex, bOrderType, colList, PrimaryKey, TableName);
        }

        /// <summary>
        /// ������ݼ�
        /// </summary>
        /// <param name="strWhere">Where�Ӿ�</param>
        /// <param name="PageSize">ÿҳ��ʾ��¼��</param>
        /// <param name="PageIndex">��ǰҳ��</param>
        /// <param name="bOrderType">�������(true-����flase-����)</param>
        /// <param name="colList">�Զ��ŷָ��Ĳ�ѯ�ֶ�����</param>
        /// <param name="fldOrder">�����ֶ�</param>
        public virtual IDataReader GetReader(string strWhere, int PageSize, int PageIndex, bool bOrderType,
                                             string colList, string fldOrder)
        {
            return GetReader(strWhere, PageSize, PageIndex, bOrderType, colList, fldOrder, TableName);
        }

        internal IDataReader GetReader(string strWhere, int PageSize, int PageIndex, bool bOrderType, string colList,
                                       string fldOrder, string sTableName)
        {
            return GetDataReaderByPage(strWhere, PageSize, PageIndex, bOrderType, colList, fldOrder, TableName);
        }

        #endregion  GetReader

        #region ��ҳ��ѯ
        private IDataReader GetDataReaderByPage(string strWhere, int PageSize, int PageIndex, bool OrderType,
                                            string colList, string fldOrder, string tblName, List<dbParam> dbParam)
        {
            string strSql = BuildSql(strWhere, PageSize, PageIndex, OrderType, colList, fldOrder, tblName);
            Database db = DatabaseFactory.CreateDatabase(connName);
            using (DbCommand cmd = db.GetSqlStringCommand(strSql))
            {
                if (dbParam != null)
                {
                    foreach (dbParam pm in dbParam)
                    {
                        db.AddInParameter(cmd, pm.ParamName, pm.ParamDbType, pm.ParamValue);
                    }
                }
                return db.ExecuteReader(cmd);
            }
        }
        /// <summary>
        /// ���ݷ�ҳ��Ϣ��ȡ��¼��DataReader
        /// </summary>
        /// <param name="strWhere">Where�Ӿ�</param>
        /// <param name="PageSize">ÿҳ��¼��</param>
        /// <param name="PageIndex">��ǰҳ��</param>
        /// <param name="OrderType">�������(true-����flase-����)</param>
        /// <param name="colList">�Զ��ŷָ��Ĳ�ѯ������</param>
        /// <param name="fldOrder">�����ֶ�����</param>
        /// <param name="tblName">����</param>
        private IDataReader GetDataReaderByPage(string strWhere, int PageSize, int PageIndex, bool OrderType,
                                                string colList, string fldOrder, string tblName)
        {
            string strSql = BuildSql(strWhere, PageSize, PageIndex, OrderType, colList, fldOrder, tblName);
            Database db = DatabaseFactory.CreateDatabase(connName);
            using (DbCommand cmd = db.GetSqlStringCommand(strSql))
            {
                return db.ExecuteReader(cmd);
            }
        }

        /// <param name="strWhere">���SQL����Where�Ӿ�</param>
        /// <param name="PageSize">ÿҳ��ʾ��¼��</param>
        /// <param name="PageIndex">��ǰ��Ҫ��ȡ��ҳ��</param>
        /// <param name="OrderType">�������(true-����flase-����)</param>
        /// <param name="colList">���ŷָ����ֶ����ַ���</param>
        /// <param name="fldOrder">�����ֶ�</param>
        /// <param name="tblName">����</param>
        private string BuildSql(string strWhere, int PageSize, int PageIndex, bool OrderType, string colList,
                                string fldOrder, string tblName)
        {
            string sColList = "";
            if (string.IsNullOrEmpty(colList) || colList == "*")
            {
                PropertyInfo[] pis =
                    typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public);
                foreach (PropertyInfo pi in pis)
                {
                    sColList += "[" + pi.Name + "],";
                }
                sColList = sColList.Substring(0, sColList.Length - 1);
            }
            else
            {
                sColList = CString.GetSQLFildList(colList);
            }
            StringBuilder strSql = new StringBuilder();
            string strOrder; // -- ��������
            if (string.IsNullOrEmpty(fldOrder))
            {
                fldOrder = PrimaryKey;
            }
            if (OrderType)
            {
                strOrder = string.Format(" order by {0} desc", fldOrder);
            }
            else
            {
                strOrder = string.Format(" order by {0} asc", fldOrder);
            }
            if (string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(string.Format("select {0} from(select {1}, row_number() over({2}) as row from {3}",
                                            sColList, sColList, strOrder, tblName));
                strSql.Append(string.Format(") a where row between {0} and {1}", (PageIndex - 1) * PageSize + 1,
                                            PageIndex * PageSize));
            }
            else
            {
                strSql.Append(string.Format("select {0} from(select {1}, row_number() over({2}) as row from {3}",
                                            sColList, sColList, strOrder, tblName));
                strSql.Append(string.Format(" where {0}", strWhere));
                strSql.Append(string.Format(") a where row between {0} and {1}", (PageIndex - 1) * PageSize + 1,
                                            PageIndex * PageSize));
            }
            return strSql.ToString();
        }

        #endregion ��ҳ��ѯ

        #region ��������

        /// <summary>
        /// ������ӻ����
        /// </summary>
        /// <param name="list"></param>
        /// <param name="eAdd"></param>
        /// <returns></returns>
        public virtual int AddUpdateList(List<T> list, AddUpdateType eAdd)
        {
            int iCount = 0;
            bool IsAdd = false;
            if (eAdd == AddUpdateType.Add)
                IsAdd = true;
            Database db = DatabaseFactory.CreateDatabase(connName);
            using (DbCommand cmd = db.GetSqlStringCommand(GetAddUpdateSql(IsAdd)))
            {
                List<dbParam> listParam = GetAddUpdatePms(null, IsAdd);
                foreach (dbParam dbpm in listParam)
                {
                    db.AddInParameter(cmd, dbpm.ParamName, dbpm.ParamDbType, null);
                }
                foreach (T model in list)
                {
                    listParam = GetAddUpdatePms(model, IsAdd);
                    foreach (dbParam dbpm in listParam)
                    {
                        cmd.Parameters[dbpm.ParamName].Value = dbpm.ParamValue;
                    }
                    if (db.ExecuteNonQuery(cmd) > 0)
                    {
                        iCount++;
                    }
                }
                return iCount;
            }
        }

        protected virtual string GetAddUpdateSql(bool IsAdd)
        {
            StringBuilder strSql = new StringBuilder();
            if (IsAdd)
            {
                StringBuilder strParameter = new StringBuilder();
                strSql.Append(string.Format("insert into {0}(", TableName));
                PropertyInfo[] pis =
                    typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public);
                for (int i = 0; i < pis.Length; i++)
                {
                    if (t.IsAutoID)
                    {
                        if (t.PrimaryKey == pis[i].Name)
                            continue;
                    }
                    strSql.Append(pis[i].Name + ","); //����SQL���ǰ�벿�� 
                    strParameter.Append("@" + pis[i].Name + ","); //�������SQL���
                }
                strSql = strSql.Replace(",", ")", strSql.Length - 1, 1);
                strParameter = strParameter.Replace(",", ")", strParameter.Length - 1, 1);
                strSql.Append(" values (");
                strSql.Append(strParameter.ToString());
            }
            else
            {
                strSql.Append("update  " + TableName + " set ");
                PropertyInfo[] pis =
                    typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public);
                for (int i = 0; i < pis.Length; i++)
                {
                    if (pis[i].Name != PrimaryKey)
                    {
                        strSql.Append(pis[i].Name + "=" + "@" + pis[i].Name + ",");
                    }
                    //strSql.Append("\r\n");
                }
                strSql = strSql.Replace(",", " ", strSql.Length - 1, 1);
                strSql.Append(" where " + PrimaryKey + "=@" + PrimaryKey);
            }
            return strSql.ToString();
        }

        protected virtual List<dbParam> GetAddUpdatePms(T model, bool IsAdd)
        {
            PropertyInfo[] pis =
                typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public);
            List<dbParam> listParam = new List<dbParam>();
            for (int i = 0; i < pis.Length; i++)
            {
                if (IsAdd)
                {
                    if (t.IsAutoID)
                    {
                        if (t.PrimaryKey == pis[i].Name)
                            continue;
                    }
                }
                if (model == null)
                    listParam.Add(new dbParam
                                      {
                                          ParamName = pis[i].Name,
                                          ParamDbType = TypeConvert.GetDbType(pis[i].PropertyType),
                                          ParamValue = null
                                      });
                else
                    listParam.Add(new dbParam
                                      {
                                          ParamName = pis[i].Name,
                                          ParamDbType = TypeConvert.GetDbType(pis[i].PropertyType),
                                          ParamValue = pis[i].GetValue(model, null)
                                      });
            }
            return listParam;
        }

        #endregion ��������

        #region GetDataTable

        /// <summary>
        /// ������ݱ�
        /// </summary>
        /// <param name="strWhere">Where�Ӿ�</param>
        /// <param name="PageSize">ÿҳ��ʾ��¼��</param>
        /// <param name="PageIndex">��ǰҳ��</param>
        /// <param name="OrderType">�������(true-����flase-����)</param>
        /// <param name="colList">�Զ��ŷָ��Ĳ�ѯ�ֶ�����</param>
        /// <param name="fldOrder">�����ֶ�</param>
        /// <returns>���ݱ�</returns>
        public virtual DataTable GetDataTable(string strWhere, int PageSize, int PageIndex, bool OrderType, string colList, string fldOrder)
        {
            string strSQL = BuildSql(strWhere, PageSize, PageIndex, OrderType, colList, fldOrder, TableName);
            Database db = DatabaseFactory.CreateDatabase(connName);
            DbCommand cmd = db.GetSqlStringCommand(strSQL);
            return db.ExecuteDataSet(cmd).Tables[0];
        }

        #endregion GetDataTable

    }
}