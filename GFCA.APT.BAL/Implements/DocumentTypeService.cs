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
    public class DocumentTypeService : ServiceBase, IDocumentTypeService
    {
        private readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        internal static DocumentTypeService CreateInstant()
        {
            var uow = UnitOfWork.CreateInstant();
            var svc = new DocumentTypeService(uow);

            return svc;
        }

        public DocumentTypeService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public IEnumerable<DocumentTypeDto> GetAll()
        {
            var dto = _uow.DocumentTypeRepository.All();
            return dto;
        }
        public DocumentTypeDto GetByCode(string code)
        {
            var dto = _uow.DocumentTypeRepository.GetByCode(code);
            return dto;
        }
        public BusinessResponse Create(DocumentTypeDto model)
        {
            var response = new BusinessResponse();
            try
            {
                var objDuplicate = _uow.DocumentTypeRepository.All().Where(w => w.DOC_TYPE_CODE.Equals(model.DOC_TYPE_CODE)).FirstOrDefault();
                if (objDuplicate != null)
                    throw new Exception("Is duplicate data");

                var dto = new DocumentTypeDto();

                dto.DOC_TYPE_CODE = model.DOC_TYPE_CODE;
                dto.DOC_TYPE_NAME = model.DOC_TYPE_NAME;
                dto.DOC_TYPE_DESC = model.DOC_TYPE_DESC;
                dto.FLAG_ROW = FLAG_ROW.SHOW;
                dto.CREATED_BY = _currentUser.UserName ?? "SYSTEM";
                dto.CREATED_DATE = DateTime.UtcNow;

                _uow.DocumentTypeRepository.Insert(dto);
                _uow.Commit();

                response.Success = true;
                response.MessageType = MESSAGE_TYPE.SUCCESS;
                response.Message = $"Document type ({model.DOC_TYPE_CODE}) has been created";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.MessageType = MESSAGE_TYPE.ERROR;
                response.Message = ex.Message;
                _logger.Error($"{ex.Message}");

            }
            finally
            {
                base.Dispose();
            }

            return response;
        }
        public BusinessResponse Edit(DocumentTypeDto model)
        {
            var response = new BusinessResponse();
            try
            {
                if (string.IsNullOrEmpty(model.DOC_TYPE_CODE))
                    throw new Exception("Please select some one to editing.");

                string code = model.DOC_TYPE_CODE;
                var dto = _uow.DocumentTypeRepository.GetByCode(code);

                dto.DOC_TYPE_CODE = model.DOC_TYPE_CODE;
                dto.DOC_TYPE_NAME = model.DOC_TYPE_NAME;
                dto.DOC_TYPE_DESC = model.DOC_TYPE_DESC;
                dto.FLAG_ROW = model.IS_ACTIVED ? FLAG_ROW.SHOW : FLAG_ROW.DELETE;

                dto.UPDATED_BY = _currentUser.UserName ?? "SYSTEM";
                dto.UPDATED_DATE = DateTime.UtcNow;

                _uow.DocumentTypeRepository.Update(dto);
                _uow.Commit();

                response.Success = true;
                response.MessageType = MESSAGE_TYPE.SUCCESS;
                response.Message = $"Document type ({model.DOC_TYPE_CODE}) has been changed";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.MessageType = MESSAGE_TYPE.ERROR;
                response.Message = ex.Message;
                _logger.Error($"{ex.Message}");
            }
            finally
            {
                base.Dispose();
            }

            return response;
        }
        public BusinessResponse Remove(DocumentTypeDto model)
        {
            var response = new BusinessResponse();
            try
            {
                if (string.IsNullOrEmpty(model.DOC_TYPE_CODE))
                    throw new Exception("not existing Document type ID");

                string code = model.DOC_TYPE_CODE;
                var dto = model;
                dto.FLAG_ROW = FLAG_ROW.DELETE;
                dto.UPDATED_BY = _currentUser.UserName ?? "SYSTEM";
                dto.UPDATED_DATE = DateTime.UtcNow;

                if (model.IS_DELETE_PERMANANT)
                {
                    _uow.DocumentTypeRepository.Delete(code);
                }
                else
                {
                    _uow.DocumentTypeRepository.Update(dto);
                }

                _uow.Commit();

                response.Success = true;
                response.MessageType = MESSAGE_TYPE.SUCCESS;
                response.Message = $"{typeof(DocumentTypeService)} has been deleted";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.MessageType = MESSAGE_TYPE.ERROR;
                response.Message = ex.Message;
                _logger.Error($"{ex.Message}");
            }
            finally
            {
                base.Dispose();
            }

            return response;
        }

    }
}
