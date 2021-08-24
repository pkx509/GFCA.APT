using GFCA.APT.BAL.Interfaces;
using GFCA.APT.Domain.Dto;
using GFCA.APT.Domain.Models;
using Newtonsoft.Json;
using Syncfusion.EJ2.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GFCA.APT.WEB.Areas.Masters.Controllers
{
    public class GLAccountController : ControllerWebBase
    {
        private readonly IBusinessProvider _biz;
        public GLAccountController(IBusinessProvider biz)
        {
            _biz = biz;
        }
         
        [HttpGet()]
        public ActionResult Index()
        { 
            return View();
        }
         
        public JsonResult UrlDataSource(DataManagerRequest dm)
        {
            _biz.LogService.Debug("UrlDataSource");
            IEnumerable dataSource = _biz.GLAccountService.GetAll();
            DataOperations operation = new DataOperations();
            List<string> str = new List<string>();
            if (dm.Search != null && dm.Search.Count > 0)
            {
                dataSource = operation.PerformSearching(dataSource, dm.Search);  //Search
            }
            if (dm.Sorted != null && dm.Sorted.Count > 0) //Sorting
            {
                dataSource = operation.PerformSorting(dataSource, dm.Sorted);
            }
            if (dm.Where != null && dm.Where.Count > 0) //Filtering
            {
                dataSource = operation.PerformFiltering(dataSource, dm.Where, dm.Where[0].Operator);
            }
            int count = dataSource.Cast<GLAccountDto>().Count();
            if (dm.Skip != 0)
            {
                dataSource = operation.PerformSkip(dataSource, dm.Skip);         //Paging
            }
            if (dm.Take != 0)
            {
                dataSource = operation.PerformTake(dataSource, dm.Take);
            }
            return dm.RequiresCounts ? Json(new { result = dataSource, count = count }) : Json(dataSource);
        }

        [HttpPost]
        public JsonResult Add(GLAccountDto value)
        {
            _biz.LogService.Debug("Add");
            dynamic data = new BusinessResponse();

            try
            {
                var biz = _biz.GLAccountService.Create(value);
                data = JsonConvert.SerializeObject(biz);
            }
            catch
            {

            }
            return Json(new { data, JsonRequestBehavior.AllowGet });

        }

        //[ValidateAntiForgeryToken]
        [HttpPost]
        public JsonResult Edit(GLAccountDto value)
        {
            _biz.LogService.Debug("Edit");
            dynamic data = new BusinessResponse();
            try
            {
                var biz = _biz.GLAccountService.Edit(value);
                data = JsonConvert.SerializeObject(biz);
            }
            catch
            {

            }

            return Json(new { data, JsonRequestBehavior.AllowGet });
        }

        [HttpPost]
        public JsonResult Delete(GLAccountDto value)
        {
            _biz.LogService.Debug("Delete");
            dynamic data = new BusinessResponse();
            try
            {
                var biz = _biz.GLAccountService.Remove(value);
                data = JsonConvert.SerializeObject(biz);
            }
            catch
            {

            }

            return Json(new { data, JsonRequestBehavior.AllowGet });
        }

    }
}