using GFCA.APT.Domain.Dto;
using GFCA.APT.Domain.Models;
using System.Collections.Generic;
namespace GFCA.APT.BAL.Interfaces
{
public interface ITB_M_PROMOTION_GROUPService
{
IEnumerable<TB_M_PROMOTION_GROUPDto> GetAll();
TB_M_PROMOTION_GROUPDto GetById(int Id);
BusinessResponse Create(TB_M_PROMOTION_GROUPDto model);
BusinessResponse Edit(TB_M_PROMOTION_GROUPDto model);
 BusinessResponse Remove(TB_M_PROMOTION_GROUPDto model);
}
}
