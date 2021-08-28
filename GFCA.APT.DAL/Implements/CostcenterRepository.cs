using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using GFCA.APT.Domain.Dto;
using GFCA.APT.DAL.Interfaces;

namespace GFCA.APT.DAL.Implements
{
    public class CostcenterRepository : RepositoryBase, ICostcenterRepository
    {
        
        public CostcenterRepository(IDbTransaction transaction): base(transaction) { }

        

 


        public  IEnumerable<TB_M_COST_CENTERDto> All()
        {
            string sqlQuery = "SELECT  *   FROM [dbo].[TB_M_COST_CENTER];";
            var query = Connection.Query<TB_M_COST_CENTERDto>(
                sql: sqlQuery
                , transaction: Transaction
                ).ToList();

            return query;
        }

        TB_M_COST_CENTERDto IRepositories<TB_M_COST_CENTERDto>.GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Insert(TB_M_COST_CENTERDto entity)
        {
            throw new System.NotImplementedException();
        }

        public void Update(TB_M_COST_CENTERDto entity)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }
    }

}
