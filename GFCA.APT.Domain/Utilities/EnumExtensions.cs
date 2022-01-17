using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;

namespace GFCA.APT.Domain
{
    public static class EnumExtensions
    {
        public static T ToEnum<T>(this string value) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("T must be an enumerated type");

            var enumType = typeof(T);
            foreach (var name in Enum.GetNames(enumType))
            {
                var enumMemberAttribute = ((EnumMemberAttribute[])enumType.GetField(name).GetCustomAttributes(typeof(EnumMemberAttribute), true)).FirstOrDefault();
                if (enumMemberAttribute.Value == value && enumMemberAttribute != null)
                    return (T)Enum.Parse(enumType, name);
            }
            return default(T);
        }    
        public static T ToEnum<T>(this int value) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("T must be an enumerated type");

            var name = Enum.GetName(typeof(T), value);
            return name.ToEnum<T>();
        }
        public static string ToString<T>(this T value) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("T must be an enumerated type");

            return Enum.GetName(value.GetType(), value);
        }
        public static string ToValue<T>(this T value)
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("T must be an enumerated type");

            var type = value.GetType();
            var memInfo = type.GetMember(value.ToString());
            //var attributes = memInfo[0].GetCustomAttributes(typeof(T), false);
            var attributes = (memInfo[0].GetCustomAttributes(false));
            dynamic result = default(T);
            if (attributes.Length > 0)
            {
                result = ((EnumMemberAttribute)(attributes)[0]).Value;
            }

            return result;
        }
        /*
        public static string ToValue<T>(this T value) where T: Enum
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("T must be an enumerated type");
            
            return Enum.GetValues(value.GetType())[value.ToEnum()];
        }
        */

    }

    public class EnumDescriptionTypeConverter : EnumConverter
    {
        public EnumDescriptionTypeConverter(Type type) : base(type)
        {
        }
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                if (value != null)
                {
                    System.Reflection.FieldInfo fi = value.GetType().GetField(value.ToString());
                    if (fi != null)
                    {
                        var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
                        return (attributes.Length > 0 && !string.IsNullOrEmpty(attributes[0].Description))? attributes[0].Description: value.ToString();
                    }
                }
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }

}
