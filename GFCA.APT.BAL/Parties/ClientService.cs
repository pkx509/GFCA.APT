using GFCA.APT.BAL.Log;
using GFCA.APT.DAL.Interfaces;
using System;

namespace GFCA.APT.BAL.Parties
{
    public class ClientService : BusinessBase, IDisposable
    {
        public ClientService(IUnitOfWork unitOfWork, ILogService logger)
            : base(unitOfWork, logger)
        {

        }

        /*
        public IEnumerable<Client> GetAll()
        {
            return _unitOfWork.Clients.Get();
        }
        public Client GetById(int Id)
        {
            return _unitOfWork.Clients.GetByID(Id);
        }
        public BusinessResponse Create(Client model)
        {
            var response = new BusinessResponse();
            try
            {

                model.CreatedBy = _currentUser.UserName;
                model.CreatedDate = DateTime.UtcNow;

                _unitOfWork.Clients.Insert(model);
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

                _unitOfWork.Clients.Update(model);
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

                _unitOfWork.Clients.Update(model);
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
