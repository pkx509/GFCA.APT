using GFCA.APT.Domain.Dto;

namespace GFCA.APT.DAL.Interfaces
{
    public interface IFixedContractRepository //: IRepositories<FixedContractDto>
    {
        int GenerateVersionNo(string documentType, string year, string month);
        int GenerateRevisionNo(string documentType, string year, string month);

        void InsertHeader(FixedContractHeaderDto entity);
        void InsertDetail(FixedContractDetailDto entity);
    }
}
