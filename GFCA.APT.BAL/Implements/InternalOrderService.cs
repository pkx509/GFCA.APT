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
    public class InternalOrderService : ServiceBase, IInternalOrderService
    {
        private readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        internal static InternalOrderService CreateInstant()
        {
            var uow = UnitOfWork.CreateInstant();
            var svc = new InternalOrderService(uow);

            return svc;
        }

        public InternalOrderService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public IEnumerable<InternalOrderDto> GetAll()
        {
            var dto = _uow.InternalOrderRepository.All();
            return dto;
        }
        public InternalOrderDto GetByCode(string code)
        {
            var dto = _uow.InternalOrderRepository.GetByCode(code);
            return dto;
        }
        public BusinessResponse Create(InternalOrderDto model)
        {
            var response = new BusinessResponse();
            try
            {
                var objDuplicate = _uow.InternalOrderRepository.All().Where(w => w.IO_CODE.Equals(model.IO_CODE)).FirstOrDefault();
                if (objDuplicate != null)
                    throw new Exception("Is duplicate data");

                var dto = new InternalOrderDto();

                dto.IO_CODE = model.IO_CODE;
                dto.IO_NAME = model.IO_NAME;
                dto.IO_DESC = model.IO_DESC;
                dto.FLAG_ROW = FLAG_ROW.SHOW;
                dto.CREATED_BY = _currentUser.UserName ?? "SYSTEM";
                dto.CREATED_DATE = DateTime.UtcNow;

                _uow.InternalOrderRepository.Insert(dto);
                _uow.Commit();

                response.Success = true;
                response.MessageType = TOAST_TYPE.SUCCESS;
                response.Message = $"Internal order ({model.IO_CODE}) has been created";
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
        public BusinessResponse Edit(InternalOrderDto model)
        {
            var response = new BusinessResponse();
            try
            {
                if (string.IsNullOrEmpty(model.IO_CODE))
                    throw new Exception("Please select some one to editing.");

                string code = model.IO_CODE;
                var dto = _uow.InternalOrderRepository.GetByCode(code);

                dto.IO_CODE = model.IO_CODE;
                dto.IO_NAME = model.IO_NAME;
                dto.IO_DESC = model.IO_DESC;
                dto.FLAG_ROW = model.IS_ACTIVED ? FLAG_ROW.SHOW : FLAG_ROW.DELETE;

                dto.UPDATED_BY = _currentUser.UserName ?? "SYSTEM";
                dto.UPDATED_DATE = DateTime.UtcNow;

                _uow.InternalOrderRepository.Update(dto);
                _uow.Commit();

                response.Success = true;
                response.MessageType = TOAST_TYPE.SUCCESS;
                response.Message = $"Internal order ({model.IO_CODE}) has been changed";
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
        public BusinessResponse Remove(InternalOrderDto model)
        {
            var response = new BusinessResponse();
            try
            {
                if (string.IsNullOrEmpty(model.IO_CODE))
                    throw new Exception("not existing Internal order ID");

                string code = model.IO_CODE;
                var dto = model;
                dto.FLAG_ROW = FLAG_ROW.DELETE;
                dto.UPDATED_BY = _currentUser.UserName ?? "SYSTEM";
                dto.UPDATED_DATE = DateTime.UtcNow;

                if (model.IS_DELETE_PERMANANT)
                {
                    _uow.InternalOrderRepository.Delete(code);
                }
                else
                {
                    _uow.InternalOrderRepository.Update(dto);
                }

                _uow.Commit();

                response.Success = true;
                response.MessageType = TOAST_TYPE.SUCCESS;
                response.Message = $"{typeof(InternalOrderService)} has been deleted";
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
