using GFCA.APT.Domain.Dto;
using GFCA.APT.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
