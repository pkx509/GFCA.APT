using GFCA.APT.BAL.Log;
using GFCA.APT.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFCA.APT.BAL.Parties
{
    class ChannelService : BusinessBase, IDisposable
    {
        public ChannelService(IUnitOfWork unitOfWork, ILogService logger)
            : base(unitOfWork, logger)
        {

        }
    }
}
