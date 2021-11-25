using GFCA.APT.BAL.Interfaces;
using GFCA.APT.DAL.Implements;
using GFCA.APT.DAL.Interfaces;
using GFCA.APT.Domain.Dto;
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
            throw new NotImplementedException();
        }
        public PromotionPlanngOverviewDto GetPromotionPlanByItemID(int DOC_PROM_PH_ID)
        {
            throw new NotImplementedException();
        }
        public PromotionPlanningFooterDto GetPromotionFooterByItemID(int DOC_PROM_PH_ID)
        {
            throw new NotImplementedException();
        }
        public BusinessResponse AddOverview(PromotionPlanngOverviewDto entity)
        {
            BusinessResponse response = new BusinessResponse();
            try
            {
                PromotionPlanngOverviewDto model = entity;
                _uow.DocumentRepository.GenerateDocNo("PP", DateTime.Today.Year, DateTime.Today.Month, model.CLIENT_CODE, model.CHANNEL_CODE, model.CUST_CODE);
                _uow.PromotionRepository.InsertOverview(model);
            }
            catch (Exception ex)
            {
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
        public BusinessResponse AddInvestment(PromotionPlanngInvestmentDto entity)
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
        public BusinessResponse AddPlanngSale(PromotionPlanngSaleDto entity)
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