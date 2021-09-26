using GFCA.APT.Domain.Dto;
using GFCA.APT.Domain.Models;
using System.Collections.Generic;

namespace GFCA.APT.BAL.Interfaces
{
    public interface IFixedContractService
    {
        IEnumerable<FixedContractDto> GetHeaderAll();
        FixedContractDto GetHeaderById(int Id);
        IEnumerable<FixedContractDto> GetDetailByHeaderId(int Id);
        BusinessResponse Create(FixedContractDto model);
        BusinessResponse Edit(FixedContractDto model);
        BusinessResponse Remove(FixedContractDto model);
    }
}
