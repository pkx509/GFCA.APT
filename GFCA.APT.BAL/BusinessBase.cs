using GFCA.APT.BAL.Log;
using GFCA.APT.DAL.Implements;
using GFCA.APT.DAL.Interfaces;
using GFCA.APT.Domain.Dto;
using GFCA.APT.Domain.Models;
using System;

namespace GFCA.APT.BAL
{
    public abstract class BusinessBase : IDisposable
    {
        protected readonly IUnitOfWork _uow;
        protected readonly ILogService _logger;
        protected UserInfoDto _currentUser;
        protected BusinessResponse _response;


        protected BusinessBase(IUnitOfWork unitOfWork, ILogService logger)
            :this(unitOfWork, new UserInfoDto(), logger)
        {

        }
        protected BusinessBase(IUnitOfWork unitOfWork, UserInfoDto currentUser, ILogService logger)
        {
            //_uow = unitOfWork ?? UnitOfWork.Create();
            _uow = unitOfWork;
            _currentUser = currentUser;
            _logger = logger;
        }

        public void Dispose()
        {
            _uow.Dispose();
        }

    }
}
