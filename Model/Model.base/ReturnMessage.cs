﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Serialization;

namespace com.Model.Base
{
    /// <summary>
    /// 服务端向客户端返回信息类
    /// 2008-01-15 新增
    /// </summary>
    [DataContract]
    public class ReturnMessage
    {
        private bool _Success=false ;
        private int _TypeID=0;
        private string _Info="";
        private string _Redirect="";
        private List<object> _Data=null;

        /// <summary>
        /// 请求状态
        /// </summary>
        [DataMember]
        public bool Success
        {
            get { return _Success; }
            set { _Success = value; }
        }
        /// <summary>
        /// 信息类型
        /// </summary>
        [DataMember]
        public int TypeID
        {
            get { return _TypeID; }
            set { _TypeID = value; }
        }
        /// <summary>
        /// 提示信息
        /// </summary>
        [DataMember]
        public string Info
        {
            get { return _Info; }
            set { _Info = value; }
        }
        /// <summary>
        /// 重转地址
        /// </summary>
        [DataMember]
        public string Redirect
        {
            get { return _Redirect; }
            set { _Redirect = value; }
        }
        /// <summary>
        /// 数据集
        /// </summary>
        [DataMember]
        public List<object> Data
        {
            get { return _Data; }
            set { _Data = value; }
        }
    }
}
