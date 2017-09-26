using System;
using System.Runtime.Serialization;
using System.Reflection;

namespace com.Model.Base
{
    [DataContract]
    [Serializable]
    public class BaseModel : Object 
    { 
        public BaseModel() { }
        private string  _PrimaryKey="";
        private bool _IsAutoID = true;
        private DataBaseEnum _DataBaseName;
        private bool _HasIdentityPK = true;
        private bool _IsExternalConn = false;
        private string _connName = "";

        public string PrimaryKey
        {
            get { return _PrimaryKey; }
            set { _PrimaryKey = value; }
        }
        public bool IsAutoID
        {
            get { return _IsAutoID; }
            set { _IsAutoID = value; }
        }
        public DataBaseEnum DataBaseName
        {
            get { return _DataBaseName; }
            set { _DataBaseName = value; }
        }
        /// <summary>
        /// �Ƿ��ⲿ�������ݿ�
        /// </summary>
        public bool IsExternalConn
        {
            get { return _IsExternalConn; }
            set { _IsExternalConn = value; }
        }
        /// <summary>
        /// �ⲿ���õ����ݿ�����
        /// </summary>
        public string connName
        {
            get { return _connName; }
            set { _connName = value; }
        }
        /// <summary>
        /// �����Ƿ��������ֶ�
        /// </summary>
        public bool HasIdentityPK
        {
            get { return _HasIdentityPK; }
            set { _HasIdentityPK = value; }
        }
    }
}
