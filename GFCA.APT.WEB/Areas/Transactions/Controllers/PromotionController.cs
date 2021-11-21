using GFCA.APT.BAL.Interfaces;
using GFCA.APT.Domain.Dto;
using GFCA.APT.Domain.Enums;
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
        public ViewResult PromotionItem(int DOC_PROM_PS_ID)
        {
            PromotionPlanningDto dto = new PromotionPlanningDto(DOC_PROM_PS_ID);

            try
            {
                
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
        public PartialViewResult ItemDetailGridTradeActivityPartial()
        {
            return PartialView();
        }

        [HttpGet]
        public PartialViewResult ItemDetailGridSummaryPartial()
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