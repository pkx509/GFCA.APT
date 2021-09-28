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
    public class ChannelService : ServiceBase, IChannelService
    {
        private readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        internal static ChannelService CreateInstant()
        {
            var uow = UnitOfWork.CreateInstant();
            var svc = new ChannelService(uow);

            return svc;
        }

        public ChannelService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public IEnumerable<ChannelDto> GetAll()
        {
            var dto = _uow.ChannelRepository.All();
            return dto;
        }
        public ChannelDto GetByCode(string code)
        {
            var dto = _uow.ChannelRepository.GetByCode(code);
            return dto;
        }
        public BusinessResponse Create(ChannelDto model)
        {
            var response = new BusinessResponse();
            try
            {
                var objDuplicate = _uow.ChannelRepository.All().Where(w => w.CHANNEL_CODE.Equals(model.CHANNEL_CODE)).FirstOrDefault();
                if (objDuplicate != null)
                    throw new Exception("Is duplicate data");

                var dto = new ChannelDto();
                dto.EMIS_CODE = model.EMIS_CODE;
                dto.CHANNEL_CODE = model.CHANNEL_CODE;
                dto.CHANNEL_NAME = model.CHANNEL_NAME;
                dto.CHANNEL_DESC = model.CHANNEL_DESC;
                dto.FLAG_ROW = FLAG_ROW.SHOW;
                dto.CREATED_BY = _currentUser.UserName ?? "SYSTEM";
                dto.CREATED_DATE = DateTime.UtcNow;

                _uow.ChannelRepository.Insert(dto);
                _uow.Commit();

                response.Success = true;
                response.MessageType = TOAST_TYPE.SUCCESS;
                response.Message = $"Channel ({model.CHANNEL_CODE}) has been created";
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
        public BusinessResponse Edit(ChannelDto model)
        {
            var response = new BusinessResponse();
            try
            {
                if (string.IsNullOrEmpty(model.CHANNEL_CODE))
                    throw new Exception("Please select some one to editing.");

                string code = model.CHANNEL_CODE;
                var dto = _uow.ChannelRepository.GetByCode(code);
                dto.EMIS_CODE = model.EMIS_CODE;
                dto.CHANNEL_CODE = model.CHANNEL_CODE;
                dto.CHANNEL_NAME = model.CHANNEL_NAME;
                dto.CHANNEL_DESC = model.CHANNEL_DESC;
                dto.FLAG_ROW = model.IS_ACTIVED ? FLAG_ROW.SHOW : FLAG_ROW.DELETE;

                dto.UPDATED_BY = _currentUser.UserName ?? "SYSTEM";
                dto.UPDATED_DATE = DateTime.UtcNow;

                _uow.ChannelRepository.Update(dto);
                _uow.Commit();

                response.Success = true;
                response.MessageType = TOAST_TYPE.SUCCESS;
                response.Message = $"Channel ({model.CHANNEL_CODE}) has been changed";
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
        public BusinessResponse Remove(ChannelDto model)
        {
            var response = new BusinessResponse();
            try
            {
                if (string.IsNullOrEmpty(model.CHANNEL_CODE))
                    throw new Exception("not existing Channel ID");

                string code = model.CHANNEL_CODE;
                var dto = model;
                dto.FLAG_ROW = FLAG_ROW.DELETE;
                dto.UPDATED_BY = _currentUser.UserName ?? "SYSTEM";
                dto.UPDATED_DATE = DateTime.UtcNow;

                if (model.IS_DELETE_PERMANANT)
                {
                    _uow.ChannelRepository.Delete(code);
                }
                else
                {
                    _uow.ChannelRepository.Update(dto);
                }

                _uow.Commit();

                response.Success = true;
                response.MessageType = TOAST_TYPE.SUCCESS;
  
                response.Message = $"Channel ({model.CHANNEL_CODE}) has been deleted";
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
