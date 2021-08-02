using GFCA.APT.BAL.Log;
using GFCA.APT.DAL;
using GFCA.APT.Domain.Enums;
using GFCA.APT.Domain.Models;
using System;
using System.Collections.Generic;

namespace GFCA.APT.BAL.Parties
{
    public class BrandService : BusinessBase
    {
        public BrandService(IUnitOfWork unitOfWork, ILogService log)
            : base(unitOfWork, log) { }

        public IEnumerable<TB_M_BRAND> GetAll()
        {
            return _unitOfWork.Brands.Get();
        }
        public TB_M_BRAND GetById(int Id)
        {
            return _unitOfWork.Brands.GetByID(Id);
        }
        public BusinessResponse Create(TB_M_BRAND model)
        {
            var response = new BusinessResponse();
            try
            {
                model.FLAG_ROW = FLAG_ROW.SHOW;
                model.CREATED_BY = _currentUser.UserName;
                model.CREATED_DATE = DateTime.UtcNow;

                _unitOfWork.Brands.Insert(model);
                _unitOfWork.Save();
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
        public BusinessResponse Edit(TB_M_BRAND model)
        {
            var response = new BusinessResponse();
            try
            {

                model.UPDATED_BY = _currentUser.UserName;
                model.UPDATED_DATE = DateTime.UtcNow;

                _unitOfWork.Brands.Update(model);
                _unitOfWork.Save();

            }
            catch (Exception ex)
            {
                _logger.Error($"{ex.Message}");
            }

            return response;
        }
        public BusinessResponse Delete(TB_M_BRAND model)
        {
            var response = new BusinessResponse();
            try
            {
                model.FLAG_ROW = FLAG_ROW.DELETE;
                model.UPDATED_BY = _currentUser.UserName;
                model.UPDATED_DATE = DateTime.UtcNow;

                _unitOfWork.Brands.Update(model);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                _logger.Error($"{ex.Message}");
            }

            return response;
        }

    }
}
