using GFCA.APT.BAL.Interfaces;
using GFCA.APT.Domain.Dto;
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
                dto.BudgetPlanHeader = _biz.BudgetPlanService.BudgetPlanByID(DOC_BGH_ID);
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
        public JsonResult CreateBudgetPlanSale(BudgetPlanDto data)
        {
            _biz.LogService.Info("CreateBudgetPlaningSale");
            string jsonData = string.Empty;
            var bizObj = new BusinessResponse();

            try
            {
               // bizObj = _biz.PromotionService.CreatePlanngSale(data);
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
        public JsonResult CreateBudgetPlanningInvestment(BudgetPlanDto data)
        {
            _biz.LogService.Info("CreateBudgetPlanningInvestment");
            string jsonData = string.Empty;
            var bizObj = new BusinessResponse();

            try
            {
               // bizObj = _biz.PromotionService.CreateInvestment(data);
            }
            catch (Exception ex)
            {
                _biz.LogService.Error("CreateBudgetPlanningInvestment : ", ex);
            }
            finally
            {
                jsonData = JsonConvert.SerializeObject(bizObj);
            }
            return Json(new { data = jsonData, JsonRequestBehavior.AllowGet });
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