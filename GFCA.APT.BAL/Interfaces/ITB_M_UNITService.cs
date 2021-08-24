using GFCA.APT.Domain.Dto;
using GFCA.APT.Domain.Models;
using System.Collections.Generic;
namespace GFCA.APT.BAL.Interfaces
{
public interface ITB_M_UNITService
{
IEnumerable<TB_M_UNITDto> GetAll();
TB_M_UNITDto GetById(int Id);
BusinessResponse Create(TB_M_UNITDto model);
BusinessResponse Edit(TB_M_UNITDto model);
 BusinessResponse Remove(TB_M_UNITDto model);
}
}
