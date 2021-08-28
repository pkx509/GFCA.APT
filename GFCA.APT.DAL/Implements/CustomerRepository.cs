using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using GFCA.APT.Domain.Dto;
using GFCA.APT.DAL.Interfaces;

namespace GFCA.APT.DAL.Implements
{
    public class CustomerRepository : RepositoryBase, ICustomerRepository
    {
        
        public CustomerRepository(IDbTransaction transaction): base(transaction) { }

     

       public   IEnumerable<TB_M_CUSTOMERDto> All()
        {
             
                string sqlQuery = @"SELECT  *    FROM TB_M_CUSTOMER;";
                var query = Connection.Query<TB_M_CUSTOMERDto>(
                    sql: sqlQuery
                    , transaction: Transaction
                    ).ToList();

                return query;
            }


        TB_M_CUSTOMERDto IRepositories<TB_M_CUSTOMERDto>.GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Insert(TB_M_CUSTOMERDto entity)
        {
            throw new System.NotImplementedException();
        }

        public void Update(TB_M_CUSTOMERDto entity)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }
    }

}
