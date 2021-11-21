using GFCA.APT.BAL.Interfaces;
using GFCA.APT.DAL.Interfaces;
using GFCA.APT.Domain.Dto;
using GFCA.APT.Domain.Enums;
using System;
using System.Collections.Generic;

namespace GFCA.APT.BAL.Implements
{
    public abstract class ServiceBase : IDocumentService, IDisposable
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

        public void GenerateNextState(DOCUMENT_STATUS documentStatus, COMMAND_TYPE documentTypeAction, ref DocumentDto document)
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

        
        public DocumentWorkFlowDto GetDocumentFlowSection(string documentType, int documentHeaderId, int version = -1, int revision = -1)
        {
            return new DocumentWorkFlowDto();
        }

        public DocumentRequesterDto GetDocumentRequesterSection(string documentType, int documentHeaderId, int version = -1, int revision = -1)
        {
            return new DocumentRequesterDto();
        }

        public IEnumerable<DocumentHistoryDto> GetDocumentHistorySection(string documentType, int documentHeaderId)
        {
            return new List<DocumentHistoryDto>();
        }

    }
}
