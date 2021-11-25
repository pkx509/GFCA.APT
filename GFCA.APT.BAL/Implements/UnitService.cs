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
    public class UnitService : ServiceBase, IUnitService
    {
        private readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        internal static UnitService CreateInstant()
        {
            var uow = UnitOfWork.CreateInstant();
            var svc = new UnitService(uow);

            return svc;
        }

        public UnitService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public IEnumerable<UnitDto> GetAll()
        {
            var dto = _uow.UnitRepository.All();
            return dto;
        }
        public UnitDto GetByCode(string code)
        {
            var dto = _uow.UnitRepository.GetByCode(code);
            return dto;
        }
        public BusinessResponse Create(UnitDto model)
        {
            var response = new BusinessResponse();
            try
            {
                var objDuplicate = _uow.UnitRepository.All().Where(w => w.UNIT_CODE.Equals(model.UNIT_CODE)).FirstOrDefault();
                if (objDuplicate != null)
                    throw new Exception("Is duplicate data");

                var dto = new UnitDto();

                dto.UNIT_CODE        = model.UNIT_CODE;
                dto.UNIT_PARENT_CODE = model.UNIT_PARENT_CODE;
                dto.UNIT_NAME        = model.UNIT_NAME;
                dto.UNIT_TYPE        = model.UNIT_TYPE;
                dto.FACTOR           = model.FACTOR;
                dto.UNIT_DESC        = model.UNIT_DESC;
                dto.FLAG_ROW         = FLAG_ROW.SHOW;
                dto.CREATED_BY       = _currentUser.UserName ?? "SYSTEM";
                dto.CREATED_DATE     = DateTime.UtcNow;

                _uow.UnitRepository.Insert(dto);
                _uow.Commit();

                response.Success = true;
                response.MessageType = MESSAGE_TYPE.SUCCESS;
                response.Message = $"Unit ({model.UNIT_CODE}) has been created";
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
        public BusinessResponse Edit(UnitDto model)
        {
            var response = new BusinessResponse();
            try
            {
                if (string.IsNullOrEmpty(model.UNIT_CODE))
                    throw new Exception("Please select some one to editing.");

                string code = model.UNIT_CODE;
                var dto = _uow.UnitRepository.GetByCode(code);

                dto.UNIT_CODE        = model.UNIT_CODE;
                dto.UNIT_PARENT_CODE = model.UNIT_PARENT_CODE;
                dto.UNIT_NAME        = model.UNIT_NAME;
                dto.UNIT_TYPE        = model.UNIT_TYPE;
                dto.FACTOR           = model.FACTOR;
                dto.UNIT_DESC        = model.UNIT_DESC;
                dto.FLAG_ROW         = model.IS_ACTIVED ? FLAG_ROW.SHOW : FLAG_ROW.DELETE;

                dto.UPDATED_BY = _currentUser.UserName ?? "SYSTEM";
                dto.UPDATED_DATE = DateTime.UtcNow;

                _uow.UnitRepository.Update(dto);
                _uow.Commit();

                response.Success = true;
                response.MessageType = MESSAGE_TYPE.SUCCESS;
                response.Message = $"Unit ({model.UNIT_CODE}) has been changed";
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
        public BusinessResponse Remove(UnitDto model)
        {
            var response = new BusinessResponse();
            try
            {
                if (string.IsNullOrEmpty(model.UNIT_CODE))
                    throw new Exception("not existing Unit ID");

                string code = model.UNIT_CODE;
                var dto = model;
                dto.FLAG_ROW = FLAG_ROW.DELETE;
                dto.UPDATED_BY = _currentUser.UserName ?? "SYSTEM";
                dto.UPDATED_DATE = DateTime.UtcNow;

                if (model.IS_DELETE_PERMANANT)
                {
                    _uow.UnitRepository.Delete(code);
                }
                else
                {
                    _uow.UnitRepository.Update(dto);
                }

                _uow.Commit();

                response.Success = true;
                response.MessageType = MESSAGE_TYPE.SUCCESS;
                response.Message = $"{typeof(UnitService)} has been deleted";
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
