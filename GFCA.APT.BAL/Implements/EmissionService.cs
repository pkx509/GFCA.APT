using GFCA.APT.BAL.Interfaces;
using GFCA.APT.DAL.Implements;
using GFCA.APT.DAL.Interfaces;
using GFCA.APT.Domain.Dto;
using GFCA.APT.Domain.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace GFCA.APT.BAL.Implements
{
    public class EmissionService : ServiceBase, IEmissionService
    {
        private readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        internal static EmissionService CreateInstant()
        {
            var uow = UnitOfWork.CreateInstant();
            var svc = new EmissionService(uow);

            return svc;
        }
        
        public EmissionService(IUnitOfWork uow): base(uow)
        {
            _uow = uow;
        }

        public BusinessResponse Create(EmissionDto model)
        {
            throw new NotImplementedException();
        }

        public BusinessResponse Delete(EmissionDto model)
        {
            throw new NotImplementedException();
        }

        public BusinessResponse Edit(EmissionDto model)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EmissionDto> GetAll()
        {
            try
            {
                _log.Debug("EmissionService.GetAll");
                var dto = _uow.EmissionRepository.All();
                return dto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EmissionDto GetByCode(string code)
        {
            try
            {
                var dto = _uow.EmissionRepository.GetById(Id);
                return dto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
