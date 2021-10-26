using GFCA.APT.Domain.Dto;
using GFCA.APT.Domain.Enums;
using System.Collections.Generic;

namespace GFCA.APT.DAL.Interfaces
{
    public interface IFixedContractRepository
    {

        IEnumerable<FixedContractHeaderDto> GetHeaderAll();
        FixedContractHeaderDto GetHeaderById(int DOC_FCD_ID);
        void InsertHeader(FixedContractHeaderDto entity);
        void UpdateHeader(FixedContractHeaderDto entity);

        FixedContractDetailDto GetDetailItem(int DOC_FCD_ID, CONDITION_TYPE conditionType = CONDITION_TYPE.PLANNING);
        IEnumerable<FixedContractDetailDto> GetDetailItems(string docCode, int docVer = -1, int docRev = -1);
        IEnumerable<FixedContractDetailDto> GetDetailItems(int DOC_FCH_ID, CONDITION_TYPE conditionType = CONDITION_TYPE.PLANNING);
        void InsertDetail(FixedContractDetailDto entity);
        void UpdateDetail(FixedContractDetailDto entity);

    }
}
