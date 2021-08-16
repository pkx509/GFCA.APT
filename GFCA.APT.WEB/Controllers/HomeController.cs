using GFCA.APT.BAL.Interfaces;
using System.Web.Mvc;

namespace GFCA.APT.WEB.Controllers
{
    public class HomeController : ControllerWebBase
    {
        public HomeController(ILogService logger) { }

        public ActionResult Index()
        {
            return View();
        }

    }
}