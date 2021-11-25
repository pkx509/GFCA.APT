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
    public class CostCenterService : ServiceBase, ICostCenterService
    {
        private readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        internal static CostCenterService CreateInstant()
        {
            var uow = UnitOfWork.CreateInstant();
            var svc = new CostCenterService(uow);

            return svc;
        }

        public CostCenterService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public IEnumerable<CostCenterDto> GetAll()
        {
            var dto = _uow.CostCenterRepository.All();
            return dto;
        }
        public CostCenterDto GetByCode(string code)
        {
            var dto = _uow.CostCenterRepository.GetByCode(code);
            return dto;
        }
        public BusinessResponse Create(CostCenterDto model)
        {
            var response = new BusinessResponse();
            try
            {
                var objDuplicate = _uow.CostCenterRepository.All().Where(w => w.CENTER_CODE.Equals(model.CENTER_CODE)).FirstOrDefault();
                if (objDuplicate != null)
                    throw new Exception("Is duplicate data");

                var dto = new CostCenterDto();

                dto.CENTER_CODE = model.CENTER_CODE;
                dto.CENTER_NAME = model.CENTER_NAME;
                dto.CENTER_DESC = model.CENTER_DESC;
                dto.FLAG_ROW = FLAG_ROW.SHOW;
                dto.CREATED_BY = _currentUser.UserName ?? "SYSTEM";
                dto.CREATED_DATE = DateTime.UtcNow;

                _uow.CostCenterRepository.Insert(dto);
                _uow.Commit();

                response.Success = true;
                response.MessageType = MESSAGE_TYPE.SUCCESS;
                response.Message = $"Cost center ({model.CENTER_CODE}) has been created";
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
        public BusinessResponse Edit(CostCenterDto model)
        {
            var response = new BusinessResponse();
            try
            {
                if (string.IsNullOrEmpty(model.CENTER_CODE))
                    throw new Exception("Please select some one to editing.");

                string code = model.CENTER_CODE;
                var dto = _uow.CostCenterRepository.GetByCode(code);

                dto.CENTER_CODE = model.CENTER_CODE;
                dto.CENTER_NAME = model.CENTER_NAME;
                dto.CENTER_DESC = model.CENTER_DESC;
                dto.FLAG_ROW = model.IS_ACTIVED ? FLAG_ROW.SHOW : FLAG_ROW.DELETE;

                dto.UPDATED_BY = _currentUser.UserName ?? "SYSTEM";
                dto.UPDATED_DATE = DateTime.UtcNow;

                _uow.CostCenterRepository.Update(dto);
                _uow.Commit();

                response.Success = true;
                response.MessageType = MESSAGE_TYPE.SUCCESS;
                response.Message = $"Cost center ({model.CENTER_CODE}) has been changed";
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
        public BusinessResponse Remove(CostCenterDto model)
        {
            var response = new BusinessResponse();
            try
            {
                if (string.IsNullOrEmpty(model.CENTER_CODE))
                    throw new Exception("not existing Cost center ID");

                string code = model.CENTER_CODE;
                var dto = model;
                dto.FLAG_ROW = FLAG_ROW.DELETE;
                dto.UPDATED_BY = _currentUser.UserName ?? "SYSTEM";
                dto.UPDATED_DATE = DateTime.UtcNow;

                if (model.IS_DELETE_PERMANANT)
                {
                    _uow.CostCenterRepository.Delete(code);
                }
                else
                {
                    _uow.CostCenterRepository.Update(dto);
                }

                _uow.Commit();

                response.Success = true;
                response.MessageType = MESSAGE_TYPE.SUCCESS;
                response.Message = $"{typeof(CostCenterService)} has been deleted";
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
