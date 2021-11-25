using GFCA.APT.Domain.Dto;
using GFCA.APT.Domain.Models;
using System.Collections.Generic;

namespace GFCA.APT.BAL.Interfaces
{
    public interface IFixedContractService : IDocumentService
    {
        IEnumerable<FixedContractHeaderDto> GetHeaderAll();
        FixedContractHeaderDto GetHeaderById(int DOC_FCH_ID);
        FixedContractHeaderDto GetHeaderByCode(string code, int ver = -1, int rev = -1);
        BusinessResponse CreateHeader(FixedContractHeaderDto model);
        BusinessResponse EditHeader(FixedContractHeaderDto model);
        BusinessResponse RemoveHeader(FixedContractHeaderDto model);

        //IEnumerable<FixedContractDetailDto> GetDetailItems(string code, int ver = -1, int rev = -1);
        IEnumerable<FixedContractDetailDto> GetDetailItems(int DOC_FCH_ID);
        FixedContractDetailDto GetDetailItem(int DOC_FCD_ID);
        BusinessResponse CreateDetail(FixedContractDetailDto model);
        BusinessResponse EditDetail(FixedContractDetailDto model);
        BusinessResponse RemoveDetail(FixedContractDetailDto model);
    }
}
