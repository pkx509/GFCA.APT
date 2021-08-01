using GFCA.APT.BAL.Log;
using GFCA.APT.DAL;
using GFCA.APT.Domain.DTO;
using GFCA.APT.Domain.Models;
using log4net;
using System;
using System.Collections.Generic;

namespace GFCA.APT.BAL.Parties
{
    public class CustomerService : BusinessBase
    {
        public CustomerService(IUnitOfWork unitOfWork, ILogService logger)
            : base(unitOfWork, logger) { }

        public object Handler(Customer payload, UserProfile currentUser)
        {

            try
            {
                //var c = _unitOfWork.Customer.Get();

                //_unitOfWork.Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _unitOfWork.Dispose();
            }

            return 1;
        }

        /*
        public IEnumerable<Client> GetAll()
        {
            return _unitOfWork.Customer.Get();
        }
        public Client GetById(int Id)
        {
            return _unitOfWork.Customer.GetByID(Id);
        }
        public BusinessResponse Create(Client model)
        {
            var response = new BusinessResponse();
            try
            {

                model.CreatedBy = _currentUser.UserName;
                model.CreatedDate = DateTime.UtcNow;

                _unitOfWork.Customer.Insert(model);
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
        public BusinessResponse Edit(Client model)
        {
            var response = new BusinessResponse();
            try
            {

                model.UpdatedBy = _currentUser.UserName;
                model.UpdatedDate = DateTime.UtcNow;

                _unitOfWork.Customer.Update(model);
                _unitOfWork.Save();

            }
            catch (Exception ex)
            {
                _logger.Error($"{ex.Message}");
            }

            return response;
        }
        public BusinessResponse Delete(Client model)
        {
            var response = new BusinessResponse();
            try
            {

                model.UpdatedBy = _currentUser.UserName;
                model.UpdatedDate = DateTime.UtcNow;

                _unitOfWork.Customer.Update(model);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                _logger.Error($"{ex.Message}");
            }

            return response;
        }
    */
    }
}
