using GFCA.APT.BAL.Log;
using GFCA.APT.BAL.Parties;
using GFCA.APT.DAL;
using GFCA.APT.DAL.Implements;
using GFCA.APT.WEB.Areas.Masters.Data;
using System.Web.Mvc;

namespace GFCA.APT.WEB.Areas.Masters.Controllers
{
    public class BrandController : ControllerWebBase
    {
        private readonly IUnitOfWork _uow;
        private readonly BrandService _brandSvc;
        public BrandController(ILogService log) : base(log)
        {
            _uow = new UnitOfWork();
            _brandSvc = new BrandService(_uow, log);
        }

        // GET: Masters/Brand
        [HttpGet()]
        public ActionResult Index()
        {
            ViewBag.dataSource = _brandSvc.GetAll();
            return View();
        }
        [HttpPost()]
        public PartialViewResult Edit(BrandViewModel value)
        {
            var service = _brandSvc.GetAll();
            ViewBag.dataSource = _brandSvc.GetAll();
            return PartialView("_BrandEditDialog", value);
        }
        [HttpPost()]
        public PartialViewResult Add()
        {
            var service = _brandSvc.GetAll();
            ViewBag.dataSource = _brandSvc.GetAll();
            return PartialView("_BrandAddDialog");
        }

        [HttpPost()]
        public JsonResult Post(BrandViewModel view)
        {

            return Json(new { Status = 200, message = Newtonsoft.Json.JsonConvert.SerializeObject(view) });
        }

        [HttpPut()]
        public JsonResult Put(BrandViewModel view)
        {

            return Json(new { Status = 200, message = Newtonsoft.Json.JsonConvert.SerializeObject(view) });
        }

        [HttpDelete()]
        public JsonResult Delete(BrandViewModel view)
        {

            return Json(new { Status = 200, message = Newtonsoft.Json.JsonConvert.SerializeObject(view) });
        }

    }
}