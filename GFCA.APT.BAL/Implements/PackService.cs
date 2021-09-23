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
    public class PackService : ServiceBase, IPackService
    {
        private readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        internal static PackService CreateInstant()
        {
            var uow = UnitOfWork.CreateInstant();
            var svc = new PackService(uow);

            return svc;
        }

        public PackService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public IEnumerable<PackDto> GetAll()
        {
            var dto = _uow.PackRepository.All();
            return dto;
        }
        public PackDto GetByCode(String code)
        {
            var dto = _uow.PackRepository.GetByCode(code);
            return dto;
        }
        public BusinessResponse Create(PackDto model)
        {
            var response = new BusinessResponse();
            try
            {
                var objDuplicate = _uow.PackRepository.All().Where(w => w.PACK_CODE.Equals(model.PACK_CODE)).FirstOrDefault();
                if (objDuplicate != null)
                    throw new Exception("Is duplicate data");

                var dto = new PackDto();

                dto.PACK_CODE = model.PACK_CODE;
                dto.PACK_NAME = model.PACK_NAME;
                dto.PACK_DESC = model.PACK_DESC;
                dto.FLAG_ROW = FLAG_ROW.SHOW;
                dto.CREATED_BY = _currentUser.UserName ?? "SYSTEM";
                dto.CREATED_DATE = DateTime.UtcNow;

                _uow.PackRepository.Insert(dto);
                _uow.Commit();

                response.Success = true;
                response.MessageType = TOAST_TYPE.SUCCESS;
                response.Message = $"Pack ({model.PACK_CODE}) has been created";
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
        public BusinessResponse Edit(PackDto model)
        {
            var response = new BusinessResponse();
            try
            {
                if (String.IsNullOrEmpty(model.PACK_CODE))
                    throw new Exception("Please select some one to editing.");

                string code = model.PACK_CODE;
                var dto = _uow.PackRepository.GetByCode(code);

                dto.PACK_CODE = model.PACK_CODE;
                dto.PACK_NAME = model.PACK_NAME;
                dto.PACK_DESC = model.PACK_DESC;
                dto.FLAG_ROW = model.IS_ACTIVED ? FLAG_ROW.SHOW : FLAG_ROW.DELETE;

                dto.UPDATED_BY = _currentUser.UserName ?? "SYSTEM";
                dto.UPDATED_DATE = DateTime.UtcNow;

                _uow.PackRepository.Update(dto);
                _uow.Commit();

                response.Success = true;
                response.MessageType = TOAST_TYPE.SUCCESS;
                response.Message = $"Pack ({model.PACK_CODE}) has been changed";
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
        public BusinessResponse Remove(PackDto model)
        {
            var response = new BusinessResponse();
            try
            {
                if (string.IsNullOrEmpty(model.PACK_CODE))
                    throw new Exception("not existing Pack ID");

                string code = model.PACK_CODE;
                var dto = model;
                dto.FLAG_ROW = FLAG_ROW.DELETE;
                dto.UPDATED_BY = _currentUser.UserName ?? "SYSTEM";
                dto.UPDATED_DATE = DateTime.UtcNow;

                if (model.IS_DELETE_PERMANANT)
                {
                    _uow.PackRepository.Delete(code);
                }
                else
                {
                    _uow.PackRepository.Update(dto);
                }

                _uow.Commit();

                response.Success = true;
                response.MessageType = TOAST_TYPE.SUCCESS;
                response.Message = $"{typeof(PackService)} has been deleted";
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
