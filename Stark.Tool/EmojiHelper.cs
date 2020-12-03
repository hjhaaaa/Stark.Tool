using System;
using System.Collections.Generic;
using System.Text;

namespace Stark.Tool
{
    public class EmojiHelper
    {
        /// <summary>
        /// Emoji转码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string UnicodeToString(string str)
        {
            string outStr = string.Empty;
            if (!string.IsNullOrEmpty(str)) {
                string[] strlist = str.Replace("\\","").Split('u');
                try {
                    for (int i = 1; i < strlist.Length; i++) {
                        //将unicode字符转为10进制整数，然后转为char中文字符
                        outStr += (char)int.Parse(strlist[i],System.Globalization.NumberStyles.HexNumber);
                    }
                } catch (FormatException ex) {
                    outStr = ex.Message;
                    throw ex;
                }
            }
            if (string.IsNullOrWhiteSpace(outStr))
                return str;
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
            if (!string.IsNullOrEmpty(str)) {
                for (int i = 0; i < str.Length; i++) {
                    //将中文字符转为10进制整数，然后转为16进制unicode字符
                    outStr += "\\u" + ((int)str[i]).ToString("x");
                }
            }
            return outStr;
        }
    }
}
