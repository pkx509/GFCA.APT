using GFCA.APT.BAL.Log;
using GFCA.APT.DAL;
using GFCA.APT.Domain.Models;
using System;
using System.Collections.Generic;

namespace GFCA.APT.BAL.Parties
{
    public class BrandService : BusinessBase
    {
        public BrandService(IUnitOfWork unitOfWork, ILogService log)
            : base(unitOfWork, log) { }

        public IEnumerable<Brand> GetAll()
        {
            return _unitOfWork.Brands.Get();
        }
        public Brand GetById(int Id)
        {
            return _unitOfWork.Brands.GetByID(Id);
        }
        public BusinessResponse Create(Brand model)
        {
            var response = new BusinessResponse();
            try
            {

                model.CreatedBy = _currentUser.UserName;
                model.CreatedDate = DateTime.UtcNow;

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
        public BusinessResponse Edit(Brand model)
        {
            var response = new BusinessResponse();
            try
            {

                model.UpdatedBy = _currentUser.UserName;
                model.UpdatedDate = DateTime.UtcNow;

                _unitOfWork.Brands.Update(model);
                _unitOfWork.Save();

            }
            catch (Exception ex)
            {
                _logger.Error($"{ex.Message}");
            }

            return response;
        }
        public BusinessResponse Delete(Brand model)
        {
            var response = new BusinessResponse();
            try
            {

                model.UpdatedBy = _currentUser.UserName;
                model.UpdatedDate = DateTime.UtcNow;

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
