using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using GFCA.APT.Domain.Dto;
using GFCA.APT.DAL.Interfaces;

namespace GFCA.APT.DAL.Implements
{
    public class ClientRepository : RepositoryBase, IClientRepository
    {

        public ClientRepository(IDbTransaction transaction): base(transaction) { }

        public ClientDto GetById(int id)
        {
            string sqlQuery = @"SELECT * FROM TB_M_CLIENT WHERE CLIENT_ID = @CLIENT_ID;";
            var query = Connection.Query<ClientDto>(
                sql: sqlQuery,
                param: new { CLIENT_ID = id }
                ,transaction: Transaction
                ).FirstOrDefault();

            return query;
        }
        public ClientDto GetByCode(string code)
        {
            string sqlQuery = @"SELECT * FROM TB_M_CLIENT WHERE CLIENT_CODE = @CLIENT_CODE";
            var query = Connection.Query<ClientDto>(
                sql: sqlQuery,
                param: new { CLIENT_CODE = code }
                ,transaction: Transaction
                ).FirstOrDefault();

            return query;
        }
        public IEnumerable<ClientDto> All()
        {
            string sqlQuery = @"SELECT * FROM TB_M_CLIENT";
            var query = Connection.Query<ClientDto>(
                sql: sqlQuery
                ,transaction: Transaction
                ).ToList();

            return query;
        }

        public void Insert(ClientDto entity)
        {
            string sqlExecute =  @"INSERT INTO TB_M_CLIENT
                                (
                                  CLIENT_CODE
                                , CLIENT_NAME
                                , CLIENT_DESC
                                , FLAG_ROW
                                , CREATED_BY
                                , CREATED_DATE
                                ) VALUES (
                                  @CLIENT_CODE
                                , @CLIENT_NAME
                                , @CLIENT_DESC
                                , @FLAG_ROW
                                , @CREATED_BY
                                , @CREATED_DATE
                                ); SELECT SCOPE_IDENTITY()
                                ";

            var parms = new
            {
                CLIENT_CODE = entity.CLIENT_CODE,
                CLIENT_NAME = entity.CLIENT_NAME,
                CLIENT_DESC = entity.CLIENT_DESC,
                FLAG_ROW = entity.FLAG_ROW,
                CREATED_BY = entity.CREATED_BY,
                CREATED_DATE = entity.CREATED_DATE?.ToDateTime2(),
            };

            entity.CLIENT_ID = Connection.ExecuteScalar<int>(
                sql: sqlExecute,
                param: parms,
                transaction: Transaction
            );

        }
        public void Update(ClientDto entity)
        {
            string sqlExecute = @"UPDATE TB_M_CLIENT
                                SET
                                  CLIENT_CODE   = @CLIENT_CODE
                                , CLIENT_NAME   = @CLIENT_NAME
                                , CLIENT_DESC   = @CLIENT_DESC
                                , FLAG_ROW     = @FLAG_ROW
                                , UPDATED_BY   = @UPDATED_BY
                                , UPDATED_DATE = @UPDATED_DATE
                                WHERE
                                CLIENT_ID = @CLIENT_ID;
                                ";

            var parms = new
        {
                CLIENT_ID = entity.CLIENT_ID,
                CLIENT_CODE = entity.CLIENT_CODE,
                CLIENT_NAME = entity.CLIENT_NAME,
                CLIENT_DESC = entity.CLIENT_DESC,
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
            string sqlExecute = @"DELETE TB_M_CLIENT WHERE CLIENT_ID = @CLIENT_ID;";
            var parms = new { CLIENT_ID = id };
            Connection.ExecuteScalar<int>(
                sql: sqlExecute,
                param: parms,
                transaction: Transaction
            );

        }

    }

}
