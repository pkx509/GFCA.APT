using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using GFCA.APT.Domain.Dto;
using GFCA.APT.DAL.Interfaces;

namespace GFCA.APT.DAL.Implements
{
    public class ChannelRepository : RepositoryBase, IChannelRepository
    {

        public ChannelRepository(IDbTransaction transaction): base(transaction) { }

        public ChannelDto GetByCode(string code)
        {
            string sqlQuery = @"SELECT * FROM TB_M_CHANNEL WHERE CHANNEL_CODE = @CHANNEL_CODE;";
            var query = Connection.Query<ChannelDto>(
                sql: sqlQuery,
                param: new { CHANNEL_CODE = code }
                ,transaction: Transaction
                ).FirstOrDefault();
       
            return query;
        }
        public IEnumerable<ChannelDto> All()
        {
            string sqlQuery = @"SELECT * FROM TB_M_CHANNEL";
            var query = Connection.Query<ChannelDto>(
                sql: sqlQuery
                ,transaction: Transaction
                ).ToList();

            return query;
        }

        public void Insert(ChannelDto entity)
        {
            string sqlExecute = @"INSERT INTO TB_M_CHANNEL
                                (
                                  CHANNEL_CODE
                                , CHANNEL_NAME
                                , CHANNEL_DESC
                                , FLAG_ROW
                                , CREATED_BY
                                , CREATED_DATE
                                ) VALUES (
                                  @CHANNEL_CODE
                                , @CHANNEL_NAME
                                , @CHANNEL_DESC
                                , @FLAG_ROW
                                , @CREATED_BY
                                , @CREATED_DATE
                                ); SELECT SCOPE_IDENTITY()
                                ";

            var parms = new
            {
                CHANNEL_CODE = entity.CHANNEL_CODE,
                CHANNEL_NAME = entity.CHANNEL_NAME,
                CHANNEL_DESC = entity.CHANNEL_DESC,
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
        public void Update(ChannelDto entity)
        {
            string sqlExecute = @"UPDATE TB_M_CHANNEL
                                SET
                                  CHANNEL_CODE = @CHANNEL_CODE
                                , CHANNEL_NAME = @CHANNEL_NAME
                                , CHANNEL_DESC = @CHANNEL_DESC
                                , FLAG_ROW     = @FLAG_ROW
                                , UPDATED_BY   = @UPDATED_BY
                                , UPDATED_DATE = @UPDATED_DATE
                                WHERE
                                CHANNEL_CODE = @CHANNEL_CODE;
                                ";

            var parms = new
            {
                CAHNNEL_CODE = entity.CHANNEL_CODE,
                CHANNEL_NAME = entity.CHANNEL_NAME,
                CHANNEL_DESC = entity.CHANNEL_DESC,
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
            string sqlExecute = @"DELETE TB_M_CHANNEL WHERE CHANNEL_CODE = @CHANNEL_CODE;";
            var parms = new { CHANNEL_CODE = code };

            Connection.ExecuteScalar<int>(
                sql: sqlExecute,
                param: parms,
                transaction: Transaction
            );

        }

    }

}
