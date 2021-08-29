using GFCA.APT.Domain.Dto;
using GFCA.APT.Domain.Models;
using System.Collections.Generic;
namespace GFCA.APT.BAL.Interfaces
{
public interface ITB_M_DISTRIBUTORService
{
IEnumerable<TB_M_DISTRIBUTORDto> GetAll();
TB_M_DISTRIBUTORDto GetById(int Id);
BusinessResponse Create(TB_M_DISTRIBUTORDto model);
BusinessResponse Edit(TB_M_DISTRIBUTORDto model);
 BusinessResponse Remove(TB_M_DISTRIBUTORDto model);
}
}
