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
    public class DistributorService : ServiceBase, IDistributorService
    {
        private readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        internal static DistributorService CreateInstant()
        {
            var uow = UnitOfWork.CreateInstant();
            var svc = new DistributorService(uow);

            return svc;
        }

        public DistributorService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public IEnumerable<DistributorDto> GetAll()
        {
            var dto = _uow.DistributorRepository.All();
            return dto;
        }
        public DistributorDto GetById(int Id)
        {
            var dto = _uow.DistributorRepository.GetById(Id);
            return dto;
        }
        public BusinessResponse Create(DistributorDto model)
        {
            var response = new BusinessResponse();
            try
            {
                var objDuplicate = _uow.DistributorRepository.All().Where(w => w.DISTB_CODE.Equals(model.DISTB_CODE)).FirstOrDefault();
                if (objDuplicate != null)
                    throw new Exception("Is duplicate data");

                var dto = new DistributorDto();

                dto.EMIS_ID = model.EMIS_ID;
                dto.DISTB_CODE = model.DISTB_CODE;
                dto.DISTB_NAME = model.DISTB_NAME;
                dto.DISTB_DESC = model.DISTB_DESC;
                dto.FLAG_ROW = FLAG_ROW.SHOW;
                dto.CREATED_BY = _currentUser.UserName ?? "SYSTEM";
                dto.CREATED_DATE = DateTime.UtcNow;

                _uow.DistributorRepository.Insert(dto);
                _uow.Commit();

                response.Success = true;
                response.MessageType = TOAST_TYPE.SUCCESS;
                response.Message = $"Distributor ({model.DISTB_CODE}) has been created";
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
        public BusinessResponse Edit(DistributorDto model)
        {
            var response = new BusinessResponse();
            try
            {
                if (model.DISTB_ID == null || model.DISTB_ID == 0)
                    throw new Exception("Please select some one to editing.");

                int id = model.DISTB_ID ?? 0;
                var dto = _uow.DistributorRepository.GetById(id);

                dto.EMIS_ID = model.EMIS_ID;
                dto.DISTB_CODE = model.DISTB_CODE;
                dto.DISTB_NAME = model.DISTB_NAME;
                dto.DISTB_DESC = model.DISTB_DESC;
                dto.FLAG_ROW = model.IS_ACTIVED ? FLAG_ROW.SHOW : FLAG_ROW.DELETE;

                dto.UPDATED_BY = _currentUser.UserName ?? "SYSTEM";
                dto.UPDATED_DATE = DateTime.UtcNow;

                _uow.DistributorRepository.Update(dto);
                _uow.Commit();

                response.Success = true;
                response.MessageType = TOAST_TYPE.SUCCESS;
                response.Message = $"Distributor ({model.DISTB_CODE}) has been changed";
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
        public BusinessResponse Remove(DistributorDto model)
        {
            var response = new BusinessResponse();
            try
            {
                if (model.DISTB_ID == null || model.DISTB_ID == 0)
                    throw new Exception("not existing Distributor ID");

                int id = model.DISTB_ID ?? 0;
                var dto = model;
                dto.FLAG_ROW = FLAG_ROW.DELETE;
                dto.UPDATED_BY = _currentUser.UserName ?? "SYSTEM";
                dto.UPDATED_DATE = DateTime.UtcNow;

                if (model.IS_DELETE_PERMANANT)
                {
                    _uow.DistributorRepository.Delete(id);
                }
                else
                {
                    _uow.DistributorRepository.Update(dto);
                }

                _uow.Commit();

                response.Success = true;
                response.MessageType = TOAST_TYPE.SUCCESS;
                response.Message = $"{typeof(DistributorService)} has been deleted";
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
