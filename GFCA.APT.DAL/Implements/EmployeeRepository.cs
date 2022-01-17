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

        public EmployeeDto GetEmployee(string email, string password)
        {
            string sqlQuery =
@"SELECT
  EMP_ID
, EMP_CODE
, PREFIX
, FIRSTNAME
, LASTNAME
, EMAIL
, PWD
, SALT
, FLAG_ROW
, CREATED_BY
, CREATED_DATE
, UPDATED_BY
, UPDATED_DATE
FROM TB_M_EMPLOYEE
WHERE EMAIL = @IN_EMAIL
AND PWD = @IN_PWD";
            
            var parms = new
            {
                IN_EMAIL = email,
                IN_PWD = password
            };

            var query = Connection.QueryFirstOrDefault<EmployeeDto>(
                sql: sqlQuery
                , param: parms
                , transaction: Transaction
                );

            return query;

        }

        public IEnumerable<EmployeeDto> All()
        {
            string sqlQuery = 
@"SELECT
  EMP_ID 'USER_ID'
, EMP_CODE
, PREFIX
, FIRSTNAME
, LASTNAME
, EMAIL
, PWD
, SALT
, FLAG_ROW
, CREATED_BY
, CREATED_DATE
, UPDATED_BY
, UPDATED_DATE
FROM TB_M_EMPLOYEE";
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
                FIRSTNAME = entity.FIRSTNAME,
                LASTNAME = entity.LASTNAME,
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
                FIRSTNAME = entity.FIRSTNAME,
                LASTNAME = entity.LASTNAME,
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

        public IEnumerable<EmployeeRoleDto> GetRoles(int empId)
        {
            string sqlQuery =
@"SELECT 
  A.EMP_ID
, A.EMP_CODE
, A.EMAIL
, B.EMP_ROLE_ID
, B.ROLE_ID
, B.PERMISSION
, C.ROLE_NAME
FROM TB_M_EMPLOYEE A
LEFT JOIN TB_P_EMP_ROLE B ON B.EMP_ID = A.EMP_ID
LEFT JOIN TB_M_ROLE C ON C.ROLE_ID = B.ROLE_ID
WHERE A.EMP_ID = @IN_EMP_ID";

            var parms = new
            {
                IN_EMP_ID = empId
            };

            var query = Connection.Query<EmployeeRoleDto>(
                sql: sqlQuery
                , param: parms
                , transaction: Transaction
                );

            return query;

        }
    }

}
