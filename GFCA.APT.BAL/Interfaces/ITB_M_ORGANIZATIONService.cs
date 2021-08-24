using GFCA.APT.Domain.Dto;
using GFCA.APT.Domain.Models;
using System.Collections.Generic;
namespace GFCA.APT.BAL.Interfaces
{
public interface ITB_M_ORGANIZATIONService
{
IEnumerable<TB_M_ORGANIZATIONDto> GetAll();
TB_M_ORGANIZATIONDto GetById(int Id);
BusinessResponse Create(TB_M_ORGANIZATIONDto model);
BusinessResponse Edit(TB_M_ORGANIZATIONDto model);
 BusinessResponse Remove(TB_M_ORGANIZATIONDto model);
}
}
