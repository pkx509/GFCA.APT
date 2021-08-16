using System.Web.Mvc;
using GFCA.APT.Domain.Dto;
using Syncfusion.EJ2.Base;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using GFCA.APT.Domain.Models;
using GFCA.APT.BAL.Interfaces;
using GFCA.APT.BAL.Implements;
using log4net;
using System.Reflection;

namespace GFCA.APT.WEB.Areas.Masters.Controllers
{

    public class BrandController : ControllerWebBase
    {
        private readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IBrandService _brandSvc;
        public BrandController()
        {
            _brandSvc = BrandService.CreateInstant();
        }

        // GET: Masters/Brand
        [HttpGet()]
        public ActionResult Index()
        {
            //ViewBag.dataSource = _brandSvc.GetAll();
            return View();
        }

        //[HttpGet]
        public ActionResult UrlDataSource(DataManagerRequest dm)
        {
            logger.Debug("UrlDataSource");
            IEnumerable DataSource = _brandSvc.GetAll();
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
            int count = DataSource.Cast<BrandDto>().Count();
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
        public PartialViewResult BeforeEdit(BrandDto value)
        {
            logger.Debug("BeforeEdit");
            //var service = _brandSvc.GetByID(value.BRAND_ID);
            //ViewBag.dataSource = _brandSvc.GetAll();
            return PartialView("_BrandEditDialog", value);
        }
        
        [HttpPost]
        public PartialViewResult BeforeAdd()
        {
            logger.Debug("BeforeAdd");
            //var service = _brandSvc.GetAll();
            //ViewBag.dataSource = _brandSvc.GetAll();
            return PartialView("_BrandAddDialog");
        }

        [HttpPost]
        public JsonResult Add(BrandDto value)
        {
            logger.Debug("Add");
            dynamic data = new BusinessResponse();

            try
            {
                var biz = _brandSvc.Create(value);
                data = JsonConvert.SerializeObject(biz);
            }
            catch
            {

            }
            return Json(new { data, JsonRequestBehavior.AllowGet });

        }
        
        //[ValidateAntiForgeryToken]
        [HttpPost]
        public JsonResult Edit(BrandDto value)
        {
            logger.Debug("Edit");
            dynamic data = new BusinessResponse();
            try
            {
                var biz = _brandSvc.Edit(value);
                data = JsonConvert.SerializeObject(biz);
            }
            catch
            {
                
            }

            return Json(new { data, JsonRequestBehavior.AllowGet });
        }

        //[ValidateAntiForgeryToken]
        [HttpPost]
        public JsonResult Delete(int key)
        {
            logger.Debug("Delete");
            dynamic data = new BusinessResponse();
            try
            {
                var value = _brandSvc.GetById(key);
                var biz = _brandSvc.Delete(value);
                data = JsonConvert.SerializeObject(biz);
            }
            catch
            {
                
            }

            return Json(new { data, JsonRequestBehavior.AllowGet });
        }

    }
}