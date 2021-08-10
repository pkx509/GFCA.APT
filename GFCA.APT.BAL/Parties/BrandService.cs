using GFCA.APT.BAL.Log;
using GFCA.APT.DAL;
using GFCA.APT.Domain.Dto;
using GFCA.APT.Domain.Enums;
using GFCA.APT.Domain.Models;
using System;
using System.Collections.Generic;

namespace GFCA.APT.BAL.Parties
{
    public class BrandService : BusinessBase, IBrandService
    {
        public BrandService(IUnitOfWork unitOfWork, ILogService log)
            : base(unitOfWork, log) { }

        public IEnumerable<BrandDto> GetAll()
        {
            var dto = _unitOfWork.Brand.GetAll();
            return dto;
        }
        public BrandDto GetByID(int Id)
        {
            var dto = _unitOfWork.Brand.GetById(Id);
            return dto;
        }
        public BusinessResponse Create(BrandDto model)
        {
            var response = new BusinessResponse();
            try
            {
                var dto = new BrandDto();

                dto.BRAND_ID = 0;
                dto.BRAND_CODE = model.BRAND_CODE;
                dto.BRAND_NAME = model.BRAND_NAME;
                //dto.FLAG_ROW = FLAG_ROW.SHOW;
                dto.CREATED_BY = _currentUser.UserName ?? "System";
                dto.CREATED_DATE = DateTime.UtcNow;

                _unitOfWork.Brand.Insert(dto);
                //_unitOfWork.Brand.Save();

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
                var dto = _unitOfWork.Brand.GetById(id);

                dto.BRAND_CODE = model.BRAND_CODE;
                dto.BRAND_NAME = model.BRAND_NAME;
                //dto.FLAG_ROW = FLAG_ROW.SHOW;

                dto.UPDATED_BY = _currentUser.UserName?? "System";
                dto.UPDATED_DATE = DateTime.UtcNow;
                
                _unitOfWork.Brand.Update(dto);
                //_unitOfWork.Commit();

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
                var dto = _unitOfWork.Brand.GetById(id);

                dto.FLAG_ROW = FLAG_ROW.DELETE;
                dto.UPDATED_BY = _currentUser.UserName ?? "System";
                dto.UPDATED_DATE = DateTime.UtcNow;

                _unitOfWork.Brand.Update(dto);
                //_unitOfWork.Commit();

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
