using GFCA.APT.BAL.Interfaces;
using GFCA.APT.Domain.Dto;
using GFCA.APT.Domain.Enums;
using GFCA.APT.Domain.Models;
using Newtonsoft.Json;
using Syncfusion.EJ2.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GFCA.APT.WEB.Areas.Transactions.Controllers
{
    public class PromotionController : ControllerWebBase
    {
        public const string DOC_TYPE_CODE = "PP";
        private readonly IBusinessProvider _biz;
        public PromotionController(IBusinessProvider biz)
        {
            _biz = biz;
        }

        // GET: Transactions/Promotions
        public ActionResult Index()
        {
            return View();
        }

        // GET: Transactions/Pending
        public ActionResult Pending()
        {
            return View();
        }


        // GET: T/Promotions/{DOC_PROM_PH_ID}}]
        [HttpGet]
        public ViewResult PromotionItem(int DOC_PROM_PH_ID)
        {
            PromotionPlanningDto dto = new PromotionPlanningDto(DOC_PROM_PH_ID);

            try
            {
                dto.DocumentData = _biz.PromotionService.GetDocumentStateSection(DOC_TYPE_CODE, DOC_PROM_PH_ID);
                dto.HistoryData = _biz.PromotionService.GetDocumentHistorySection(DOC_TYPE_CODE, DOC_PROM_PH_ID);
                dto.RequesterData = _biz.PromotionService.GetDocumentRequesterSection(DOC_TYPE_CODE, DOC_PROM_PH_ID);
                dto.WorkflowData = _biz.PromotionService.GetDocumentWorkFlowSection(DOC_TYPE_CODE, DOC_PROM_PH_ID);

                dto.OverviewData = _biz.PromotionService.GetPromotionPlanByItemID(DOC_PROM_PH_ID);
                //dto.DetailSaleData = _biz.PromotionService.GetSaleDataByHeaderID(DOC_PROM_PH_ID);
                //dto.DetailInvesmentData = _biz.PromotionService.GetInvestmentByHeaderID(DOC_PROM_PH_ID);
                dto.FooterData = _biz.PromotionService.GetPromotionFooterByItemID(DOC_PROM_PH_ID);
            }
            catch
            {
             
            }
            return View(dto);
        }

        // GET: T/Promotions/{DOC_PROM_PH_ID}/S/{DOC_PROM_PS_ID}]
        [HttpGet]
        public ActionResult PromotionSaleDetail(int DOC_PROM_PH_ID, int DOC_PROM_PS_ID, PAGE_MODE PS_MODE = PAGE_MODE.EDITING)
        {
            _biz.LogService.Info("PromotionSaleDetail");
            PromotionPlanningDto dto = new PromotionPlanningDto(DOC_PROM_PS_ID);
            //PromotionPlanningSaleDto dto = new PromotionPlanningSaleDto();
            try
            {
                dto.DocumentData = _biz.PromotionService.GetDocumentStateSection(DOC_TYPE_CODE, DOC_PROM_PH_ID);
                dto.HistoryData = _biz.PromotionService.GetDocumentHistorySection(DOC_TYPE_CODE, DOC_PROM_PH_ID);
                dto.RequesterData = _biz.PromotionService.GetDocumentRequesterSection(DOC_TYPE_CODE, DOC_PROM_PH_ID);
                dto.WorkflowData = _biz.PromotionService.GetDocumentWorkFlowSection(DOC_TYPE_CODE, DOC_PROM_PH_ID);

                dto.OverviewData = _biz.PromotionService.GetPromotionPlanByItemID(DOC_PROM_PH_ID);
                dto.DetailSaleItem = _biz.PromotionService.GetSaleDataByItemID(DOC_PROM_PS_ID);
                if (DOC_PROM_PS_ID != 0)
                    dto.DataMode = PS_MODE;

                //dto.DetailSaleData = _biz.PromotionService.GetSaleDataByHeaderID(DOC_PROM_PH_ID);
                //dto.DetailInvesmentData = _biz.PromotionService.GetInvestmentByHeaderID(DOC_PROM_PH_ID);
                dto.FooterData = _biz.PromotionService.GetPromotionFooterByItemID(DOC_PROM_PH_ID);
            }
            catch(Exception ex)
            {
                _biz.LogService.Error("PromotionSaleDetail : ", ex);
            }
            return View(dto);
        }

        // GET: T/Promotions/{DOC_PROM_PH_ID}/I/{DOC_PROM_PI_ID}]
        [HttpGet]
        public ActionResult PromotionInvestmentDetail(int DOC_PROM_PH_ID, int DOC_PROM_PI_ID)
        {
            _biz.LogService.Info("PromotionInvestmentDetail");
            PromotionPlanningDto dto = new PromotionPlanningDto(DOC_PROM_PI_ID);
            //PromotionPlanningInvestmentDto dto = new PromotionPlanningInvestmentDto();
            try
            {

                dto.DocumentData = _biz.PromotionService.GetDocumentStateSection(DOC_TYPE_CODE, DOC_PROM_PH_ID);
                dto.HistoryData = _biz.PromotionService.GetDocumentHistorySection(DOC_TYPE_CODE, DOC_PROM_PH_ID);
                dto.RequesterData = _biz.PromotionService.GetDocumentRequesterSection(DOC_TYPE_CODE, DOC_PROM_PH_ID);
                dto.WorkflowData = _biz.PromotionService.GetDocumentWorkFlowSection(DOC_TYPE_CODE, DOC_PROM_PH_ID);

                dto.OverviewData = _biz.PromotionService.GetPromotionPlanByItemID(DOC_PROM_PH_ID);
                dto.DetailInvesmentItem = _biz.PromotionService.GetInvestmentByItemID(DOC_PROM_PH_ID, DOC_PROM_PI_ID);
                
                //dto.DetailSaleData = _biz.PromotionService.GetSaleDataByHeaderID(DOC_PROM_PH_ID);
                //dto.DetailInvesmentData = _biz.PromotionService.GetInvestmentByHeaderID(DOC_PROM_PH_ID);
                dto.FooterData = _biz.PromotionService.GetPromotionFooterByItemID(DOC_PROM_PH_ID);

            }
            catch (Exception ex)
            {
                _biz.LogService.Error("PromotionInvestmentDetail : ", ex);
            }

            return View(dto);
        }
        /*
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
        public PartialViewResult ItemDetailGridSalePartial()
        {
            return PartialView();
        }

        [HttpGet]
        public PartialViewResult ItemDetailGridInvestmentPartial()
        {
            return PartialView();
        }

        [HttpGet]
        public PartialViewResult ItemFooterPartial()
        {
            return PartialView();
        }
        */
        [HttpPost]
        public JsonResult UrlPromotionHeaderList(DataManagerRequest dm)
        {
            _biz.LogService.Info("UrlPromotionHeaderList");
            IEnumerable dataSource = _biz.PromotionService.GetPromotionPlanAll();
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
            int count = dataSource.Cast<PromotionPlanngOverviewDto>().Count();
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
        public JsonResult UrlPromotionPlanningSaleList(int DOC_PROM_PH_ID, DataManagerRequest dm)
        {
            _biz.LogService.Info("UrlPromotionPlanningSaleList");
            IEnumerable dataSource = _biz.PromotionService.GetSaleDataByHeaderID(DOC_PROM_PH_ID);
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
            int count = dataSource.Cast<PromotionPlanningSaleDto>().Count();
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
        public JsonResult UrlPromotionPlanningInvesmentList(int DOC_PROM_PH_ID, DataManagerRequest dm)
        {
            _biz.LogService.Info("UrlPromotionPlanningInvesmentList");
            IEnumerable dataSource = _biz.PromotionService.GetInvestmentByHeaderID(DOC_PROM_PH_ID);
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
            int count = dataSource.Cast<PromotionPlanningInvestmentDto>().Count();
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
        public JsonResult CreatePromotionPlanngOverview(PromotionPlanngOverviewDto data)
        {
            _biz.LogService.Info("CreatePromotionPlanngOverview");
            string jsonData = string.Empty;
            var bizObj = new BusinessResponse();

            try
            {
                bizObj = _biz.PromotionService.CreateOverview(data);
            }
            catch (Exception ex)
            {
                _biz.LogService.Error("CreatePromotionPlanngOverview : ", ex);
            }
            finally
            {
                jsonData = JsonConvert.SerializeObject(bizObj);
            }
            return Json(new { data = jsonData, JsonRequestBehavior.AllowGet }); ;
        }

        [HttpPost]
        public JsonResult CreatePromotionPlanngSale(PromotionPlanningSaleDto data)
        {
            _biz.LogService.Info("CreatePromotionPlanngSale");
            string jsonData = string.Empty;
            var bizObj = new BusinessResponse();

            try
            {
                bizObj = _biz.PromotionService.CreatePlanngSale(data);
            }
            catch (Exception ex)
            {
                _biz.LogService.Error("CreatePromotionPlanngSale : ", ex);
            }
            finally
            {
                jsonData = JsonConvert.SerializeObject(bizObj);
            }
            return Json(new { data = jsonData, JsonRequestBehavior.AllowGet });
        }

        [HttpPost]
        public JsonResult CreatePromotionPlanningInvestment(PromotionPlanningInvestmentDto data)
        {
            _biz.LogService.Info("CreatePromotionPlanningInvestment");
            string jsonData = string.Empty;
            var bizObj = new BusinessResponse();

            try
            {
                bizObj = _biz.PromotionService.CreateInvestment(data);
            }
            catch (Exception ex)
            {
                _biz.LogService.Error("CreatePromotionPlanningInvestment : ", ex);
            }
            finally
            {
                jsonData = JsonConvert.SerializeObject(bizObj);
            }
            return Json(new { data = jsonData, JsonRequestBehavior.AllowGet });
        }

        [HttpPost]
        public JsonResult EditPromotionPlanngSale(PromotionPlanningSaleDto data)
        {
            _biz.LogService.Info("EditPromotionPlanngSale");
            string jsonData = string.Empty;
            var bizObj = new BusinessResponse();

            try
            {
                bizObj = _biz.PromotionService.EditPlanngSale(data);
            }
            catch (Exception ex)
            {
                _biz.LogService.Error("EditPromotionPlanngSale : ", ex);
            }
            finally
            {
                jsonData = JsonConvert.SerializeObject(bizObj);
            }
            return Json(new { data = jsonData, JsonRequestBehavior.AllowGet });
        }

        [HttpPost]
        public JsonResult EditPromotionPlanningInvestment(PromotionPlanningInvestmentDto data)
        {
            _biz.LogService.Info("EditPromotionPlanningInvestment");
            string jsonData = string.Empty;
            var bizObj = new BusinessResponse();

            try
            {
                bizObj = _biz.PromotionService.EditInvestment(data);
            }
            catch (Exception ex)
            {
                _biz.LogService.Error("EditPromotionPlanningInvestment : ", ex);
            }
            finally
            {
                jsonData = JsonConvert.SerializeObject(bizObj);
            }
            return Json(new { data = jsonData, JsonRequestBehavior.AllowGet });
        }

        [HttpPost]
        public JsonResult DeletePromotionPlanngSale(PromotionPlanningSaleDto data)
        {
            _biz.LogService.Info("DeletePromotionPlanngSale");
            string jsonData = string.Empty;
            var bizObj = new BusinessResponse();

            try
            {
                bizObj = _biz.PromotionService.RemovePlanngSale(data);
            }
            catch (Exception ex)
            {
                _biz.LogService.Error("DeletePromotionPlanngSale : ", ex);
            }
            finally
            {
                jsonData = JsonConvert.SerializeObject(bizObj);
            }
            return Json(new { data = jsonData, JsonRequestBehavior.AllowGet });
        }

        [HttpPost]
        public JsonResult DeletePromotionPlanningInvestment(PromotionPlanningInvestmentDto data)
        {
            _biz.LogService.Info("DeletePromotionPlanningInvestment");
            string jsonData = string.Empty;
            var bizObj = new BusinessResponse();

            try
            {
                bizObj = _biz.PromotionService.RemoveInvestment(data);
            }
            catch (Exception ex)
            {
                _biz.LogService.Error("DeletePromotionPlanningInvestment : ", ex);
            }
            finally
            {
                jsonData = JsonConvert.SerializeObject(bizObj);
            }
            return Json(new { data = jsonData, JsonRequestBehavior.AllowGet });
        }

    }
}