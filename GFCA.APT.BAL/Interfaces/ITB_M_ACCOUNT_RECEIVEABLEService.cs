using GFCA.APT.Domain.Dto;
using GFCA.APT.Domain.Models;
using System.Collections.Generic;
namespace GFCA.APT.BAL.Interfaces
{
public interface ITB_M_ACCOUNT_RECEIVEABLEService
{
IEnumerable<TB_M_ACCOUNT_RECEIVEABLEDto> GetAll();
TB_M_ACCOUNT_RECEIVEABLEDto GetById(int Id);
BusinessResponse Create(TB_M_ACCOUNT_RECEIVEABLEDto model);
BusinessResponse Edit(TB_M_ACCOUNT_RECEIVEABLEDto model);
 BusinessResponse Remove(TB_M_ACCOUNT_RECEIVEABLEDto model);
}
}
