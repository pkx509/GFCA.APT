using GFCA.APT.Domain.Dto;
using GFCA.APT.Domain.Models;
using System.Collections.Generic;
namespace GFCA.APT.BAL.Interfaces
{
public interface ITB_M_BUDGET_TYPEService
{
IEnumerable<TB_M_BUDGET_TYPEDto> GetAll();
TB_M_BUDGET_TYPEDto GetById(int Id);
BusinessResponse Create(TB_M_BUDGET_TYPEDto model);
BusinessResponse Edit(TB_M_BUDGET_TYPEDto model);
 BusinessResponse Remove(TB_M_BUDGET_TYPEDto model);
}
}
