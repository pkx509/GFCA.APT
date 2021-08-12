using GFCA.APT.BAL.Log;
using GFCA.APT.BAL.Parties;
using GFCA.APT.DAL.Implements;
using System.Web.Mvc;
using GFCA.APT.Domain.Dto;
using Syncfusion.EJ2.Base;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using GFCA.APT.DAL.Interfaces;
using System;

namespace GFCA.APT.WEB.Areas.Masters.Controllers
{
    //[TypeFilter(typeof(LogActionFilter), Arguments = new object[] { 10 })]
    public class BrandController : ControllerWebBase
    {
        private readonly IBrandService _brandSvc;
        public BrandController(ILogService log) : base(log)
        {
            _brandSvc = BrandService.CreateInstant(log);
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
            //var service = _brandSvc.GetByID(value.BRAND_ID);
            //ViewBag.dataSource = _brandSvc.GetAll();
            return PartialView("_BrandEditDialog", value);
        }
        
        [HttpPost]
        public PartialViewResult BeforeAdd()
        {
            //var service = _brandSvc.GetAll();
            //ViewBag.dataSource = _brandSvc.GetAll();
            return PartialView("_BrandAddDialog");
        }

        [HttpPost]
        public JsonResult Add(BrandDto value)
        {
            if (!ModelState.IsValid)
                return Json(new { Status = 500, message = "Invalid model", JsonRequestBehavior.AllowGet });

            var isDuplicateCode = _brandSvc.GetAll()
                .FirstOrDefault(o => o.BRAND_CODE.Equals(value.BRAND_CODE));
            if (isDuplicateCode != null)
                return Json(new { Status = 500, data = JsonConvert.SerializeObject(value),  message = "Duplicate Code", JsonRequestBehavior.AllowGet });

            var msg = _brandSvc.Create(value);
            var objData = _brandSvc.GetAll().FirstOrDefault(o => o.BRAND_CODE.Equals(value.BRAND_CODE));
            
            return Json(new { Status = 200, data = JsonConvert.SerializeObject(objData) }, JsonRequestBehavior.AllowGet);
        }
        
        //[ValidateAntiForgeryToken]
        [HttpPost]
        public JsonResult Edit(BrandDto value)
        {
            if (!ModelState.IsValid)
                return Json(new { Status = 500, message = "Invalid model", JsonRequestBehavior.AllowGet });

            _brandSvc.Edit(value);
            return Json(new { Status = 200, data = JsonConvert.SerializeObject(value), JsonRequestBehavior.AllowGet });
        }

        //[ValidateAntiForgeryToken]
        [HttpPost]
        public JsonResult Delete(int key)
        {
            var value = _brandSvc.GetById(key);
            _brandSvc.Delete(value);
            return Json(new { Status = 200, message = JsonConvert.SerializeObject(value), JsonRequestBehavior.AllowGet });
        }

    }
}