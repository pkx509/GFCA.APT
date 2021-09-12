using GFCA.APT.Domain.Dto;
using GFCA.APT.Domain.Models;
using System.Collections.Generic;

namespace GFCA.APT.BAL.Interfaces
{
    public interface ISizeService
    {
        IEnumerable<SizeDto> GetAll();
        SizeDto GetById(int Id);
        BusinessResponse Create(SizeDto model);
        BusinessResponse Edit(SizeDto model);
        BusinessResponse Remove(SizeDto model);
    }
}
