using Dapper;
using GFCA.APT.DAL.Interfaces;
using GFCA.APT.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace GFCA.APT.DAL.Implements
{
    public class EmissionRepository : RepositoryBase, IEmissionRepository
    {
        public EmissionRepository(IDbTransaction transaction) : base(transaction) { }

        public void Add(EmissionDto entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EmissionDto> All()
        {
            string sqlQuery = "SELECT * FROM TB_M_EMISSION;";
            var query = Connection.Query<EmissionDto>(
                sql: sqlQuery,
                transaction: Transaction
                ).ToList();

            return query;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public EmissionDto GetById(int id)
        {
            string sqlQuery = "SELECT * FROM TB_M_EMISSION WHERE BRAND_ID = @EMIS_ID;";
            var query = Connection.Query<EmissionDto>(
                sql: sqlQuery,
                param: new { EMIS_ID = id },
                transaction: Transaction
                ).FirstOrDefault();

            return query;
        }

        public void Update(EmissionDto entity)
        {
            throw new NotImplementedException();
        }
    }
}
