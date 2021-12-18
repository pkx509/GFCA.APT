using GFCA.APT.BAL.Interfaces;
using GFCA.APT.DAL.Implements;
using GFCA.APT.DAL.Interfaces;
using GFCA.APT.Domain.Dto;
using GFCA.APT.Domain.HTTP.Controls;
using GFCA.APT.Domain.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace GFCA.APT.BAL.Implements
{
    public class PromotionService : ServiceBase, IPromotionService
    {
        private readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        #region [ constructor ]
        internal static PromotionService CreateInstant()
        {
            var uow = UnitOfWork.CreateInstant();
            var svc = new PromotionService(uow);

            return svc;
        }
        public PromotionService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
        #endregion [ constructor ]

        #region [ header ]
        public IEnumerable<PromotionPlanngOverviewDto> GetPromotionPlanAll()
        {
            try
            {
                var doch = _uow.PromotionRepository.GetPromotionPlanAll();
                return doch;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public PromotionPlanngOverviewDto GetPromotionPlanByItemID(int DOC_PROM_PH_ID)
        {
            try
            {
                var docdo = _uow.PromotionRepository.GetPromotionPlanByItemID(DOC_PROM_PH_ID);
                return docdo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public PromotionPlanningFooterDto GetPromotionFooterByItemID(int DOC_PROM_PH_ID)
        {
            try
            {
                var dto = new PromotionPlanningFooterDto();
                return dto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public BusinessResponse CreateOverview(PromotionPlanngOverviewDto entity)
        {
            BusinessResponse response = new BusinessResponse();
            try
            {
                PromotionPlanngOverviewDto dto = entity;
                var dummyDoc = _uow.DocumentRepository.GenerateDocNo(dto.DOC_TYPE_CODE, dto.CUST_CODE, DateTime.Today.Year, DateTime.Today.Month);
                DocumentDto doc = dummyDoc;
                doc.REQUESTER = "System";
                //_uow.DocumentRepository.Insert(doc);

                dto.DOC_CODE = dummyDoc.DOC_CODE;
                dto.DOC_VER = dummyDoc.DOC_VER;
                dto.DOC_REV = dummyDoc.DOC_REV;
                dto.CUST_CODE = dummyDoc.CUST_CODE;
                dto.DOC_STATUS = GFCA.APT.Domain.Enums.DOCUMENT_STATUS.DRAFT;
                //model.CUST_NAME = 
                dto.CREATED_BY = doc.REQUESTER;
                _uow.PromotionRepository.InsertOverview(dto);
                
                _uow.Commit();
                response.Success = true;
                response.MessageType = MESSAGE_TYPE.SUCCESS;
                response.Message = string.Empty;
                response.Data = dto;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.MessageType = MESSAGE_TYPE.ERROR;
                response.Message = ex.Message;
                _logger.Error("CreateOverview : ", ex);
            }

            return response;
        }
        public BusinessResponse EditOverview(PromotionPlanngOverviewDto entity)
        {
            BusinessResponse response = new BusinessResponse();
            try
            {
                PromotionPlanngOverviewDto dto = entity;
                _uow.PromotionRepository.UpdateOverview(dto);

                _uow.Commit();
                response.Success = true;
                response.MessageType = MESSAGE_TYPE.SUCCESS;
                response.Message = string.Empty;
                response.Data = dto;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.MessageType = MESSAGE_TYPE.ERROR;
                response.Message = ex.Message;
                _logger.Error("EditOverview : ", ex);
            }
            return response;
        }
        public BusinessResponse RemoveOverview(int DOC_PROM_PH_ID)
        {
            BusinessResponse response = new BusinessResponse();
            try
            {
                _uow.PromotionRepository.DeleteOverview(DOC_PROM_PH_ID);

                _uow.Commit();
                response.Success = true;
                response.MessageType = MESSAGE_TYPE.SUCCESS;
                response.Message = string.Empty;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.MessageType = MESSAGE_TYPE.ERROR;
                response.Message = ex.Message;
                _logger.Error("RemoveOverview : ", ex);
            }
            return response;
        }
        #endregion [ header ]

        #region [ Investment ]
        public IEnumerable<PromotionPlanningInvestmentDto> GetInvestmentByHeaderID(int DOC_PROM_PH_ID)
        {
            try
            {
                var docdi = _uow.PromotionRepository.GetInvestmentByHeaderID(DOC_PROM_PH_ID);
                return docdi;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public PromotionPlanningInvestmentDto GetInvestmentByItemID(int DOC_PROM_PH_ID, int DOC_PROM_PI_ID)
        {
            try
            {
                var docdi = _uow.PromotionRepository.GetInvestmentByItemID(DOC_PROM_PI_ID);
                if (docdi == null)
                {
                    docdi = new PromotionPlanningInvestmentDto();
                    docdi.DOC_PROM_PH_ID = DOC_PROM_PH_ID;
                    //docdi.DOC_PROM_PS_ID = 
                }
                return docdi;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public BusinessResponse CreateInvestment(PromotionPlanningInvestmentDto entity)
        {
            BusinessResponse response = new BusinessResponse();
            try
            {
                PromotionPlanningInvestmentDto dto = entity;
                dto.CREATED_BY = "System";
                _uow.PromotionRepository.InsertInvestment(dto);
                _uow.Commit();
                response.Success = true;
                response.MessageType = MESSAGE_TYPE.SUCCESS;
                response.Message = string.Empty;
                response.Data = dto;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.MessageType = MESSAGE_TYPE.ERROR;
                response.Message = ex.Message;
                _logger.Error("CreateInvestment : ", ex);
            }
            return response;
        }
        public BusinessResponse EditInvestment(PromotionPlanningInvestmentDto entity)
        {
            BusinessResponse response = new BusinessResponse();
            try
            {
                PromotionPlanningInvestmentDto dto = entity;
                dto.UPDATED_BY = "System";
                _uow.PromotionRepository.UpdateInvestment(dto);
                _uow.Commit();
                response.Success = true;
                response.MessageType = MESSAGE_TYPE.SUCCESS;
                response.Message = string.Empty;
                response.Data = dto;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.MessageType = MESSAGE_TYPE.ERROR;
                response.Message = ex.Message;
                _logger.Error("EditInvestment : ", ex);
            }
            return response;
        }
        public BusinessResponse RemoveInvestment(PromotionPlanningInvestmentDto entity)
        {
            BusinessResponse response = new BusinessResponse();
            try
            {
                _uow.PromotionRepository.DeleteInvestment(entity.DOC_PROM_PI_ID);
                _uow.Commit();
                response.Success = true;
                response.MessageType = MESSAGE_TYPE.SUCCESS;
                response.Message = string.Empty;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.MessageType = MESSAGE_TYPE.ERROR;
                response.Message = ex.Message;
                _logger.Error("RemoveInvestment : ", ex);
            }
            return response;
        }
        #endregion [ Investment ]

        #region [ Sale ]
        public IEnumerable<PromotionPlanningSaleDto> GetSaleDataByHeaderID(int DOC_PROM_PH_ID)
        {
            try
            {
                var docds = _uow.PromotionRepository.GetSaleDataByHeaderID(DOC_PROM_PH_ID);
                return docds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public PromotionPlanningSaleDto GetSaleDataByItemID(int DOC_PROM_PS_ID)
        {
            try
            {
                var docds = _uow.PromotionRepository.GetSaleDataByItemID(DOC_PROM_PS_ID);
                if (docds == null)
                    docds = new PromotionPlanningSaleDto();
                return docds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public BusinessResponse CreatePlanngSale(PromotionPlanningSaleDto entity)
        {
            BusinessResponse response = new BusinessResponse();
            try
            {
                PromotionPlanningSaleDto dto = entity;
                dto.CREATED_BY = "System";
                _uow.PromotionRepository.InsertPlanngSale(dto);
                _uow.Commit();
                response.Success = true;
                response.MessageType = MESSAGE_TYPE.SUCCESS;
                response.Message = string.Empty;
                response.Data = dto;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.MessageType = MESSAGE_TYPE.ERROR;
                response.Message = ex.Message;
                _logger.Error("CreatePlanngSale : ", ex);
            }
            return response;
        }
        public BusinessResponse EditPlanngSale(PromotionPlanningSaleDto entity)
        {
            BusinessResponse response = new BusinessResponse();
            try
            {
                PromotionPlanningSaleDto dto = entity;

                _uow.PromotionRepository.UpdatePlanngSale(dto);
                _uow.Commit();
                response.Success = true;
                response.MessageType = MESSAGE_TYPE.SUCCESS;
                response.Message = string.Empty;
                response.Data = dto;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.MessageType = MESSAGE_TYPE.ERROR;
                response.Message = ex.Message;
                _logger.Error("EditPlanngSale : ", ex);
            }
            return response;
        }
        public BusinessResponse RemovePlanngSale(PromotionPlanningSaleDto entity)
        {
            BusinessResponse response = new BusinessResponse();
            try
            {
                _uow.PromotionRepository.DeletePlanngSale(entity.DOC_PROM_PS_ID);

                _uow.Commit();
                response.Success = true;
                response.MessageType = MESSAGE_TYPE.SUCCESS;
                response.Message = string.Empty;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.MessageType = MESSAGE_TYPE.ERROR;
                response.Message = ex.Message;
                _logger.Error("RemovePlanngSale : ", ex);
            }
            return response;
        }

        public BusinessResponse SubmitPromotionPlanng(int DOC_PROM_PH_ID)
        {
            BusinessResponse response = new BusinessResponse();
            try
            {
                _uow.PromotionRepository.SubmitPromotionPlanng(DOC_PROM_PH_ID);

                _uow.Commit();
                response.Success = true;
                response.MessageType = MESSAGE_TYPE.SUCCESS;
                response.Message = string.Empty;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.MessageType = MESSAGE_TYPE.ERROR;
                response.Message = ex.Message;
                _logger.Error("RemovePlanngSale : ", ex);
            }
            return response;
        }

        public BusinessResponse ApprovePromotionPlanng(int DOC_PROM_PH_ID)
        {
            BusinessResponse response = new BusinessResponse();
            try
            {
                _uow.PromotionRepository.ApprovePromotionPlanng(DOC_PROM_PH_ID);

                _uow.Commit();
                response.Success = true;
                response.MessageType = MESSAGE_TYPE.SUCCESS;
                response.Message = string.Empty;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.MessageType = MESSAGE_TYPE.ERROR;
                response.Message = ex.Message;
                _logger.Error("RemovePlanngSale : ", ex);
            }
            return response;
        }
        #endregion [ Sale ]

    }
}