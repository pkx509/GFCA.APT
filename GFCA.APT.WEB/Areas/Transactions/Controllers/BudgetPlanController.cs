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

        // GET: T/BudgetPlan
        [HttpGet()]
        public ActionResult Index()
        {
            return View();
        }

    }
}