using GFCA.APT.BAL.Interfaces;
using GFCA.APT.Domain.Dto;
using GFCA.APT.Domain.Enums;
using GFCA.APT.Domain.Models;
using Newtonsoft.Json;
using Syncfusion.EJ2.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GFCA.APT.WEB.Areas.Transactions.Controllers
{
    public class BudgetPlanController : ControllerWebBase
    {
        private readonly IBusinessProvider _biz;
        public BudgetPlanController(IBusinessProvider biz)
        {
            _biz = biz;
        }

        // GET: T/BudgetPlans
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        // GET: T/BudgetPlans/{DocCode}
        [HttpGet]
        public ViewResult BudgetPlanItem(int DOC_BGH_ID)
        {
            BudgetPlanDto dto = new BudgetPlanDto(DOC_BGH_ID);


            try
            {
                dto.DOC_BGH_ID = DOC_BGH_ID;
                dto.BudgetPlanHeader = _biz.BudgetPlanService.BudgetPlanByID(DOC_BGH_ID);
                dto.DetailSaleData = _biz.BudgetPlanService.GetDetailSalesItems(DOC_BGH_ID);

                dto.DetailInvesmentData = _biz.BudgetPlanService.GetDetailInvItems(DOC_BGH_ID);

                //  dto.DocumentData = _biz.PromotionService.GetDocumentStateSection(DOC_TYPE_CODE, DOC_PROM_PH_ID);
                //  dto.HistoryData = _biz.PromotionService.GetDocumentHistorySection(DOC_TYPE_CODE, DOC_PROM_PH_ID);
                //  dto.RequesterData = _biz.PromotionService.GetDocumentRequesterSection(DOC_TYPE_CODE, DOC_PROM_PH_ID);
                //   dto.WorkflowData = _biz.PromotionService.GetDocumentWorkFlowSection(DOC_TYPE_CODE, DOC_PROM_PH_ID);

                //  dto.OverviewData = _biz.PromotionService.GetPromotionPlanByItemID(DOC_PROM_PH_ID);
                //dto.DetailSaleData = _biz.PromotionService.GetSaleDataByHeaderID(DOC_PROM_PH_ID);
                //dto.DetailInvesmentData = _biz.PromotionService.GetInvestmentByHeaderID(DOC_PROM_PH_ID);
                //   dto.FooterData = _biz.PromotionService.GetPromotionFooterByItemID(DOC_PROM_PH_ID);
            }
            catch
            {

            }
            return View(dto);


        }

        [HttpPost]
        public JsonResult UrlBudgetPlanHeaderList(DataManagerRequest dm)
        {
            _biz.LogService.Debug("UrlFixedContractHeaderList");
            IEnumerable dataSource = _biz.BudgetPlanService.GetHeaderAll();
            DataOperations operation = new DataOperations();
            List<string> str = new List<string>();
            if (dm.Search != null && dm.Search.Count > 0) // Search
            {
                dataSource = operation.PerformSearching(dataSource, dm.Search);
            }
            if (dm.Sorted != null && dm.Sorted.Count > 0) // Sorting
            {
                dataSource = operation.PerformSorting(dataSource, dm.Sorted);
            }
            if (dm.Where != null && dm.Where.Count > 0) // Filtering
            {
                dataSource = operation.PerformFiltering(dataSource, dm.Where, dm.Where[0].Operator);
            }
               int count = dataSource.Cast<BudgetPlanHeaderDto>().Count();
           // int count = dataSource.Cast<FixedContractHeaderDto>().Count();

            if (dm.Skip != 0) // Paging
            {
                dataSource = operation.PerformSkip(dataSource, dm.Skip);
            }
            if (dm.Take != 0)
            {
                dataSource = operation.PerformTake(dataSource, dm.Take);
            }
            return dm.RequiresCounts ? Json(new { result = dataSource, count = count }) : Json(dataSource);
        }

        [HttpPost]
        public ActionResult BudgetPlanSaleDetail(int DOC_BGH_ID, BudgetPlanDto data)
        {
            BudgetPlanDto detailDto = new BudgetPlanDto();
            try
            {

            }
            catch
            {

            }

            return RedirectToAction("BudgetPlanItem", new { DOC_BGH_ID = DOC_BGH_ID });
        }

       
      
        [HttpGet]
        public ActionResult BudgetPlanSaleDetail_backup(int DOC_BGH_ID, int DOC_BGH_SALES_ID, PAGE_MODE PS_MODE = PAGE_MODE.EDITING)
        {
            _biz.LogService.Info("PromotionSaleDetail");
            BudgetPlanDto dto = new BudgetPlanDto(DOC_BGH_ID);
         
            try
            {
                /*

                dto.DocumentData = _biz.PromotionService.GetDocumentStateSection(DOC_TYPE_CODE, DOC_PROM_PH_ID);
                dto.HistoryData = _biz.PromotionService.GetDocumentHistorySection(DOC_TYPE_CODE, DOC_PROM_PH_ID);
                dto.RequesterData = _biz.PromotionService.GetDocumentRequesterSection(DOC_TYPE_CODE, DOC_PROM_PH_ID);
                dto.WorkflowData = _biz.PromotionService.GetDocumentWorkFlowSection(DOC_TYPE_CODE, DOC_PROM_PH_ID);

                dto.OverviewData = _biz.PromotionService.GetPromotionPlanByItemID(DOC_PROM_PH_ID);
                dto.DetailSaleItem = _biz.PromotionService.GetSaleDataByItemID(DOC_PROM_PS_ID);
                if (DOC_PROM_PS_ID != 0)
                    dto.DataMode = PS_MODE;

    
                dto.FooterData = _biz.PromotionService.GetPromotionFooterByItemID(DOC_PROM_PH_ID);
                */

            }
            catch (Exception ex)
            {
                _biz.LogService.Error("BudgetPlanSaleDetail : ", ex);
            }
            return View(dto);
        }

    
 
        // GET: T/Promotions/{DOC_PROM_PH_ID}/I/{DOC_PROM_PI_ID}]
        [HttpGet]
        public ActionResult BudgetPlanSaleDetail(int DOC_BGH_ID, int DOC_BGH_SALES_ID)
        {
            _biz.LogService.Info("PromotionInvestmentDetail");
            BudgetPlanDto dto = new BudgetPlanDto(DOC_BGH_ID);
            try
            {
               // dto.BudgetPlanHeader
                dto.DetailSaleItem = _biz.BudgetPlanService.GetDetailSalesItem(DOC_BGH_SALES_ID);

                if (dto.DetailSaleItem == null)
                    dto.DetailSaleItem = new BudgetPlanSaleDto();


                /*
                dto.DocumentData = _biz.PromotionService.GetDocumentStateSection(DOC_TYPE_CODE, DOC_PROM_PH_ID);
                dto.HistoryData = _biz.PromotionService.GetDocumentHistorySection(DOC_TYPE_CODE, DOC_PROM_PH_ID);
                dto.RequesterData = _biz.PromotionService.GetDocumentRequesterSection(DOC_TYPE_CODE, DOC_PROM_PH_ID);
                dto.WorkflowData = _biz.PromotionService.GetDocumentWorkFlowSection(DOC_TYPE_CODE, DOC_PROM_PH_ID);

                dto.OverviewData = _biz.PromotionService.GetPromotionPlanByItemID(DOC_PROM_PH_ID);
                dto.DetailInvesmentItem = _biz.PromotionService.GetInvestmentByItemID(DOC_PROM_PH_ID, DOC_PROM_PI_ID);

      
                dto.FooterData = _biz.PromotionService.GetPromotionFooterByItemID(DOC_PROM_PH_ID);
                */


            }
            catch (Exception ex)
            {
                _biz.LogService.Error("BudgetPlanSaleDetail : ", ex);
            }

            return View(dto);
        }

        [HttpGet]
        public ActionResult BudgetPlanInvestmentDetail(int DOC_BGH_ID, int DOC_BGH_INV_ID)
        {
            _biz.LogService.Info("BudgetPlanInvestmentDetail");
            BudgetPlanDto dto = new BudgetPlanDto(DOC_BGH_ID);
            try
            {
                dto.DetailInvesmentItem = _biz.BudgetPlanService.GetDetailInvItem(DOC_BGH_INV_ID);
                if (dto.DetailInvesmentItem == null)
                    dto.DetailInvesmentItem = new BudgetPlanInvestmentDto();

                /*
                dto.DocumentData = _biz.PromotionService.GetDocumentStateSection(DOC_TYPE_CODE, DOC_PROM_PH_ID);
                dto.HistoryData = _biz.PromotionService.GetDocumentHistorySection(DOC_TYPE_CODE, DOC_PROM_PH_ID);
                dto.RequesterData = _biz.PromotionService.GetDocumentRequesterSection(DOC_TYPE_CODE, DOC_PROM_PH_ID);
                dto.WorkflowData = _biz.PromotionService.GetDocumentWorkFlowSection(DOC_TYPE_CODE, DOC_PROM_PH_ID);

                dto.OverviewData = _biz.PromotionService.GetPromotionPlanByItemID(DOC_PROM_PH_ID);
                dto.DetailInvesmentItem = _biz.PromotionService.GetInvestmentByItemID(DOC_PROM_PH_ID, DOC_PROM_PI_ID);

      
                dto.FooterData = _biz.PromotionService.GetPromotionFooterByItemID(DOC_PROM_PH_ID);
                */


            }
            catch (Exception ex)
            {
                _biz.LogService.Error("BudgetPlanInvestmentDetail : ", ex);
            }

            return View(dto);
        }

        [HttpPost]
        public JsonResult UrlBudgetPlanSaleList(int DOC_BGH_ID, DataManagerRequest dm)
        {
            _biz.LogService.Info("UrlBudgetPlanSaleList");
            IEnumerable dataSource = _biz.BudgetPlanService.GetDetailSalesItems(DOC_BGH_ID);
            DataOperations operation = new DataOperations();
            List<string> str = new List<string>();
            if (dm.Search != null && dm.Search.Count > 0) // Search
            {
                dataSource = operation.PerformSearching(dataSource, dm.Search);
            }
            if (dm.Sorted != null && dm.Sorted.Count > 0) // Sorting
            {
                dataSource = operation.PerformSorting(dataSource, dm.Sorted);
            }
            if (dm.Where != null && dm.Where.Count > 0) // Filtering
            {
                dataSource = operation.PerformFiltering(dataSource, dm.Where, dm.Where[0].Operator);
            }
            int count = dataSource.Cast<BudgetPlanSaleDto>().Count();
            if (dm.Skip != 0) // Paging
            {
                dataSource = operation.PerformSkip(dataSource, dm.Skip);
            }
            if (dm.Take != 0)
            {
                dataSource = operation.PerformTake(dataSource, dm.Take);
            }
            return dm.RequiresCounts ? Json(new { result = dataSource, count = count }) : Json(dataSource);
        }


        [HttpPost]
        public JsonResult UrlBudgetPlanInvsList(int DOC_BGH_ID, DataManagerRequest dm)
        {
            _biz.LogService.Info("UrlBudgetPlanInvsList");
            IEnumerable dataSource = _biz.BudgetPlanService.GetDetailInvItems(DOC_BGH_ID);
            DataOperations operation = new DataOperations();
            List<string> str = new List<string>();
            if (dm.Search != null && dm.Search.Count > 0) // Search
            {
                dataSource = operation.PerformSearching(dataSource, dm.Search);
            }
            if (dm.Sorted != null && dm.Sorted.Count > 0) // Sorting
            {
                dataSource = operation.PerformSorting(dataSource, dm.Sorted);
            }
            if (dm.Where != null && dm.Where.Count > 0) // Filtering
            {
                dataSource = operation.PerformFiltering(dataSource, dm.Where, dm.Where[0].Operator);
            }
            int count = dataSource.Cast<BudgetPlanInvestmentDto>().Count();
            if (dm.Skip != 0) // Paging
            {
                dataSource = operation.PerformSkip(dataSource, dm.Skip);
            }
            if (dm.Take != 0)
            {
                dataSource = operation.PerformTake(dataSource, dm.Take);
            }
            return dm.RequiresCounts ? Json(new { result = dataSource, count = count }) : Json(dataSource);
        }


        [HttpPost]
        public JsonResult CreateBudgetPlanHeader(BudgetPlanHeaderDto data)
        {
            _biz.LogService.Debug("CreateFixedContractHeader");
            dynamic d = new BusinessResponse();

            try
            {
                var biz = _biz.BudgetPlanService.CreateHeader(data);
                d = JsonConvert.SerializeObject(biz);
            }
            catch
            {

            }
            return Json(new { data = d, JsonRequestBehavior.AllowGet });
        }

        [HttpPost]
        public JsonResult CreateBudgetPlanSale(BudgetPlanSaleDto data)
        {
            _biz.LogService.Info("CreateBudgetPlaningSale");
            string jsonData = string.Empty;
            var bizObj = new BusinessResponse();

            try
            {
               bizObj = _biz.BudgetPlanService.CreateSalesDetail(data);
            }
            catch (Exception ex)
            {
                _biz.LogService.Error("CreateBudgetPlaningSale : ", ex);
            }
            finally
            {
                jsonData = JsonConvert.SerializeObject(bizObj);
            }
            return Json(new { data = jsonData, JsonRequestBehavior.AllowGet });
        }


        [HttpPost]
        public JsonResult EditBudgetPlanSale(BudgetPlanSaleDto data)
        {
            _biz.LogService.Info("EditBudgetPlaningSale");
            string jsonData = string.Empty;
            var bizObj = new BusinessResponse();

            try
            {
                bizObj = _biz.BudgetPlanService.EditBudgetPlanSale(data);
            }
            catch (Exception ex)
            {
                _biz.LogService.Error("CreateBudgetPlaningSale : ", ex);
            }
            finally
            {
                jsonData = JsonConvert.SerializeObject(bizObj);
            }
            return Json(new { data = jsonData, JsonRequestBehavior.AllowGet });
        }

        [HttpPost]
        public JsonResult DeleteBudgetPlanSale(BudgetPlanSaleDto data)
        {
            _biz.LogService.Info("DeleteBudgetPlanSale");
            string jsonData = string.Empty;
            var bizObj = new BusinessResponse();

            try
            {
                bizObj = _biz.BudgetPlanService.RemoveBudgetPlanSale(data.DOC_BGH_SALES_ID);
            }
            catch (Exception ex)
            {
                _biz.LogService.Error("DeleteBudgetPlanSale : ", ex);
            }
            finally
            {
                jsonData = JsonConvert.SerializeObject(bizObj);
            }
            return Json(new { data = jsonData, JsonRequestBehavior.AllowGet });
        }




        [HttpPost]
        public JsonResult CreateBudgetPlanInvestment(BudgetPlanInvestmentDto data)
        {
            _biz.LogService.Info("CreateBudgetPlanInvestment");
            string jsonData = string.Empty;
            var bizObj = new BusinessResponse();

            try
            {
                bizObj = _biz.BudgetPlanService.CreateInvestmentDetail(data);
            }
            catch (Exception ex)
            {
                _biz.LogService.Error("CreateBudgetPlanInvestment : ", ex);
            }
            finally
            {
                jsonData = JsonConvert.SerializeObject(bizObj);
            }
            return Json(new { data = jsonData, JsonRequestBehavior.AllowGet });
        }


        [HttpPost]
        public JsonResult EditBudgetPlanInvestment(BudgetPlanInvestmentDto data)
        {
            _biz.LogService.Info("EditBudgetPlanningInvestment");
            string jsonData = string.Empty;
            var bizObj = new BusinessResponse();

            try
            {
                bizObj = _biz.BudgetPlanService.EditBudgetInvsSale(data);
            }
            catch (Exception ex)
            {
                _biz.LogService.Error("EditBudgetPlanningInvestment : ", ex);
            }
            finally
            {
                jsonData = JsonConvert.SerializeObject(bizObj);
            }
            return Json(new { data = jsonData, JsonRequestBehavior.AllowGet });
        }


        
        [HttpPost]
        public JsonResult DeleteBudgetPlanInvestment(BudgetPlanInvestmentDto data)
        {
            _biz.LogService.Info("DeleteBudgetPlanInvestment");
            string jsonData = string.Empty;
            var bizObj = new BusinessResponse();

            try
            {
                bizObj = _biz.BudgetPlanService.RemoveBudgetInvsSale(data.DOC_BGH_INV_ID);
            }
            catch (Exception ex)
            {
                _biz.LogService.Error("DeleteBudgetPlanInvestment : ", ex);
            }
            finally
            {
                jsonData = JsonConvert.SerializeObject(bizObj);
            }
            return Json(new { data = jsonData, JsonRequestBehavior.AllowGet });
        }
        //DeleteBudgetPlanSale

        public JsonResult ExampleBulkCreate()
        {
            _biz.LogService.Info("ExampleBulkCreate");
            string jsonData = string.Empty;
            var bizObj = new BusinessResponse();

            try
            {
                IList<TableStagingDto> dto = new List<TableStagingDto>();

                dto = GenerateDataFromExcelFile();

                bizObj = _biz.BudgetPlanService.ExampleCreateByBulk(dto);
            }
            catch (Exception ex)
            {
                _biz.LogService.Error("ExampleBulkCreate : ", ex);
            }
            finally
            {
                jsonData = JsonConvert.SerializeObject(bizObj);
            }
            return Json(new { data = jsonData, JsonRequestBehavior.AllowGet });
        }

        private IList<TableStagingDto> GenerateDataFromExcelFile()
        {
            IList<TableStagingDto> dto = new List<TableStagingDto>();
            for (int i = 0; i < 10; i++)
            {
                dto.Add(new TableStagingDto() 
                {
                    ROW_INDEX = i+1,
                    BUDGET_AMOUNT = 100 + i,
                    FISCAL_MONTH = DateTime.Today.Month,
                    FISCAL_YEAR = DateTime.Today.Year,
                    PROD_CODE = $"PCODE-{i}",
                    UPLOAD_BY = "System",
                    UPLOAD_DATE = DateTime.Now,
                });
            }
            
            return dto;

        }

        [HttpGet]
        public PartialViewResult ItemHeaderPartial()
        {
            return PartialView();
        }
        [HttpGet]
        public PartialViewResult ItemDetailPartial()
        {
            return PartialView();
        }
        [HttpGet]
        public PartialViewResult ItemDetailGridSalePartial()
        {
            return PartialView();
        }
        [HttpGet]
        public PartialViewResult ItemDetailGridInvestmentPartial()
        {
            return PartialView();
        }
        [HttpGet]
        public PartialViewResult ItemFooterPartial()
        {
            return PartialView();
        }

    }
}