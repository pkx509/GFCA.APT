using GFCA.APT.Domain.Dto;
using GFCA.APT.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFCA.APT.BAL.Interfaces
{
    public interface IOrganizationService
    {
        IEnumerable<OrganizationDto> GetAll();
        OrganizationDto GetById(int Id);
        BusinessResponse Create(OrganizationDto model);
        BusinessResponse Edit(OrganizationDto model);
        BusinessResponse Remove(OrganizationDto model);
    }
}
