using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using GFCA.APT.Domain.Dto;
using GFCA.APT.DAL.Interfaces;

namespace GFCA.APT.DAL.Implements
{
    public class SizeRepository : RepositoryBase, ISizeRepository
    {
        
        public SizeRepository(IDbTransaction transaction): base(transaction) { }

       

        public IEnumerable<SizeDto> All()
        {
            string sqlQuery = @"SELECT * FROM TB_M_SIZE;";
            var query = Connection.Query<SizeDto>(
                sql: sqlQuery
                , transaction: Transaction
                ).ToList();

            return query;
        }



        public SizeDto GetByCode(string code)
        {
            throw new System.NotImplementedException();
        }

        public void Insert(SizeDto entity)
        {
            throw new System.NotImplementedException();
        }

        public void Update(SizeDto entity)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(string code)
        {
            throw new System.NotImplementedException();
        }

    }

}
