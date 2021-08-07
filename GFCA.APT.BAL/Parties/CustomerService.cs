using GFCA.APT.BAL.Log;
using GFCA.APT.DAL;
using GFCA.APT.Domain.Dto;
using GFCA.APT.Domain.Models;
using log4net;
using System;
using System.Collections.Generic;

namespace GFCA.APT.BAL.Parties
{
    public class CustomerService : BusinessBase
    {
        public CustomerService(IUnitOfWork unitOfWork, ILogService logger)
            : base(unitOfWork, logger) { }

        
    }
}
