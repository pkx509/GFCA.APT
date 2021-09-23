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
    public class ClientService : ServiceBase, IClientService
    {
        private readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        internal static ClientService CreateInstant()
        {
            var uow = UnitOfWork.CreateInstant();
            var svc = new ClientService(uow);

            return svc;
        }

        public ClientService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public IEnumerable<ClientDto> GetAll()
        {
            var dto = _uow.ClientRepository.All();
            return dto;
        }
        public ClientDto GetByCode(string code)
        {
            var dto = _uow.ClientRepository.GetByCode(code);
            return dto;
        }
        public BusinessResponse Create(ClientDto model)
        {
            var response = new BusinessResponse();
            try
            {
                var objDuplicate = _uow.ClientRepository.All().Where(w => w.CLIENT_CODE.Equals(model.CLIENT_CODE)).FirstOrDefault();
                if (objDuplicate != null)
                    throw new Exception("Is duplicate data");

                var dto = new ClientDto();

                dto.CLIENT_CODE = model.CLIENT_CODE;
                dto.CLIENT_NAME = model.CLIENT_NAME;
                dto.CLIENT_DESC = model.CLIENT_DESC;
                dto.FLAG_ROW = FLAG_ROW.SHOW;
                dto.CREATED_BY = _currentUser.UserName ?? "SYSTEM";
                dto.CREATED_DATE = DateTime.UtcNow;

                _uow.ClientRepository.Insert(dto);
                _uow.Commit();

                response.Success = true;
                response.MessageType = TOAST_TYPE.SUCCESS;
                response.Message = $"Client ({model.CLIENT_CODE}) has been created";
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
        public BusinessResponse Edit(ClientDto model)
        {
            var response = new BusinessResponse();
            try
            {
                if (string.IsNullOrEmpty(model.CLIENT_CODE))
                    throw new Exception("Please select some one to editing.");

                string code = model.CLIENT_CODE;
                var dto = _uow.ClientRepository.GetByCode(code);

                dto.CLIENT_CODE = model.CLIENT_CODE;
                dto.CLIENT_NAME = model.CLIENT_NAME;
                dto.CLIENT_DESC = model.CLIENT_DESC;
                dto.FLAG_ROW = model.IS_ACTIVED ? FLAG_ROW.SHOW : FLAG_ROW.DELETE;

                dto.UPDATED_BY = _currentUser.UserName ?? "SYSTEM";
                dto.UPDATED_DATE = DateTime.UtcNow;

                _uow.ClientRepository.Update(dto);
                _uow.Commit();

                response.Success = true;
                response.MessageType = TOAST_TYPE.SUCCESS;
                response.Message = $"Client ({model.CLIENT_CODE}) has been changed";
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
        public BusinessResponse Remove(ClientDto model)
        {
            var response = new BusinessResponse();
            try
            {
                if (string.IsNullOrEmpty(model.CLIENT_CODE))
                    throw new Exception("not existing ClientID");

                string code = model.CLIENT_CODE;
                var dto = model;
                dto.FLAG_ROW = FLAG_ROW.DELETE;
                dto.UPDATED_BY = _currentUser.UserName ?? "SYSTEM";
                dto.UPDATED_DATE = DateTime.UtcNow;

                if (model.IS_DELETE_PERMANANT)
                {
                    _uow.ClientRepository.Delete(code);
                }
                else
                {
                    _uow.ClientRepository.Update(dto);
                }

                _uow.Commit();

                response.Success = true;
                response.MessageType = TOAST_TYPE.SUCCESS;
                response.Message = $"{typeof(ClientService)} has been deleted";
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
