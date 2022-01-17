using GFCA.APT.BAL.Interfaces;
using GFCA.APT.WEB.CustomAttributes;
using MvcBreadCrumbs;
using System.Web.Mvc;

namespace GFCA.APT.WEB.Controllers
{
    //[Authorizer(Roles = "Root, Administrators, Users")]
    //[Authorizer]
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