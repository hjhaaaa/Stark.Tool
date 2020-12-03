using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Stark.Tool
{
    /// <summary>
    /// 帮助类
    /// </summary>
    public static class StringsHelper
    {


        public static bool NullOrEmpty(this string str)
        {
            if (str != null)
            {
                //str = str.Trim(new[] { '\t', '\r', '\n', ' ' });
                str = str.Trim();
            }
            return string.IsNullOrEmpty(str);
        }

        public static bool NullOrEmpty<T>(this IEnumerable<T> me)
        {
            return me == null || !me.Any();
        }

        #region 邮箱/手机号 加*
        /// <summary>
        /// 邮箱显示 加*
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string MaxLengthReplaceEmail(this string s)
        {
            var indexOf = s.IndexOf("@");
            var length = s.Length;
            var result = s.Substring(0, 3) + "****" + s.Substring(indexOf, length - indexOf);
            return result;
        }

        #endregion

        #region 编码/解码

        /// <summary>
        /// Emoji转码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string UnicodeToString(string str)
        {
            string outStr = string.Empty;
            if (!string.IsNullOrEmpty(str))
            {
                string[] strlist = str.Replace("\\", "").Split('u');
                try
                {
                    for (int i = 1; i < strlist.Length; i++)
                    {
                        //将unicode字符转为10进制整数，然后转为char中文字符
                        outStr += (char)int.Parse(strlist[i], System.Globalization.NumberStyles.HexNumber);
                    }
                }
                catch (FormatException ex)
                {
                    outStr = ex.Message;
                }
            }
            return outStr;
        }

        /// <summary>
        /// 字符串转Emoji
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string StringToUnicode(string str)
        {
            string outStr = string.Empty;
            if (!string.IsNullOrEmpty(str))
            {
                for (int i = 0; i < str.Length; i++)
                {
                    //将中文字符转为10进制整数，然后转为16进制unicode字符
                    outStr += "\\u" + ((int)str[i]).ToString("x");
                }
            }
            return outStr;
        }

        public static string UrlEncode(this string str)
        {
            if (str == null)
                return null;
            var url = HttpUtility.UrlEncode(str.Trim());
            return url;

        }
        public static string UrlDecode(this string str)
        {

            var url = HttpUtility.UrlDecode(str);
            return url;

        }
        /// <summary>
        /// base64加密
        /// </summary>
        /// <param name="str"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public static string Base64(this string str, Encoding e)
        {
            return Convert.ToBase64String(e.GetBytes(str));
        }
        /// <summary>
        /// base64解码
        /// </summary>
        /// <param name="s"> </param>
        /// <param name="e"> </param>
        /// <returns> </returns>
        public static string DeBase64(this string s, Encoding e)
        {
            return e.GetString(DeBase64(s));
        }
        /// <summary>
        /// 	base64解码
        /// </summary>
        /// <param name="s"> </param>
        /// <returns> </returns>
        public static byte[] DeBase64(this string s)
        {
            return Convert.FromBase64String(s);
        }

        /// <summary>
        /// 	md5编码
        /// </summary>
        /// <param name="s"> </param>
        /// <param name="e"> </param>
        /// <returns> </returns>
        public static string MD5(this string s, Encoding e)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            return BitConverter.ToString(md5.ComputeHash(e.GetBytes(s))).Replace("-", string.Empty).ToLower();
        }

        /// <summary>
        /// 	md5编码
        /// </summary>
        /// <param name="s"> </param>
        /// <param name="e"> </param>
        /// <returns> </returns>
        public static byte[] MD5Bytes(this string s, Encoding e)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            return md5.ComputeHash(e.GetBytes(s));
        }
        #endregion

        /// <summary>
        /// 密码 加密
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string ChangePassword(string password)
        {
            return ("ZuTuan" + password + "Mg").MD5(Encoding.UTF8);
        }

        #region 验证格式
        /// <summary>
        /// 验证邮箱格式
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsEmail(this string s)
        {
            if (s != null)
                s = s.Trim().ToLower();
            if (s.NullOrEmpty())
                return false;

            return Regex.IsMatch(s, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

        /// <summary>
        /// 验证密码格式（6-20数字+字母）
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsPassword(this string s)
        {
            if (s != null)
                s = s.Trim();
            if (s.NullOrEmpty())
                return false;
            //^.{6,20}$  密码长度
            var reg = new Regex("^(?![0-9]+$)(?![a-zA-Z]+$)[0-9A-Za-z]{6,20}$");
            return reg.IsMatch(s);
        }

        /// <summary>
        /// 是否PId 三段式
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsPId(this string s)
        {
            if (s != null)
                s = s.Trim();
            if (s.NullOrEmpty())
                return false;
            var reg = new Regex(@"^mm_[0-9]{5,12}_[0-9]{5,12}_[0-9]{5,12}$");
            return reg.IsMatch(s);

        }

        /// <summary>
        /// 验证手机合法性
        /// </summary>
        /// <param name="s">手机号</param>
        /// <returns></returns>
        public static bool IsMobile(this string s)
        {
            if (s.NullOrEmpty())
                return false;
            var reg =
                new Regex(
                    @"^1\d{10}$");
            return reg.IsMatch(s);
        }

        /// <summary>
        /// 是否QQ格式
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsQQ(this string s)
        {
            if (s != null)
                s = s.Trim();
            if (s.NullOrEmpty())
                return false;
            var reg = new Regex("[1-9][0-9]{4,14}");
            return reg.IsMatch(s);
        }

        /// <summary>
        /// 验证是否是正整数
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        //public static bool IsNumber(this string s)
        //{
        //    if (s != null)
        //        s = s.Trim().ToLower();
        //    if (s.NullOrEmpty())
        //        return false;

        //    return Regex.IsMatch(s,@"^^[1-9]\d*$");
        //}
        public static bool IsNumber(this string s)
        {
            if (s != null)
                s = s.Trim().ToLower();
            if (s.NullOrEmpty())
                return false;

            return Regex.IsMatch(s, @"^\d*$");
        }
        #endregion

        /// <summary>
        /// 是否是可用的url
        /// </summary>
        /// <param name="urlString"></param>
        /// <returns></returns>
        public static bool IsValidUrl(this string urlString)
        {
            Uri uri;
            return Uri.TryCreate(urlString, UriKind.Absolute, out uri)
                && (uri.Scheme == Uri.UriSchemeHttp
                 || uri.Scheme == Uri.UriSchemeHttps
                 || uri.Scheme == Uri.UriSchemeFtp
                 || uri.Scheme == Uri.UriSchemeMailto
                   );
        }
    }
}
