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
        public CustomerPartyDto GetById(int Id)
        {
            var dto = _uow.CustomerPartyRepository.GetById(Id);
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

                dto.CUST_ID = model.CUST_ID;
                dto.ACC_ID = model.ACC_ID;
                dto.DISTB_ID = model.DISTB_ID;
                dto.CHANNEL_ID = model.CHANNEL_ID;
                dto.VENDOR_ID = model.VENDOR_ID;
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
                response.Message = $"Customer party {model.PARTY_ID} has been created";
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
                if (model.PARTY_ID == null || model.PARTY_ID == 0)
                    throw new Exception("Please select some one to editing.");

                int id = model.PARTY_ID ?? 0;
                var dto = _uow.CustomerPartyRepository.GetById(id);

                dto.ACC_ID = model.ACC_ID;
                dto.DISTB_ID = model.DISTB_ID;
                dto.CHANNEL_ID = model.CHANNEL_ID;
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
                response.Message = $"Customer party {model.PARTY_ID} has been changed";
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
                if (model.PARTY_ID == null || model.PARTY_ID == 0)
                    throw new Exception("not existing Customer party ID");

                int id = model.PARTY_ID ?? 0;
                var dto = model;
                dto.FLAG_ROW = FLAG_ROW.DELETE;
                dto.UPDATED_BY = _currentUser.UserName ?? "System";
                dto.UPDATED_DATE = DateTime.UtcNow;

                if (model.IS_DELETE_PERMANANT)
                {
                    _uow.CustomerPartyRepository.Delete(id);
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
