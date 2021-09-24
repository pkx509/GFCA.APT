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
    public class CustomerPartyService : ServiceBase, ICustomerPartyService
    {
        private readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        internal static CustomerPartyService CreateInstant()
        {
            var uow = UnitOfWork.CreateInstant();
            var svc = new CustomerPartyService(uow);

            return svc;
        }

        public CustomerPartyService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public IEnumerable<CustomerPartyDto> GetAll()
        {
            var dto = _uow.CustomerPartyRepository.All();
            return dto;
        }
        public CustomerPartyDto GetByCode(string code)
        {
            var dto = _uow.CustomerPartyRepository.GetByCode(code);
            return dto;
        }
        public BusinessResponse Create(CustomerPartyDto model)
        {
            var response = new BusinessResponse();
            try
            {
                var objDuplicate = _uow.CustomerPartyRepository.All().Where(w => w.CUST_CODE.Equals(model.CUST_CODE) && w.VENDOR_CODE.Equals(model.VENDOR_CODE)).FirstOrDefault();
                if (objDuplicate != null)
                    throw new Exception("Is duplicate data");

                var dto = new CustomerPartyDto();

                dto.CUST_CODE = model.CUST_CODE;
                dto.ACC_CODE = model.ACC_CODE;
                dto.DISTB_CODE = model.DISTB_CODE;
                dto.CHANNEL_CODE = model.CHANNEL_CODE;
                dto.VENDOR_CODE = model.VENDOR_CODE;
                dto.FLAG_ROW = FLAG_ROW.SHOW;
                dto.CREATED_BY = _currentUser.UserName ?? "System";
                dto.CREATED_DATE = DateTime.UtcNow;

                _uow.CustomerPartyRepository.Insert(dto);
                _uow.Commit();

                response.Success = true;
                response.MessageType = TOAST_TYPE.SUCCESS;
                response.Message = $"Customer party {model.CUST_CODE} has been created";
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
        public BusinessResponse Edit(CustomerPartyDto model)
        {
            var response = new BusinessResponse();
            try
            {
                if (string.IsNullOrEmpty(model.CUST_CODE))
                    throw new Exception("Please select some one to editing.");

                string code = model.CUST_CODE;
                var dto = _uow.CustomerPartyRepository.GetByCode(code);

                dto.CUST_CODE = model.CUST_CODE;
                dto.ACC_CODE = model.ACC_CODE;
                dto.DISTB_CODE = model.DISTB_CODE;
                dto.CHANNEL_CODE = model.CHANNEL_CODE;
                dto.VENDOR_CODE = model.VENDOR_CODE;
                dto.FLAG_ROW = model.IS_ACTIVED ? FLAG_ROW.SHOW : FLAG_ROW.DELETE;
                dto.UPDATED_BY = _currentUser.UserName ?? "System";
                dto.UPDATED_DATE = DateTime.UtcNow;

                _uow.CustomerPartyRepository.Update(dto);
                _uow.Commit();

                response.Success = true;
                response.MessageType = TOAST_TYPE.SUCCESS;
                response.Message = $"Customer party {model.CUST_CODE} has been changed";
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
        public BusinessResponse Remove(CustomerPartyDto model)
        {
            var response = new BusinessResponse();
            try
            {
                if (string.IsNullOrEmpty(model.CUST_CODE))
                    throw new Exception("not existing Customer party ID");

                string code = model.CUST_CODE;
                var dto = model;
                dto.FLAG_ROW = FLAG_ROW.DELETE;
                dto.UPDATED_BY = _currentUser.UserName ?? "System";
                dto.UPDATED_DATE = DateTime.UtcNow;

                if (model.IS_DELETE_PERMANANT)
                {
                    _uow.CustomerPartyRepository.Delete(code);
                }
                else
                {
                    _uow.CustomerPartyRepository.Update(dto);
                }

                _uow.Commit();

                response.Success = true;
                response.MessageType = TOAST_TYPE.SUCCESS;
                response.Message = $"{typeof(CustomerPartyService)} has been deleted";
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
