using GFCA.APT.BAL.Log;
using GFCA.APT.DAL.Implements;
using GFCA.APT.DAL.Interfaces;
using GFCA.APT.Domain.Dto;
using GFCA.APT.Domain.Enums;
using GFCA.APT.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GFCA.APT.BAL.Parties
{
    public class BrandService : BusinessBase, IBrandService
    {
        public static BrandService CreateInstant(ILogService log)
        {
            var uow = UnitOfWork.CreateInstant();
            var svc = new BrandService(uow, log);

            return svc;
        }

        public BrandService(IUnitOfWork unitOfWork, ILogService log)
            : base(unitOfWork, log) { }

        public IEnumerable<BrandDto> GetAll()
        {
            var dto = _uow.BrandRepository.All();
            return dto;
        }
        public BrandDto GetById(int Id)
        {
            var dto = _uow.BrandRepository.GetById(Id);
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
                dto.BRAND_NAME = model.BRAND_NAME;
                //dto.FLAG_ROW = FLAG_ROW.SHOW;
                dto.CREATED_BY = _currentUser.UserName ?? "System";
                dto.CREATED_DATE = DateTime.UtcNow;

                _uow.BrandRepository.Add(dto);
                _uow.Commit();

                response.Message = $"{typeof(BrandService)} has been created";
            }
            catch (Exception ex)
            {
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
                if (model.BRAND_ID == null || model.BRAND_ID == 0)
                    throw new Exception("not existing BrandID");

                dynamic id = model.BRAND_ID ?? 0;
                var dto = _uow.BrandRepository.GetById(id);

                dto.BRAND_CODE = model.BRAND_CODE;
                dto.BRAND_NAME = model.BRAND_NAME;
                //dto.FLAG_ROW = FLAG_ROW.SHOW;

                dto.UPDATED_BY = _currentUser.UserName?? "System";
                dto.UPDATED_DATE = DateTime.UtcNow;
                
                _uow.BrandRepository.Update(dto);
                _uow.Commit();

                response.Message = $"{typeof(BrandService)} has been changed";
            }
            catch (Exception ex)
            {
                _logger.Error($"{ex.Message}");
            }
            finally
            {
                base.Dispose();
            }

            return response;
        }
        public BusinessResponse Delete(BrandDto model)
        {
            var response = new BusinessResponse();
            try
            {
                if (model.BRAND_ID == null || model.BRAND_ID == 0)
                    throw new Exception("not existing BrandID");

                dynamic id = model.BRAND_ID ?? 0;
                //var dto = _uow.BrandRepository.GetById(id);
                var dto = model;
                dto.FLAG_ROW = FLAG_ROW.DELETE;
                dto.UPDATED_BY = _currentUser.UserName ?? "System";
                dto.UPDATED_DATE = DateTime.UtcNow;

                _uow.BrandRepository.Update(dto);
                _uow.Commit();

                response.Message = $"{typeof(BrandService)} has been deleted";
            }
            catch (Exception ex)
            {
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
