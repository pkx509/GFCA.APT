using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using GFCA.APT.Domain.Dto;
using GFCA.APT.DAL.Interfaces;

namespace GFCA.APT.DAL.Implements
{
    public class CustomerRepository : RepositoryBase, ICustomerRepository
    {
        
        public CustomerRepository(IDbTransaction transaction): base(transaction) { }

        public CustomerDto GetById(int id)
        {
            string sqlQuery = @"SELECT * FROM TB_M_CUSTOMER WHERE CUST_ID = @CUST_ID;";
            var query = Connection.Query<CustomerDto>(
                sql: sqlQuery,
                param: new { CUST_ID = id }
                ,transaction: Transaction
                ).FirstOrDefault();

            return query;
        }
        public CustomerDto GetByCode(string code)
        {
            string sqlQuery = @"SELECT * FROM TB_M_CUSTOMER WHERE CUST_CODE = @CUST_CODE;";
            var query = Connection.Query<CustomerDto>(
                sql: sqlQuery,
                param: new { CUST_CODE = code }
                ,transaction: Transaction
                ).FirstOrDefault();

            return query;
        }
        public IEnumerable<CustomerDto> All()
        {
            string sqlQuery = @"SELECT * FROM TB_M_CUSTOMER";
            var query = Connection.Query<CustomerDto>(
                sql: sqlQuery
                ,transaction: Transaction
                ).ToList();

            return query;
        }

        public void Insert(CustomerDto entity)
        {
            string sqlExecute = @"INSERT INTO TB_M_CUSTOMER
                                (
                                  CUST_CODE
                                , CUST_NAME
                                , CUST_ABV
                                , CUST_GROUP1
                                , CUST_DESC
                                , FLAG_ROW
                                , CREATED_BY
                                , CREATED_DATE
                                ) VALUES (
                                  @CUST_CODE
                                , @CUST_NAME
                                , @CUST_ABV
                                , @CUST_GROUP1
                                , @CUST_DESC
                                , @FLAG_ROW
                                , @CREATED_BY
                                , @CREATED_DATE
                                ); SELECT SCOPE_IDENTITY()
                                ";

            var parms = new
            {
                CUST_CODE = entity.CUST_CODE,
                CUST_NAME = entity.CUST_NAME,
                CUST_ABV = entity.CUST_ABV,
                CUST_GROUP1 = entity.CUST_GROUP1,
                CUST_DESC = entity.CUST_DESC,
                FLAG_ROW = entity.FLAG_ROW,
                CREATED_BY = entity.CREATED_BY,
                CREATED_DATE = entity.CREATED_DATE?.ToDateTime2(),
            };

            entity.CUST_ID = Connection.ExecuteScalar<int>(
                sql: sqlExecute,
                param: parms,
                transaction: Transaction
            );

        }
        public void Update(CustomerDto entity)
        {
            string sqlExecute = @"UPDATE TB_M_CUSTOMER
                                SET
                                  CUST_CODE   = @CUST_CODE
                                , CUST_NAME   = @CUST_NAME
                                , CUST_ABV    = @CUST_ABV
                                , CUST_GROUP1 = @CUST_GROUP1
                                , CUST_DESC   = @CUST_DESC
                                , FLAG_ROW     = @FLAG_ROW
                                , UPDATED_BY   = @UPDATED_BY
                                , UPDATED_DATE = @UPDATED_DATE
                                WHERE
                                CUST_ID = @CUST_ID;
                                ";

            var parms = new
            {
                CUST_ID = entity.CUST_ID,
                CUST_CODE = entity.CUST_CODE,
                CUST_NAME = entity.CUST_NAME,
                CUST_ABV = entity.CUST_ABV,
                CUST_GROUP1 = entity.CUST_GROUP1,
                CUST_DESC = entity.CUST_DESC,
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
            string sqlExecute = @"DELETE TB_M_CUSTOMER WHERE CUST_ID = @CUST_ID;";
            var parms = new { CUST_ID = id };

            Connection.ExecuteScalar<int>(
                sql: sqlExecute,
                param: parms,
                transaction: Transaction
            );

        }

    }

}
