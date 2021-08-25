using GFCA.APT.BAL.Implements;
using GFCA.APT.BAL.Interfaces;
using GFCA.APT.Domain.Enums;
using GFCA.APT.Domain.HTTP.Controls;
using log4net;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace GFCA.APT.WEB.Areas.Transactions.Controllers
{
    public class FixedContractController : ControllerWebBase
    {
        private readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        // GET: Transactions/FixedContract
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DocumentItem(int id)
        {
            logger.Debug($"QueryString is {id}");
            return View();
        }

    }
}