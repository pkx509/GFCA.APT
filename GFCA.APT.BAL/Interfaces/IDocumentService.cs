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
        IEnumerable<Domain.Dto.Workflow.CommandDto> GetDocumentCommands(string documentType, int documentStatusId = 0);
        Domain.Dto.Workflow.CommandDto PostDocument(string documentType, int documentHeaderId, Domain.Dto.Workflow.CommandDto command);
        //Section Document History
    }
}
