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
    public class PromotionGroupService : ServiceBase, IPromotionGroupService
    {
        private readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        internal static PromotionGroupService CreateInstant()
        {
            var uow = UnitOfWork.CreateInstant();
            var svc = new PromotionGroupService(uow);

            return svc;
        }

        public IEnumerable<PromotionGroupDto> GetAll()
        {
            var dto = _uow.PromotionGroupRepository.All();
            return dto;
        }

        public PromotionGroupDto GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public BusinessResponse Create(PromotionGroupDto model)
        {
            var response = new BusinessResponse();
            try
            {

                if (string.IsNullOrEmpty(model.CLIENT_CODE))
                    throw new Exception("Client not exist.");

                if (string.IsNullOrEmpty(model.CUST_CODE))
                    throw new Exception("Customer not exist.");

                if (string.IsNullOrEmpty(model.CHANNEL_CODE))
                    throw new Exception("Channel not exist.");

                if (string.IsNullOrEmpty(model.PROGP_CODE))
                    throw new Exception("Promotion Group not exist.");

                var objDuplicate = _uow.PromotionGroupRepository.All()
                    .Where(w => 
                    w.CLIENT_CODE.ToUpper().Equals(model.CLIENT_CODE.ToUpper()) &&
                    w.CUST_CODE.ToUpper().Equals(model.CUST_CODE.ToUpper()) &&
                    w.CHANNEL_CODE.ToUpper().Equals(model.CHANNEL_CODE.ToUpper()) &&
                    w.PROGP_CODE.ToUpper().Equals(model.PROGP_CODE.ToUpper()))
                    .FirstOrDefault();
                if (objDuplicate != null)
                    throw new Exception("Is duplicate data");

                var dto = new PromotionGroupDto();


                dto = new PromotionGroupDto
                {
                    PROGP_ID = model.PROGP_ID,
                    CHANNEL_ID = model.CHANNEL_ID,
                    CUST_ID = model.CUST_ID,
                    CLIENT_ID = model.CLIENT_ID,
                    PROGP_CODE = model.PROGP_CODE,
                    PROGP_NAME = model.PROGP_NAME,
                    PROGP_DESC = model.PROGP_DESC,
                    FLAG_ROW = FLAG_ROW.SHOW,
                    CREATED_BY = _currentUser.UserName ?? "System",
                    CREATED_DATE = DateTime.UtcNow,
                    UPDATED_BY = model.UPDATED_BY,
                    UPDATED_DATE = model.UPDATED_DATE,
                };



                _uow.PromotionGroupRepository.Insert(dto);
                _uow.Commit();

                response.Success = true;
                response.MessageType = MESSAGE_TYPE.SUCCESS;
                response.Message = $"PromotionGroup ({model.PROGP_CODE}) has been created";
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

        public BusinessResponse Edit(PromotionGroupDto model)
        {
            var response = new BusinessResponse();
            try
            {

                if (model.PROGP_ID == 0)
                    throw new Exception("Please select some one to editing.");


                if (_uow.ClientRepository.All().Where(w => w.CLIENT_CODE == model.CLIENT_CODE).Count() < 1)
                {

                    throw new Exception("Client not exist.");
                }

                if (_uow.CustomerRepository.All().Where(w => w.CUST_CODE == model.CUST_CODE).Count() < 1)
                {

                    throw new Exception("Customer not exist.");
                }

                if (_uow.ChannelRepository.All().Where(w => w.CHANNEL_CODE == model.CHANNEL_CODE).Count() < 1)
                {

                    throw new Exception("Channel not exist.");
                }


                string code = model.PROGP_CODE;
                var dto = _uow.PromotionGroupRepository.GetByCode(code);





                dto.CHANNEL_ID = model.CHANNEL_ID;
                dto.CUST_ID = model.CUST_ID;
                dto.CLIENT_ID = model.CLIENT_ID;
                dto.PROGP_CODE = model.PROGP_CODE;
                dto.PROGP_NAME = model.PROGP_NAME;
                dto.PROGP_DESC = model.PROGP_DESC;
                dto.FLAG_ROW = FLAG_ROW.SHOW;
               

                dto.FLAG_ROW = model.IS_ACTIVED ? FLAG_ROW.SHOW : FLAG_ROW.DELETE;

                dto.UPDATED_BY = _currentUser.UserName ?? "System";
                dto.UPDATED_DATE = DateTime.UtcNow;

                _uow.PromotionGroupRepository.Update(dto);
                _uow.Commit();

                response.Success = true;
                response.MessageType = MESSAGE_TYPE.SUCCESS;
                response.Message = $"Brand ({model.PROGP_CODE}) has been changed";
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

        public BusinessResponse Remove(PromotionGroupDto model)
        {
            var response = new BusinessResponse();
            try
            {
                if (model.PROGP_ID == 0)
                    throw new Exception("not existing PROGP_ID");

                string code = model.PROGP_CODE;
                //var dto = _uow.BrandRepository.GetById(id);
                var dto = model;
                dto.FLAG_ROW = FLAG_ROW.DELETE;
                dto.UPDATED_BY = _currentUser.UserName ?? "System";
                dto.UPDATED_DATE = DateTime.UtcNow;

                if (model.IS_DELETE_PERMANANT)
                {
                    _uow.PromotionGroupRepository.Delete(code);
                }
                else
                {
                    _uow.PromotionGroupRepository.Update(dto);
                }

                _uow.Commit();

                response.Success = true;
                response.MessageType = MESSAGE_TYPE.SUCCESS;
                response.Message = $"{typeof(BrandService)} has been deleted";
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

        public PromotionGroupService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }


    }
}
