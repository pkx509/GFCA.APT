using GFCA.APT.Domain.Dto;
using GFCA.APT.Domain.Models;
using System.Collections.Generic;
namespace GFCA.APT.BAL.Interfaces
{
public interface ITB_M_DOCUMENT_TYPEService
{
IEnumerable<TB_M_DOCUMENT_TYPEDto> GetAll();
TB_M_DOCUMENT_TYPEDto GetById(int Id);
BusinessResponse Create(TB_M_DOCUMENT_TYPEDto model);
BusinessResponse Edit(TB_M_DOCUMENT_TYPEDto model);
 BusinessResponse Remove(TB_M_DOCUMENT_TYPEDto model);
}
}
