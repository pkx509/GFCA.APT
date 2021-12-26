using GFCA.APT.BAL.Interfaces;
using MvcBreadCrumbs;
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

        [BreadCrumb(Clear = false, Order = 0, Label = "Portal")]
        public ActionResult Index()
        {
            return View();
        }

    }
}