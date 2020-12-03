using System;
using System.Linq;

namespace Stark.Tool
{
    public static class TypeExtensions
    {
        /// <summary>
        /// 获取Sql字段字符串
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetSqlFieldString(this Type type)
        {
            try {
                return string.Join(',',type.GetProperties().Select(x => x.Name).ToList());
            } catch (Exception) {
                return "";
            }
        }


        public static string GetGenericTypeName(this Type type)
        {
            var typeName = string.Empty;

            if (type.IsGenericType) {
                var genericTypes = string.Join(",",type.GetGenericArguments().Select(t => t.Name).ToArray());
                typeName = $"{type.Name.Remove(type.Name.IndexOf('`'))}<{genericTypes}>";
            } else {
                typeName = type.Name;
            }

            return typeName;
        }

        public static string GetGenericTypeName(this object @object)
        {
            return @object.GetType().GetGenericTypeName();
        }
    }
}
