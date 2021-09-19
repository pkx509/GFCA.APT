using GFCA.APT.BAL.Interfaces;
using GFCA.APT.DAL.Implements;
using GFCA.APT.DAL.Interfaces;
using GFCA.APT.Domain.Dto;
using GFCA.APT.Domain.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GFCA.APT.BAL.Implements
{
    public class FixedContractService: ServiceBase, IFixedContractService
    {
        private readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        internal static FixedContractService CreateInstant()
        {
            var uow = UnitOfWork.CreateInstant();
            var svc = new FixedContractService(uow);

            return svc;
        }

        public FixedContractService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public EmployeeDto GetHeaderById(int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EmployeeDto> GetDeatilByHeaderId(int Id)
        {
            throw new NotImplementedException();
        }

        public BusinessResponse Create(EmployeeDto model)
        {
            BusinessResponse response = null;
            try
            {

                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return response;
        }

        public BusinessResponse Edit(EmployeeDto model)
        {
            try
            {

                throw new NotImplementedException();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IEnumerable<EmployeeDto> GetHeaderAll()
        {
            try
            {

                throw new NotImplementedException();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public BusinessResponse Remove(EmployeeDto model)
        {
            try
            {

                throw new NotImplementedException();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
