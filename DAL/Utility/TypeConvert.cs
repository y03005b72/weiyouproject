using System;
using System.Collections.Generic;
using System.Text;

namespace com.Utility
{
    public static class TypeConvert
    {
        #region ��DbTypeת��ΪType
        /// <summary>
        /// �����ݿ��е������ֶ�ת��ΪDateTime,���dbValueΪDBNull.Value,�򷵻�DateTime.MinValue;
        /// </summary>
        /// <param name="dbValue">Ҫת�������ݿ��ֶ�ֵ</param>
        /// <returns>����</returns>
        public static DateTime ToDateTime(object dbValue)
        {
            if (dbValue == null || dbValue == DBNull.Value)
            {
                return DateTime.MinValue;
            }
            return Convert.ToDateTime(dbValue);
        }

        /// <summary>
        /// ��������ת�������ݿ��ֶ�ֵ�����timeֵΪtime.MinValue����ת��ΪDBNull.Value
        /// </summary>
        /// <param name="time">Ҫת��������</param>
        /// <returns></returns>
        public static Object ToDBValue(DateTime time)
        {
            if (time == DateTime.MinValue)
                return DBNull.Value;

            return time;
        }
        public static Object ToDBValue(string value)
        {
            if( string.IsNullOrEmpty(value))
            {
                return DBNull.Value;
            }
            return value;
        }

        public static Object ToDBValue(int d)
        {
           
            if (d ==int.MinValue)
                return DBNull.Value;
            return d;
        } 

        /// <summary>
        /// �����͵����ݿ��ֶ�ֵת��ΪSystem.Int32�����ֵΪDBNull.Value,��ת��Ϊ -1
        /// </summary>
        /// <param name="dbValue">���͵����ݿ�ֵ</param>
        /// <returns></returns>
        public static int ToInt32(object dbValue)
        {
            if (dbValue == null || dbValue == DBNull.Value)
            {
                return int.MinValue;
            }
            return Convert.ToInt32(dbValue);
        }

        /// <summary>
        ///  �����͵����ݿ��ֶ�ֵת��ΪSystem.Int64�����ֵΪDBNull.Value,��ת��Ϊ -1
        /// </summary>
        /// <param name="dbValue">���͵����ݿ�ֵ</param>
        /// <returns></returns>
        public static Int64 ToInt64(object dbValue)
        {
            if (dbValue == null || dbValue == DBNull.Value)
            {
                return Int64.MinValue;
            }
            return Convert.ToInt64(dbValue);
        }
        #endregion ��DbTypeת��ΪType

        #region ��IDataReader�ж�ȡֵ
        /// <summary>
        /// ֱ�Ӵ�dataReader�ж�ȡ��i�е�����ֵ�����ֵΪ�գ��򷵻�DateTime.MinValue
        /// </summary>
        /// <param name="dataReader">Ҫ���ж�ȡ���ݵ�dataReader</param>
        /// <param name="i">dataReader�е�������</param>
        /// <returns></returns>
        public static DateTime GetDateTime(System.Data.IDataReader dataReader, int i)
        {
            if (dataReader.IsDBNull(i))
            {
                return DateTime.MinValue;
            }
            return dataReader.GetDateTime(i);
        }

        /// <summary>
        /// ֱ�Ӵ�dataReader�ж�ȡ��i�е�ֵ,���ֵΪ�գ��򷵻�Int.MinValue
        /// </summary>
        /// <param name="dataReader">Ҫ��ȡ���ݵ�dataReader</param>
        /// <param name="i">dataReader�е�������</param>
        /// <returns></returns>
        public static int GetInt32(System.Data.IDataReader dataReader ,int i)
        {
            if(dataReader.IsDBNull(i))
            {
                return int.MinValue;
            }
            return dataReader.GetInt32(i);
        }

        /// <summary>
        /// ֱ�Ӵ�dataReader�ж�ȡ��i�е�ֵ,���ֵΪ�գ��򷵻ؿմ�
        /// </summary>
        /// <param name="dataReader">Ҫ��ȡ���ݵ�dataReader</param>
        /// <param name="i">dataReader�е�������</param>
        /// <returns></returns>
        public static string GetString(System.Data.IDataReader dataReader, int i)
        {
            if (dataReader.IsDBNull(i))
            {
                return "";
            }
            else
            {
                return dataReader.GetString(i);
            }
        }
        #endregion ��IDataReader�ж�ȡֵ

        #region ��Type����ת��ΪDbType
        public static System.Data.DbType GetDbType(System.Type sysType)
        {
            System.Data.DbType dbType = System.Data.DbType.String;
           
            switch (sysType.Name)
            {
                case "String":
                    dbType = System.Data.DbType.String;
                    break;
                case "Int32": 
                    dbType = System.Data.DbType.Int32;
                    break;
                case "DateTime":
                    dbType = System.Data.DbType.DateTime;
                    break;
                case "Double":
                    dbType = System.Data.DbType.Double;
                    break;
                case  "Boolean":
                    dbType = System.Data.DbType.Boolean;
                    break;
                case "Guid":
                    dbType = System.Data.DbType.Guid;
                    break;
                default:
                    break;
            }
            return dbType;
        }
        #endregion ��Type����ת��ΪDbType
    }
}
