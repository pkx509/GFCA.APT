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
            throw new NotImplementedException();
        }
        public PromotionPlanningFooterDto GetPromotionFooterByItemID(int DOC_PROM_PH_ID)
        {
            throw new NotImplementedException();
        }
        public BusinessResponse CreateOverview(PromotionPlanngOverviewDto entity)
        {
            BusinessResponse response = new BusinessResponse();
            try
            {
                PromotionPlanngOverviewDto model = entity;
                var dummyDoc = _uow.DocumentRepository.GenerateDocNo(model.DOC_TYPE_CODE, model.CUST_CODE, DateTime.Today.Year, DateTime.Today.Month);
                DocumentDto doc = dummyDoc;
                doc.REQUESTER = "System";
                //_uow.DocumentRepository.Insert(doc);

                model.DOC_CODE = dummyDoc.DOC_CODE;
                model.DOC_VER = dummyDoc.DOC_VER;
                model.DOC_REV = dummyDoc.DOC_REV;
                model.CUST_CODE = dummyDoc.CUST_CODE;
                //model.CUST_NAME = 
                model.CREATED_BY = doc.REQUESTER;
                _uow.PromotionRepository.InsertOverview(model);
                
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
                _logger.Error("PromotionService : ", ex);
            }

            return response;
        }
        public BusinessResponse EditOverview(PromotionPlanngOverviewDto entity)
        {
            BusinessResponse response = new BusinessResponse();
            try
            {
                PromotionPlanngOverviewDto model = entity;
            }
            catch (Exception ex)
            {
                _logger.Error("EditOverview : ", ex);
            }
            return response;
        }
        public BusinessResponse RemoveOverview(int DOC_PROM_PH_ID)
        {
            BusinessResponse response = new BusinessResponse();
            try
            {

            }
            catch (Exception ex)
            {
                _logger.Error("RemoveOverview : ", ex);
            }
            return response;
        }
        #endregion [ header ]

        #region [ Investment ]
        public IEnumerable<PromotionPlanngInvestmentDto> GetInvestmentByHeaderID(int DOC_PROM_PH_ID)
        {
            throw new NotImplementedException();
        }
        public PromotionPlanngInvestmentDto GetInvestmentByItemID(int DOC_PROM_PI_ID)
        {
            throw new NotImplementedException();
        }
        public BusinessResponse CreateInvestment(PromotionPlanngInvestmentDto entity)
        {
            BusinessResponse response = new BusinessResponse();
            try
            {
                PromotionPlanngInvestmentDto model = entity;
            }
            catch (Exception ex)
            {
                _logger.Error("AddInvestment : ", ex);
            }
            return response;
        }
        public BusinessResponse EditInvestment(PromotionPlanngInvestmentDto entity)
        {
            BusinessResponse response = new BusinessResponse();
            try
            {
                PromotionPlanngInvestmentDto model = entity;
            }
            catch (Exception ex)
            {
                _logger.Error("EditInvestment : ", ex);
            }
            return response;
        }
        public BusinessResponse RemoveInvestment(int DOC_PROM_PI_ID)
        {
            BusinessResponse response = new BusinessResponse();
            try
            {

            }
            catch (Exception ex)
            {
                _logger.Error("RemoveInvestment : ", ex);
            }
            return response;
        }
        #endregion [ Investment ]

        #region [ Sale ]
        public IEnumerable<PromotionPlanngSaleDto> GetSaleDataByHeaderID(int DOC_PROM_PH_ID)
        {
            throw new NotImplementedException();
        }
        public PromotionPlanngSaleDto GetSaleDataByItemID(int DOC_PROM_PS_ID)
        {
            throw new NotImplementedException();
        }
        public BusinessResponse CreatePlanngSale(PromotionPlanngSaleDto entity)
        {
            BusinessResponse response = new BusinessResponse();
            try
            {
                PromotionPlanngSaleDto model = entity;
            }
            catch (Exception ex)
            {
                _logger.Error("AddPlanngSale : ", ex);
            }
            return response;
        }
        public BusinessResponse EditPlanngSale(PromotionPlanngSaleDto entity)
        {
            BusinessResponse response = new BusinessResponse();
            try
            {
                PromotionPlanngSaleDto model = entity;
            }
            catch (Exception ex)
            {
                _logger.Error("EditPlanngSale : ", ex);
            }
            return response;
        }
        public BusinessResponse RemovePlanngSale(int DOC_PROM_PS_ID)
        {
            BusinessResponse response = new BusinessResponse();
            try
            {

            }
            catch (Exception ex)
            {
                _logger.Error("RemovePlanngSale : ", ex);
            }
            return response;
        }
        #endregion [ Sale ]

    }
}