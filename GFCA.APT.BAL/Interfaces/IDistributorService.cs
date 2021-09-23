using GFCA.APT.Domain.Dto;
using GFCA.APT.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFCA.APT.BAL.Interfaces
{
    public interface IDistributorService
    {
        IEnumerable<DistributorDto> GetAll();
        DistributorDto GetByCode(string code);
        BusinessResponse Create(DistributorDto model);
        BusinessResponse Edit(DistributorDto model);
        BusinessResponse Remove(DistributorDto model);
    }
}
