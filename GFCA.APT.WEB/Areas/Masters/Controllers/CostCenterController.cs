using GFCA.APT.BAL.Interfaces;
using System.Web.Mvc;

namespace GFCA.APT.WEB.Areas.Masters.Controllers
{
    public class CostCenterController : ControllerWebBase
    {
        public CostCenterController(ILogService log)
        {

        }
        // GET: Masters/CostCenter
        public ActionResult Index()
        {
            return View();
        }
    }
}