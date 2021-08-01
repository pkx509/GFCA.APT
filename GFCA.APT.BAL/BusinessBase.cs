using GFCA.APT.BAL.Log;
using GFCA.APT.DAL;
using GFCA.APT.Domain.DTO;
using GFCA.APT.Domain.Models;
using System;

namespace GFCA.APT.BAL
{
    public abstract class BusinessBase : IDisposable
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly ILogService _logger;
        protected UserProfile _currentUser;
        protected BusinessResponse _response;


        protected BusinessBase(IUnitOfWork unitOfWork, ILogService logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        protected BusinessBase(IUnitOfWork unitOfWork, UserProfile currentUser, ILogService logger)
        {
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
            _logger = logger;
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

    }
}
