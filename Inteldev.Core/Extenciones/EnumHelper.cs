using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Inteldev.Core.Extenciones
{
    public static class EnumHelper
    {
        /// <summary>
        /// Retrieve the description on the enum, e.g.
        /// [Description("Bright Pink")]
        /// BrightPink = 2,
        /// Then when you pass in the enum, it will retrieve the description
        /// </summary>
        /// <param name="en">The Enumeration</param>
        /// <returns>A string representing the friendly name</returns>
        //public static string GetDescription(this Enum en)
        //{
        //    Type type = en.GetType();

        //    MemberInfo[] memInfo = type.GetMember(en.ToString());

        //    if (memInfo != null && memInfo.Length > 0)
        //    {
        //        object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

        //        if (attrs != null && attrs.Length > 0)
        //        {
        //            return ((DescriptionAttribute)attrs[0]).Description;
        //        }
        //    }

        //    return en.ToString();
        //}

        public static string GetDescription(this Enum value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());
            var attribute = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as DescriptionAttribute;

            return (attribute != null && attribute.Description != "") ? attribute.Description : value.ToString();
        }

        public static object[] GetValuesAndDescriptions(Type enumType, string[] Excluir = null)
        {
            var values = Enum.GetValues(enumType).Cast<object>();
            var valuesAndDescriptions = from value in values
                                        where Excluir == null || !Excluir.Any(p => p == value.ToString())
                                        select new
                                        {
                                            Value = value,
                                            Description = (value.GetType().GetMember(value.ToString())[0].GetCustomAttributes(true).OfType<DescriptionAttribute>().Count() > 0 ?
                                                value.GetType().GetMember(value.ToString())[0].GetCustomAttributes(true).OfType<DescriptionAttribute>().First().Description
                                                : value.ToString().SplitCamelCase())
                                        };
            return valuesAndDescriptions.ToArray();
        }

        public static object prueba()
        {
            var aaaa = "123";
            return aaaa;

        }

    }
}
