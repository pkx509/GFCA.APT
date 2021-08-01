using GFCA.APT.BAL.Log;
using System.Web.Mvc;

namespace GFCA.APT.WEB.Controllers
{
    public class HomeController : ControllerWebBase
    {
        public HomeController(ILogService logger):base(logger) { }

        public ActionResult Index()
        {
            return View();
        }

    }
}