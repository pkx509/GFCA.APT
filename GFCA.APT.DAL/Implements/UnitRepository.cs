using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using GFCA.APT.Domain.Dto;
using GFCA.APT.DAL.Interfaces;

namespace GFCA.APT.DAL.Implements
{
    public class UnitRepository : RepositoryBase, IUnitRepository
    {
        
        public UnitRepository(IDbTransaction transaction): base(transaction) { }

    

        public IRepositories<TB_M_UNITDto> All()
        {
            return null;

        }

        TB_M_UNITDto IRepositories<TB_M_UNITDto>.GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Insert(TB_M_UNITDto entity)
        {
            throw new System.NotImplementedException();
        }

        public void Update(TB_M_UNITDto entity)
        {
            throw new System.NotImplementedException();
        }

        IEnumerable<TB_M_UNITDto> IRepositories<TB_M_UNITDto>.All()
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }
    }

}
