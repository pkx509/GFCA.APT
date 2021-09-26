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
using log4net;

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

        // GET: T/FixedContract
        [HttpGet()]
        public ActionResult Index()
        {
            return View();
        }

        //[HttpGet]
        public JsonResult UrlDataSource(DataManagerRequest dm)
        {
            _biz.LogService.Debug("UrlDataSource");
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
            int count = dataSource.Cast<ChannelDto>().Count();
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

        public ActionResult DocumentItem(int id)
        {
            // logger.Debug($"QueryString is {id}");
            return View();
        }
    }
}