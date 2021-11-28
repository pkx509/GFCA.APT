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
    public class PromotionController : ControllerWebBase
    {
        public const string DOC_TYPE_CODE = "PP";
        private readonly IBusinessProvider _biz;
        public PromotionController(IBusinessProvider biz)
        {
            _biz = biz;
        }

        // GET: Transactions/Promotions
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ViewResult PromotionItem(int DOC_PROM_PH_ID)
        {
            PromotionPlanningDto dto = new PromotionPlanningDto(DOC_PROM_PH_ID);

            try
            {
                dto.DocumentData = _biz.PromotionService.GetDocumentStateSection(DOC_TYPE_CODE, DOC_PROM_PH_ID);
                dto.HistoryData = _biz.PromotionService.GetDocumentHistorySection(DOC_TYPE_CODE, DOC_PROM_PH_ID);
                dto.RequesterData = _biz.PromotionService.GetDocumentRequesterSection(DOC_TYPE_CODE, DOC_PROM_PH_ID);
                dto.WorkflowData = _biz.PromotionService.GetDocumentWorkFlowSection(DOC_TYPE_CODE, DOC_PROM_PH_ID);

                dto.OverviewData = _biz.PromotionService.GetPromotionPlanByItemID(DOC_PROM_PH_ID);
                //dto.DetailSaleData = _biz.PromotionService.GetSaleDataByHeaderID(DOC_PROM_PH_ID);
                //dto.DetailInvesmentData = _biz.PromotionService.GetInvestmentByHeaderID(DOC_PROM_PH_ID);
                dto.FooterData = _biz.PromotionService.GetPromotionFooterByItemID(DOC_PROM_PH_ID);
            }
            catch
            {
             
            }
            return View(dto);
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

        [HttpPost]
        public JsonResult UrlPromotionHeaderList(DataManagerRequest dm)
        {
            _biz.LogService.Debug("UrlPromotionHeaderList");
            IEnumerable dataSource = _biz.PromotionService.GetPromotionPlanAll();
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
            int count = dataSource.Cast<PromotionPlanngOverviewDto>().Count();
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
        public JsonResult CreatePromotionPlanngOverview(PromotionPlanngOverviewDto data)
        {
            _biz.LogService.Debug("CreatePromotionPlanngOverview");
            dynamic d = new BusinessResponse();

            try
            {
                var biz = _biz.PromotionService.CreateOverview(data);
                d = JsonConvert.SerializeObject(biz);
            }
            catch
            {

            }
            return Json(new { data = d, JsonRequestBehavior.AllowGet });
        }

        // GET: T/Promotions/Sale/{DOC_FCH_ID}/{DOC_FCD_ID}]
        [HttpGet]
        public ActionResult PromotionSaleDetail(int DOC_FCH_ID, int DOC_FCD_ID)
        {
            return View();
        }

        // GET: T/Promotions/Investment/{DOC_FCH_ID}/{DOC_FCD_ID}]
        [HttpGet]
        public ActionResult PromotionInvestmentDetail(int DOC_FCH_ID, int DOC_FCD_ID)
        {
            return View();
        }



    }
}