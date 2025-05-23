﻿using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using GFCA.APT.Domain.Dto;
using GFCA.APT.DAL.Interfaces;

namespace GFCA.APT.DAL.Implements
{
    public class CostCenterRepository : RepositoryBase, ICostCenterRepository
    {
        
        public CostCenterRepository(IDbTransaction transaction): base(transaction) { }

        

 


        public IEnumerable<CostCenterDto> All()
        {
            string sqlQuery = "SELECT  *   FROM [dbo].[TB_M_COST_CENTER];";
            var query = Connection.Query<CostCenterDto>(
                sql: sqlQuery
                , transaction: Transaction
                ).ToList();

            return query;
        }

        public CostCenterDto GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Insert(CostCenterDto entity)
        {
            throw new System.NotImplementedException();
        }

        public void Update(CostCenterDto entity)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }
    }

}
