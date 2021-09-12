using GFCA.APT.BAL.Interfaces;
using GFCA.APT.DAL.Implements;
using GFCA.APT.DAL.Interfaces;
using GFCA.APT.Domain.Dto;
using GFCA.APT.Domain.Enums;
using GFCA.APT.Domain.HTTP.Controls;
using GFCA.APT.Domain.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GFCA.APT.BAL.Implements
{
    public class SizeService : ServiceBase, ISizeService
    {
        private readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        internal static SizeService CreateInstant()
        {
            var uow = UnitOfWork.CreateInstant();
            var svc = new SizeService(uow);

            return svc;
        }

        public SizeService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public IEnumerable<SizeDto> GetAll()
        {
            var dto = _uow.SizeRepository.All();
            return dto;
        }


 

        SizeDto ISizeService.GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public BusinessResponse Create(SizeDto model)
        {
            throw new NotImplementedException();
        }

        public BusinessResponse Edit(SizeDto model)
        {
            throw new NotImplementedException();
        }

        public BusinessResponse Remove(SizeDto model)
        {
            throw new NotImplementedException();
        }
    }
}
