using GFCA.APT.Domain.Dto;
using GFCA.APT.Domain.Models;
using System.Collections.Generic;
namespace GFCA.APT.BAL.Interfaces
{
public interface ITB_M_BRANDService
{
IEnumerable<TB_M_BRANDDto> GetAll();
TB_M_BRANDDto GetById(int Id);
BusinessResponse Create(TB_M_BRANDDto model);
BusinessResponse Edit(TB_M_BRANDDto model);
 BusinessResponse Remove(TB_M_BRANDDto model);
}
}
