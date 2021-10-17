using GFCA.APT.Domain.Dto;

namespace GFCA.APT.DAL.Interfaces
{
    public interface IDocumentRepository : IRepositories<DocumentDto>
    {
        DocumentDto GenerateDocNo(string docTypeCode, string docCode);
        DocumentDto GenerateDocNo(string docTypeCode, string docYear, string docMonth, string clientCode, string channelCode, string custCode);
        bool ValidateFixedContract(string docTypeCode, string docYear, string docMonth);
    }
}
