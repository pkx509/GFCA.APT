using GFCA.APT.Domain.Dto;
using System.Collections.Generic;

namespace GFCA.APT.BAL.Interfaces
{
    public interface IDocumentService
    {
        //Section Document State
        DocumentStateDto GetDocumentStateSection(string documentType, int documentHeaderId, int version = -1, int revision = -1);
        DocumentWorkFlowDto GetDocumentWorkFlowSection(string documentType, int documentHeaderId, int version = -1, int revision = -1);
        DocumentRequesterDto GetDocumentRequesterSection(string documentType, int documentHeaderId, int version = -1, int revision = -1);
        IEnumerable<DocumentHistoryDto> GetDocumentHistorySection(string documentType, int documentHeaderId);
        //Section Document History
    }
}
