using Dapper;
using GFCA.APT.Domain;
using System;
using System.Data;

namespace GFCA.APT.DAL
{
    public class StringEnumTypeHandler<T> : SqlMapper.TypeHandler<T> where T : struct, IConvertible
    {
        static StringEnumTypeHandler()
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumeration type");
            }
        }

        public override T Parse(object value)
        {
            string v = value as string;
            //return (T)Enum.Parse(typeof(T), Convert.ToString(value));
            return v.ToEnum<T>();
        }

        public override void SetValue(IDbDataParameter parameter, T value)
        {
            parameter.Value = value.ToString();
            //parameter.DbType = DbType.AnsiString;
            parameter.DbType = DbType.String;
        }

    }
}
