using GFCA.APT.BAL.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GFCA.APT.WEB.Areas.Masters.Controllers
{
    public class CostCenterController : ControllerWebBase
    {
        public CostCenterController(ILogService log) : base(log)
        {

        }
        // GET: Masters/CostCenter
        public ActionResult Index()
        {
            return View();
        }
    }
}