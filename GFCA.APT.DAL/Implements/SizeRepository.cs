using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using GFCA.APT.Domain.Dto;
using GFCA.APT.DAL.Interfaces;

namespace GFCA.APT.DAL.Implements
{
    public class SizeRepository : RepositoryBase, ISizeRepository
    {

        public SizeRepository(IDbTransaction transaction): base(transaction) { }

        public SizeDto GetById(int id)
        {
            string sqlQuery = @"SELECT * FROM TB_M_SIZE WHERE SIZE_ID = @SIZE_ID;";
            var query = Connection.Query<SizeDto>(
                sql: sqlQuery,
                param: new { SIZE_ID = id }
                ,transaction: Transaction
                ).FirstOrDefault();

            return query;
        }
        public SizeDto GetByCode(string code)
        {
            string sqlQuery = @"SELECT * FROM TB_M_SIZE WHERE SIZE_CODE = @SIZE_CODE;";
            var query = Connection.Query<SizeDto>(
                sql: sqlQuery,
                param: new { SIZE_CODE = code }
                ,transaction: Transaction
                ).FirstOrDefault();
       
            return query;
        }
        public IEnumerable<SizeDto> All()
        {
            string sqlQuery = @"SELECT * FROM TB_M_SIZE";
            var query = Connection.Query<SizeDto>(
                sql: sqlQuery
                ,transaction: Transaction
                ).ToList();

            return query;
        }

        public void Insert(SizeDto entity)
        {
            string sqlExecute = @"INSERT INTO TB_M_SIZE
                                (
                                  SIZE_CODE
                                , SIZE_NAME
                                , SIZE_DESC
                                , FLAG_ROW
                                , CREATED_BY
                                , CREATED_DATE
                                ) VALUES (
                                  @SIZE_CODE
                                , @SIZE_NAME
                                , @SIZE_DESC
                                , @FLAG_ROW
                                , @CREATED_BY
                                , @CREATED_DATE
                                ); SELECT SCOPE_IDENTITY()
                                ";

            var parms = new
            {
                SIZE_CODE = entity.SIZE_CODE,
                SIZE_NAME = entity.SIZE_NAME,
                SIZE_DESC = entity.SIZE_DESC,
                FLAG_ROW = entity.FLAG_ROW,
                CREATED_BY = entity.CREATED_BY,
                CREATED_DATE = entity.CREATED_DATE?.ToDateTime2(),
            };

            entity.SIZE_ID = Connection.ExecuteScalar<int>(
                sql: sqlExecute,
                param: parms,
                transaction: Transaction
            );

        }
        public void Update(SizeDto entity)
        {
            string sqlExecute = @"UPDATE TB_M_SIZE
                                SET
                                  SIZE_CODE   = @SIZE_CODE
                                , SIZE_NAME   = @SIZE_NAME
                                , SIZE_DESC   = @SIZE_DESC
                                , FLAG_ROW     = @FLAG_ROW
                                , UPDATED_BY   = @UPDATED_BY
                                , UPDATED_DATE = @UPDATED_DATE
                                WHERE
                                SIZE_ID = @SIZE_ID;
                                ";

            var parms = new
        {
                SIZE_ID = entity.SIZE_ID,
                SIZE_CODE = entity.SIZE_CODE,
                SIZE_NAME = entity.SIZE_NAME,
                SIZE_DESC = entity.SIZE_DESC,
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
            string sqlExecute = @"DELETE TB_M_SIZE WHERE SIZE_ID = @SIZE_ID;";
            var parms = new { SIZE_ID = id };

            Connection.ExecuteScalar<int>(
                sql: sqlExecute,
                param: parms,
                transaction: Transaction
            );

        }

    }

}
