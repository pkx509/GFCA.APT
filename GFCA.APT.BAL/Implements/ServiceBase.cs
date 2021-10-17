using GFCA.APT.DAL.Interfaces;
using GFCA.APT.Domain.Dto;
using GFCA.APT.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFCA.APT.BAL.Implements
{
    public abstract class ServiceBase : IDisposable
    {
        //protected readonly ILog _logger;
        protected IUnitOfWork _uow;
        protected UserInfoDto _currentUser;
        //protected BusinessResponse _response;

        protected ServiceBase(IUnitOfWork unitOfWork)
            : this(unitOfWork, new UserInfoDto())
        {

        }
        protected ServiceBase(IUnitOfWork unitOfWork, UserInfoDto currentUser)
        {
            //_uow = unitOfWork ?? UnitOfWork.Create();
            _uow = unitOfWork;
            _currentUser = currentUser;
            //_logger = logger;
        }
        /*
        #region [ Debug ]
        public void Debug(string message)
        {
            _logger.Debug(message);
        }
        public void Debug(string message, Exception exception)
        {
            _logger.Debug(message, exception);
        }
        #endregion [ Debug ]
        #region [ Error ]
        public void Error(string message)
        {
            _logger.Error(message);
        }
        public void Error(string message, Exception exception)
        {
            _logger.Error(message, exception);
        }
        #endregion [ Error ]
        #region [ Fatal ]
        public void Fatal(string message)
        {
            _logger.Fatal(message);
        }

        public void Fatal(string message, Exception exception)
        {
            _logger.Fatal(message, exception);
        }
        #endregion [ Fatal ]
        #region [ Error ]
        public void Info(string message)
        {
            _logger.Info(message);
        }

        public void Info(string message, Exception exception)
        {
            _logger.Info(message, exception);
        }
        #endregion [ Error ]
        #region [ Warn ]
        public void Warn(string message)
        {
            _logger.Warn(message);
        }
        public void Warn(string message, Exception exception)
        {
            _logger.Warn(message, exception);
        }
        #endregion [ Warn ]
        */

        protected void GenerateNextState(DOCUMENT_STATUS documentStatus, COMMAND_TYPE documentTypeAction, ref DocumentDto document)
        {
            
            if (documentTypeAction == COMMAND_TYPE.CONFIRM)
            {
                //Version + 1
                document.DOC_VER += 1;

                if (document.DOC_STATUS == DOCUMENT_STATUS.DRAFT)
                {
                    //document.FLOW_NEXT = "";
                }

            }

            if (documentTypeAction == COMMAND_TYPE.SUBMIT || 
                documentTypeAction == COMMAND_TYPE.APPROVE || 
                documentTypeAction == COMMAND_TYPE.REVIEW)
            {
                //Revision + 1
                document.DOC_REV += 1;
            }
        }

        public void Dispose()
        {
            _uow.Dispose();
        }

    }
}
