using GFCA.APT.Domain.Dto;
using System.Collections.Generic;

namespace GFCA.APT.DAL.Interfaces
{
    public interface IFixedContractRepository
    {

        IEnumerable<FixedContractHeaderDto> GetHeaderAll();
        void InsertHeader(FixedContractHeaderDto entity);
        void UpdateHeader(FixedContractHeaderDto entity);

        IEnumerable<FixedContractDetailDto> GetDetailItems(string docCode, int docVer = -1, int docRev = -1);
        void InsertDetail(FixedContractDetailDto entity);
        void UpdateDetail(FixedContractDetailDto entity);

    }
}
