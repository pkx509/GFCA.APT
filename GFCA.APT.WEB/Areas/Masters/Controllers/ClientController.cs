using GFCA.APT.BAL.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GFCA.APT.WEB.Areas.Masters.Controllers
{
    public class ClientController : ControllerWebBase
    {
        public ClientController(ILogService log) : base(log)
        {

        }
        // GET: Masters/Client
        public ActionResult Index()
        {
            return View();
        }
    }
}