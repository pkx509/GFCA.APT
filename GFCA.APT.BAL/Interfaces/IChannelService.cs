using GFCA.APT.Domain.Dto;
using GFCA.APT.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFCA.APT.BAL.Interfaces
{
    public interface IChannelService
    {
        IEnumerable<ChannelDto> GetAll();
        ChannelDto GetByCode(string code);
        BusinessResponse Create(ChannelDto model);
        BusinessResponse Edit(ChannelDto model);
        BusinessResponse Remove(ChannelDto model);
    }
}
