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
    public class OrganizationService : ServiceBase, IOrganizationService
    {
        private readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        internal static OrganizationService CreateInstant()
        {
            var uow = UnitOfWork.CreateInstant();
            var svc = new OrganizationService(uow);

            return svc;
        }

        public OrganizationService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public IEnumerable<OrganizationDto> GetAll()
        {
            var dto = _uow.OrganizationRepository.All();
            return dto;
        }
        public OrganizationDto GetByCode(string code)
        {
            var dto = _uow.OrganizationRepository.GetByCode(code);
            return dto;
        }
        public BusinessResponse Create(OrganizationDto model)
        {
            var response = new BusinessResponse();
            try
            {
                var objDuplicate = _uow.OrganizationRepository.All().Where(w => w.ORG_CODE.Equals(model.ORG_CODE)).FirstOrDefault();
                if (objDuplicate != null)
                    throw new Exception("Is duplicate data");

                var dto = new OrganizationDto();

                dto.ORG_CODE = model.ORG_CODE;
                dto.REPORT_TO = model.REPORT_TO;
                dto.COMP_CODE = model.COMP_CODE;
                dto.HIERACHY_ID = model.HIERACHY_ID;
                dto.ORG_NAME = model.ORG_NAME;
                dto.ORG_ABBR = model.ORG_ABBR;
                dto.ORG_DESC = model.ORG_DESC;
                dto.FLAG_ORG = model.FLAG_ORG;
                dto.FLAG_ROW     = FLAG_ROW.SHOW;
                dto.CREATED_BY   = _currentUser.UserName ?? "SYSTEM";
                dto.CREATED_DATE = DateTime.UtcNow;

                _uow.OrganizationRepository.Insert(dto);
                _uow.Commit();

                response.Success = true;
                response.MessageType = TOAST_TYPE.SUCCESS;
                response.Message = $"Organization ({model.ORG_CODE}) has been created";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.MessageType = TOAST_TYPE.ERROR;
                response.Message = ex.Message;
                _logger.Error($"{ex.Message}");

            }
            finally
            {
                base.Dispose();
            }

            return response;
        }
        public BusinessResponse Edit(OrganizationDto model)
        {
            var response = new BusinessResponse();
            try
            {
                if (string.IsNullOrEmpty(model.ORG_CODE))
                    throw new Exception("Please select some one to editing.");

                string code = model.ORG_CODE;
                var dto = _uow.OrganizationRepository.GetByCode(code);

                dto.ORG_CODE = model.ORG_CODE;
                dto.REPORT_TO = model.REPORT_TO;
                dto.COMP_CODE = model.COMP_CODE;
                dto.HIERACHY_ID = model.HIERACHY_ID;
                dto.ORG_NAME = model.ORG_NAME;
                dto.ORG_ABBR = model.ORG_ABBR;
                dto.ORG_DESC = model.ORG_DESC;
                dto.FLAG_ORG = model.FLAG_ORG;
                dto.FLAG_ROW = model.IS_ACTIVED ? FLAG_ROW.SHOW : FLAG_ROW.DELETE;
                dto.UPDATED_BY = _currentUser.UserName ?? "SYSTEM";
                dto.UPDATED_DATE = DateTime.UtcNow;

                _uow.OrganizationRepository.Update(dto);
                _uow.Commit();

                response.Success = true;
                response.MessageType = TOAST_TYPE.SUCCESS;
                response.Message = $"Organization ({model.ORG_CODE}) has been changed";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.MessageType = TOAST_TYPE.ERROR;
                response.Message = ex.Message;
                _logger.Error($"{ex.Message}");
            }
            finally
            {
                base.Dispose();
            }

            return response;
        }
        public BusinessResponse Remove(OrganizationDto model)
        {
            var response = new BusinessResponse();
            try
            {
                if (string.IsNullOrEmpty(model.ORG_CODE))
                    throw new Exception("not existing Organization ID");

                string code = model.ORG_CODE;
                var dto = model;
                dto.FLAG_ROW = FLAG_ROW.DELETE;
                dto.UPDATED_BY = _currentUser.UserName ?? "SYSTEM";
                dto.UPDATED_DATE = DateTime.UtcNow;

                if (model.IS_DELETE_PERMANANT)
                {
                    _uow.OrganizationRepository.Delete(code);
                }
                else
                {
                    _uow.OrganizationRepository.Update(dto);
                }

                _uow.Commit();

                response.Success = true;
                response.MessageType = TOAST_TYPE.SUCCESS;
                response.Message = $"{typeof(OrganizationService)} has been deleted";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.MessageType = TOAST_TYPE.ERROR;
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
