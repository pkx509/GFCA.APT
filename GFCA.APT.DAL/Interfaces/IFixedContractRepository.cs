using GFCA.APT.Domain.Dto;
using System.Collections.Generic;

namespace GFCA.APT.DAL.Interfaces
{
    public interface IFixedContractRepository
    {
        IEnumerable<FixedContractDetailDto> GetDetailAll();
        IEnumerable<FixedContractHeaderDto> GetHeaderAll();
        FixedContractDto GetDetailItem(string docCode, int docVer = -1, int docRev = -1);
        
        void InsertHeader(FixedContractHeaderDto entity);
        void InsertDetail(FixedContractDetailDto entity);

        void UpdateHeader(FixedContractHeaderDto entity);
        void UpdateDetail(FixedContractDetailDto entity);

    }
}
