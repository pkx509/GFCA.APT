using GFCA.APT.Domain.Dto;
using GFCA.APT.Domain.Models;
using System.Collections.Generic;
namespace GFCA.APT.BAL.Interfaces
{
    public interface ITB_M_ACTIVITYService
    {
        IEnumerable<TB_M_ACTIVITYDto> GetAll();
        TB_M_ACTIVITYDto GetById(int Id);
        BusinessResponse Create(TB_M_ACTIVITYDto model);
        BusinessResponse Edit(TB_M_ACTIVITYDto model);
        BusinessResponse Remove(TB_M_ACTIVITYDto model);
    }
}
