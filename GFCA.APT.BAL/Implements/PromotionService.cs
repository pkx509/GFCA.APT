using GFCA.APT.BAL.Interfaces;
using GFCA.APT.DAL.Implements;
using GFCA.APT.DAL.Interfaces;
using GFCA.APT.Domain.Dto;
using GFCA.APT.Domain.Enums;
using GFCA.APT.Domain.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
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
            IList<PromotionPlanngOverviewDto> result = new List<PromotionPlanngOverviewDto>();
            try
            {
                result = _uow.PromotionRepository.GetPromotionPlanAll().ToList();
            }
            catch (Exception ex)
            {
                _logger.Error("GetPromotionPlanAll : ", ex);
                //throw ex;
            }
            finally
            {
                _uow.Dispose();
            }

            return result;
        }
        public PromotionPlanngOverviewDto GetPromotionPlanByItemID(int DOC_PROM_PH_ID)
        {
            PromotionPlanngOverviewDto result = new PromotionPlanngOverviewDto();
            try
            {
                result = _uow.PromotionRepository.GetPromotionPlanByItemID(DOC_PROM_PH_ID);
            }
            catch (Exception ex)
            {
                _logger.Error("GetPromotionPlanByItemID : ", ex);
                //throw ex;
            }
            finally
            {
                _uow.Dispose();
            }
            return result;
        }
        public PromotionPlanningFooterDto GetPromotionFooterByItemID(int DOC_PROM_PH_ID)
        {
            PromotionPlanningFooterDto result = new PromotionPlanningFooterDto();
            try
            {
                var dto = new PromotionPlanningFooterDto();
            }
            catch (Exception ex)
            {
                _logger.Error("GetPromotionFooterByItemID : ", ex);
                //throw ex;
            }
            finally
            {
                _uow.Dispose();
            }
            return result;
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
                response = BusinessResponse.CreateInstance(MESSAGE_TYPE.SUCCESS);
            }
            catch (Exception ex)
            {
                response = BusinessResponse.CreateInstance(MESSAGE_TYPE.ERROR, ex.Message);
                _logger.Error("CreateOverview : ", ex);
            }
            finally
            {
                _uow.Dispose();
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
                response = BusinessResponse.CreateInstance(MESSAGE_TYPE.SUCCESS);
            }
            catch (Exception ex)
            {
                response = BusinessResponse.CreateInstance(MESSAGE_TYPE.ERROR, ex.Message);
                _logger.Error("EditOverview : ", ex);
            }
            finally
            {
                _uow.Dispose();
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
                response = BusinessResponse.CreateInstance(MESSAGE_TYPE.SUCCESS);
            }
            catch (Exception ex)
            {
                response = BusinessResponse.CreateInstance(MESSAGE_TYPE.ERROR, ex.Message);
                _logger.Error("RemoveOverview : ", ex);
            }
            finally
            {
                _uow.Dispose();
            }
            return response;
        }
        #endregion [ header ]

        #region [ Investment ]
        public IEnumerable<PromotionPlanningInvestmentDto> GetInvestmentByHeaderID(int DOC_PROM_PH_ID)
        {
            IList<PromotionPlanningInvestmentDto> result = new List<PromotionPlanningInvestmentDto>();
            try
            {
                result = _uow.PromotionRepository.GetInvestmentByHeaderID(DOC_PROM_PH_ID).ToList();
            }
            catch (Exception ex)
            {
                _logger.Error("RemoveOverview : ", ex);
                //throw ex;
            }
            finally
            {
                _uow.Dispose();
            }
            return result;
        }
        public PromotionPlanningInvestmentDto GetInvestmentByItemID(int DOC_PROM_PH_ID, int DOC_PROM_PI_ID)
        {
            var result = new PromotionPlanningInvestmentDto();
            try
            {
                result = _uow.PromotionRepository.GetInvestmentByItemID(DOC_PROM_PI_ID);
                if (result == null)
                {
                    result = new PromotionPlanningInvestmentDto();
                    result.DOC_PROM_PH_ID = DOC_PROM_PH_ID;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("GetInvestmentByItemID : ", ex);
                //throw ex;
            }
            finally
            {
                _uow.Dispose();
            }

            return result;
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
                response = BusinessResponse.CreateInstance(MESSAGE_TYPE.SUCCESS);
            }
            catch (Exception ex)
            {
                response = BusinessResponse.CreateInstance(MESSAGE_TYPE.ERROR, ex.Message);
                _logger.Error("CreateInvestment : ", ex);
            }
            finally
            {
                _uow.Dispose();
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
                response = BusinessResponse.CreateInstance(MESSAGE_TYPE.SUCCESS);
            }
            catch (Exception ex)
            {
                response = BusinessResponse.CreateInstance(MESSAGE_TYPE.ERROR, ex.Message);
                _logger.Error("EditInvestment : ", ex);
            }
            finally
            {
                _uow.Dispose();
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
                response = BusinessResponse.CreateInstance(MESSAGE_TYPE.SUCCESS);
            }
            catch (Exception ex)
            {
                response = BusinessResponse.CreateInstance(MESSAGE_TYPE.ERROR, ex.Message);
                _logger.Error("RemoveInvestment : ", ex);
            }
            finally
            {
                _uow.Dispose();
            }
            return response;
        }
        #endregion [ Investment ]

        #region [ Sale ]

        public IEnumerable<PromotionPlanningSaleDto> GetSaleDataByHeaderID(int DOC_PROM_PH_ID)
        {
            IList<PromotionPlanningSaleDto> result = new List<PromotionPlanningSaleDto>();
            try
            {
                result = _uow.PromotionRepository.GetSaleDataByHeaderID(DOC_PROM_PH_ID).ToList();
            }
            catch (Exception ex)
            {
                _logger.Error("GetInvestmentByHeaderID : ", ex);
                //throw ex;
            }
            finally
            {
                _uow.Dispose();
            }
            return result;
        }
        public PromotionPlanningSaleDto GetSaleDataByItemID(int DOC_PROM_PS_ID)
        {
            var result = new PromotionPlanningSaleDto();
            try
            {
                var docds = _uow.PromotionRepository.GetSaleDataByItemID(DOC_PROM_PS_ID);
                if (docds == null)
                    docds = new PromotionPlanningSaleDto();
                return docds;
            }
            catch (Exception ex)
            {
                _logger.Error("GetSaleDataByItemID : ", ex);
                //throw ex;
            }
            finally
            {
                _uow.Dispose();
            }
            return result;
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
                response = BusinessResponse.CreateInstance(MESSAGE_TYPE.SUCCESS);
            }
            catch (Exception ex)
            {
                response = BusinessResponse.CreateInstance(MESSAGE_TYPE.ERROR, ex.Message);
                _logger.Error("CreatePlanngSale : ", ex);
            }
            finally
            {
                _uow.Dispose();
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
                response = BusinessResponse.CreateInstance(MESSAGE_TYPE.SUCCESS);
            }
            catch (Exception ex)
            {
                response = BusinessResponse.CreateInstance(MESSAGE_TYPE.ERROR, ex.Message);
                _logger.Error("EditPlanngSale : ", ex);
            }
            finally
            {
                _uow.Dispose();
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
                response = BusinessResponse.CreateInstance(MESSAGE_TYPE.SUCCESS);
            }
            catch (Exception ex)
            {
                response = BusinessResponse.CreateInstance(MESSAGE_TYPE.ERROR, ex.Message);
                _logger.Error("RemovePlanngSale : ", ex);
            }
            finally
            {
                _uow.Dispose();
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
                response = BusinessResponse.CreateInstance(MESSAGE_TYPE.SUCCESS);
            }
            catch (Exception ex)
            {
                response = BusinessResponse.CreateInstance(MESSAGE_TYPE.ERROR, ex.Message);
                _logger.Error("SubmitPromotionPlanng : ", ex);
            }
            finally
            {
                _uow.Dispose();
            }
            return response;
        }
        public BusinessResponse ApprovePromotionPlanng(int DOC_PROM_PH_ID)
        {
            BusinessResponse response = new BusinessResponse();
            try
            {
                _uow.PromotionRepository.ApprovePromotionPlanng(DOC_PROM_PH_ID);
                _uow.PromotionRepository.ApprovePromotionPlanngInvsDetail(DOC_PROM_PH_ID);
                _uow.PromotionRepository.ApprovePromotionPlanngSaleDetail(DOC_PROM_PH_ID);
                _uow.Commit();
                response = BusinessResponse.CreateInstance(MESSAGE_TYPE.SUCCESS);
            }
            catch (Exception ex)
            {
                response = BusinessResponse.CreateInstance(MESSAGE_TYPE.ERROR, ex.Message);
                _logger.Error("ApprovePromotionPlanng : ", ex);
            }
            finally
            {
                _uow.Dispose();
            }
            return response;
        }
        public IEnumerable<PromotionPlanngOverviewDto> GetPromotionPlanAllByStatus(string DOC_STATUS = "")
        {
            IEnumerable<PromotionPlanngOverviewDto> result = new List<PromotionPlanngOverviewDto>();
            try
            {
                result = _uow.PromotionRepository.GetPromotionPlanAllByStatus(DOC_STATUS);
            }
            catch (Exception ex)
            {
                _logger.Error("ApprovePromotionPlanng : ", ex);
                //throw ex;
            }
            finally
            {
                _uow.Dispose();
            }
            return result;
        }

        public BusinessResponse ApprovePromotionSelect(List<int> Ids)
        {
            BusinessResponse response = new BusinessResponse();
            try
            {

                foreach (int DOC_PROM_PH_ID in Ids)
                {
                    _uow.PromotionRepository.ApprovePromotionPlanng(DOC_PROM_PH_ID);
                    _uow.PromotionRepository.ApprovePromotionPlanngInvsDetail(DOC_PROM_PH_ID);
                    _uow.PromotionRepository.ApprovePromotionPlanngSaleDetail(DOC_PROM_PH_ID);
                    _uow.Commit();
                    response = BusinessResponse.CreateInstance(MESSAGE_TYPE.SUCCESS);
                }
            }
            catch (Exception ex)
            {
                response = BusinessResponse.CreateInstance(MESSAGE_TYPE.ERROR, ex.Message);
                _logger.Error("ApprovePromotionSelect : ", ex);
            }
            finally
            {
                _uow.Dispose();
            }
            return response;
        }
        
        #endregion [ Sale ]

    }
}