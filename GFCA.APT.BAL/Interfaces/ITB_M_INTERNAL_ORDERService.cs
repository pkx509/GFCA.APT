using GFCA.APT.Domain.Dto;
using GFCA.APT.Domain.Models;
using System.Collections.Generic;
namespace GFCA.APT.BAL.Interfaces
{
public interface ITB_M_INTERNAL_ORDERService
{
IEnumerable<TB_M_INTERNAL_ORDERDto> GetAll();
TB_M_INTERNAL_ORDERDto GetById(int Id);
BusinessResponse Create(TB_M_INTERNAL_ORDERDto model);
BusinessResponse Edit(TB_M_INTERNAL_ORDERDto model);
 BusinessResponse Remove(TB_M_INTERNAL_ORDERDto model);
}
}
