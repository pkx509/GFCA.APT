using GFCA.APT.Domain.Dto;
using GFCA.APT.Domain.Models;
using System.Collections.Generic;

namespace GFCA.APT.BAL.Interfaces
{
    public interface IFixedContractService
    {
        IEnumerable<FixedContractHeaderDto> GetHeaderAll();
        FixedContractHeaderDto GetHeaderById(int id);
        FixedContractHeaderDto GetHeaderByCode(string code, int ver = -1, int rev = -1);
        BusinessResponse CreateHeader(FixedContractHeaderDto model);
        BusinessResponse EditHeader(FixedContractHeaderDto model);
        
        IEnumerable<FixedContractDetailDto> GetDetailItems(string code, int ver = -1, int rev = -1);
        BusinessResponse CreateDetail(FixedContractDetailDto model);
        BusinessResponse EditDetail(FixedContractDetailDto model);
        
        BusinessResponse RemoveDetail(FixedContractDetailDto model);
    }
}
