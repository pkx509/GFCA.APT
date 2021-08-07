using GFCA.APT.BAL.Log;
using GFCA.APT.DAL;
using GFCA.APT.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFCA.APT.BAL.Parties
{
    public class ChannelService : BusinessBase
    {
        public ChannelService(IUnitOfWork unitOfWork, ILogService logger)
            : base(unitOfWork, logger)
        {

        }

        public BusinessResponse Create(TB_M_CHANNEL model)
        {
            throw new NotImplementedException();
        }

        public BusinessResponse Delete(TB_M_CHANNEL model)
        {
            throw new NotImplementedException();
        }

        public BusinessResponse Edit(TB_M_CHANNEL model)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TB_M_CHANNEL> GetAll()
        {
            throw new NotImplementedException();
        }

        public TB_M_CHANNEL GetById(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
