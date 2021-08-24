using GFCA.APT.Domain.Dto;
using GFCA.APT.Domain.Models;
using System.Collections.Generic;
namespace GFCA.APT.BAL.Interfaces
{
public interface ITB_M_CLIENTService
{
IEnumerable<TB_M_CLIENTDto> GetAll();
TB_M_CLIENTDto GetById(int Id);
BusinessResponse Create(TB_M_CLIENTDto model);
BusinessResponse Edit(TB_M_CLIENTDto model);
 BusinessResponse Remove(TB_M_CLIENTDto model);
}
}
