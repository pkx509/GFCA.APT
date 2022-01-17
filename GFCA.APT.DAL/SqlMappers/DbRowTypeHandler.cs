using Dapper;
using System;
using System.Data;
using GFCA.APT.Domain;
using GFCA.APT.Domain.Enums;

namespace GFCA.APT.DAL
{
    internal class DbRowTypeHandler : SqlMapper.TypeHandler<ROW_TYPE>
    {
        public override ROW_TYPE Parse(object value)
        {
            string v = value as string;
            return v.ToEnum<ROW_TYPE>();
        }

        public override void SetValue(IDbDataParameter parameter, ROW_TYPE value)
        {
            parameter.Value = value;
        }
    }
    
}
