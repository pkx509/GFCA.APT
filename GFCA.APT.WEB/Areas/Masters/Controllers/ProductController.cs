using GFCA.APT.BAL.Log;
using GFCA.APT.BAL.Parties;
using GFCA.APT.DAL;
using System.Web.Mvc;
using GFCA.APT.DAL.Implements;

namespace GFCA.APT.WEB.Areas.Masters.Controllers
{
    public class ProductController : ControllerWebBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IBrandService _brandSvc;
        public ProductController(ILogService log) : base(log)
        {
            _uow = new UnitOfWork();
            _brandSvc = new BrandService(_uow, log);
        }
        
      
        public ActionResult Index()
        {
            return View();
        }
    }
}