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
        private const string DOC_TYPE_CODE = "FC";
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

        // GET: T/FixedContracts/{id}
        public ActionResult CreateFixedContractDetail()
        {
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
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

        // GET: T/FixedContracts/{DOC_FCH_ID}]
        //[HttpGet]
        public ActionResult FixedContractItem(int DOC_FCH_ID)
        {
            var dto = new FixedContractDto();
            try
            {
                FixedContractHeaderDto headerDto = _biz.FixedContractService.GetHeaderById(DOC_FCH_ID);
                var docFlow = _biz.FixedContractService.GetDocumentWorkFlowSection(DOC_TYPE_CODE, DOC_FCH_ID);
                var docRequester =_biz.FixedContractService.GetDocumentRequesterSection(DOC_TYPE_CODE, DOC_FCH_ID);
                var docHistory = _biz.FixedContractService.GetDocumentHistorySection(DOC_TYPE_CODE, DOC_FCH_ID);
                
                //Document Infomation
                dto.WorkflowData = docFlow as DocumentWorkFlowDto;
                dto.DocumentData = new DocumentStateDto();
                dto.RequesterData = docRequester as DocumentRequesterDto;
                dto.HistoryData = docHistory as IEnumerable<DocumentHistoryDto>;
                
                //Fixed contrac Infomation
                dto.HeaderData = headerDto as FixedContractHeaderDto;
                //no use this line bcause already use for bind grid at function UrlFixedContractDetailList
                //dto.DetailData = _biz.FixedContractService.GetDetailItems(DOC_FCH_ID);
                dto.FooterData = new FixedContractFooterDto();
                
            }
            catch
            {

            }

            return View(dto);
        }
        // GET: T/FixedContracts/{DOC_FCH_ID}/{DOC_FCD_ID}]
        [HttpGet]
        public ActionResult FixedContractDetail(int DOC_FCH_ID, int DOC_FCD_ID)
        {
            FixedContractDto dto = new FixedContractDto();
            try
            {
                var item = _biz.FixedContractService.GetDetailItem(DOC_FCD_ID);
                dto.DetailItem = item;
            }
            catch
            {

            }

            return View(dto.DetailItem);
        }

        [HttpPost]
        public ActionResult FixedContractDetail(int DOC_FCH_ID, FixedContractDto data)
        {
            FixedContractDto detailDto = new FixedContractDto();
            try
            {

            }
            catch
            {

            }

            return RedirectToAction("FixedContractItem", new { DOC_FCH_ID = DOC_FCH_ID });
        }

        [HttpPost]
        public JsonResult UrlFixedContractDetailList(int DOC_FCH_ID, DataManagerRequest dm)
        {
            _biz.LogService.Debug("UrlFixedContractDetailList");
            IEnumerable dataSource = _biz.FixedContractService.GetDetailItems(DOC_FCH_ID);
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

        [HttpGet]
        public PartialViewResult ItemHeaderPartial()
        {
            return PartialView();
        }
        [HttpGet]
        public PartialViewResult ItemDetailPartial()
        {
            return PartialView();
        }
        [HttpGet]
        public PartialViewResult ItemFooterPartial()
        {
            return PartialView();
        }


        [HttpGet]
        public PartialViewResult ItemDetailGridFixedContractPartial()
        {
            return PartialView();
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
        public JsonResult CreateFixedContractDetail(FixedContractDetailDto data)
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
        public JsonResult EditFixedContractDetail(FixedContractDetailDto data)
        {

            _biz.LogService.Debug("EditFixedContractDetail");
            string jsonData = string.Empty;
            var bizObj = new BusinessResponse();


            try
            {

                bizObj = _biz.FixedContractService.EditDetail(data);

            }
            catch (Exception ex)
            {
                _biz.LogService.Error("EditFixedContractDetail : ", ex);

            }
            finally
            {
                jsonData = JsonConvert.SerializeObject(bizObj);
            }
            return Json(new { data = jsonData, JsonRequestBehavior.AllowGet });
        }

        [HttpPost]
        public JsonResult DeleteFixedContractDetail(FixedContractDetailDto data)
        {
            _biz.LogService.Debug("DeleteFixedContractDetail");
            string jsonData = string.Empty;
            var bizObj = new BusinessResponse();
            try
            {
                bizObj = _biz.FixedContractService.RemoveDetail(data);
            }
            catch (Exception ex)
            {
                _biz.LogService.Error("DeleteFixedContractDetail : ", ex);
            }
            finally
            {
                jsonData = JsonConvert.SerializeObject(bizObj);
            }
            return Json(new { data = jsonData, JsonRequestBehavior.AllowGet });
        }

        [HttpPost]
        public JsonResult DeleteFixedContractHeader(FixedContractHeaderDto data)
        {
            _biz.LogService.Debug("DeleteFixedContractHeader");
            string jsonData = string.Empty;
            var bizObj = new BusinessResponse();
            try
            {
                bizObj = _biz.FixedContractService.RemoveHeader(data);
            }
            catch (Exception ex)
            {
                _biz.LogService.Error("DeleteFixedContractHeader : ", ex);
            }
            finally
            {
                jsonData = JsonConvert.SerializeObject(bizObj);
            }
            return Json(new { data = jsonData, JsonRequestBehavior.AllowGet });
        }

        // GET: T/FixedContracts/{DocCode}]
        [HttpGet]
        public ActionResult getFixedContractHeader(string DocCode)
        {
            var header = new GFCA.APT.Domain.Dto.FixedContractHeaderDto();
            header = _biz.FixedContractService.GetHeaderById(2);

            return View(header);
        }
    }
}