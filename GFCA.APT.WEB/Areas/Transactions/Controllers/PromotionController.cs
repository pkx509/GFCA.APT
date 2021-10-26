using GFCA.APT.BAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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