using System.Web.Mvc;
using GFCA.APT.Domain.Dto;
using Syncfusion.EJ2.Base;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GFCA.APT.BAL.Interfaces;
using GFCA.APT.Domain.Models;
using Newtonsoft.Json;
using System;

namespace GFCA.APT.WEB.Areas.Transactions.Controllers
{

    public class FixedContractController : ControllerWebBase
    {
        // private readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        // GET: Transactions/FixedContract
        private readonly IBusinessProvider _biz;
        public FixedContractController(IBusinessProvider biz)
        {
            _biz = biz;
        }

        // GET: T/FixedContracts
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult UrlFixedContractHeaderList(DataManagerRequest dm)
        {
            _biz.LogService.Debug("UrlFixedContractHeaderList");
            IEnumerable dataSource = _biz.FixedContractService.GetHeaderAll();
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
            int count = dataSource.Cast<FixedContractHeaderDto>().Count();
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

        // GET: T/FixedContracts/{DocCode}
        [HttpGet]
        public ActionResult FixedContractDetail(string DocCode)
        {
            _biz.LogService.Debug("FixedContractDetail");
            dynamic d = new BusinessResponse();

            try
            {
                var biz = _biz.FixedContractService.GetDetailByCode(DocCode);
                d = JsonConvert.SerializeObject(biz);
                //FixedContractDto
            }
            catch
            {

            }
            //return Json(new { data = d, JsonRequestBehavior.AllowGet });
            return View();
        }

        [HttpGet]
        public JsonResult UrlFixedContractDetailList(DataManagerRequest dm)
        {
            _biz.LogService.Debug("UrlFixedContractDetailList");
            IEnumerable dataSource = _biz.FixedContractService.GetDetailAll();
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
            int count = dataSource.Cast<FixedContractDetailDto>().Count();
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
        public JsonResult CreateFixedContractHeader(FixedContractHeaderDto data)
        {
            _biz.LogService.Debug("CreateFixedContractHeader");
            dynamic d = new BusinessResponse();

            try
            {
                var biz = _biz.FixedContractService.CreateHeader(data);
                d = JsonConvert.SerializeObject(biz);
            }
            catch
            {

            }
            return Json(new { data = d, JsonRequestBehavior.AllowGet });
        }

        [HttpPost]
        public JsonResult CreateFixedContractDetail(FixedContractDto data)
        {
            _biz.LogService.Debug("CreateFixedContractDetail");
            dynamic d = new BusinessResponse();

            try
            {
                var biz = _biz.FixedContractService.CreateDetail(data);
                d = JsonConvert.SerializeObject(biz);
            }
            catch
            {

            }
            return Json(new { data = d, JsonRequestBehavior.AllowGet });
        }

        [HttpPost]
        public JsonResult EditFixedContractDetail(FixedContractDto data)
        {
            _biz.LogService.Debug("EditFixedContractDetail");
            dynamic d = new BusinessResponse();

            try
            {
                var biz = _biz.FixedContractService.EditDetail(data);
                d = JsonConvert.SerializeObject(biz);
            }
            catch
            {

            }
            return Json(new { data = d, JsonRequestBehavior.AllowGet });
        }

        [HttpPost]
        public JsonResult DeleteFixedContractDetail(FixedContractDetailDto data)
        {
            _biz.LogService.Debug("DeleteFixedContractDetail");
            dynamic d = new BusinessResponse();

            try
            {
                var biz = _biz.FixedContractService.RemoveDetail(data);
                d = JsonConvert.SerializeObject(biz);
            }
            catch
            {

            }
            return Json(new { data = d, JsonRequestBehavior.AllowGet });
        }
    }
}