using GFCA.APT.BAL.Interfaces;
using log4net;
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