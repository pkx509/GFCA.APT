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

        public ClientRepository(IDbTransaction transaction) : base(transaction) { }


        public IEnumerable<TB_M_CLIENTDto> All()
        {
            string sqlQuery = @"SELECT *  FROM [dbo].[TB_M_CLIENT]";
            var query = Connection.Query<TB_M_CLIENTDto>(
                sql: sqlQuery
                , transaction: Transaction
                ).ToList();

            return query;
        }



        TB_M_CLIENTDto IRepositories<TB_M_CLIENTDto>.GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Insert(TB_M_CLIENTDto entity)
        {
            throw new System.NotImplementedException();
        }

        public void Update(TB_M_CLIENTDto entity)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }
    }

}
