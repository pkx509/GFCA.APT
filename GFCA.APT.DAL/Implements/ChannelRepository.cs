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

        public ChannelRepository(IDbTransaction transaction) : base(transaction) { }


       

        TB_M_CHANNELDto IRepositories<TB_M_CHANNELDto>.GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Insert(TB_M_CHANNELDto entity)
        {
            throw new System.NotImplementedException();
        }

        public void Update(TB_M_CHANNELDto entity)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<TB_M_CHANNELDto> All()
        {
            string sqlQuery = @"SELECT * FROM TB_M_CHANNEL;";
            var query = Connection.Query<TB_M_CHANNELDto>(
                sql: sqlQuery
                , transaction: Transaction
                ).ToList();

            return query;
        }
    }

}
