using GFCA.APT.Domain.Dto;
using GFCA.APT.Domain.Models;
using System.Collections.Generic;

namespace GFCA.APT.BAL.Interfaces
{
    public interface IEmissionService
    {
        IEnumerable<EmissionDto> GetAll();
        EmissionDto GetById(int Id);
        BusinessResponse Create(EmissionDto model);
        BusinessResponse Edit(EmissionDto model);
        BusinessResponse Delete(EmissionDto model);
    }
}
