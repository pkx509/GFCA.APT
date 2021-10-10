using GFCA.APT.Domain.Dto;
using GFCA.APT.Domain.Models;
using System.Collections.Generic;

namespace GFCA.APT.BAL.Interfaces
{
    public interface IFixedContractService
    {
        IEnumerable<FixedContractHeaderDto> GetHeaderAll();
        FixedContractHeaderDto GetHeaderById(int headerId);
        
        IEnumerable<FixedContractDetailDto> GetDetailByCode(string code, int ver = -1, int rev = -1);
        IEnumerable<FixedContractDetailDto> GetDetailAll();
        FixedContractDetailDto GetDetailById(int detailId);
        
        BusinessResponse CreateHeader(FixedContractHeaderDto model);
        BusinessResponse EditHeader(FixedContractHeaderDto model);
        
        BusinessResponse CreateDetail(FixedContractDto model);
        BusinessResponse EditDetail(FixedContractDto model);
        
        BusinessResponse RemoveDetail(FixedContractDetailDto model);
    }
}
