using GFCA.APT.Domain.Dto;
using GFCA.APT.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFCA.APT.BAL.Interfaces
{
    public interface IUnitService
    {
        IEnumerable<UnitDto> GetAll();
        UnitDto GetById(int Id);
        BusinessResponse Create(UnitDto model);
        BusinessResponse Edit(UnitDto model);
        BusinessResponse Remove(UnitDto model);
    }
}
