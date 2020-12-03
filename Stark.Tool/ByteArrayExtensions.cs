using System;
using System.Collections.Generic;
using System.Text;

namespace Stark.Tool
{
    /// <summary>
    /// byte数组的扩展操作
    /// </summary>
    public static class ByteArrayExtensions
    {
        /// <summary>
        /// 转换成任意编码的string
        /// </summary>
        /// <param name="bs"></param>
        /// <param name="en"></param>
        /// <returns></returns>
        public static string ToStr(this byte[] bs, Encoding en)
        {
            return en.GetString(bs);
        }

        /// <summary>
        /// 转换为utf8编码的string
        /// </summary>
        /// <param name="bs"></param>
        /// <returns></returns>
        public static string ToUTF8String(this byte[] bs)
        {
            return ToStr(bs, Encoding.UTF8);
        }


    }
}
