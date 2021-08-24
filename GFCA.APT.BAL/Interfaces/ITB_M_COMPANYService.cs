using GFCA.APT.Domain.Dto;
using GFCA.APT.Domain.Models;
using System.Collections.Generic;
namespace GFCA.APT.BAL.Interfaces
{
public interface ITB_M_COMPANYService
{
IEnumerable<TB_M_COMPANYDto> GetAll();
TB_M_COMPANYDto GetById(int Id);
BusinessResponse Create(TB_M_COMPANYDto model);
BusinessResponse Edit(TB_M_COMPANYDto model);
 BusinessResponse Remove(TB_M_COMPANYDto model);
}
}
