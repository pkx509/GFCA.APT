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
using System.Text;
using System.Threading.Tasks;

namespace GFCA.APT.BAL.Implements
{
   public  class GLAccountService : ServiceBase, IGLAccountService
    {
        private readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        internal static GLAccountService CreateInstant()
        {
            var uow = UnitOfWork.CreateInstant();
            var svc = new GLAccountService(uow);

            return svc;
        }

        public GLAccountService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public IEnumerable<GLAccountDto> GetAll()
        {
            var dto = _uow.GLAccountRepository.All();
            return dto;
        }
        public GLAccountDto GetById(int Id)
        {
            var dto = _uow.GLAccountRepository.GetById(Id);
            return dto;
        }
        public BusinessResponse Create(GLAccountDto model)
        {
            var response = new BusinessResponse();
            try
            {
                var objDuplicate = _uow.GLAccountRepository.All().Where(w => w.ACC_CODE.Equals(model.ACC_CODE)).FirstOrDefault();
                if (objDuplicate != null)
                    throw new Exception("Is duplicate data");

                var dto = new GLAccountDto();

                dto.IO_ID = model.IO_ID;
                dto.CENTER_ID = model.CENTER_ID;
                dto.FUND_ID = model.FUND_ID;
                dto.FUND_CENTER_ID = model.FUND_CENTER_ID;
                dto.ACC_CODE = model.ACC_CODE;
                dto.ACC_NAME = model.ACC_NAME;
                dto.ACC_TYPE = model.ACC_TYPE;
                dto.ACC_TYPE_DESC = model.ACC_TYPE_DESC;
                dto.ACC_GROUP1 = model.ACC_GROUP1;
                dto.ACC_GROUP1_DESC = model.ACC_GROUP1_DESC;
                dto.ACC_GROUP2 = model.ACC_GROUP2;
                dto.ACC_GROUP2_DESC = model.ACC_GROUP2_DESC;
                dto.ACC_REMARK = model.ACC_REMARK;

                dto.FLAG_ROW = FLAG_ROW.SHOW;
                dto.CREATED_BY = _currentUser.UserName ?? "System";
                dto.CREATED_DATE = DateTime.UtcNow;

                _uow.GLAccountRepository.Insert(dto);
                _uow.Commit();

                response.Success = true;
                response.MessageType = TOAST_TYPE.SUCCESS;
                response.Message = $"GLAccount ({model.ACC_CODE}) has been created";
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

        public BusinessResponse Edit(GLAccountDto model)
        {
            var response = new BusinessResponse();
            try
            {
                if (model.ACC_ID == null || model.ACC_ID == 0)
                    throw new Exception("Please select some one to editing.");
                 
                var dto = _uow.GLAccountRepository.GetById(model.ACC_ID); 
               

                dto.IO_ID = model.IO_ID;
                dto.CENTER_ID = model.CENTER_ID;
                dto.FUND_ID = model.FUND_ID;
                dto.FUND_CENTER_ID = model.FUND_CENTER_ID;
                dto.ACC_CODE = model.ACC_CODE;
                dto.ACC_NAME = model.ACC_NAME;
                dto.ACC_TYPE = model.ACC_TYPE;
                dto.ACC_TYPE_DESC = model.ACC_TYPE_DESC;
                dto.ACC_GROUP1 = model.ACC_GROUP1;
                dto.ACC_GROUP1_DESC = model.ACC_GROUP1_DESC;
                dto.ACC_GROUP2 = model.ACC_GROUP2;
                dto.ACC_GROUP2_DESC = model.ACC_GROUP2_DESC;
                dto.ACC_REMARK = model.ACC_REMARK;

                dto.FLAG_ROW = model.IS_ACTIVED ? FLAG_ROW.SHOW : FLAG_ROW.DELETE;
                dto.UPDATED_BY = _currentUser.UserName ?? "System";
                dto.UPDATED_DATE = DateTime.UtcNow;

                _uow.GLAccountRepository.Update(dto);
                _uow.Commit();

                response.Success = true;
                response.MessageType = TOAST_TYPE.SUCCESS;
                response.Message = $"GL-Account ({model.ACC_CODE}) has been changed";
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
        public BusinessResponse Remove(GLAccountDto model)
        {
            var response = new BusinessResponse();
            try
            {
                if (model.ACC_ID == null || model.ACC_ID == 0)
                    throw new Exception("not existing ACC_ID");
                  
                var dto = model;
                dto.FLAG_ROW = FLAG_ROW.DELETE;
                dto.UPDATED_BY = _currentUser.UserName ?? "System";
                dto.UPDATED_DATE = DateTime.UtcNow;

                if (model.IS_DELETE_PERMANANT)
                {
                    _uow.GLAccountRepository.Delete(model.ACC_ID);
                }
                else
                {
                    _uow.GLAccountRepository.Update(dto);
                }

                _uow.Commit();

                response.Success = true;
                response.MessageType = TOAST_TYPE.SUCCESS;
                response.Message = $"{typeof(GLAccountService)} has been deleted";
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
