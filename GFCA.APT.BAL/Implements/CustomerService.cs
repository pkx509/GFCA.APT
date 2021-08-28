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
    public class CustomerService : ServiceBase, ICustomerService
    {
        private readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        internal static CustomerService CreateInstant()
        {
            var uow = UnitOfWork.CreateInstant();
            var svc = new CustomerService(uow);

            return svc;
        }

        public CustomerService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public IEnumerable<CustomerDto> GetAll()
        {
            var dto = _uow.CustomerRepository.All();
            return dto;
        }
        public CustomerDto GetById(int Id)
        {
            var dto = _uow.CustomerRepository.GetById(Id);
            return dto;
        }
        public BusinessResponse Create(CustomerDto model)
        {
            var response = new BusinessResponse();
            try
            {
                var objDuplicate = _uow.CustomerRepository.All().Where(w => w.CUST_CODE.Equals(model.CUST_CODE)).FirstOrDefault();
                if (objDuplicate != null)
                    throw new Exception("Is duplicate data");

                var dto = new CustomerDto();

                dto.CUST_CODE = model.CUST_CODE;
                dto.CUST_NAME = model.CUST_NAME;
                dto.CUST_ABV = model.CUST_ABV;
                dto.CUST_GROUP1 = model.CUST_GROUP1;
                dto.CUST_DESC = model.CUST_DESC;
                dto.FLAG_ROW = FLAG_ROW.SHOW;
                dto.CREATED_BY = _currentUser.UserName ?? "SYSTEM";
                dto.CREATED_DATE = DateTime.UtcNow;

                _uow.CustomerRepository.Insert(dto);
                _uow.Commit();

                response.Success = true;
                response.MessageType = TOAST_TYPE.SUCCESS;
                response.Message = $"Customer ({model.CUST_CODE}) has been created";
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
        public BusinessResponse Edit(CustomerDto model)
        {
            var response = new BusinessResponse();
            try
            {
                if (model.CUST_ID == null || model.CUST_ID == 0)
                    throw new Exception("Please select some one to editing.");

                int id = model.CUST_ID ?? 0;
                var dto = _uow.CustomerRepository.GetById(id);

                dto.CUST_CODE = model.CUST_CODE;
                dto.CUST_NAME = model.CUST_NAME;
                dto.CUST_ABV = model.CUST_ABV;
                dto.CUST_GROUP1 = model.CUST_GROUP1;
                dto.CUST_DESC = model.CUST_DESC;
                dto.FLAG_ROW = model.IS_ACTIVED ? FLAG_ROW.SHOW : FLAG_ROW.DELETE;

                dto.UPDATED_BY = _currentUser.UserName ?? "SYSTEM";
                dto.UPDATED_DATE = DateTime.UtcNow;

                _uow.CustomerRepository.Update(dto);
                _uow.Commit();

                response.Success = true;
                response.MessageType = TOAST_TYPE.SUCCESS;
                response.Message = $"Customer ({model.CUST_CODE}) has been changed";
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
        public BusinessResponse Remove(CustomerDto model)
        {
            var response = new BusinessResponse();
            try
            {
                if (model.CUST_ID == null || model.CUST_ID == 0)
                    throw new Exception("not existing Customer ID");

                int id = model.CUST_ID ?? 0;
                var dto = model;
                dto.FLAG_ROW = FLAG_ROW.DELETE;
                dto.UPDATED_BY = _currentUser.UserName ?? "SYSTEM";
                dto.UPDATED_DATE = DateTime.UtcNow;

                if (model.IS_DELETE_PERMANANT)
                {
                    _uow.CustomerRepository.Delete(id);
                }
                else
                {
                    _uow.CustomerRepository.Update(dto);
                }

                _uow.Commit();

                response.Success = true;
                response.MessageType = TOAST_TYPE.SUCCESS;
                response.Message = $"{typeof(CustomerService)} has been deleted";
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
