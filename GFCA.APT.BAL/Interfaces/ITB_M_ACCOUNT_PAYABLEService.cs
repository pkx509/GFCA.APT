using GFCA.APT.Domain.Dto;
using GFCA.APT.Domain.Models;
using System.Collections.Generic;
namespace GFCA.APT.BAL.Interfaces
{
    public interface ITB_M_ACCOUNT_PAYABLEService
    {
        IEnumerable<TB_M_ACCOUNT_PAYABLEDto> GetAll();
        TB_M_ACCOUNT_PAYABLEDto GetById(int Id);
        BusinessResponse Create(TB_M_ACCOUNT_PAYABLEDto model);
        BusinessResponse Edit(TB_M_ACCOUNT_PAYABLEDto model);
        BusinessResponse Remove(TB_M_ACCOUNT_PAYABLEDto model);
    }
}
