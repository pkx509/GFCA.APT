using GFCA.APT.Domain.Dto;
using GFCA.APT.Domain.Models;
using System.Collections.Generic;

namespace GFCA.APT.BAL.Interfaces
{
    public interface IPromotionGroupService
    {
        IEnumerable<PromotionGroupDto> GetAll();
        PromotionGroupDto GetById(int Id);
        BusinessResponse Create(PromotionGroupDto model);
        BusinessResponse Edit(PromotionGroupDto model);
        BusinessResponse Remove(PromotionGroupDto model);
    }
}
