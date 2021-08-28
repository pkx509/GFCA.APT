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
    public class BudgetTypeService : ServiceBase, IBudgetTypeService
    {
        private readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        internal static BudgetTypeService CreateInstant()
        {
            var uow = UnitOfWork.CreateInstant();
            var svc = new BudgetTypeService(uow);

            return svc;
        }

        public BudgetTypeService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public IEnumerable<BudgetTypeDto> GetAll()
        {
            var dto = _uow.BudgetTypeRepository.All();
            return dto;
        }
        public BudgetTypeDto GetById(int Id)
        {
            var dto = _uow.BudgetTypeRepository.GetById(Id);
            return dto;
        }
        public BusinessResponse Create(BudgetTypeDto model)
        {
            var response = new BusinessResponse();
            try
            {
                var objDuplicate = _uow.BudgetTypeRepository.All().Where(w => w.BG_TYPE_CODE.Equals(model.BG_TYPE_CODE)).FirstOrDefault();
                if (objDuplicate != null)
                    throw new Exception("Is duplicate data");

                var dto = new BudgetTypeDto();

                dto.BG_TYPE_CODE = model.BG_TYPE_CODE;
                dto.BG_TYPE_NAME = model.BG_TYPE_NAME;
                dto.BG_TYPE_DESC = model.BG_TYPE_DESC;
                dto.FLAG_ROW = FLAG_ROW.SHOW;
                dto.CREATED_BY = _currentUser.UserName ?? "System";
                dto.CREATED_DATE = DateTime.UtcNow;

                _uow.BudgetTypeRepository.Insert(dto);
                _uow.Commit();

                response.Success = true;
                response.MessageType = TOAST_TYPE.SUCCESS;
                response.Message = $"Budget type ({model.BG_TYPE_CODE}) has been created";
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
        public BusinessResponse Edit(BudgetTypeDto model)
        {
            var response = new BusinessResponse();
            try
            {
                if (model.BG_TYPE_ID == null || model.BG_TYPE_ID == 0)
                    throw new Exception("Please select some one to editing.");

                int id = model.BG_TYPE_ID ?? 0;
                var dto = _uow.BudgetTypeRepository.GetById(id);

                dto.BG_TYPE_CODE = model.BG_TYPE_CODE;
                dto.BG_TYPE_NAME = model.BG_TYPE_NAME;
                dto.BG_TYPE_DESC = model.BG_TYPE_DESC;
                dto.FLAG_ROW = model.IS_ACTIVED ? FLAG_ROW.SHOW : FLAG_ROW.DELETE;

                dto.UPDATED_BY = _currentUser.UserName ?? "System";
                dto.UPDATED_DATE = DateTime.UtcNow;

                _uow.BudgetTypeRepository.Update(dto);
                _uow.Commit();

                response.Success = true;
                response.MessageType = TOAST_TYPE.SUCCESS;
                response.Message = $"Budget type ({model.BG_TYPE_CODE}) has been changed";
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
        public BusinessResponse Remove(BudgetTypeDto model)
        {
            var response = new BusinessResponse();
            try
            {
                if (model.BG_TYPE_ID == null || model.BG_TYPE_ID == 0)
                    throw new Exception("not existing Budget type ID");

                int id = model.BG_TYPE_ID ?? 0;
                var dto = model;
                dto.FLAG_ROW = FLAG_ROW.DELETE;
                dto.UPDATED_BY = _currentUser.UserName ?? "System";
                dto.UPDATED_DATE = DateTime.UtcNow;

                if (model.IS_DELETE_PERMANANT)
                {
                    _uow.BudgetTypeRepository.Delete(id);
                }
                else
                {
                    _uow.BudgetTypeRepository.Update(dto);
                }

                _uow.Commit();

                response.Success = true;
                response.MessageType = TOAST_TYPE.SUCCESS;
                response.Message = $"{typeof(BudgetTypeService)} has been deleted";
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
