using Dapper;
using System;
using System.Data;
using GFCA.APT.Domain.Enums;
using GFCA.APT.Domain;

namespace GFCA.APT.DAL.Utilities
{
    public class FLAG_TYPE_Handler : SqlMapper.ITypeHandler
    {
        public object Parse(Type destinationType, object value)
        {
            if (destinationType == typeof(ROW_TYPE))
            {
                return ((string)value).ToEnum<ROW_TYPE>();
            }
            else
            {
                return ROW_TYPE.SHOW;
            }
        }

        public void SetValue(IDbDataParameter parameter, object value)
        {
            parameter.DbType = DbType.String;
            parameter.Value = (string)((dynamic)value);
        }
    }
}
