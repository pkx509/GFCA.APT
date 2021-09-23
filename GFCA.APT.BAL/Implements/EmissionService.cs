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
    public class EmissionService : ServiceBase, IEmissionService
    {
        private readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        internal static EmissionService CreateInstant()
        {
            var uow = UnitOfWork.CreateInstant();
            var svc = new EmissionService(uow);

            return svc;
        }
        
        public EmissionService(IUnitOfWork uow): base(uow)
        {
            _uow = uow;
        }

        public IEnumerable<EmissionDto> GetAll()
        {
            var dto = _uow.EmissionRepository.All();
            return dto;
        }
        public EmissionDto GetByCode(string code)
        {
            var dto = _uow.EmissionRepository.GetByCode(code);
            return dto;
        }
        public BusinessResponse Create(EmissionDto model)
        {
            var response = new BusinessResponse();
            try
            {
                var objDuplicate = _uow.EmissionRepository.All().Where(w => w.EMIS_CODE.Equals(model.EMIS_CODE)).FirstOrDefault();
                if (objDuplicate != null)
                    throw new Exception("Is duplicate data");

                var dto = new EmissionDto();

                dto.EMIS_CODE = model.EMIS_CODE;
                dto.EMIS_NAME = model.EMIS_NAME;
                dto.EMIS_DESC = model.EMIS_DESC;
                dto.FLAG_ROW = FLAG_ROW.SHOW;
                dto.CREATED_BY = _currentUser.UserName ?? "SYSTEM";
                dto.CREATED_DATE = DateTime.UtcNow;

                _uow.EmissionRepository.Insert(dto);
                _uow.Commit();

                response.Success = true;
                response.MessageType = TOAST_TYPE.SUCCESS;
                response.Message = $"Emission ({model.EMIS_CODE}) has been created";
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
        public BusinessResponse Edit(EmissionDto model)
        {
            var response = new BusinessResponse();
            try
            {
                if (string.IsNullOrEmpty(model.EMIS_CODE))
                    throw new Exception("Please select some one to editing.");

                string code = model.EMIS_CODE;
                var dto = _uow.EmissionRepository.GetByCode(code);

                dto.EMIS_CODE = model.EMIS_CODE;
                dto.EMIS_NAME = model.EMIS_NAME;
                dto.EMIS_DESC = model.EMIS_DESC;
                dto.FLAG_ROW = model.IS_ACTIVED ? FLAG_ROW.SHOW : FLAG_ROW.DELETE;

                dto.UPDATED_BY = _currentUser.UserName ?? "SYSTEM";
                dto.UPDATED_DATE = DateTime.UtcNow;

                _uow.EmissionRepository.Update(dto);
                _uow.Commit();

                response.Success = true;
                response.MessageType = TOAST_TYPE.SUCCESS;
                response.Message = $"Emission ({model.EMIS_CODE}) has been changed";
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
        public BusinessResponse Remove(EmissionDto model)
        {
            var response = new BusinessResponse();
            try
            {
                if (string.IsNullOrEmpty(model.EMIS_CODE))
                    throw new Exception("not existing Emission ID");

                string code = model.EMIS_CODE;
                var dto = model;
                dto.FLAG_ROW = FLAG_ROW.DELETE;
                dto.UPDATED_BY = _currentUser.UserName ?? "SYSTEM";
                dto.UPDATED_DATE = DateTime.UtcNow;

                if (model.IS_DELETE_PERMANANT)
                {
                    _uow.EmissionRepository.Delete(code);
                }
                else
                {
                    _uow.EmissionRepository.Update(dto);
                }

                _uow.Commit();

                response.Success = true;
                response.MessageType = TOAST_TYPE.SUCCESS;
                response.Message = $"{typeof(EmissionService)} has been deleted";
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
