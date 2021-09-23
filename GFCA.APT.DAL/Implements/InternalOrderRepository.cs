using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using GFCA.APT.Domain.Dto;
using GFCA.APT.DAL.Interfaces;

namespace GFCA.APT.DAL.Implements
{
    public class InternalOrderRepository : RepositoryBase, IInternalOrderRepository
    {

        public InternalOrderRepository(IDbTransaction transaction): base(transaction) { }

        public InternalOrderDto GetByCode(string code)
        {
            string sqlQuery = @"SELECT * FROM TB_M_INTERNAL_ORDER WHERE IO_CODE = @IO_CODE;";
            var query = Connection.Query<InternalOrderDto>(
                sql: sqlQuery,
                param: new { IO_CODE = code }
                ,transaction: Transaction
                ).FirstOrDefault();
       
            return query;
        }
        public IEnumerable<InternalOrderDto> All()
        {
            string sqlQuery = @"SELECT * FROM TB_M_INTERNAL_ORDER";
            var query = Connection.Query<InternalOrderDto>(
                sql: sqlQuery
                ,transaction: Transaction
                ).ToList();

            return query;
        }

        public void Insert(InternalOrderDto entity)
        {
            string sqlExecute = @"INSERT INTO TB_M_INTERNAL_ORDER
                                (
                                  IO_CODE
                                , IO_NAME
                                , IO_DESC
                                , FLAG_ROW
                                , CREATED_BY
                                , CREATED_DATE
                                ) VALUES (
                                  @IO_CODE
                                , @IO_NAME
                                , @IO_DESC
                                , @FLAG_ROW
                                , @CREATED_BY
                                , @CREATED_DATE
                                ); SELECT SCOPE_IDENTITY()
                                ";

            var parms = new
            {
                IO_CODE = entity.IO_CODE,
                IO_NAME = entity.IO_NAME,
                IO_DESC = entity.IO_DESC,
                FLAG_ROW = entity.FLAG_ROW,
                CREATED_BY = entity.CREATED_BY,
                CREATED_DATE = entity.CREATED_DATE?.ToDateTime2(),
            };

            entity.IO_ID = Connection.ExecuteScalar<int>(
                sql: sqlExecute,
                param: parms,
                transaction: Transaction
            );

        }
        public void Update(InternalOrderDto entity)
        {
            string sqlExecute = @"UPDATE TB_M_INTERNAL_ORDER
                                SET
                                  IO_NAME   = @IO_NAME
                                , IO_DESC   = @IO_DESC
                                , FLAG_ROW     = @FLAG_ROW
                                , UPDATED_BY   = @UPDATED_BY
                                , UPDATED_DATE = @UPDATED_DATE
                                WHERE
                                IO_CODE = @IO_CODE;
                                ";

            var parms = new
        {
                IO_CODE = entity.IO_CODE,
                IO_NAME = entity.IO_NAME,
                IO_DESC = entity.IO_DESC,
                FLAG_ROW = entity.FLAG_ROW,
                UPDATED_BY = entity.UPDATED_BY,
                UPDATED_DATE = entity.UPDATED_DATE?.ToDateTime2()
            };

            Connection.ExecuteScalar<int>(
                sql: sqlExecute,
                param: parms,
                transaction: Transaction
            );

        }

        public void Delete(string code)
        {
            string sqlExecute = @"DELETE TB_M_INTERNAL_ORDER WHERE IO_CODE = @IO_CODE;";
            var parms = new { IO_CODE = code };

            Connection.ExecuteScalar<int>(
                sql: sqlExecute,
                param: parms,
                transaction: Transaction
            );

        }

    }

}
