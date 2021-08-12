using GFCA.APT.BAL.Log;
using GFCA.APT.BAL.Warehouses;
using GFCA.APT.DAL;
using GFCA.APT.DAL.Implements;
using GFCA.APT.Domain.Dto;
using Newtonsoft.Json;
using Syncfusion.EJ2.Base;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace GFCA.APT.WEB.Areas.Masters.Controllers
{
    public class ProductController : ControllerWebBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IProductService _productSvc;
        public ProductController(ILogService log) : base(log)
        {
            _uow = new UnitOfWork();
            _productSvc = new ProductService(_uow, log);
        }

        [HttpGet()]
        public ActionResult Index()
        {
            //ViewBag.dataSource = _productSvc.GetAll();
        

            return View();
        }

        //[HttpGet]
        public ActionResult UrlDataSource(DataManagerRequest dm)
        {
            IEnumerable DataSource = _productSvc.GetAll();
            DataOperations operation = new DataOperations();
            List<string> str = new List<string>();
            if (dm.Search != null && dm.Search.Count > 0)
            {
                DataSource = operation.PerformSearching(DataSource, dm.Search);  //Search
            }
            if (dm.Sorted != null && dm.Sorted.Count > 0) //Sorting
            {
                DataSource = operation.PerformSorting(DataSource, dm.Sorted);
            }
            if (dm.Where != null && dm.Where.Count > 0) //Filtering
            {
                DataSource = operation.PerformFiltering(DataSource, dm.Where, dm.Where[0].Operator);
            }
            int count = DataSource.Cast<ProductDto>().Count();
            if (dm.Skip != 0)
            {
                DataSource = operation.PerformSkip(DataSource, dm.Skip);         //Paging
            }
            if (dm.Take != 0)
            {
                DataSource = operation.PerformTake(DataSource, dm.Take);
            }
            return dm.RequiresCounts ? Json(new { result = DataSource, count = count }) : Json(DataSource);
        }

        [HttpPost]
        public PartialViewResult BeforeEdit(ProductDto value)
        {
            //var service = _brandSvc.GetByID(value.BRAND_ID);
            //ViewBag.dataSource = _brandSvc.GetAll();
            return PartialView("_ProductEditDialog", value);
        }

        [HttpPost]
        public PartialViewResult BeforeAdd()
        {
            //var service = _brandSvc.GetAll();
            //ViewBag.dataSource = _brandSvc.GetAll();
            return PartialView("_ProductAddDialog");
        }


        //[AcceptVerbs(HttpVerbs.Post)]
        [HttpPost]
        public JsonResult Add(ProductDto value)
        {
            if (!ModelState.IsValid)
                return Json(new { Status = 500, message = "Invalid model", JsonRequestBehavior.AllowGet });

            var isDuplicateCode = _productSvc.GetAll()
                .FirstOrDefault(o => o.PROD_CODE.Equals(value.PROD_CODE));
            if (isDuplicateCode != null)
                return Json(new { Status = 500, data = JsonConvert.SerializeObject(value), message = "Duplicate Code", JsonRequestBehavior.AllowGet });

            _productSvc.Create(value);

            var objData = _productSvc.GetAll().FirstOrDefault(o => o.PROD_CODE.Equals(value.PROD_CODE));

            return Json(new { Status = 200, data = JsonConvert.SerializeObject(objData) }, JsonRequestBehavior.AllowGet);
        }

    }
}