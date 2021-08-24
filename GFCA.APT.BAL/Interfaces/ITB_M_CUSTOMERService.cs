using GFCA.APT.Domain.Dto;
using GFCA.APT.Domain.Models;
using System.Collections.Generic;
namespace GFCA.APT.BAL.Interfaces
{
public interface ITB_M_CUSTOMERService
{
IEnumerable<TB_M_CUSTOMERDto> GetAll();
TB_M_CUSTOMERDto GetById(int Id);
BusinessResponse Create(TB_M_CUSTOMERDto model);
BusinessResponse Edit(TB_M_CUSTOMERDto model);
 BusinessResponse Remove(TB_M_CUSTOMERDto model);
}
}
