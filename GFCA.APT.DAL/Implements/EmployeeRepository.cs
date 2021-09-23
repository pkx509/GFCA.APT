using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using GFCA.APT.Domain.Dto;
using GFCA.APT.DAL.Interfaces;

namespace GFCA.APT.DAL.Implements
{
    public class EmployeeRepository : RepositoryBase, IEmployeeRepository
    {

        public EmployeeRepository(IDbTransaction transaction): base(transaction) { }

        public EmployeeDto GetByCode(string code)
        {
            string sqlQuery = @"SELECT * FROM TB_M_EMPLOYEE WHERE EMP_CODE = @EMP_CODE;";
            var query = Connection.Query<EmployeeDto>(
                sql: sqlQuery,
                param: new { EMP_CODE = code }
                ,transaction: Transaction
                ).FirstOrDefault();
       
            return query;
        }
        public IEnumerable<EmployeeDto> All()
        {
            string sqlQuery = @"SELECT * FROM TB_M_EMPLOYEE";
            var query = Connection.Query<EmployeeDto>(
                sql: sqlQuery
                ,transaction: Transaction
                ).ToList();

            return query;
        }

        public void Insert(EmployeeDto entity)
        {
            string sqlExecute = @"INSERT INTO TB_M_EMPLOYEE
                                (
                                  EMP_CODE
                                , PREFIX
                                , NAME_FIRST
                                , NAME_LAST
                                , EMAIL
                                , FLAG_ROW
                                , CREATED_BY
                                , CREATED_DATE
                                ) VALUES (
                                  @EMP_CODE
                                , @PREFIX
                                , @NAME_FIRST
                                , @NAME_LAST
                                , @EMAIL
                                , @FLAG_ROW
                                , @CREATED_BY
                                , @CREATED_DATE
                                ); SELECT SCOPE_IDENTITY()
                                ";

            var parms = new
            {
                EMP_CODE = entity.EMP_CODE,
                PREFIX = entity.PREFIX,
                NAME_FIRST = entity.NAME_FIRST,
                NAME_LAST = entity.NAME_LAST,
                EMAIL = entity.EMAIL,
                FLAG_ROW = entity.FLAG_ROW,
                CREATED_BY = entity.CREATED_BY,
                CREATED_DATE = entity.CREATED_DATE?.ToDateTime2(),
            };

            Connection.ExecuteScalar<int>(
                sql: sqlExecute,
                param: parms,
                transaction: Transaction
            );

        }
        public void Update(EmployeeDto entity)
        {
            string sqlExecute = @"UPDATE TB_M_EMPLOYEE
                                SET
                                  PREFIX        = @PREFIX
                                , NAME_FIRST    = @NAME_FIRST
                                , NAME_LAST     = @NAME_LAST
                                , EMAIL         = @EMAIL
                                , FLAG_ROW      = @FLAG_ROW
                                , UPDATED_BY    = @UPDATED_BY
                                , UPDATED_DATE  = @UPDATED_DATE
                                WHERE
                                EMP_CODE = @EMP_CODE;
                                ";

            var parms = new
            {
                EMP_CODE = entity.EMP_CODE,
                PREFIX = entity.PREFIX,
                NAME_FIRST = entity.NAME_FIRST,
                NAME_LAST = entity.NAME_LAST,
                EMAIL = entity.EMAIL,
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
            string sqlExecute = @"DELETE TB_M_EMPLOYEE WHERE EMP_CODE = @EMP_CODE;";
            var parms = new { EMP_CODE = code };

            Connection.ExecuteScalar<int>(
                sql: sqlExecute,
                param: parms,
                transaction: Transaction
            );

        }

    }

}
