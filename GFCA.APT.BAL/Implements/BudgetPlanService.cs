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
    //IBudgetPlanService

    public class BudgetPlanService : ServiceBase, IBudgetPlanService
    {
        private readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        #region [ constructor ]
        internal static BudgetPlanService CreateInstant()
        {
            var uow = UnitOfWork.CreateInstant();
            var svc = new BudgetPlanService(uow);

            return svc;
        }
        public BudgetPlanService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
        #endregion [ constructor ]

        #region [ header ]
        public IEnumerable<BudgetPlanHeaderDto> GetHeaderAll()
        {
            try
            {
                var doch = _uow.BudgetPlanRepository.GetBudgetPlanAll();
                return doch;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public FixedContractHeaderDto GetHeaderById(int id)
        {
            try
            {
                var doch = _uow.FixedContractRepository.GetFixedContractAll()
                    .Where(w => w.DOC_FCH_ID == id);
                return doch.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public FixedContractHeaderDto GetHeaderByCode(string code, int ver = -1, int rev = -1)
        {
            try
            {
                
                //FC-YYYYMM-VVRR
                string docCode = code.Substring(0, 9);
                var doch = _uow.FixedContractRepository.GetFixedContractAll()
                    .Where(w => w.DOC_CODE.Contains(code));


                if (ver > 0) //get by specify version/revision
                {
                    string docVersion = $"{docCode}-{ver.ToString("00")}";
                    doch = doch.Where(w => w.DOC_CODE.Contains(docVersion));

                    if (rev > 0)
                    {
                        string docRevision = $"{docCode}-{docVersion}{rev.ToString("00")}";
                        doch = doch.Where(w => w.DOC_CODE.Contains(docRevision));

                    }
                }

                return doch.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BusinessResponse CreateHeader(BudgetPlanHeaderDto model)
        {
            BusinessResponse response = new BusinessResponse(false, MESSAGE_TYPE.WARNING, string.Empty);
            try
            {
                var doch = model;

                var doc = _uow.DocumentRepository.GenerateDocNo("BP", doch.CUST_CODE, System.DateTime.Now.Year, System.DateTime.Now.Month);

              

                var COMPANY = _uow.CompanyRepository.GetByCode(doch.COMP_CODE);
                if (COMPANY == null)
                {
                    throw new Exception("Company");

                }


                var CUSTTOMER = _uow.CustomerRepository.GetByCode(doch.CUST_CODE);
                if (CUSTTOMER == null)
                {
                    throw new Exception("Customer");

                }


                if (doch.FISCAL_YEAR < 2020 || doch.FISCAL_YEAR > 9999)
                {
                    throw new Exception("Year");

                }


                //generate document no

                //generate document no

                doc.DOC_STATUS = DOCUMENT_STATUS.DRAFT;
                doc.FLOW_CURRENT = "";
                doc.FLOW_NEXT = "";
                doc.COMP_CODE = doch.COMP_CODE;
                doc.COMP_NAME = doch.COMP_NAME;
                doc.CUST_CODE = doch.CUST_CODE;
                doc.CUST_NAME = doch.CUST_NAME;
                doc.ORG_CODE = doch.ORG_CODE;
                doc.ORG_NAME = doch.ORG_NAME;
                doc.REQUESTER = "System";
            //_uow.DocumentRepository.Insert(doc);
                doch.DOC_VER = doc.DOC_VER;
                doch.DOC_REV = doc.DOC_REV;
                doch.DOC_CODE = doc.DOC_CODE;
                /*
                doch.DOC_TYPE_CODE = "";
                //doch.DOC_CODE      = "";
                doch.DOC_VER       = doc.DOC_VER.ToString();
                doch.DOC_REV       = doc.DOC_REV.ToString();
                doch.DOC_MONTH     = "";
                doch.DOC_YEAR      = "";
                doch.DOC_STATUS    = "";
                doch.FLOW_CURRENT  = "";
                doch.FLOW_NEXT     = "";
                //doch.REQUESTER     = "";
                //doch.DOC_FCH_ID    = 0;
                doch.CLIENT_CODE   = "";
                doch.CLIENT_NAME   = "";
                doch.CUST_CODE     = "";
                doch.CHANNEL_CODE  = "";
                doch.CHANNEL_NAME  = "";
                doch.FLAG_ROW      = "S";
                */
                doch.CREATED_BY = doc.REQUESTER;
                doch.CREATED_DATE = DateTime.UtcNow;
                _uow.BudgetPlanRepository.InsertBudgetPlanHeaderHeader(doch);

                _uow.Commit();
                response.Success = true;
                response.MessageType = MESSAGE_TYPE.SUCCESS;
                response.Message = string.Empty;
                response.Data = doch;
            }
            catch (Exception ex)
            {
                _uow.Dispose();
                response.Success = false;
                response.MessageType = MESSAGE_TYPE.ERROR;
                response.Message = ex.Message;
                _logger.Error("CreateHeader", ex);
            }

            return response;
        }
        public BusinessResponse EditHeader(FixedContractHeaderDto model)
        {
            BusinessResponse response = new BusinessResponse(false, MESSAGE_TYPE.WARNING, string.Empty);
            try
            {
                var doch = model;

                var doc = _uow.DocumentRepository.GenerateDocNo(doch.DOC_TYPE_CODE, doch.DOC_CODE);
                //generate document no

                GenerateNextState(doch.DOC_STATUS, doch.COMMAND_TYPE, ref doc);

                doc.DOC_STATUS = DOCUMENT_STATUS.DRAFT;
                doc.FLOW_CURRENT = "";
                doc.FLOW_NEXT = "";
                doc.REQUESTER = "System";

                _uow.DocumentRepository.Insert(doc);
                doch.DOC_FCH_ID = doc.DOC_VER;
                doch.DOC_CODE = doc.DOC_CODE;
                
                doch.REQUESTER = doc.REQUESTER;
                doch.CREATED_BY = doc.REQUESTER;
                doch.CREATED_DATE = DateTime.UtcNow;
                _uow.FixedContractRepository.InsertFixedContractHeader(doch);

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
            }

            return response;
        }
        public BusinessResponse RemoveHeader(FixedContractHeaderDto model)
        {
            BusinessResponse response = new BusinessResponse();
            try
            {
                _uow.FixedContractRepository.DeleteFixedContractHeader(model.DOC_FCH_ID);
                
                _uow.Commit();
                response.Success = true;
                response.MessageType = MESSAGE_TYPE.SUCCESS;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.MessageType = MESSAGE_TYPE.ERROR;
                _logger.Error("RemoveHeader : ", ex);
            }

            return response;
        }
        #endregion [ header ]

        #region [ detail ]
        public FixedContractDetailDto GetDetailItem(int DOC_FCD_ID)
        {
            var documentType = "FC";
            FixedContractDto dto = new FixedContractDto(DOC_FCD_ID);
            try
            {
                if (dto.DataMode == PAGE_MODE.CREATING)
                    return dto.DetailItem;

                var docd = _uow.FixedContractRepository.GetDetailItem(DOC_FCD_ID);
                //dto. = docd;
                var doch = _uow.FixedContractRepository.GetFixedContractByItemID(docd.DOC_FCH_ID);
                dto.HeaderData = doch;
                dto.DocumentData = _uow.DocumentRepository.GetDocumentStateFlow(documentType, doch.DOC_FCH_ID);
                dto.HistoryData = _uow.DocumentRepository.GetDocumentHistories(doch.DOC_FCH_ID);

                return docd;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<FixedContractDetailDto> GetDetailItems(int DOC_FCH_ID)
        {
            try
            {
                var docd = _uow.FixedContractRepository.GetDetailItems(DOC_FCH_ID);
                return docd;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<FixedContractDetailDto> GetDetailItems(string code, int ver = -1, int rev = -1)
        {
            try
            {
                /*
                string docCode = code.Substring(0, 9);

                //FC-YYYYMM-VVRR
                var docd = _uow.FixedContractRepository.GetDetailAll()
                    .Where(w => w.DOC_CODE.Contains(docCode));
                

                if (ver > 0) 
                {
                    string docVersion = $"{docCode}-{ver.ToString("00")}";
                    docd = docd.Where(w => w.DOC_CODE.Contains(docVersion));

                    if (rev > 0)
                    {
                        string docRevision = $"{docCode}-{docVersion}{rev.ToString("00")}";
                        docd = docd.Where(w => w.DOC_CODE.Contains(docRevision));

                    }
                }
                */
                var docd = _uow.FixedContractRepository.GetDetailItems(7);
                return docd;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public BusinessResponse CreateDetail(FixedContractDetailDto model)
        {
            BusinessResponse response = new BusinessResponse(false, MESSAGE_TYPE.WARNING, string.Empty);
            try
            {
                
                var docd = model;
                var doch = _uow.FixedContractRepository.GetFixedContractByItemID(docd.DOC_FCH_ID);
                //validate new document
                //docd.DOC_FCD_ID = 1;
                docd.DOC_FCH_ID = doch.DOC_FCH_ID;
                docd.DOC_CODE = doch.DOC_CODE;
                docd.DOC_VER = doch.DOC_VER;
                docd.DOC_REV = doch.DOC_REV;
                docd.ACC_CODE = docd.ACC_CODE;
                docd.CONDITION_TYPE = CONDITION_TYPE.PLANNING;
                docd.CONTRACT_CATE = docd.CONTRACT_CATE;

                docd.CREATED_BY = "System";
                docd.CREATED_DATE = DateTime.UtcNow;
                _uow.FixedContractRepository.InsertFixedContractDetail(docd);

                
                _uow.Commit();
                response.Success = true;
                response.MessageType = MESSAGE_TYPE.SUCCESS;
                response.Message = string.Empty;
                response.Data = docd;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.MessageType = MESSAGE_TYPE.ERROR;
                response.Message = ex.Message;
            }

            return response;
        }
        public BusinessResponse EditDetail(FixedContractDetailDto model)
        {
            BusinessResponse response = new BusinessResponse(false, MESSAGE_TYPE.WARNING, string.Empty);
            try
            {
                var docd = model;

                //validate new document
                docd.DOC_FCD_ID = docd.DOC_FCD_ID;
                docd.DOC_FCH_ID = docd.DOC_FCH_ID;
                docd.DOC_CODE = docd.DOC_CODE;
                docd.CONDITION_TYPE = CONDITION_TYPE.PLANNING;
                //docd.DOC_VER = doch.DOC_VER;
                //docd.DOC_REV = doch.DOC_REV;

                docd.CREATED_BY = "System";
                docd.CREATED_DATE = DateTime.UtcNow;
                _uow.FixedContractRepository.UpdateFixedContractDetail(docd);

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
                _logger.Error("EditDetail : ", ex);
            }

            return response;
        }
        public BusinessResponse RemoveDetail(FixedContractDetailDto model)
        {
            BusinessResponse response = new BusinessResponse();
            try
            {
                _uow.FixedContractRepository.DeleteFixedContractDetail(model.DOC_FCD_ID);
                
                _uow.Commit();
                response.Success = true;
                response.MessageType = MESSAGE_TYPE.SUCCESS;
                response.Message = string.Empty;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.MessageType = MESSAGE_TYPE.ERROR;
                _logger.Error("RemoveDetail : ", ex);
            }

            return response;
        }

     





        public BudgetPlanHeaderDto BudgetPlanHeaderDto(int id)
        {
            try
            {
                var doch = _uow.BudgetPlanRepository.GetBudgetPlanID(id);
                return doch;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BudgetPlanHeaderDto BudgetPlanByID(int DOC_BGH_ID)
        {
            try
            {
                var doch = _uow.BudgetPlanRepository.GetBudgetPlanID(DOC_BGH_ID);
                return doch;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BusinessResponse CreateHeader(PromotionPlanningSaleDto model)
        {
            throw new NotImplementedException();
        }

        public BusinessResponse CreateSalesDetail(BudgetPlanSaleDto model)
        {
            BusinessResponse response = new BusinessResponse(false, MESSAGE_TYPE.WARNING, string.Empty);
            try
            {
                var dto = model;



                dto.DOC_BGH_ID = model.DOC_BGH_ID;
             //  dto.DOC_BGH_SALES_ID = model.DOC_BGH_SALES_ID;
                dto.BRAND_CODE = model.BRAND_CODE;
                dto.PACK_CODE = model.PACK_CODE;
                dto.SIZE_CODE = model.SIZE_CODE;
                dto.PRD_CODE = model.PRD_CODE;
                dto.COST_ELEMENT_CODE = model.COST_ELEMENT_CODE;
                dto.COST_CENTER = model.COST_CENTER;
                dto.YEAR = model.YEAR;
                dto.MONTH = model.MONTH;
                dto.TOTAL = model.TOTAL;
                dto.M1 = model.M1;
                dto.M2 = model.M2;
                dto.M3 = model.M3;
                dto.M4 = model.M4;
                dto.M5 = model.M5;
                dto.M6 = model.M6;
                dto.M7 = model.M7;
                dto.M8 = model.M8;
                dto.M9 = model.M9;
                dto.M10 = model.M10;
                dto.M11 = model.M11;
                dto.M12 = model.M12;
                dto.FLAG_ROW = model.FLAG_ROW;
                dto.CREATED_BY = model.CREATED_BY;
                dto.CREATED_DATE = model.CREATED_DATE;
                dto.UPDATED_BY = model.UPDATED_BY;
                dto.UPDATED_DATE = model.UPDATED_DATE;
                dto.CREATED_BY = _currentUser.UserName ?? "System";
                dto.CREATED_DATE = DateTime.UtcNow;
                _uow.BudgetPlanRepository.InsertBudgetPlanSale(dto);

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
                _logger.Error("CreateHeader", ex);
            }

            return response;
        }

        public BusinessResponse CreateInvestmentDetail(BudgetPlanInvestmentDto model)
        {
            BusinessResponse response = new BusinessResponse(false, MESSAGE_TYPE.WARNING, string.Empty);
            try
            {
                var dto = model;



                dto.DOC_BGH_ID = model.DOC_BGH_ID;
                //  dto.DOC_BGH_SALES_ID = model.DOC_BGH_SALES_ID;
                dto.BRAND_CODE = model.BRAND_CODE;
                dto.PACK_CODE = model.PACK_CODE;
                dto.SIZE_CODE = model.SIZE_CODE;
                dto.PRD_CODE = model.PRD_CODE;
                dto.ACTIVITY_CODE = model.ACTIVITY_CODE;
                dto.COST_ELEMENT_CODE = model.COST_ELEMENT_CODE;
                dto.COST_CENTER = model.COST_CENTER;
                dto.YEAR = model.YEAR;
                dto.MONTH = model.MONTH;
                dto.TOTAL = model.TOTAL;
                dto.M1 = model.M1;
                dto.M2 = model.M2;
                dto.M3 = model.M3;
                dto.M4 = model.M4;
                dto.M5 = model.M5;
                dto.M6 = model.M6;
                dto.M7 = model.M7;
                dto.M8 = model.M8;
                dto.M9 = model.M9;
                dto.M10 = model.M10;
                dto.M11 = model.M11;
                dto.M12 = model.M12;
                dto.FLAG_ROW = model.FLAG_ROW;
                dto.CREATED_BY = model.CREATED_BY;
                dto.CREATED_DATE = model.CREATED_DATE;
                dto.UPDATED_BY = model.UPDATED_BY;
                dto.UPDATED_DATE = model.UPDATED_DATE;
                dto.CREATED_BY = _currentUser.UserName ?? "System";
                dto.CREATED_DATE = DateTime.UtcNow;
                _uow.BudgetPlanRepository.InsertBudgetPlanInvestment(dto);

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
                _logger.Error("CreateInvestmentDetail", ex);
            }

            return response;
        }

        public IEnumerable<BudgetPlanSaleDto> GetDetailSalesItems(int DOC_BGH_ID)
        {
            try
            {
                var doch = _uow.BudgetPlanRepository.GetDetailSalesItems(DOC_BGH_ID);
                return doch;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<BudgetPlanInvestmentDto> GetDetailInvItems(int DOC_BGH_ID)
        {
            try
            {
                var doch = _uow.BudgetPlanRepository.GetDetailInvItems(DOC_BGH_ID);
                return doch;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BudgetPlanSaleDto GetDetailSalesItem(int DOC_BGH_SALES_ID)
        {
            try
            {
                

                var doch = _uow.BudgetPlanRepository.GetDetailSalesItem(DOC_BGH_SALES_ID);
                return doch;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BudgetPlanInvestmentDto GetDetailInvItem(int DOC_BGH_INV_ID)
        {
            try
            {
                var doch = _uow.BudgetPlanRepository.GetDetailInvItem(DOC_BGH_INV_ID);
                return doch;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BusinessResponse EditBudgetPlanSale(BudgetPlanSaleDto model)
        {
          

            BusinessResponse response = new BusinessResponse();
            try
            {
                BudgetPlanSaleDto dto = model;

                dto.UPDATED_BY = _currentUser.UserName ?? "System";
                _uow.BudgetPlanRepository.UpdateBudgetPlanSale(dto);
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
                _logger.Error("EditBudgetPlanSale : ", ex);
            }
            return response;

 
        }

        public BusinessResponse EditBudgetInvsSale(BudgetPlanInvestmentDto model)
        {

            BusinessResponse response = new BusinessResponse();
            try
            {
                BudgetPlanInvestmentDto dto = model;

                dto.UPDATED_BY = _currentUser.UserName ?? "System";
                _uow.BudgetPlanRepository.UpdateBudgetInvsSale(dto);
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
                _logger.Error("EditBudgetInvsSale : ", ex);
            }
            return response;

        }

        public BusinessResponse RemoveBudgetPlanSale(long DOC_BGH_SALES_ID)
        {
            BusinessResponse response = new BusinessResponse();
            try
            {
              
                
                _uow.BudgetPlanRepository.DeleteBudgetPlanSale(DOC_BGH_SALES_ID);
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
                _logger.Error("RemoveBudgetPlanSale : ", ex);
            }
            return response;
        }

        public BusinessResponse RemoveBudgetInvsSale(long DOC_BGH_INV_ID)
        {
            BusinessResponse response = new BusinessResponse();
            try
            {


                _uow.BudgetPlanRepository.DeleteBudgetInvsSale(DOC_BGH_INV_ID);
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
                _logger.Error("RemoveBudgetInvsSale : ", ex);
            }
            return response;
        }




        #endregion [ detail ]


    }
}
