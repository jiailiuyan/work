using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Project.Common
{
    /// <summary>
    /// ���ݽ�����
    /// </summary>
    public class DataParser
    {
        #region �����ַ���������
        /// <summary>
        /// int?
        /// </summary>
        /// <param name="strValue">Դ�ַ���</param>
        /// <returns></returns>
        public static int? ParseIntT(string strValue)
        {
            int value;
            if (int.TryParse(strValue, out value))
            {
                return (int?)value;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// decimal
        /// </summary>
        /// <param name="strValue">Դ�ַ���</param>
        /// <returns></returns>
        public static decimal? ParseDecimalT(string strValue)
        {
            decimal value;
            if (decimal.TryParse(strValue, out value))
            {
                return (decimal?)value;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// double
        /// </summary>
        /// <param name="strValue">Դ�ַ���</param>
        /// <returns></returns>
        public static double? ParseDoubleT(string strValue)
        {
            double value;
            if (double.TryParse(strValue, out value))
            {
                return (double?)value;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// float
        /// </summary>
        /// <param name="strValue">Դ�ַ���</param>
        /// <returns></returns>
        public static float? ParseFloatT(string strValue)
        {
            float value;
            if (float.TryParse(strValue, out value))
            {
                return (float?)value;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// DateTime
        /// </summary>
        /// <param name="strValue">��Ϊ��Ч�����ڸ�ʽ</param>
        /// <returns></returns>
        public static DateTime? ParseDateTimeT(string strValue)
        {
            DateTime value;
            if (DateTime.TryParse(strValue, out value))
            {
                return (DateTime?)value;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// short
        /// </summary>
        /// <param name="strValue">Դ�ַ���</param>
        /// <returns></returns>
        public static short? ParseShortT(string strValue)
        {
            short value;
            if (short.TryParse(strValue, out value))
            {
                return (short?)value;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// long
        /// </summary>
        /// <param name="strValue">Դ�ַ���</param>
        /// <returns></returns>
        public static long? ParseLongT(string strValue)
        {
            long value;
            if (long.TryParse(strValue, out value))
            {
                return (long?)value;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// byte
        /// </summary>
        /// <param name="strValue">Դ�ַ���</param>
        /// <returns></returns>
        public static byte? ParseByteT(string strValue)
        {
            byte value;
            if (byte.TryParse(strValue, out value))
            {
                return (byte?)value;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// bool
        /// </summary>
        /// <param name="strValue">�ַ���true������True,����Ϊfalse,�մ�Ϊnull</param>
        /// <returns></returns>
        public static bool? ParseBoolT(string strValue)
        {
            if (string.IsNullOrEmpty(strValue))
            {
                return null;
            }
            else
            {
                bool value;
                if (bool.TryParse(strValue.ToLower(), out value))
                {
                    return (bool?)value;
                }
                else
                {
                    return null;
                }
            }

        }
        #endregion �����ַ���������

        #region �����ַ�����ָ������
        /// <summary>
        /// int
        /// </summary>
        /// <param name="strValue">Դ�ַ���</param>
        /// <returns></returns>
        public static int ParseInt(string strValue)
        {
            return int.Parse(strValue);
        }
        /// <summary>
        /// decimal
        /// </summary>
        /// <param name="strValue">Դ�ַ���</param>
        /// <returns></returns>
        public static decimal ParseDecimal(string strValue)
        {
            return decimal.Parse(strValue);
        }

        /// <summary>
        /// double
        /// </summary>
        /// <param name="strValue">Դ�ַ���</param>
        /// <returns></returns>
        public static double ParseDouble(string strValue)
        {
            return double.Parse(strValue);
        }
        /// <summary>
        /// float
        /// </summary>
        /// <param name="strValue">Դ�ַ���</param>
        /// <returns></returns>
        public static float ParseFloat(string strValue)
        {
            return float.Parse(strValue);
        }
        /// <summary>
        /// DateTime
        /// </summary>
        /// <param name="strValue">Դ�ַ���,��Ϊ��Ч�����ڸ�ʽ</param>
        /// <returns></returns>
        public static DateTime ParseDateTime(string strValue)
        {
            return DateTime.Parse(strValue);
        }
        /// <summary>
        /// short
        /// </summary>
        /// <param name="strValue">Դ�ַ���</param>
        /// <returns></returns>
        public static short ParseShort(string strValue)
        {
            return short.Parse(strValue);
        }
        /// <summary>
        /// long
        /// </summary>
        /// <param name="strValue">Դ�ַ���</param>
        /// <returns></returns>
        public static long ParseLong(string strValue)
        {
            return long.Parse(strValue);
        }
        /// <summary>
        /// byte
        /// </summary>
        /// <param name="strValue">Դ�ַ���</param>
        /// <returns></returns>
        public static byte ParseByte(string strValue)
        {
            return byte.Parse(strValue);
        }
        /// <summary>
        /// bool
        /// </summary>
        /// <param name="strValue">�ַ���true������True,����Ϊfalse,�մ�Ϊnull</param>
        /// <returns></returns>
        public static bool ParseBool(string strValue)
        {
            return bool.Parse(strValue);

        }
        #endregion �����ַ�����ָ������

        #region ��datarow��ĳ��ȡ����ֵ
        /// <summary>
        /// GetItemDecimalT,��ֵ���ؿ�(null)
        /// </summary>
        /// <param name="row">������</param>
        /// <param name="columnName">������</param>
        /// <returns></returns>
        public static decimal? GetItemDecimalT(DataRow row, string columnName)
        {
            if (row.IsNull(columnName))
            {
                return null;
            }
            else
            {
                return (decimal)row[columnName];
            }
        }
        /// <summary>
        /// GetItemLongT,��ֵ���ؿ�(null)
        /// </summary>
        /// <param name="row">������</param>
        /// <param name="columnName">������</param>
        /// <returns></returns>
        public static long? GetItemLongT(DataRow row, string columnName)
        {
            if (row.IsNull(columnName))
            {
                return null;
            }
            else
            {
                return (long)row[columnName];
            }
        }
        /// <summary>
        /// GetItemIntT,��ֵ���ؿ�(null)
        /// </summary>
        /// <param name="row">������</param>
        /// <param name="columnName">������</param>
        /// <returns></returns>
        public static int? GetItemIntT(DataRow row, string columnName)
        {
            if (row.IsNull(columnName))
            {
                return null;
            }
            else
            {
                return (int)row[columnName];
            }
        }
        /// <summary>
        /// GetItemDoubleT,��ֵ���ؿ�(null)
        /// </summary>
        /// <param name="row">������</param>
        /// <param name="columnName">������</param>
        /// <returns></returns>
        public static double? GetItemDoubleT(DataRow row, string columnName)
        {
            if (row.IsNull(columnName))
            {
                return null;
            }
            else
            {
                return (double)row[columnName];
            }
        }
        /// <summary>
        /// GetItemFloatT,��ֵ���ؿ�(null)
        /// </summary>
        /// <param name="row">������</param>
        /// <param name="columnName">������</param>
        /// <returns></returns>
        public static float? GetItemFloatT(DataRow row, string columnName)
        {
            if (row.IsNull(columnName))
            {
                return null;
            }
            else
            {
                return (float)row[columnName];
            }
        }
        /// <summary>
        /// GetItemShortT,��ֵ���ؿ�(null)
        /// </summary>
        /// <param name="row">������</param>
        /// <param name="columnName">������</param>
        /// <returns></returns>
        public static short? GetItemShortT(DataRow row, string columnName)
        {
            if (row.IsNull(columnName))
            {
                return null;
            }
            else
            {
                return (short)row[columnName];
            }
        }
        /// <summary>
        /// GetItemByteT,��ֵ���ؿ�(null)
        /// </summary>
        /// <param name="row">������</param>
        /// <param name="columnName">������</param>
        /// <returns></returns>
        public static byte? GetItemByteT(DataRow row, string columnName)
        {
            if (row.IsNull(columnName))
            {
                return null;
            }
            else
            {
                return (byte)row[columnName];
            }
        }
        /// <summary>
        /// GetItemBoolT,��ֵ���ؿ�(null)
        /// </summary>
        /// <param name="row">������</param>
        /// <param name="columnName">������</param>
        /// <returns></returns>
        public static bool? GetItemBoolT(DataRow row, string columnName)
        {
            if (row.IsNull(columnName))
            {
                return null;
            }
            else
            {
                return (bool)row[columnName];
            }
        }
        /// <summary>
        /// GetItemDateTimeT,��ֵ���ؿ�(null)
        /// </summary>
        /// <param name="row">������</param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public static DateTime? GetItemDateTimeT(DataRow row, string columnName)
        {
            if (row.IsNull(columnName))
            {
                return null;
            }
            else
            {
                return (DateTime)row[columnName];
            }
        }
        #endregion ��datarow��ĳ��ȡ����ֵ

        #region ��datarow��ĳ��ȡֵ,�Ƿ���ֵ
        /// <summary>
        /// GetItemString,��ֵ���ظ÷�������Ĭ��ֵ
        /// </summary>
        /// <param name="row">������</param>
        /// <param name="columnName">������</param>
        /// <returns></returns>
        public static string GetItemString(DataRow row, string columnName)
        {
            if (row.IsNull(columnName))
            {
                return string.Empty;
            }
            else
            {
                return row[columnName].ToString();
            }
        }
        /// <summary>
        /// GetItemGuid,��ֵ���ظ÷�������Ĭ��ֵ
        /// </summary>
        /// <param name="row">������</param>
        /// <param name="columnName">������</param>
        /// <returns></returns>
        public static Guid GetItemGuid(DataRow row, string columnName)
        {
            if (row.IsNull(columnName))
            {
                return Guid.Empty;
            }
            else
            {
                return (Guid)row[columnName];
            }
        }
        /// <summary>
        /// GetItemObject,��ֵ���ظ÷�������Ĭ��ֵ
        /// </summary>
        /// <param name="row">������</param>
        /// <param name="columnName">������</param>
        /// <returns></returns>
        public static object GetItemObject(DataRow row, string columnName)
        {
            return row[columnName];
        }
        /// <summary>
        /// GetItemByteArray,��ֵ���ظ÷�������Ĭ��ֵ
        /// </summary>
        /// <param name="row">������</param>
        /// <param name="columnName">������</param>
        /// <returns></returns>
        public static byte[] GetItemByteArray(DataRow row, string columnName)
        {
            if (row.IsNull(columnName))
            {
                return new byte[] { };
            }
            else
            {
                return row[columnName] as byte[];
            }
        }
        /// <summary>
        /// GetItemDecimal,��ֵ���ظ÷�������Ĭ��ֵ
        /// </summary>
        /// <param name="row">������</param>
        /// <param name="columnName">������</param>
        /// <returns></returns>
        public static decimal GetItemDecimal(DataRow row, string columnName)
        {
            if (row.IsNull(columnName))
            {
                return 0;
            }
            else
            {
                return (decimal)row[columnName];
            }
        }
        /// <summary>
        /// GetItemLong,��ֵ���ظ÷�������Ĭ��ֵ
        /// </summary>
        /// <param name="row">������</param>
        /// <param name="columnName">������</param>
        /// <returns></returns>
        public static long GetItemLong(DataRow row, string columnName)
        {
            if (row.IsNull(columnName))
            {
                return 0;
            }
            else
            {
                return (long)row[columnName];
            }
        }
        /// <summary>
        /// GetItemInt,��ֵ���ظ÷�������Ĭ��ֵ
        /// </summary>
        /// <param name="row">������</param>
        /// <param name="columnName">������</param>
        /// <returns></returns>
        public static int GetItemInt(DataRow row, string columnName)
        {
            if (row.IsNull(columnName))
            {
                return 0;
            }
            else
            {
                return (int)row[columnName];
            }
        }
        /// <summary>
        /// GetItemDouble,��ֵ���ظ÷�������Ĭ��ֵ
        /// </summary>
        /// <param name="row">������</param>
        /// <param name="columnName">������</param>
        /// <returns></returns>
        public static double GetItemDouble(DataRow row, string columnName)
        {
            if (row.IsNull(columnName))
            {
                return 0;
            }
            else
            {
                return (double)row[columnName];
            }
        }
        /// <summary>
        /// GetItemFloat,��ֵ���ظ÷�������Ĭ��ֵ
        /// </summary>
        /// <param name="row">������</param>
        /// <param name="columnName">������</param>
        /// <returns></returns>
        public static float GetItemFloat(DataRow row, string columnName)
        {
            if (row.IsNull(columnName))
            {
                return 0;
            }
            else
            {
                return (float)row[columnName];
            }
        }
        /// <summary>
        /// GetItemShort,��ֵ���ظ÷�������Ĭ��ֵ
        /// </summary>
        /// <param name="row">������</param>
        /// <param name="columnName">������</param>
        /// <returns></returns>
        public static short GetItemShort(DataRow row, string columnName)
        {
            if (row.IsNull(columnName))
            {
                return 0;
            }
            else
            {
                return (short)row[columnName];
            }
        }
        /// <summary>
        /// GetItemByte,��ֵ���ظ÷�������Ĭ��ֵ
        /// </summary>
        /// <param name="row">������</param>
        /// <param name="columnName">������</param>
        /// <returns></returns>
        public static byte GetItemByte(DataRow row, string columnName)
        {
            if (row.IsNull(columnName))
            {
                return 0;
            }
            else
            {
                return (byte)row[columnName];
            }
        }
        /// <summary>
        /// GetItemBool,��ֵ���ظ÷�������Ĭ��ֵ
        /// </summary>
        /// <param name="row">������</param>
        /// <param name="columnName">������</param>
        /// <returns></returns>
        public static bool GetItemBool(DataRow row, string columnName)
        {
            if (row.IsNull(columnName))
            {
                return false;
            }
            else
            {
                return (bool)row[columnName];
            }
        }
        /// <summary>
        /// GetItemDateTime,��ֵ���ظ÷���1900-01-01
        /// </summary>
        /// <param name="row">������</param>
        /// <param name="columnName">������</param>
        /// <returns></returns>
        public static DateTime GetItemDateTime(DataRow row, string columnName)
        {
            if (row.IsNull(columnName))
            {
                return DateTime.Parse("1900-01-01");
            }
            else
            {
                return (DateTime)row[columnName];
            }
        }
        #endregion ��datarow��ĳ��ȡֵ,�Ƿ���ֵ
    }
}
