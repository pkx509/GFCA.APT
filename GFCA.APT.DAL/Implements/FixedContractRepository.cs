using GFCA.APT.DAL.Interfaces;
using GFCA.APT.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFCA.APT.DAL.Implements
{
    public class FixedContractRepository : RepositoryBase, IFixedContractRepository
    {

        public FixedContractRepository(IDbTransaction transaction) : base(transaction) { }

        public IEnumerable<FixedContractDto> All()
        {
            throw new NotImplementedException();
        }

        public void Delete(string code)
        {
            throw new NotImplementedException();
        }

        public FixedContractDto GetByCode(string code)
        {
            throw new NotImplementedException();
        }

        public void Insert(FixedContractDto entity)
        {
            throw new NotImplementedException();
        }

        public void Update(FixedContractDto entity)
        {
            throw new NotImplementedException();
        }
    }
}
