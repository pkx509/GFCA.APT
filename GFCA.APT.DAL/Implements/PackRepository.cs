using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using GFCA.APT.Domain.Dto;
using GFCA.APT.DAL.Interfaces;

namespace GFCA.APT.DAL.Implements
{
    public class PackRepository : RepositoryBase, IPackRepository
    {

        public PackRepository(IDbTransaction transaction): base(transaction) { }

        public PackDto GetById(int id)
        {
            string sqlQuery = @"SELECT * FROM TB_M_PACK WHERE PACK_ID = @PACK_ID;";
            var query = Connection.Query<PackDto>(
                sql: sqlQuery,
                param: new { PACK_ID = id }
                ,transaction: Transaction
                ).FirstOrDefault();

            return query;
        }
        public PackDto GetByCode(string code)
        {
            string sqlQuery = @"SELECT * FROM TB_M_PACK WHERE PACK_CODE = @PACK_CODE";
            var query = Connection.Query<PackDto>(
                sql: sqlQuery,
                param: new { PACK_CODE = code }
                ,transaction: Transaction
                ).FirstOrDefault();

            return query;
        }
        public IEnumerable<PackDto> All()
        {
            string sqlQuery = @"SELECT * FROM TB_M_PACK";
            var query = Connection.Query<PackDto>(
                sql: sqlQuery
                ,transaction: Transaction
                ).ToList();

            return query;
        }

        public void Insert(PackDto entity)
        {
            string sqlExecute =  @"INSERT INTO TB_M_PACK
                                (
                                  PACK_CODE
                                , PACK_NAME
                                , PACK_DESC
                                , FLAG_ROW
                                , CREATED_BY
                                , CREATED_DATE
                                ) VALUES (
                                  @PACK_CODE
                                , @PACK_NAME
                                , @PACK_DESC
                                , @FLAG_ROW
                                , @CREATED_BY
                                , @CREATED_DATE
                                ); SELECT SCOPE_IDENTITY()
                                ";

            var parms = new
            {
                PACK_CODE = entity.PACK_CODE,
                PACK_NAME = entity.PACK_NAME,
                PACK_DESC = entity.PACK_DESC,
                FLAG_ROW = entity.FLAG_ROW,
                CREATED_BY = entity.CREATED_BY,
                CREATED_DATE = entity.CREATED_DATE?.ToDateTime2(),
            };

            entity.PACK_ID = Connection.ExecuteScalar<int>(
                sql: sqlExecute,
                param: parms,
                transaction: Transaction
            );

        }
        public void Update(PackDto entity)
        {
            string sqlExecute = @"UPDATE TB_M_PACK
                                SET
                                  PACK_CODE   = @PACK_CODE
                                , PACK_NAME   = @PACK_NAME
                                , PACK_DESC   = @PACK_DESC
                                , FLAG_ROW     = @FLAG_ROW
                                , UPDATED_BY   = @UPDATED_BY
                                , UPDATED_DATE = @UPDATED_DATE
                                WHERE
                                PACK_ID = @PACK_ID;
                                ";

            var parms = new
        {
                PACK_ID = entity.PACK_ID,
                PACK_CODE = entity.PACK_CODE,
                PACK_NAME = entity.PACK_NAME,
                PACK_DESC = entity.PACK_DESC,
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

        public void Delete(int id)
        {
            string sqlExecute = @"DELETE TB_M_PACK WHERE PACK_ID = @PACK_ID;";
            var parms = new { PACK_ID = id };
            Connection.ExecuteScalar<int>(
                sql: sqlExecute,
                param: parms,
                transaction: Transaction
            );

        }

    }

}
