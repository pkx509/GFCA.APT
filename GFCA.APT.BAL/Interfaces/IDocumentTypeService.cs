using GFCA.APT.Domain.Dto;
using GFCA.APT.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFCA.APT.BAL.Interfaces
{
    public interface IDocumentTypeService
    {
        IEnumerable<DocumentTypeDto> GetAll();
        DocumentTypeDto GetById(int Id);
        BusinessResponse Create(DocumentTypeDto model);
        BusinessResponse Edit(DocumentTypeDto model);
        BusinessResponse Remove(DocumentTypeDto model);
    }
}
