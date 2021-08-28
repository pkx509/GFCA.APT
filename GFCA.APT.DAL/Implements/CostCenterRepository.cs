using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using GFCA.APT.Domain.Dto;
using GFCA.APT.DAL.Interfaces;

namespace GFCA.APT.DAL.Implements
{
    public class CostCenterRepository : RepositoryBase, ICostCenterRepository
    {
        
        public CostCenterRepository(IDbTransaction transaction): base(transaction) { }

        public CostCenterDto GetById(int id)
        {
            string sqlQuery = @"SELECT * FROM TB_M_COST_CENTER WHERE CENTER_ID = @CENTER_ID;";
            var query = Connection.Query<CostCenterDto>(
                sql: sqlQuery,
                param: new { CENTER_ID = id }
                ,transaction: Transaction
                ).FirstOrDefault();

            return query;
        }
        public CostCenterDto GetByCode(string code)
        {
            string sqlQuery = @"SELECT * FROM TB_M_COST_CENTER WHERE CENTER_CODE = @CENTER_CODE;";
            var query = Connection.Query<CostCenterDto>(
                sql: sqlQuery,
                param: new { CENTER_CODE = code }
                ,transaction: Transaction
                ).FirstOrDefault();

            return query;
        }
        public IEnumerable<CostCenterDto> All()
        {
            string sqlQuery = @"SELECT * FROM TB_M_COST_CENTER";
            var query = Connection.Query<CostCenterDto>(
                sql: sqlQuery
                ,transaction: Transaction
                ).ToList();

            return query;
        }

        public void Insert(CostCenterDto entity)
        {
            string sqlExecute = @"INSERT INTO TB_M_COST_CENTER
                                (
                                  CENTER_CODE
                                , CENTER_NAME
                                , CENTER_DESC
                                , FLAG_ROW
                                , CREATED_BY
                                , CREATED_DATE
                                ) VALUES (
                                  @CENTER_CODE
                                , @CENTER_NAME
                                , @CENTER_DESC
                                , @FLAG_ROW
                                , @CREATED_BY
                                , @CREATED_DATE
                                ); SELECT SCOPE_IDENTITY()
                                ";

            var parms = new
            {
                CENTER_CODE = entity.CENTER_CODE,
                CENTER_NAME = entity.CENTER_NAME,
                CENTER_DESC = entity.CENTER_DESC,
                FLAG_ROW = entity.FLAG_ROW,
                CREATED_BY = entity.CREATED_BY,
                CREATED_DATE = entity.CREATED_DATE?.ToDateTime2(),
            };

            entity.CENTER_ID = Connection.ExecuteScalar<int>(
                sql: sqlExecute,
                param: parms,
                transaction: Transaction
            );

        }
        public void Update(CostCenterDto entity)
        {
            string sqlExecute = @"UPDATE TB_M_COST_CENTER
                                SET
                                  CENTER_CODE   = @CENTER_CODE
                                , CENTER_NAME   = @CENTER_NAME
                                , CENTER_DESC   = @CENTER_DESC
                                , FLAG_ROW     = @FLAG_ROW
                                , UPDATED_BY   = @UPDATED_BY
                                , UPDATED_DATE = @UPDATED_DATE
                                WHERE
                                CENTER_ID = @CENTER_ID;
                                ";

            var parms = new
            {
                CENTER_ID = entity.CENTER_ID,
                CENTER_CODE = entity.CENTER_CODE,
                CENTER_NAME = entity.CENTER_NAME,
                CENTER_DESC = entity.CENTER_DESC,
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
            string sqlExecute = @"DELETE TB_M_COST_CENTER WHERE CENTER_ID = @CENTER_ID;";
            var parms = new { CENTER_ID = id };

            Connection.ExecuteScalar<int>(
                sql: sqlExecute,
                param: parms,
                transaction: Transaction
            );

        }

    }

}
