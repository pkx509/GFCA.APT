using GFCA.APT.BAL.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GFCA.APT.WEB.Areas.Transactions.Controllers
{
    public class FixedContractController : ControllerWebBase
    {
        public FixedContractController(ILogService log): base(log) { }
        
        // GET: Transactions/FixedContract
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DocumentItem(int id)
        {
            _logger.Debug($"QueryString is {id}");
            return View();
        }

    }
}