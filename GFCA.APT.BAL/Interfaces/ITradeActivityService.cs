﻿using GFCA.APT.Domain.Dto;
using GFCA.APT.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFCA.APT.BAL.Interfaces
{
    public interface ITradeActivityService
    {
        IEnumerable<TradeActivityDto> GetAll();
        TradeActivityDto GetById(int Id);
        BusinessResponse Create(TradeActivityDto model);
        BusinessResponse Edit(TradeActivityDto model);
        BusinessResponse Remove(TradeActivityDto model);
    }
}
