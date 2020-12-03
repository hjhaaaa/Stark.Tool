using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Stark.Tool
{
    public static class EnumExtensions
    {
        /// <summary>
        /// 获取枚举的说明
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum en)
        {
            return en.GetType()
             .GetMember(en.ToString())
             .FirstOrDefault()?
             .GetCustomAttribute<DescriptionAttribute>()?
             .Description ?? en.ToString();
        }
    }
}
