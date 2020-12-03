using System;
using System.Collections.Generic;
using System.Text;

namespace Stark.Tool
{
    public static class IEnumerableExtendsions
    {
        /// <summary>
        /// 链接字符串
        /// </summary>
        /// <param name="values"></param>
        /// <param name="separator">分隔符</param>
        /// <returns></returns>
        public static string Join(this IEnumerable<string> values, string separator)
        {
            return string.Join(separator, values);
        }
    }
}
