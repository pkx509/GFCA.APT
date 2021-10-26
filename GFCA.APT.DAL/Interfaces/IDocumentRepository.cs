using GFCA.APT.Domain.Dto;
using System.Collections.Generic;

namespace GFCA.APT.DAL.Interfaces
{
    public interface IDocumentRepository : IRepositories<DocumentDto>
    {
        DocumentStateFlowDto GetDocumentStateFlow(int headerId, string documentType);
        IEnumerable<DocumentHistoryDto> GetDocumentHistories(int headerId);
        DocumentDto GenerateDocNo(string docTypeCode, string docCode);
        DocumentDto GenerateDocNo(string docTypeCode, int docYear, int docMonth, string clientCode, string channelCode, string custCode);
        bool ValidateFixedContract(string docTypeCode, string docYear, string docMonth);
    }
}
