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
    public class TradeActivityService : ServiceBase, ITradeActivityService
    {

        private readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #region [ Constructor ]
        internal static TradeActivityService CreateInstant()
        {
            var uow = UnitOfWork.CreateInstant();
            var svc = new TradeActivityService(uow);

            return svc;
        }
        public TradeActivityService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
        #endregion

        public IEnumerable<TradeActivityDto> GetAll()
        {
            var dto = _uow.TradeActivityRepository.All();
            return dto;
        }

        public TradeActivityDto GetByCode(string code)
        {
            var dto = _uow.TradeActivityRepository.GetByCode(code);
            return dto;
        }

        public BusinessResponse Create(TradeActivityDto model)
        {
            var response = new BusinessResponse();
            try
            {
                //start process
                var data = model;
                var objDuplicated = _uow.TradeActivityRepository.All()
                                    .Where(o => o.ACTIVITY_CODE.Equals(model.ACTIVITY_CODE))
                                    .FirstOrDefault();
                if (objDuplicated != null)
                    throw new DataDuplicateException(objDuplicated.ACTIVITY_CODE);

                
                _uow.TradeActivityRepository.Insert(model);
                _uow.Commit();

                //end process
                response.Data = data;
                response.Success = true;
                response.Message = "TradeActivity has been created";
                response.MessageType = MESSAGE_TYPE.SUCCESS;
                _logger.Info(response.Message);
            }
            catch (DataDuplicateException ex)
            {
                _logger.Debug(model);
                _logger.Debug($"Error while process: {ex}");
                response.Success = false;
                response.MessageType = MESSAGE_TYPE.WARNING;
                response.Message = ex.Message;
            }
            catch (Exception ex)
            {
                _logger.Debug(model);
                _logger.Error($"Error while process: {ex}");
                response.Success = false;
                response.MessageType = MESSAGE_TYPE.ERROR;
                response.Message = ex.Message;
            }
            finally
            {
                this.Dispose();
            }

            return response;
        }

        public BusinessResponse Edit(TradeActivityDto model)
        {
            var response = new BusinessResponse();
            try
            {
                //start process
                if (string.IsNullOrEmpty(model.ACTIVITY_CODE))
                    throw new DataNoSelectionException();

                model.FLAG_ROW = model.IS_ACTIVED ? FLAG_ROW.SHOW : FLAG_ROW.DELETE;
                var data = model;
                _uow.TradeActivityRepository.Update(model);
                _uow.Commit();

                //end process
                response.Data = data;
                response.Success = true;
                response.MessageType = MESSAGE_TYPE.SUCCESS;
                response.Message = "TradeActivity has been updated";
                _logger.Info(response.Message);
            }
            catch (DataNoSelectionException ex)
            {
                _logger.Debug(model);
                _logger.Error($"Error while process: {ex}");
                response.Success = false;
                response.MessageType = MESSAGE_TYPE.WARNING;
                response.Message = ex.Message;
            }
            catch (Exception ex)
            {
                _logger.Debug(model);
                _logger.Error($"Error while process: {ex}");
                response.Success = false;
                response.MessageType = MESSAGE_TYPE.ERROR;
                response.Message = ex.Message;
            }
            finally
            {
                this.Dispose();
            }

            return response;
        }

        public BusinessResponse Remove(TradeActivityDto model)
        {
            var response = new BusinessResponse();
            try
            {

                //start process
                var data = model;
                if (model.IS_DELETE)
                {
                    string code = model.ACTIVITY_CODE;
                    _uow.TradeActivityRepository.Delete(code);
                }
                else
                {
                    if (model.FLAG_ROW == FLAG_ROW.DELETE)
                    {
                        _uow.TradeActivityRepository.Update(model);
                    }
                }
                _uow.Commit();

                //end process
                response.Data = data;
                response.Success = true;
                response.MessageType = MESSAGE_TYPE.SUCCESS;
                response.Message = "TradeActivity has been deleted";
                _logger.Info(response.Message);
            }
            catch (Exception ex)
            {
                _logger.Debug(model);
                _logger.Error($"Error while process: {ex}");
                response.Success = false;
                response.MessageType = MESSAGE_TYPE.ERROR;
                response.Message = ex.Message;
            }
            finally
            {
                this.Dispose();
            }

            return response;
        }
    }
}
