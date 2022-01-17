using Dapper;
using System;
using System.Collections.Generic;
using System.Data;

namespace GFCA.APT.DAL.Utilities
{
    public static class DapperExtensions
    {
        /*
         Connection.QueryDynamic(() => new
                {
                    DOC_HEAD_ID = default(int),
                    DOC_TYPE_CODE = default(string),
                    DOC_TYPE_NAME = default(string),
                    DOC_CODE = default(string)
                }, sqlQuery, parms, Transaction);
         */
        public static IEnumerable<T> QueryDynamic<T>(this IDbConnection connection, Func<T> typeBuilder, string sql, object param, IDbTransaction trans)
        {
            return connection.Query<T>(sql, param, trans);
        }
    }
}
