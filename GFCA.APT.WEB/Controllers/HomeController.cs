using GFCA.APT.BAL.Interfaces;
using System.Web.Mvc;

namespace GFCA.APT.WEB.Controllers
{
    public class HomeController : ControllerWebBase
    {
        private readonly IBusinessProvider _biz;
        public HomeController(IBusinessProvider biz) 
        {
            _biz = biz;
        }

        public ActionResult Index()
        {
            return View();
        }

    }
}