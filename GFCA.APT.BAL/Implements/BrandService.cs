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
    public class BrandService : ServiceBase, IBrandService
    {
        private readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        internal static BrandService CreateInstant()
        {
            var uow = UnitOfWork.CreateInstant();
            var svc = new BrandService(uow);

            return svc;
        }

        public BrandService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public IEnumerable<BrandDto> GetAll()
        {
            var dto = _uow.BrandRepository.All();
            return dto;
        }
        public BrandDto GetByCode(string code)
        {
            var dto = _uow.BrandRepository.GetByCode(code);
            return dto;
        }
        public BusinessResponse Create(BrandDto model)
        {
            var response = new BusinessResponse();
            try
            {
                var objDuplicate = _uow.BrandRepository.All().Where(w => w.BRAND_CODE.Equals(model.BRAND_CODE)).FirstOrDefault();
                if (objDuplicate != null)
                    throw new Exception("Is duplicate data");

                var dto = new BrandDto();

                //dto.BRAND_ID = 0;
                dto.BRAND_CODE = model.BRAND_CODE;
                dto.CLIENT_CODE = model.CLIENT_CODE;
                dto.BRAND_NAME = model.BRAND_NAME;
                dto.BRAND_DESC = model.BRAND_DESC;
                dto.FLAG_ROW = FLAG_ROW.SHOW;
                dto.CREATED_BY = _currentUser.UserName ?? "System";
                dto.CREATED_DATE = DateTime.UtcNow;

                _uow.BrandRepository.Insert(dto);
                _uow.Commit();

                response.Success = true;
                response.MessageType = MESSAGE_TYPE.SUCCESS;
                response.Message = $"Brand ({model.BRAND_CODE}) has been created";
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
        public BusinessResponse Edit(BrandDto model)
        {
            var response = new BusinessResponse();
            try
            {
                if (string.IsNullOrEmpty(model.BRAND_CODE))
                    throw new Exception("Please select some one to editing.");

                string code = model.BRAND_CODE;
                var dto = _uow.BrandRepository.GetByCode(code);

                dto.BRAND_CODE = model.BRAND_CODE;
                dto.CLIENT_CODE = model.CLIENT_CODE;
                dto.BRAND_NAME = model.BRAND_NAME;
                dto.BRAND_DESC = model.BRAND_DESC;
                dto.FLAG_ROW = model.IS_ACTIVED ? FLAG_ROW.SHOW : FLAG_ROW.DELETE;

                dto.UPDATED_BY = _currentUser.UserName ?? "System";
                dto.UPDATED_DATE = DateTime.UtcNow;

                _uow.BrandRepository.Update(dto);
                _uow.Commit();

                response.Success = true;
                response.MessageType = MESSAGE_TYPE.SUCCESS;
                response.Message = $"Brand ({model.BRAND_CODE}) has been changed";
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
        public BusinessResponse Remove(BrandDto model)
        {
            var response = new BusinessResponse();
            try
            {
                if (string.IsNullOrEmpty(model.BRAND_CODE))
                    throw new Exception("not existing BrandID");

                string code = model.BRAND_CODE;
                //var dto = _uow.BrandRepository.GetById(id);
                var dto = model;
                dto.FLAG_ROW = FLAG_ROW.DELETE;
                dto.UPDATED_BY = _currentUser.UserName ?? "System";
                dto.UPDATED_DATE = DateTime.UtcNow;

                if (model.IS_DELETE_PERMANANT)
                {
                    _uow.BrandRepository.Delete(code);
                }
                else
                {
                    _uow.BrandRepository.Update(dto);
                }

                _uow.Commit();

                response.Success = true;
                response.MessageType = MESSAGE_TYPE.SUCCESS;
          
                response.Message = $"Brand ({model.BRAND_CODE}) has been deleted";
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

    }
}
