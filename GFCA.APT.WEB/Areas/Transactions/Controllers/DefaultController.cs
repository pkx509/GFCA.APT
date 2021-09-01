using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GFCA.APT.WEB.Areas.Transactions.Controllers
{
    public class DefaultController : ControllerWebBase
    {
        // GET: Transactions/Default
        public ActionResult Index()
        {
            return View();
        }
    }
}