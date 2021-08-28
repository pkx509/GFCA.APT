using Dapper;
using GFCA.APT.DAL.Interfaces;
using GFCA.APT.Domain.Dto;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace GFCA.APT.DAL.Implements
{
    public class TradeActivityRepository : RepositoryBase, ITradeActivityRepository
    {
        public TradeActivityRepository(IDbTransaction transaction) : base(transaction) { }
        
        public IEnumerable<TradeActivityDto> All()
        {
            string sqlQuery = @"";
            var query = Connection.Query<TradeActivityDto>(
                sql: sqlQuery
                , transaction: Transaction
                ).ToList();

            return query;
        }

        public TradeActivityDto GetById(int id)
        {
            string sqlQuery = @"";
            var parms = new
            {
            };
            var query = Connection.Query<TradeActivityDto>(
                sql: sqlQuery,
                param: parms
                , transaction: Transaction
                ).FirstOrDefault();

            return query;

        }

        public void Insert(TradeActivityDto entity)
        {
            string sqlExecute = @"";
            var parms = new
            {
            };

            Connection.ExecuteScalar<int>(
                sql: sqlExecute,
                param: parms,
                transaction: Transaction
            );
        }

        public void Update(TradeActivityDto entity)
        {
            string sqlExecute = @"";
            var parms = new 
            {
            };

            Connection.ExecuteScalar<int>(
                sql: sqlExecute,
                param: parms,
                transaction: Transaction
            );
        }

        public void Delete(int id)
        {
            string sqlExecute =@"";
            var parms = new
            {
            };

            Connection.ExecuteScalar<int>(
                sql: sqlExecute,
                param: parms,
                transaction: Transaction
            );
        }

    }
}
