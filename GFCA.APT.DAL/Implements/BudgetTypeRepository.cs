using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using GFCA.APT.Domain.Dto;
using GFCA.APT.DAL.Interfaces;

namespace GFCA.APT.DAL.Implements
{
    public class BudgetTypeRepository : RepositoryBase, IBudgetTypeRepository
    {
        
        public BudgetTypeRepository(IDbTransaction transaction): base(transaction) { }

        public BudgetTypeDto GetById(int id)
        {
            string sqlQuery = @"SELECT * FROM TB_M_BUDGET_TYPE WHERE BG_TYPE_ID = @BG_TYPE_ID;";
            var query = Connection.Query<BudgetTypeDto>(
                sql: sqlQuery,
                param: new { BG_TYPE_ID = id }
                ,transaction: Transaction
                ).FirstOrDefault();

            return query;
        }
        public BudgetTypeDto GetByCode(string code)
        {
            string sqlQuery = @"SELECT * FROM TB_M_BUDGET_TYPE WHERE BG_TYPE_CODE = @BG_TYPE_CODE;";
            var query = Connection.Query<BudgetTypeDto>(
                sql: sqlQuery,
                param: new { BG_TYPE_CODE = code }
                ,transaction: Transaction
                ).FirstOrDefault();

            return query;
        }
        public IEnumerable<BudgetTypeDto> All()
        {
            string sqlQuery = @"SELECT * FROM TB_M_BUDGET_TYPE;";
            var query = Connection.Query<BudgetTypeDto>(
                sql: sqlQuery
                ,transaction: Transaction
                ).ToList();

            return query;
        }

        public void Insert(BudgetTypeDto entity)
        {
            string sqlExecute = @"INSERT INTO TB_M_BUDGET_TYPE
                                (
                                  BG_TYPE_CODE
                                , BG_TYPE_NAME
                                , BG_TYPE_DESC
                                , FLAG_ROW
                                , CREATED_BY
                                , CREATED_DATE
                                ) VALUES (
                                  @BG_TYPE_CODE
                                , @BG_TYPE_NAME
                                , @BG_TYPE_DESC
                                , @FLAG_ROW
                                , @CREATED_BY
                                , @CREATED_DATE
                                ); SELECT SCOPE_IDENTITY()
                                ";

            var parms = new
            {
                BG_TPE_CODE = entity.BG_TYPE_CODE,
                BG_TYPE_NAME = entity.BG_TYPE_NAME,
                BG_TYPE_DESC = entity.BG_TYPE_DESC,
                FLAG_ROW = entity.FLAG_ROW,
                CREATED_BY = entity.CREATED_BY,
                CREATED_DATE = entity.CREATED_DATE?.ToDateTime2(),
            };

            /*
            entity.BG_TYPE_ID = Connection.ExecuteScalar<int>(
                sql: sqlExecute,
                param: parms,
                transaction: Transaction
            );
            */
            Connection.ExecuteScalar<int>(
                sql: sqlExecute,
                param: parms,
                transaction: Transaction
            );
        }
        public void Update(BudgetTypeDto entity)
        {
            string sqlExecute = @"UPDATE TB_M_BUDGET_TYPE
                                SET
                                      BG_TYPE_NAME = @BG_TYPE_NAME
                                    , BG_TYPE_DESC   = @BG_TYPE_DESC
                                    , FLAG_ROW     = @FLAG_ROW
                                    , UPDATED_BY   = @UPDATED_BY
                                    , UPDATED_DATE = @UPDATED_DATE
                                WHERE
                                BG_TYPE_CODE = @BG_TYPE_CODE;
                                ";

            var parms = new
            {
                BG_TYPE_CODE = entity.BG_TYPE_CODE,
                BG_TYPE_NAME = entity.BG_TYPE_NAME,
                BG_TYPE_DESC = entity.BG_TYPE_DESC,
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

            string sqlExecute = @"DELETE TB_M_BUDGET_TYPE WHERE BG_TYPE_CODE = @BG_TYPE_CODE;";

            var parms = new { BG_TYPE_CODE = code };

            Connection.ExecuteScalar<int>(
                sql: sqlExecute,
                param: parms,
                transaction: Transaction
            );

        }

    }

}
