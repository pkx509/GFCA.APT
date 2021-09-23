using GFCA.APT.Domain.Dto;
using GFCA.APT.Domain.Models;
using System.Collections.Generic;

namespace GFCA.APT.BAL.Interfaces
{
    public interface IDocumentTypeService
    {
        IEnumerable<DocumentTypeDto> GetAll();
        DocumentTypeDto GetByCode(string code);
        BusinessResponse Create(DocumentTypeDto model);
        BusinessResponse Edit(DocumentTypeDto model);
        BusinessResponse Remove(DocumentTypeDto model);
    }
}
