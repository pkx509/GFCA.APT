using System.Web.Mvc;
using GFCA.APT.Domain.Dto;
using Syncfusion.EJ2.Base;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GFCA.APT.BAL.Interfaces;
using GFCA.APT.Domain.Models;
using Newtonsoft.Json;
using System;

namespace GFCA.APT.WEB.Areas.Transactions.Controllers
{
    public class PromotionController : ControllerWebBase
    {
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
        public ViewResult PromotionItem(string DocCode)
        {
            return View();
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

        // GET: T/Promotions/TradeActivity/{DOC_FCH_ID}/{DOC_FCD_ID}]
        [HttpGet]
        public ActionResult PromotionTradeActivityDetail(int DOC_FCH_ID, int DOC_FCD_ID)
        {

            FixedContractDto detailDto = new FixedContractDto();
            try
            {
                detailDto = _biz.FixedContractService.GetDetailItem(DOC_FCD_ID);

            }
            catch
            {

            }

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