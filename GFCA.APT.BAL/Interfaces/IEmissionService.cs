using GFCA.APT.Domain.Dto;
using GFCA.APT.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFCA.APT.BAL.Interfaces
{
    public interface IEmissionService
    {
        IEnumerable<EmissionDto> GetAll();
        EmissionDto GetByCode(string code);
        BusinessResponse Create(EmissionDto model);
        BusinessResponse Edit(EmissionDto model);
        BusinessResponse Remove(EmissionDto model);
    }
}
