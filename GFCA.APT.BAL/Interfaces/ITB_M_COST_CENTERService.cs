using GFCA.APT.Domain.Dto;
using GFCA.APT.Domain.Models;
using System.Collections.Generic;
namespace GFCA.APT.BAL.Interfaces
{
    public interface ITB_M_COST_CENTERService
    {
        IEnumerable<TB_M_COST_CENTERDto> GetAll();
        TB_M_COST_CENTERDto GetById(int Id);
        BusinessResponse Create(TB_M_COST_CENTERDto model);
        BusinessResponse Edit(TB_M_COST_CENTERDto model);
        BusinessResponse Remove(TB_M_COST_CENTERDto model);
    }
}
