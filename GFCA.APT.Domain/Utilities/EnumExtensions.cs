using System;
using System.Runtime.Serialization;

namespace GFCA.APT.Domain
{
    public static class EnumExtensions
    {
        public static T ToEnum<T>(this string value) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("T must be an enumerated type");

            return (T)Enum.Parse(typeof(T), value, true);
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
}
