using GFCA.APT.BAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GFCA.APT.WEB.Areas.Transactions.Controllers
{
    public class DefaultController : ControllerWebBase
    {
        private readonly IBusinessProvider _biz;
        public DefaultController(IBusinessProvider biz)
        {
            _biz = biz;
        }

        // GET: Transactions/Default
        public ActionResult Index()
        {
            _biz.LogService.Debug("Documents List");
            return View();
        }

        // GET: Transactions/Default
        public ActionResult Index(string yyyy)
        {
            _biz.LogService.Debug($"Documents List {yyyy}");
            return View();
        }

        [HttpGet]
        public PartialViewResult ItemHeaderPartial(string DocCode)
        {
            return PartialView();
        }

        [HttpGet]
        public PartialViewResult ItemFooterPartial(string DocCode)
        {
            return PartialView();
        }

    }
}