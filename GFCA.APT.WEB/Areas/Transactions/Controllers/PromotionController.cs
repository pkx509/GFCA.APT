using GFCA.APT.BAL.Interfaces;
using GFCA.APT.Domain.Dto;
using GFCA.APT.Domain.Enums;
using Syncfusion.EJ2.Base;
using System;
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
                //dto.DocumentData = _biz.PromotionService.GetDocumentFlowSection();
                //dto.HistoryData = _biz.PromotionService.GetDocumentHistorySection(DOC_TYPE_CODE, DOC_PROM_PH_ID);
                //dto.RequesterData = _biz.PromotionService.GetDocumentRequesterSection(DOC_TYPE_CODE, DOC_PROM_PH_ID);
                //dto.WorkflowData = _biz.PromotionService.GetDocumentFlowSection(DOC_TYPE_CODE, DOC_PROM_PH_ID);

                dto.OverviewData = _biz.PromotionService.GetPromotionPlanByItemID(DOC_PROM_PH_ID);
                //dto.DetailSaleData = _biz.PromotionService.GetSaleDataByHeaderID(DOC_PROM_PH_ID);
                //dto.DetailInvesmentData = _biz.PromotionService.GetInvestmentByHeaderID(DOC_PROM_PH_ID);
                dto.FooterData = _biz.PromotionService.GetPromotionFooterByItemID(DOC_PROM_PH_ID);
            }
            catch (Exception ex)
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
            return Json(new EmptyResult());
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