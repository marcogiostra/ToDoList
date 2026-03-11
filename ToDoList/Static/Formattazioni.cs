using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Static
{
    public class Formattazioni
    {
        #region DBNull2_FUNCTION
        public static string DBNull2String(object campo)
        {
            try
            {
                if (campo is null)
                    return "";
                else if (campo.Equals(DBNull.Value))
                    return "";
                else
                    return Convert.ToString(campo);
            }
            catch (Exception)
            {
                return "";
            }
        }
        public static decimal DBNull2Decimal(object campo)
        {
            try
            {
                if (campo is null)
                    return 0;
                else if (campo.Equals(DBNull.Value))
                    return 0;
                else
                    return Convert.ToDecimal(campo);
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public static int DBNull2Int(object campo)
        {
            try
            {
                if (campo is null)
                    return 0;
                else if (campo.Equals(DBNull.Value))
                    return 0;
                else
                    return Convert.ToInt32(campo);
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public static bool DBNull2Bool(object campo)
        {
            try
            {
                if (campo is null)
                    return false;
                else if (campo.Equals(DBNull.Value))
                    return false;
                else
                    return Convert.ToBoolean(campo);
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static Single DBNull2Single(object campo)
        {
            try
            {
                if (campo is null)
                    return 0;
                else if (campo.Equals(DBNull.Value))
                    return 0;
                else
                    return Convert.ToSingle(campo);
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public static DateTime? DBNull2DateTimeNull(object campo)
        {
            try
            {
                if (campo is null)
                    return null;
                else if (campo.Equals(DBNull.Value))
                    return null;
                else
                    return Convert.ToDateTime(campo);
            }
            catch (Exception)
            {
                return null;
            }

        }
        /*
         * public static decimal? DBNull2DecimalNull(object campo)
        {
            try
            {
                if (campo is null)
                    return null;
                else if (campo.Equals(DBNull.Value))
                    return null;
                else
                    return Convert.ToDecimal(campo);
            }
            catch (Exception)
            {
                return 0;
            }
        }
         * */
        #endregion DBNull2_FUNCTION


        #region 2DBNull_FUNCTION
        public static object String2DBNull(string campo)
        {
            try
            {
                if (string.IsNullOrEmpty(campo))
                    return DBNull.Value;
                else
                    return campo;
            }
            catch (Exception)
            {
                return DBNull.Value;
            }
        }
        public static object Decimal2DBNull(decimal campo)
        {
            try
            {
                if (campo == 0)
                    return DBNull.Value;
                else
                    return campo;
            }
            catch (Exception)
            {
                return DBNull.Value;
            }
        }

        /*
        public static string Uniqueidentyfier2String(string pValore)
        {
            if (pValore.Length == 38)
            {
                string temp = pValore;
                temp = temp.Substring(1);
                temp = temp.Substring(0, 36);
                pValore = temp;
            }

            return pValore;

        }
        */
        #endregion 2DBNull_FUNCTION
    }
}

