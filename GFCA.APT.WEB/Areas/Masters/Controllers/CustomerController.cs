using System.Web.Mvc;
using GFCA.APT.Domain.Dto;
using Syncfusion.EJ2.Base;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using GFCA.APT.Domain.Models;
using GFCA.APT.BAL.Interfaces;
using System.Reflection;

namespace GFCA.APT.WEB.Areas.Masters.Controllers
{

    public class CustomerController : ControllerWebBase
    {
        private readonly IBusinessProvider _biz;
        public CustomerController(IBusinessProvider biz)
        {
            _biz = biz;
        }

        // GET: M/Customer
        [HttpGet()]
        public ActionResult Index()
        {
            return View();
        }

        //[HttpGet]
        public JsonResult UrlDataSource(DataManagerRequest dm)
        {
            _biz.LogService.Debug("UrlDataSource");
            IEnumerable dataSource = _biz.CustomerService.GetAll();
            DataOperations operation = new DataOperations();
            List<string> str = new List<string>();
            if (dm.Search != null && dm.Search.Count > 0) // Search
            {
                dataSource = operation.PerformSearching(dataSource, dm.Search);
            }
            if (dm.Sorted != null && dm.Sorted.Count > 0) // Sorting
            {
                dataSource = operation.PerformSorting(dataSource, dm.Sorted);
            }
            if (dm.Where != null && dm.Where.Count > 0) // Filtering
            {
                dataSource = operation.PerformFiltering(dataSource, dm.Where, dm.Where[0].Operator);
            }
            int count = dataSource.Cast<CustomerDto>().Count();
            if (dm.Skip != 0) // Paging
            {
                dataSource = operation.PerformSkip(dataSource, dm.Skip);
            }
            if (dm.Take != 0)
            {
                dataSource = operation.PerformTake(dataSource, dm.Take);
            }
            return dm.RequiresCounts ? Json(new { result = dataSource, count = count }) : Json(dataSource);
        }

        [HttpPost]
        public JsonResult Add(CustomerDto value)
        {
            _biz.LogService.Debug("Add");
            dynamic data = new BusinessResponse();

            try
            {
                var biz = _biz.CustomerService.Create(value);
                data = JsonConvert.SerializeObject(biz);
            }
            catch
            {

            }
            return Json(new { data, JsonRequestBehavior.AllowGet });

        }

        //[ValidateAntiForgeryToken]
        [HttpPost]
        public JsonResult Edit(CustomerDto value)
        {
            _biz.LogService.Debug("Edit");
            dynamic data = new BusinessResponse();
            try
            {
                var biz = _biz.CustomerService.Edit(value);
                data = JsonConvert.SerializeObject(biz);
            }
            catch
            {

            }

            return Json(new { data, JsonRequestBehavior.AllowGet });
        }

        [HttpPost]
        public JsonResult Delete(CustomerDto value)
        {
            _biz.LogService.Debug("Delete");
            dynamic data = new BusinessResponse();
            try
            {
                var biz = _biz.CustomerService.Remove(value);
                data = JsonConvert.SerializeObject(biz);
            }
            catch
            {

            }

            return Json(new { data, JsonRequestBehavior.AllowGet });
        }

    }
}