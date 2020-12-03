using System.Linq;
using System.Reflection;

namespace System
{
    /// <summary>
    /// 用途：Object 类型扩展类
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// 对象是否为null
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsNull(this object o) => o is null;
        /// <summary>
        /// 对象是否为null
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsNotNull(this object o) => !IsNull(o);
        /// <summary>
        /// 获取Json
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static string GetJson(this object o) => o.IsNull() ? "" : Newtonsoft.Json.JsonConvert.SerializeObject(o);

        public static T ToModel<T>(this object str)
        where T : new()
        {
            return str.IsNull() ? default : Newtonsoft.Json.JsonConvert.DeserializeObject<T>(str.GetJson());
        }

        #region Int32类型

        /// <summary>
        /// 转换INT32类型
        /// </summary>
        /// <param name="s"></param>
        /// <param name="defautValue"></param>
        /// <returns></returns>
        public static int ToInt(this object obj, int defautValue = 0)
        {
            if (obj.IsNull()) return defautValue;
            try {
                return Convert.ToInt32(obj);
            }
            catch (Exception) {
                return defautValue;
            }
        }

        #endregion

        #region Int64类型

        /// <summary>
        /// 转换INT64类型
        /// </summary>
        /// <param name="s"></param>
        /// <param name="defautValue"></param>
        /// <returns></returns>
        public static long ToInt64(this object obj, long defautValue = 0)
        {
            if (obj.IsNull()) return defautValue;
            try {
                return Convert.ToInt64(obj);
            }
            catch (Exception) {
                return defautValue;
            }
        }

        /// <summary>
        /// 是否未INT64类型
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsInt64(this object s)
        {
            try {
                Convert.ToInt64(s);
                return true;
            }
            catch (Exception) {
                return false;
            }
        }

        #endregion

        #region Float类型

        /// <summary>
        /// 转换成FLOAT类型
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defautValue"></param>
        /// <returns></returns>
        public static float ToFloat(this object obj, float defautValue = 0)
        {
            try {
                return Convert.ToSingle(obj);
            }
            catch (Exception) {
                return defautValue;
            }
        }

        #endregion

        #region Double类型

        /// <summary>
        /// 转换成Double类型
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static double ToDouble(this object obj, double defaultValue = 0)
        {
            try {
                return Convert.ToDouble(obj);
            }
            catch (Exception) {
                return defaultValue;
            }
        }



        #endregion

        #region Decimal类型

        /// <summary>
        /// 转换成Decimal类型
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this object obj, decimal defaultValue = 0M)
        {
            try {
                return Convert.ToDecimal(obj);
            }
            catch (Exception) {
                return defaultValue;
            }
        }

        #endregion



        /// <summary>
        /// 获取值
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="obj"></param>
        /// <param name="def">默认值</param>
        /// <returns></returns>
        public static T GetValue<T>(this object obj, T def = default)
        {
            if (obj is null) return def;
            try {
                return (T)System.Convert.ChangeType(obj, typeof(T));
            }
            catch (Exception) {
                return def;
            }
        }

        /// <summary>
        /// 获取Sql字段字符串
        /// </summary>
        /// <returns></returns>
        public static string GetSqlField<T>() where T : class
        {
            try {
                return string.Join(',', typeof(T).GetProperties().Select(x => x.Name).ToList());
            }
            catch (Exception) {
                return "";
            }
        }

    }
}
