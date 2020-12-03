using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Stark.Tool
{
    /// <summary>
    /// 用途：string 类型扩展类
    /// 创建：2018-07-06，Ace Han，创建此扩展类
    /// 修订：2019-04-18，Ace Han，补充类和方法的注释
    /// </summary>
    public static class stringExtensions
    {
        /// <summary>
        /// 截取开始位置到结束位置的内容
        /// </summary>
        /// <param name=""></param>
        /// <param name="befstr"></param>
        /// <param name="endstr"></param>
        /// <returns></returns>
        public static string TrimStr(this string str, string befstr, string endstr)
        {
            try
            {
                var befint = str.IndexOf(befstr);
                var endint = str.IndexOf(endstr);
                befint = befint + befstr.Length;
                return str.Substring(befint, endint - befint);
            }
            catch (Exception)
            {
                return "";
            }
        }
        /// <summary>
        /// 是 null 空字符串或空格时返回 true，否则返回 false
        /// </summary>
        /// <param name="s">当前字符串</param>
        /// <returns></returns>
        public static Boolean IsEmpty(this string s) => string.IsNullOrWhiteSpace(s);


        /// <summary>
        /// 不是 null 空字符串或空格时返回 true，否则返回 false
        /// </summary>
        /// <param name="s">当前字符串</param>
        /// <returns></returns>
        public static Boolean IsNotEmpty(this string s) => !string.IsNullOrWhiteSpace(s);


        /// <summary>
        /// JSON转成对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <returns></returns>
        public static T ToObject<T>(this string str)
            where T : new()
        {
            return str.IsNull() ? default : Newtonsoft.Json.JsonConvert.DeserializeObject<T>(str);
        }


        #region Byte类型

        /// <summary>
        /// 转换成Byte类型
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static byte ToByte(this string s)
        {
            byte b = 0;
            byte.TryParse(s, out b);
            return b;
        }
        /// <summary>
        /// 获取byte[]
        /// Encoding是UTF8
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static byte[] ToByteArray(this string s)
        {
            return Encoding.UTF8.GetBytes(s);
        }

        public static byte[] Base64ToBytes(this string s)
        {
            return Convert.FromBase64String(s);
        }

        /// <summary>
		/// 将指定进制字符串转换为等效字节数组
		/// </summary>
		/// <param name="value"></param>
		/// <param name="radix">字符串中数字的基数，必须为2 ~ 36</param>
		/// <param name="width">每个字节占用的字符长度</param>
		/// <returns></returns>
		public static byte[] ToByteArray(this string value, int radix, int width)
        {
            List<byte> list = new List<byte>();
            for (int i = 0; i < value.Length; i += width)
            {
                byte? b;
                if (i + width <= value.Length)
                {
                    b = value.Substring(i, width).ToByte(radix);
                }
                else
                {
                    b = value.Substring(i, value.Length - i).ToByte(radix);
                }
                if (b == null)
                {
                    return null;
                }
                list.Add(b.Value);
            }
            return list.ToArray();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <param name="radix"></param>
        /// <returns></returns>
        public static byte? ToByte(this string value, int radix)
        {
            ulong? num = value.ToUInt64(radix);
            if (num == null)
            {
                return null;
            }
            return new byte?((byte)num.GetValueOrDefault());
        }

        /// <summary>
		/// 将数字字符串转换为64位有符号整数
		/// </summary>
		/// <param name="value"></param>
		/// <param name="radix">字符串中数字的基数，必须为2 ~ 36</param>
		/// <returns></returns>
		public static ulong? ToUInt64(this string value, int radix)
        {
            if (radix < 2 && radix > 36)
            {
                return null;
            }
            ulong num = 0UL;
            int i = 0;
            while (i < value.Length)
            {
                char c = value[i];
                int num2;
                if (c >= '0' && c <= '9')
                {
                    num2 = (int)(c - '0');
                }
                else if (c >= 'a' && c <= 'z')
                {
                    num2 = (int)('\n' + c - 'a');
                }
                else
                {
                    if (c < 'A' || c > 'Z')
                    {
                        return null;
                    }
                    num2 = (int)('\n' + c - 'A');
                }
                if (num2 < radix)
                {
                    num *= (ulong)((long)radix);
                    num += (ulong)((long)num2);
                    i++;
                    continue;
                }
                return null;
            }
            return new ulong?(num);
        }


        #endregion

        #region Bool类型

        /// <summary>
        /// 转换成Bool类型
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool ToBool(this string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return false;
            }
            s = s.ToLower();
            if (s == "true" || s == "1")
            {
                return true;
            }
            return false;
        }

        #endregion

        #region DateTime类型

        /// <summary>
        /// 转换日期格式
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string s)
        {
            DateTime time;
            if (DateTime.TryParse(s, out time))
            {
                return time;
            }
            else
            {
                return DateTime.Parse("1900-01-01");
            }
        }

        /// <summary>
        /// 判断是否日期格式
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsDateTime(this string s)
        {
            DateTime time;
            if (DateTime.TryParse(s, out time))
            {
                return true;
            }
            return false;
        }

        #endregion

        /// <summary>
        /// 手机号显示 加*
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string MaxLengthReplaceMobile(this string s)
        {
            if (s.IsEmpty())
                return "";
            var length = s.Length;
            var result = s.Substring(0, 3) + "****" + s.Substring(length - 4, 4);
            return result;
        }
        /// <summary>
        /// 隐藏支付宝账号
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string HideAlipay(this string s)
        {
            if (s.IsEmpty())
                return "";
            var length = s.Length; var result = s;
            if (length > 5)
                result = s.Substring(0, 5) + "**********";
            return result;
        }

        /// <summary>
        /// 隐藏姓名
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string HideName(this string s)
        {
            if (s.IsEmpty())
                return "";
            var result = s.Substring(0, 1) + "****";
            return result;
        }
        /// <summary>
        /// 隐藏字符串
        /// </summary>
        /// <param name="str"></param>
        /// <param name="startIndex">开始下标</param>
        /// <param name="endIndex">结束下标</param>
        /// <returns></returns>
        public static string HideString(this string str, int startIndex, int endIndex)
        {
            startIndex = Math.Abs(startIndex);
            endIndex = Math.Abs(endIndex);
            if (str.IsEmpty())
                return "";
            if (endIndex - startIndex < 1)
            {
                throw new Exception("没有需要隐藏的字符");
            }
            if (str.Length <= (startIndex + 1))
            {
                return str;
            }
            if (str.Length <= (endIndex + 1))
            {
                endIndex = str.Length - 1;
            }
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < endIndex - startIndex; i++)
                sb.Append("*");
            return $"{str.Substring(0, startIndex)}{sb}{str.Substring(endIndex)}";
        }
        /// <summary>
        /// 隐藏手机号码中间加*
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string MobileXing(this string s)
        {
            string r = !s.IsEmpty() ? s.Trim() : "";
            if (r.Length > 3)
                r = r.Remove(3, 1).Insert(3, "*");
            if (r.Length > 4)
                r = r.Remove(4, 1).Insert(4, "*");
            if (r.Length > 5)
                r = r.Remove(5, 1).Insert(5, "*");
            if (r.Length > 6)
                r = r.Remove(6, 1).Insert(6, "*");
            return r;
        }

        /// <summary>
        /// Url编码
        /// </summary>
        public static readonly Regex UrlParamsRex = new Regex(@"[\?&](([^&=]+)=([^&=#]*))", RegexOptions.Compiled);

        /// <summary>
        /// 获取Url参数
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetUrlParams(this string uri)
        {
            var matches = UrlParamsRex.Matches(uri);
            var keyValues = new Dictionary<string, string>(matches.Count);
            foreach (Match m in matches)
                keyValues.Add(Uri.UnescapeDataString(m.Groups[2].Value), Uri.UnescapeDataString(m.Groups[3].Value));

            return keyValues;
        }
        public static bool IsPid(this string s)
        {
            if (s.NullOrEmpty())
                return false;
            var reg =
                new Regex(
                    @"^mm_[0-9]{5,12}_[0-9]{5,12}_[0-9]{5,12}");
            return reg.IsMatch(s);
        }

        /// <summary>
        /// 获取MD5值
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public static string GetMD5(this string s, Encoding e)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            return BitConverter.ToString(md5.ComputeHash(e.GetBytes(s))).Replace("-", string.Empty).ToUpper();
        }

        public static string GetMD5(this string s)
        {
            return GetMD5(s, Encoding.UTF8);
        }
        public static bool IsGroupMessage(this string s)
        {
            return s.EndsWith("@chatroom");
        }

        public static string GetGroupFromUser(this string s)
        {
            var aa = s.Split(":\n");
            if (aa.Length > 1)
            {
                return aa[0];
            }
            return "";
        }
        public static string GetGroupContext(this string s)
        {
            var aa = s.Split(":\n");
            if (aa.Length > 1)
            {
                return aa[1];
            }
            return "";
        }

        public static string ToCutString(this string content, int cutlength)
        {
            if (string.IsNullOrEmpty(content))
            {
                return "";
            }

            if (content.Length >= cutlength)
            {
                content = content.Substring(0, cutlength) + "...";
            }

            return content;
        }
    }
}
