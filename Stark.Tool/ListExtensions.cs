using System;
using System.Collections.Generic;
using System.Linq;

namespace Stark.Tool
{
    /// <summary>
    /// List扩展类
    /// </summary>
    public static class ListExtensions
    {
        /// <summary>
        /// IList深拷贝
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static IList<T> Clone<T>(this IList<T> list) where T : ICloneable
        {
            return list.Select(item => (T)item.Clone()).ToList();
        }

        /// <summary>
        /// List深拷贝
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static List<T> Clone<T>(this List<T> list) where T : ICloneable
        {
            return list.Select(item => (T)item.Clone()).ToList();
        }
    }
}
