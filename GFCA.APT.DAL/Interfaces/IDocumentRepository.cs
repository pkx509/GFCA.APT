using GFCA.APT.Domain.Dto;
using System;
using System.Collections.Generic;

namespace GFCA.APT.DAL.Interfaces
{
    public interface IDocumentRepository : IRepositories<DocumentDto>
    {
        DocumentStateDto GetDocumentStateFlow(string documentType, int headerId);
        IEnumerable<DocumentHistoryDto> GetDocumentHistories(int headerId);
        DocumentDto GenerateDocNo(string docTypeCode, string custCode, int docYear = 0, int docMonth = 0);
        bool ValidateFixedContract(string docTypeCode, string docYear, string docMonth);
    }
}
