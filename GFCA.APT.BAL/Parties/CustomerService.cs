using GFCA.APT.BAL.Log;
using GFCA.APT.DAL.Interfaces;

namespace GFCA.APT.BAL.Parties
{
    public class CustomerService : BusinessBase
    {
        public CustomerService(IUnitOfWork unitOfWork, ILogService logger)
            : base(unitOfWork, logger) { }

        
    }
}
