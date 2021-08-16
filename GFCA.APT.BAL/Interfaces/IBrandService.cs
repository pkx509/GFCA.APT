using GFCA.APT.Domain.Dto;
using GFCA.APT.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFCA.APT.BAL.Interfaces
{
    public interface IBrandService
    {
        IEnumerable<BrandDto> GetAll();
        BrandDto GetById(int Id);
        BusinessResponse Create(BrandDto model);
        BusinessResponse Edit(BrandDto model);
        BusinessResponse Delete(BrandDto model);
    }
}
