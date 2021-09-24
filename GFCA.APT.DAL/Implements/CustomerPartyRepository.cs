using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using GFCA.APT.Domain.Dto;
using GFCA.APT.DAL.Interfaces;

namespace GFCA.APT.DAL.Implements
{
    public class CustomerPartyRepository : RepositoryBase, ICustomerPartyRepository
    {
        
        public CustomerPartyRepository(IDbTransaction transaction): base(transaction) { }

        public CustomerPartyDto GetByCode(string code)
        {
            string sqlQuery = @"SELECT * FROM TB_P_CUSTOMER_PARTY
                                WHERE PARTY_CODE = @PARTY_CODE;";
            var query = Connection.Query<CustomerPartyDto>(
                sql: sqlQuery,
                param: new { PARTY_CODE = code }
                ,transaction: Transaction
                ).FirstOrDefault();

            return query;
        }
 
        public IEnumerable<CustomerPartyDto> All()
        {
            string sqlQuery = @"SELECT * FROM TB_P_CUSTOMER_PARTY;";
            var query = Connection.Query<CustomerPartyDto>(
                sql: sqlQuery
                ,transaction: Transaction
                ).ToList();

            return query;
        }

        public void Insert(CustomerPartyDto entity)
        {
            string sqlExecute = @"INSERT INTO TB_P_CUSTOMER_PARTY
                                (
                                  CUST_CODE
                                , ACC_CODE
                                , DISTB_CODE
                                , CHANNEL_CODE
                                , VENDOR_CODE
                                , FLAG_ROW
                                , CREATED_BY
                                , CREATED_DATE
                                ) VALUES (
                                  @CUST_CODE
                                , @ACC_CODE
                                , @DISTB_CODE
                                , @CHANNEL_CODE
                                , @VENDOR_CODE
                                , @FLAG_ROW
                                , @CREATED_BY
                                , @CREATED_DATE
                                ); SELECT SCOPE_IDENTITY()
                                ";

            var parms = new
            {
                CUST_CODE    = entity.CUST_CODE,
                ACC_CODE     = entity.ACC_CODE,
                DISTB_CODE   = entity.DISTB_CODE,
                CHANNEL_CODE = entity.CHANNEL_CODE,
                VENDOR_CODE  = entity.VENDOR_CODE,
                FLAG_ROW     = entity.FLAG_ROW,
                CREATED_BY   = entity.CREATED_BY,
                CREATED_DATE = entity.CREATED_DATE?.ToDateTime2()
            };

            Connection.ExecuteScalar<int>(
                sql: sqlExecute,
                param: parms,
                transaction: Transaction
            );

        }
        public void Update(CustomerPartyDto entity)
        {
            string sqlExecute = @"UPDATE TB_P_CUSTOMER_PARTY
                                SET
                                      CUST_CODE     = @CUST_CODE
                                    , ACC_CODE      = @ACC_CODE
                                    , DISTB_CODE    = @DISTB_CODE
                                    , CHANNEL_CODE  = @CHANNEL_CODE
                                    , VENDOR_CODE   = @VENDOR_CODE
                                    , FLAG_ROW      = @FLAG_ROW
                                    , UPDATED_BY    = @UPDATED_BY
                                    , UPDATED_DATE  = @UPDATED_DATE
                                    WHERE
                                    PARTY_ID = @PARTY_ID
                                    ";

            var parms = new
            {
                CUST_CODE    = entity.CUST_CODE,
                ACC_CODE     = entity.ACC_CODE,
                DISTB_CODE   = entity.DISTB_CODE,
                CHANNEL_CODE = entity.CHANNEL_CODE,
                VENDOR_CODE  = entity.VENDOR_CODE,
                FLAG_ROW     = entity.FLAG_ROW,
                UPDATED_BY   = entity.UPDATED_BY,
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

            string sqlExecute = @"DELETE TB_P_CUSTOMER_PARTY
                                WHERE
                                PARTY_CODE = @PARTY_CODE;
                                ";
            var parms = new { PARTY_CODE = code };

            Connection.ExecuteScalar<int>(
                sql: sqlExecute,
                param: parms,
                transaction: Transaction
            );

        }

    }

}
