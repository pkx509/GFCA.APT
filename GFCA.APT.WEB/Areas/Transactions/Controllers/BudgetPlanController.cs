using GFCA.APT.BAL.Interfaces;
using System;
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
        public ViewResult BudgetPlanItem(string DocCode)
        {
            return View();
        }

        [HttpGet]
        public PartialViewResult ItemHeaderPartial(string DocCode)
        {
            return PartialView();
        }
        [HttpGet]
        public PartialViewResult ItemDetailPartial(string DocCode)
        {
            return PartialView();
        }
        [HttpGet]
        public PartialViewResult ItemDetailGridSalePartial(string DocCode)
        {
            return PartialView();
        }
        [HttpGet]
        public PartialViewResult ItemDetailGridInvestmentPartial(string DocCode)
        {
            return PartialView();
        }

    }
}