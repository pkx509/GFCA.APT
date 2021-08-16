using GFCA.APT.BAL.Interfaces;
using System.Web.Mvc;

namespace GFCA.APT.WEB.Areas.Masters.Controllers
{
    public class ClientController : ControllerWebBase
    {
        public ClientController(ILogService log)
        {

        }
        // GET: Masters/Client
        public ActionResult Index()
        {
            return View();
        }
    }
}