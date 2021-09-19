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
    public class EmployeeService : ServiceBase, IEmployeeService
    {
        private readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        internal static EmployeeService CreateInstant()
        {
            var uow = UnitOfWork.CreateInstant();
            var svc = new EmployeeService(uow);

            return svc;
        }

        public EmployeeService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public IEnumerable<EmployeeDto> GetAll()
        {
            var dto = _uow.EmployeeRepository.All();
            return dto;
        }
        public EmployeeDto GetById(int Id)
        {
            var dto = _uow.EmployeeRepository.GetById(Id);
            return dto;
        }
        public BusinessResponse Create(EmployeeDto model)
        {
            var response = new BusinessResponse();
            try
            {
                var objDuplicate = _uow.EmployeeRepository.All().Where(w => w.EMP_CODE.Equals(model.EMP_CODE)).FirstOrDefault();
                if (objDuplicate != null)
                    throw new Exception("Is duplicate data");

                var dto = new EmployeeDto();

                dto.EMP_CODE = model.EMP_CODE;
                dto.PREFIX = model.PREFIX;
                dto.NAME_FIRST = model.NAME_FIRST;
                dto.NAME_LAST = model.NAME_LAST;
                dto.EMAIL = model.EMAIL;
                dto.FLAG_ROW = FLAG_ROW.SHOW;
                dto.CREATED_BY = _currentUser.UserName ?? "SYSTEM";
                dto.CREATED_DATE = DateTime.UtcNow;

                _uow.EmployeeRepository.Insert(dto);
                _uow.Commit();

                response.Success = true;
                response.MessageType = TOAST_TYPE.SUCCESS;
                response.Message = $"Employee ({model.EMP_CODE}) has been created";
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
        public BusinessResponse Edit(EmployeeDto model)
        {
            var response = new BusinessResponse();
            try
            {
                if (model.EMP_ID == null || model.EMP_ID == 0)
                    throw new Exception("Please select some one to editing.");

                int id = model.EMP_ID ?? 0;
                var dto = _uow.EmployeeRepository.GetById(id);

                dto.EMP_CODE = model.EMP_CODE;
                dto.PREFIX = model.PREFIX;
                dto.NAME_FIRST = model.NAME_FIRST;
                dto.NAME_LAST = model.NAME_LAST;
                dto.EMAIL = model.EMAIL;
                dto.FLAG_ROW = model.IS_ACTIVED ? FLAG_ROW.SHOW : FLAG_ROW.DELETE;

                dto.UPDATED_BY = _currentUser.UserName ?? "SYSTEM";
                dto.UPDATED_DATE = DateTime.UtcNow;

                _uow.EmployeeRepository.Update(dto);
                _uow.Commit();

                response.Success = true;
                response.MessageType = TOAST_TYPE.SUCCESS;
                response.Message = $"Employee ({model.EMP_CODE}) has been changed";
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
        public BusinessResponse Remove(EmployeeDto model)
        {
            var response = new BusinessResponse();
            try
            {
                if (model.EMP_ID == null || model.EMP_ID == 0)
                    throw new Exception("not existing Employee ID");

                int id = model.EMP_ID ?? 0;
                var dto = model;
                dto.FLAG_ROW = FLAG_ROW.DELETE;
                dto.UPDATED_BY = _currentUser.UserName ?? "SYSTEM";
                dto.UPDATED_DATE = DateTime.UtcNow;

                if (model.IS_DELETE_PERMANANT)
                {
                    _uow.EmployeeRepository.Delete(id);
                }
                else
                {
                    _uow.EmployeeRepository.Update(dto);
                }

                _uow.Commit();

                response.Success = true;
                response.MessageType = TOAST_TYPE.SUCCESS;
                response.Message = $"{typeof(EmployeeService)} has been deleted";
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
