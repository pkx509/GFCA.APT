using GFCA.APT.Domain.Dto;
using GFCA.APT.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFCA.APT.BAL.Interfaces
{
    public interface IFixedContractService
    {
        IEnumerable<FixedContractDto> GetHeaderAll();
        FixedContractDto GetHeaderById(int Id);
        IEnumerable<FixedContractDto> GetDeatilByHeaderId(int Id);
        BusinessResponse Create(FixedContractDto model);
        BusinessResponse Edit(FixedContractDto model);
        BusinessResponse Remove(FixedContractDto model);
    }
}
