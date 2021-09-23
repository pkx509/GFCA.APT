using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using GFCA.APT.Domain.Dto;
using GFCA.APT.DAL.Interfaces;

namespace GFCA.APT.DAL.Implements
{
    public class CompanyRepository : RepositoryBase, ICompanyRepository
    {
        
        public CompanyRepository(IDbTransaction transaction): base(transaction) { }

        public CompanyDto GetByCode(string code)
        {
            string sqlQuery = @"SELECT * FROM TB_M_COMPANY WHERE COMP_CODE = @COMP_CODE;";
            var query = Connection.Query<CompanyDto>(
                sql: sqlQuery,
                param: new { COMP_CODE = code }
                ,transaction: Transaction
                ).FirstOrDefault();

            return query;
        }

        public IEnumerable<CompanyDto> All()
        {
            string sqlQuery = @"SELECT * FROM TB_M_COMPANY";
            var query = Connection.Query<CompanyDto>(
                sql: sqlQuery
                ,transaction: Transaction
                ).ToList();

            return query;
        }

        public void Insert(CompanyDto entity)
        {
            string sqlExecute = @"INSERT INTO TB_M_COMPANY
                                (
                                  COMP_CODE
                                , COMP_NAME
                                , ADDRESS
                                , FLAG_ROW
                                , CREATED_BY
                                , CREATED_DATE
                                ) VALUES (
                                  @COMP_CODE
                                , @COMP_NAME
                                , @ADDRESS
                                , @FLAG_ROW
                                , @CREATED_BY
                                , @CREATED_DATE
                                ); SELECT SCOPE_IDENTITY()
                                ";

            var parms = new
            {
                COMP_CODE = entity.COMP_CODE,
                COMP_NAME = entity.COMP_NAME,
                ADDRESS = entity.ADDRESS,
                FLAG_ROW = entity.FLAG_ROW,
                CREATED_BY = entity.CREATED_BY,
                CREATED_DATE = entity.CREATED_DATE?.ToDateTime2(),
            };
            /*
            entity.COMP_ID = Connection.ExecuteScalar<int>(
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
        public void Update(CompanyDto entity)
        {
            string sqlExecute = @"UPDATE TB_M_COMPANY
                                SET
                                  COMP_NAME   = @COMP_NAME
                                , ADDRESS   = @ADDRESS
                                , FLAG_ROW     = @FLAG_ROW
                                , UPDATED_BY   = @UPDATED_BY
                                , UPDATED_DATE = @UPDATED_DATE
                                WHERE
                                COMP_CODE = @COMP_CODE;
                                ";

            var parms = new
            {
                //COMP_ID = entity.COMP_ID,
                COMP_CODE = entity.COMP_CODE,
                COMP_NAME = entity.COMP_NAME,
                ADDRESS = entity.ADDRESS,
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
            string sqlExecute = @"DELETE TB_M_COMPANY WHERE COMP_CODE = @COMP_CODE;";
            var parms = new { COMP_CODE = code };

            Connection.ExecuteScalar<int>(
                sql: sqlExecute,
                param: parms,
                transaction: Transaction
            );

        }

    }

}
