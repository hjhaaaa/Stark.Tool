using System;

namespace Stark.Tool
{
    /// <summary>
    /// DateTime 类型扩展类
    /// </summary>
    public static class DateTimeExtensions
    {
        private static readonly DateTime timeZero = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// DateTime 转为 13 位 Unix 时间戳
        /// </summary>
        /// <param name="dt">日期时间</param>
        /// <returns></returns>
        public static long ToTimestamp(this DateTime dt)
        {
            return (long)(dt - timeZero).TotalMilliseconds;
        }

        /// <summary>
        /// DateTime 转为 10 位 Unix 时间戳
        /// </summary>
        /// <param name="dt">日期时间</param>
        /// <returns></returns>
        public static int ToShortTimestamp(this DateTime dt)
        {
            return (int)(dt - timeZero).TotalSeconds;
        }

        /// <summary>
        /// 13 位时间戳转为 DateTime
        /// </summary>
        /// <param name="timestamp">13 位时间戳</param>
        /// <returns></returns>
        public static DateTime ToDateTime(this long timestamp)
        {

            if (timestamp.ToString().Length == 13) {
                return timeZero.AddMilliseconds(timestamp);
            }

            return new DateTime(0001, 1, 1);
        }

        /// <summary>
        /// 10 位时间戳转为 DateTime
        /// </summary>
        /// <param name="timestamp">10 位时间戳</param>
        /// <returns></returns>
        public static DateTime ToDateTime(this int timestamp)
        {
            //DateTime timeZero = GetTimeZero();
            if (timestamp.ToString().Length == 10) {
                return timeZero.AddSeconds(timestamp);
            }

            return new DateTime(0001, 1, 1);
        }

        /// <summary>
        /// 获取星期的日期
        /// </summary>
        /// <param name="dt">时间（一般为当前时间）</param>
        /// <param name="weekday">周几</param>
        /// <param name="Number">前/后几周</param>
        /// <returns></returns>
        public static DateTime GetWeekOfDate(DateTime dt, DayOfWeek weekday, int Number)
        {
            int wd1 = (int)weekday;
            int wd2 = (int)dt.DayOfWeek;
            return wd2 == wd1 ? dt.AddDays(7 * Number) : dt.AddDays(7 * Number - wd2 + wd1);
        }
        /// <summary>
        /// 转换成yyyy-MM-dd字符串
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToDateString(this DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd");
        }
        /// <summary>
        /// 转换成yyyy-MM-dd HH:mm:ss字符串
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToDatetimeString(this DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
