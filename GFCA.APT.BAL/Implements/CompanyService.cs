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
    public class CompanyService : ServiceBase, ICompanyService
    {
        private readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        internal static CompanyService CreateInstant()
        {
            var uow = UnitOfWork.CreateInstant();
            var svc = new CompanyService(uow);

            return svc;
        }

        public CompanyService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public IEnumerable<CompanyDto> GetAll()
        {
            var dto = _uow.CompanyRepository.All();
            return dto;
        }
        public CompanyDto GetByCode(string code)
        {
            var dto = _uow.CompanyRepository.GetByCode(code);
            return dto;
        }
        public BusinessResponse Create(CompanyDto model)
        {
            var response = new BusinessResponse();
            try
            {
                var objDuplicate = _uow.CompanyRepository.All().Where(w => w.COMP_CODE.Equals(model.COMP_CODE)).FirstOrDefault();
                if (objDuplicate != null)
                    throw new Exception("Is duplicate data");

                var dto = new CompanyDto();

                dto.COMP_CODE = model.COMP_CODE;
                dto.COMP_NAME = model.COMP_NAME;
                dto.ADDRESS = model.ADDRESS;
                dto.FLAG_ROW = FLAG_ROW.SHOW;
                dto.CREATED_BY = _currentUser.UserName ?? "SYSTEM";
                dto.CREATED_DATE = DateTime.UtcNow;

                _uow.CompanyRepository.Insert(dto);
                _uow.Commit();

                response.Success = true;
                response.MessageType = TOAST_TYPE.SUCCESS;
                response.Message = $"Company ({model.COMP_CODE}) has been created";
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
        public BusinessResponse Edit(CompanyDto model)
        {
            var response = new BusinessResponse();
            try
            {
                if (string.IsNullOrEmpty(model.COMP_CODE))
                    throw new Exception("Please select some one to editing.");

                string code = model.COMP_CODE;
                var dto = _uow.CompanyRepository.GetByCode(code);

                dto.COMP_CODE = model.COMP_CODE;
                dto.COMP_NAME = model.COMP_NAME;
                dto.ADDRESS = model.ADDRESS;
                dto.FLAG_ROW = model.IS_ACTIVED ? FLAG_ROW.SHOW : FLAG_ROW.DELETE;

                dto.UPDATED_BY = _currentUser.UserName ?? "SYSTEM";
                dto.UPDATED_DATE = DateTime.UtcNow;

                _uow.CompanyRepository.Update(dto);
                _uow.Commit();

                response.Success = true;
                response.MessageType = TOAST_TYPE.SUCCESS;
                response.Message = $"Company ({model.COMP_CODE}) has been changed";
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
        public BusinessResponse Remove(CompanyDto model)
        {
            var response = new BusinessResponse();
            try
            {
                if (string.IsNullOrEmpty(model.COMP_CODE))
                    throw new Exception("not existing Company ID");

                string code = model.COMP_CODE;
                var dto = model;
                dto.FLAG_ROW = FLAG_ROW.DELETE;
                dto.UPDATED_BY = _currentUser.UserName ?? "SYSTEM";
                dto.UPDATED_DATE = DateTime.UtcNow;

                if (model.IS_DELETE_PERMANANT)
                {
                    _uow.CompanyRepository.Delete(code);
                }
                else
                {
                    _uow.CompanyRepository.Update(dto);
                }

                _uow.Commit();

                response.Success = true;
                response.MessageType = TOAST_TYPE.SUCCESS;
                response.Message = $"{typeof(CompanyService)} has been deleted";
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
