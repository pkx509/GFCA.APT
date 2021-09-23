using GFCA.APT.BAL.Interfaces;
using GFCA.APT.Domain.Dto;
using GFCA.APT.Domain.Models;
using Newtonsoft.Json;
using Syncfusion.EJ2.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace GFCA.APT.WEB.Areas.Masters.Controllers
{
    public class TradeActivityController : ControllerWebBase
    {
        private readonly IBusinessProvider _biz;
        public TradeActivityController(IBusinessProvider biz)
        {
            _biz = biz;
        }

        public ActionResult Index()
        {
            return View();
        }


        public JsonResult UrlDataSource(DataManagerRequest dm)
        {
            _biz.LogService.Debug("UrlDataSource");
            IEnumerable<TradeActivityDto> dataSource;
            int count = 0;
            DataOperations operation = new DataOperations();

            try
            {
                dataSource = _biz.TradeActivityService.GetAll();
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
                count = dataSource.Cast<TradeActivityDto>().Count();
                if (dm.Skip != 0)
                {
                    dataSource = operation.PerformSkip(dataSource, dm.Skip);         //Paging
                }
                if (dm.Take != 0)
                {
                    dataSource = operation.PerformTake(dataSource, dm.Take);
                }

            }
            catch (Exception ex)
            {
                _biz.LogService.Error(ex.Message);
                throw;
            }
            return dm.RequiresCounts ? Json(new { result = dataSource, count = count }) : Json(dataSource);
        }

        [HttpPost]
        public JsonResult Add(TradeActivityDto value)
        {
            _biz.LogService.Debug("Add");
            dynamic data = new BusinessResponse();

            try
            {
                var biz = _biz.TradeActivityService.Create(value);
                data = JsonConvert.SerializeObject(biz);
            }
            catch
            {

            }
            return Json(new { data, JsonRequestBehavior.AllowGet });

        }

        //[ValidateAntiForgeryToken]
        [HttpPost]
        public JsonResult Edit(TradeActivityDto value)
        {
            _biz.LogService.Debug("Edit");
            dynamic data = new BusinessResponse();
            try
            {
                var biz = _biz.TradeActivityService.Edit(value);
                data = JsonConvert.SerializeObject(biz);
            }
            catch
            {

            }

            return Json(new { data, JsonRequestBehavior.AllowGet });
        }

        [HttpPost]
        public JsonResult Delete(TradeActivityDto value)
        {
            _biz.LogService.Debug("Delete");
            dynamic data = new BusinessResponse();

            value.IS_ACTIVED = false;
            value.FLAG_ROW = "D";
            try
            {
                var biz = _biz.TradeActivityService.Remove(value);
                data = JsonConvert.SerializeObject(biz);
            }
            catch
            {

            }

            return Json(new { data, JsonRequestBehavior.AllowGet });
        }

    }
}