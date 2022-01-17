using GFCA.APT.BAL.Interfaces;
using GFCA.APT.DAL.Interfaces;
using GFCA.APT.Domain.Dto;
using GFCA.APT.Domain.Dto.Workflow;
using GFCA.APT.Domain.Enums;
using GFCA.APT.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public DocumentStateDto GetDocumentStateSection(string documentType, int documentHeaderId)
        {
            try
            {
                var docStateFlow = _uow.DocumentRepository.GetDocumentStateFlow(documentType, documentHeaderId);
                if (docStateFlow == null)
                    return new DocumentStateDto();

                return docStateFlow;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DocumentWorkFlowDto GetDocumentWorkFlowSection(string documentType, int documentHeaderId, int version = -1, int revision = -1)
        {
            try
            {
                return new DocumentWorkFlowDto();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DocumentRequesterDto GetDocumentRequesterSection(string documentType, int documentHeaderId, int version = -1, int revision = -1)
        {
            try
            {

                return new DocumentRequesterDto();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<DocumentHistoryDto> GetDocumentHistorySection(string documentType, int documentHeaderId)
        {
            try
            {
                var docHistories =  _uow.DocumentRepository.GetDocumentHistories(documentType, documentHeaderId);
                if (docHistories == null)
                    return new List<DocumentHistoryDto>();

                return docHistories;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region [ Workflow ]
        public IEnumerable<Domain.Dto.Workflow.CommandDto> GetDocumentCommands(string documentType, int documentStatusId = 0)
        {
            IEnumerable<Domain.Dto.Workflow.CommandDto> result = new List<Domain.Dto.Workflow.CommandDto>();
            try
            {
                result = _uow.WorkflowRepository.GetCommands(documentType, documentStatusId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public BusinessResponse PostDocument(string documentType, int documentHeaderId, CommandDto command)
        {
            BusinessResponse result = BusinessResponse.CreateInstance(MESSAGE_TYPE.WARNING);
            try
            {

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public async Task<BusinessResponse> PostDocumentAsync(string documentType, int documentHeaderId, CommandDto command)
        {
            BusinessResponse result = BusinessResponse.CreateInstance(MESSAGE_TYPE.WARNING);
            try
            {

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        #endregion [ Workflow ]


    }
}
