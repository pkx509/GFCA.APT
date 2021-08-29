using GFCA.APT.Domain.Dto;
using GFCA.APT.Domain.Models;
using System.Collections.Generic;
namespace GFCA.APT.BAL.Interfaces
{
public interface ITB_M_CHANNELService
{
IEnumerable<TB_M_CHANNELDto> GetAll();
TB_M_CHANNELDto GetById(int Id);
BusinessResponse Create(TB_M_CHANNELDto model);
BusinessResponse Edit(TB_M_CHANNELDto model);
 BusinessResponse Remove(TB_M_CHANNELDto model);
}
}
