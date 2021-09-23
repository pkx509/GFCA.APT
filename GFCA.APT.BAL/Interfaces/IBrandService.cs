using GFCA.APT.Domain.Dto;
using GFCA.APT.Domain.Models;
using System.Collections.Generic;

namespace GFCA.APT.BAL.Interfaces
{
    public interface IBrandService
    {
        IEnumerable<BrandDto> GetAll();
        BrandDto GetByCode(string code);
        BusinessResponse Create(BrandDto model);
        BusinessResponse Edit(BrandDto model);
        BusinessResponse Remove(BrandDto model);
    }
}
