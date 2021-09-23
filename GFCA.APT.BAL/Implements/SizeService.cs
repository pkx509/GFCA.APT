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
        public SizeDto GetByCode(string code)
        {
            var dto = _uow.SizeRepository.GetByCode(code);
            return dto;
        }
        public BusinessResponse Create(SizeDto model)
        {
            var response = new BusinessResponse();
            try
            {
                var objDuplicate = _uow.SizeRepository.All().Where(w => w.SIZE_CODE.Equals(model.SIZE_CODE)).FirstOrDefault();
                if (objDuplicate != null)
                    throw new Exception("Is duplicate data");

                var dto = new SizeDto();

                dto.SIZE_CODE = model.SIZE_CODE;
                dto.SIZE_NAME = model.SIZE_NAME;
                dto.SIZE_DESC = model.SIZE_DESC;
                dto.FLAG_ROW = FLAG_ROW.SHOW;
                dto.CREATED_BY = _currentUser.UserName ?? "SYSTEM";
                dto.CREATED_DATE = DateTime.UtcNow;

                _uow.SizeRepository.Insert(dto);
                _uow.Commit();

                response.Success = true;
                response.MessageType = TOAST_TYPE.SUCCESS;
                response.Message = $"Size ({model.SIZE_CODE}) has been created";
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
        public BusinessResponse Edit(SizeDto model)
        {
            var response = new BusinessResponse();
            try
            {
                if (string.IsNullOrEmpty(model.SIZE_CODE))
                    throw new Exception("Please select some one to editing.");

                string code = model.SIZE_CODE;
                var dto = _uow.SizeRepository.GetByCode(code);

                dto.SIZE_CODE = model.SIZE_CODE;
                dto.SIZE_NAME = model.SIZE_NAME;
                dto.SIZE_DESC = model.SIZE_DESC;
                dto.FLAG_ROW = model.IS_ACTIVED ? FLAG_ROW.SHOW : FLAG_ROW.DELETE;

                dto.UPDATED_BY = _currentUser.UserName ?? "SYSTEM";
                dto.UPDATED_DATE = DateTime.UtcNow;

                _uow.SizeRepository.Update(dto);
                _uow.Commit();

                response.Success = true;
                response.MessageType = TOAST_TYPE.SUCCESS;
                response.Message = $"Size ({model.SIZE_CODE}) has been changed";
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
        public BusinessResponse Remove(SizeDto model)
        {
            var response = new BusinessResponse();
            try
            {
                if (string.IsNullOrEmpty(model.SIZE_CODE))
                    throw new Exception("not existing Size ID");

                string code = model.SIZE_CODE;
                var dto = model;
                dto.FLAG_ROW = FLAG_ROW.DELETE;
                dto.UPDATED_BY = _currentUser.UserName ?? "SYSTEM";
                dto.UPDATED_DATE = DateTime.UtcNow;

                if (model.IS_DELETE_PERMANANT)
                {
                    _uow.SizeRepository.Delete(code);
                }
                else
                {
                    _uow.SizeRepository.Update(dto);
                }

                _uow.Commit();

                response.Success = true;
                response.MessageType = TOAST_TYPE.SUCCESS;
                response.Message = $"{typeof(SizeService)} has been deleted";
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
