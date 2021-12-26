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
using System.Web;
using ClosedXML.Excel;
using System.IO;
using System.Data;

namespace GFCA.APT.WEB.Areas.Transactions.Controllers
{
    //public class SaleForecastController : ControllerWebBase
    //{
    //    private readonly IBusinessProvider _biz;
    //    public SaleForecastController(IBusinessProvider biz)
    //    {
    //        _biz = biz;
    //    }

    //    // GET: Transactions/SaleForecast
    //    public ActionResult Index()
    //    {
    //        return View();
    //    }

    //    [HttpGet]
    //    public ViewResult SaleForecastItem(string DocCode)
    //    {
    //        return View();
    //    }

    //    [HttpGet]
    //    public PartialViewResult ItemHeaderPartial()
    //    {
    //        return PartialView();
    //    }
    //    [HttpGet]
    //    public PartialViewResult ItemDetailPartial()
    //    {
    //        return PartialView();
    //    }
    //    [HttpGet]
    //    public PartialViewResult ItemFooterPartial()
    //    {
    //        return PartialView();
    //    }


    //}
    public class SaleForecastController : ControllerWebBase
    {
        public class JsonResultModel
        {
            public bool ret { get; set; }
            public string message { get; set; }
            public object data { get; set; }
        }
        private const string DOC_TYPE_CODE = "SF";
        // private readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        // GET: Transactions/SaleForecast
        private readonly IBusinessProvider _biz;
        public SaleForecastController(IBusinessProvider biz)
        {
            _biz = biz;
        }

        // GET: T/SaleForecasts
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        // GET: T/SaleForecasts/{id}
        public ActionResult CreateSaleForecastDetail()
        {
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public JsonResult UrlSaleForecastHeaderList(DataManagerRequest dm)
        {
            _biz.LogService.Debug("UrlSaleForecastHeaderList");
            IEnumerable dataSource = _biz.SaleForecastService.GetHeaderAll();
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
            int count = dataSource.Cast<SaleForecastHeaderDto>().Count();
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

        // GET: T/SaleForecasts/{DOC_SFCH_ID}]
        //[HttpGet]
        public ActionResult SaleForecastItem(int DOC_SFCH_ID)
        {

            var dto = new SaleForecastDto();
            try
            {
                SaleForecastHeaderDto headerDto = _biz.SaleForecastService.GetHeaderById(DOC_SFCH_ID);
                var docFlow = _biz.SaleForecastService.GetDocumentWorkFlowSection(DOC_TYPE_CODE, DOC_SFCH_ID);
                var docRequester = _biz.SaleForecastService.GetDocumentRequesterSection(DOC_TYPE_CODE, DOC_SFCH_ID);
                var docHistory = _biz.SaleForecastService.GetDocumentHistorySection(DOC_TYPE_CODE, DOC_SFCH_ID);

                //Document Infomation
                dto.WorkflowData = docFlow as DocumentWorkFlowDto;
                dto.DocumentData = new DocumentStateDto();
                dto.RequesterData = docRequester as DocumentRequesterDto;
                dto.HistoryData = docHistory as IEnumerable<DocumentHistoryDto>;

                //Sale Forecast Infomation
                dto.HeaderData = headerDto as SaleForecastHeaderDto;
                //no use this line bcause already use for bind grid at function UrlSaleForecastDetailList
                //dto.DetailData = _biz.SaleForecastService.GetDetailItems(DOC_SFCH_ID);
                dto.FooterData = new SaleForecastFooterDto();

            }
            catch
            {

            }

            return View(dto);
        }
        // GET: T/SaleForecasts/{DOC_SFCH_ID}/{DOC_SFCD_ID}]
        [HttpGet]
        public ActionResult SaleForecastDetail(int DOC_SFCH_ID, int DOC_SFCD_ID)
        {
            SaleForecastDto dto = new SaleForecastDto();
            try
            {
                var item = _biz.SaleForecastService.GetDetailItem(DOC_SFCD_ID);
                dto.DetailItem = item;
            }
            catch
            {

            }

            return View(dto.DetailItem);
        }

        [HttpPost]
        public ActionResult SaleForecastDetail(int DOC_SFCH_ID, SaleForecastDto data)
        {
            SaleForecastDto detailDto = new SaleForecastDto();
            try
            {

            }
            catch
            {

            }

            return RedirectToAction("SaleForecastItem", new { DOC_SFCH_ID = DOC_SFCH_ID });
        }

        [HttpPost]
        public JsonResult UrlSaleForecastDetailList(int DOC_SFCH_ID, DataManagerRequest dm)
        {
            _biz.LogService.Debug("UrlSaleForecastDetailList");
            IEnumerable dataSource = _biz.SaleForecastService.GetDetailItems(DOC_SFCH_ID);
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
            int count = dataSource.Cast<SaleForecastDetailDto>().Count();
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
        public PartialViewResult ItemDetailGridSaleForecastPartial()
        {
            return PartialView();
        }

        [HttpPost]
        public JsonResult CreateSaleForecastHeader(SaleForecastHeaderDto data)
        {
            _biz.LogService.Debug("CreateSaleForecastHeader");
            dynamic d = new BusinessResponse();

            try
            {
                var biz = _biz.SaleForecastService.CreateHeader(data);
                d = JsonConvert.SerializeObject(biz);
            }
            catch
            {

            }
            return Json(new { data = d, JsonRequestBehavior.AllowGet });
        }

        [HttpPost]
        public JsonResult CreateSaleForecastDetail(SaleForecastDetailDto data)
        {
            _biz.LogService.Debug("CreateSaleForecastDetail");
            dynamic d = new BusinessResponse();

            try
            {
                var biz = _biz.SaleForecastService.CreateDetail(data);
                d = JsonConvert.SerializeObject(biz);
            }
            catch
            {

            }
            return Json(new { data = d, JsonRequestBehavior.AllowGet });
        }
        [HttpPost]
        public JsonResult CreateSaleForecastDetailList(List<SaleForecastDetailDto> data)
        {
            _biz.LogService.Debug("CreateSaleForecastDetailList");
            dynamic d = new BusinessResponse();

            try
            {
                var biz = _biz.SaleForecastService.CreateDetailList(data);
                d = JsonConvert.SerializeObject(biz);
            }
            catch
            {

            }
            return Json(new { data = d, JsonRequestBehavior.AllowGet });
        }

        [HttpPost]
        public JsonResult EditSaleForecastDetail(SaleForecastDetailDto data)
        {

            _biz.LogService.Debug("EditSaleForecastDetail");
            string jsonData = string.Empty;
            var bizObj = new BusinessResponse();


            try
            {

                bizObj = _biz.SaleForecastService.EditDetail(data);

            }
            catch (Exception ex)
            {
                _biz.LogService.Error("EditSaleForecastDetail : ", ex);

            }
            finally
            {
                jsonData = JsonConvert.SerializeObject(bizObj);
            }
            return Json(new { data = jsonData, JsonRequestBehavior.AllowGet });
        }

        [HttpPost]
        public JsonResult DeleteSaleForecastDetail(SaleForecastDetailDto data)
        {
            _biz.LogService.Debug("DeleteSaleForecastDetail");
            dynamic d = new BusinessResponse();

            try
            {
                var biz = _biz.SaleForecastService.RemoveDetail(data);
                d = JsonConvert.SerializeObject(biz);
            }
            catch
            {

            }
            return Json(new { data = d, JsonRequestBehavior.AllowGet });
        }


        // GET: T/SaleForecasts/{DocCode}]
        [HttpGet]
        public ActionResult getSaleForecastHeader(string DocCode)
        {
            var header = new GFCA.APT.Domain.Dto.SaleForecastHeaderDto();
            header = _biz.SaleForecastService.GetHeaderById(2);

            return View(header);
        }

        public DataTable CreateDataTableDetail(List<SaleForecastDetailDto> data)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("DOC_SFCH_ID", typeof(int));
            dt.Columns.Add("DOC_SFCD_ID", typeof(int));
            //dt.Columns.Add("DOC_CODE", typeof(string));
            //dt.Columns.Add(new DataColumn { ColumnName = "DOC_VER", DataType = typeof(int), AllowDBNull = true });
            //dt.Columns.Add(new DataColumn { ColumnName = "DOC_REV", DataType = typeof(int), AllowDBNull = true });
            dt.Columns.Add("BRAND_CODE", typeof(string));
            dt.Columns.Add("BRAND_NAME", typeof(string));
            dt.Columns.Add("SIZE", typeof(string));
            dt.Columns.Add("UOM", typeof(string));
            dt.Columns.Add("PACK", typeof(string));
            dt.Columns.Add("PACK_NAME", typeof(string));
            dt.Columns.Add("PROD_CODE", typeof(string));
            dt.Columns.Add("PROD_NAME", typeof(string));
            dt.Columns.Add(new DataColumn { ColumnName = "YEAR", DataType = typeof(int), AllowDBNull = true });
            dt.Columns.Add(new DataColumn { ColumnName = "M1Sales", DataType = typeof(decimal), AllowDBNull = true });
            dt.Columns.Add(new DataColumn { ColumnName = "M1FOC", DataType = typeof(decimal), AllowDBNull = true });
            dt.Columns.Add(new DataColumn { ColumnName = "M2Sales", DataType = typeof(decimal), AllowDBNull = true });
            dt.Columns.Add(new DataColumn { ColumnName = "M2FOC", DataType = typeof(decimal), AllowDBNull = true });
            dt.Columns.Add(new DataColumn { ColumnName = "M3Sales", DataType = typeof(decimal), AllowDBNull = true });
            dt.Columns.Add(new DataColumn { ColumnName = "M3FOC", DataType = typeof(decimal), AllowDBNull = true });
            dt.Columns.Add(new DataColumn { ColumnName = "M4Sales", DataType = typeof(decimal), AllowDBNull = true });
            dt.Columns.Add(new DataColumn { ColumnName = "M4FOC", DataType = typeof(decimal), AllowDBNull = true });
            dt.Columns.Add(new DataColumn { ColumnName = "M5Sales", DataType = typeof(decimal), AllowDBNull = true });
            dt.Columns.Add(new DataColumn { ColumnName = "M5FOC", DataType = typeof(decimal), AllowDBNull = true });
            dt.Columns.Add(new DataColumn { ColumnName = "M6Sales", DataType = typeof(decimal), AllowDBNull = true });
            dt.Columns.Add(new DataColumn { ColumnName = "M6FOC", DataType = typeof(decimal), AllowDBNull = true });
            dt.Columns.Add(new DataColumn { ColumnName = "M7Sales", DataType = typeof(decimal), AllowDBNull = true });
            dt.Columns.Add(new DataColumn { ColumnName = "M7FOC", DataType = typeof(decimal), AllowDBNull = true });
            dt.Columns.Add(new DataColumn { ColumnName = "M8Sales", DataType = typeof(decimal), AllowDBNull = true });
            dt.Columns.Add(new DataColumn { ColumnName = "M8FOC", DataType = typeof(decimal), AllowDBNull = true });
            dt.Columns.Add(new DataColumn { ColumnName = "M9Sales", DataType = typeof(decimal), AllowDBNull = true });
            dt.Columns.Add(new DataColumn { ColumnName = "M9FOC", DataType = typeof(decimal), AllowDBNull = true });
            dt.Columns.Add(new DataColumn { ColumnName = "M10Sales", DataType = typeof(decimal), AllowDBNull = true });
            dt.Columns.Add(new DataColumn { ColumnName = "M10FOC", DataType = typeof(decimal), AllowDBNull = true });
            dt.Columns.Add(new DataColumn { ColumnName = "M11Sales", DataType = typeof(decimal), AllowDBNull = true });
            dt.Columns.Add(new DataColumn { ColumnName = "M11FOC", DataType = typeof(decimal), AllowDBNull = true });
            dt.Columns.Add(new DataColumn { ColumnName = "M12Sales", DataType = typeof(decimal), AllowDBNull = true });
            dt.Columns.Add(new DataColumn { ColumnName = "M12FOC", DataType = typeof(decimal), AllowDBNull = true });
            dt.Columns.Add(new DataColumn { ColumnName = "TotalSales", DataType = typeof(decimal), AllowDBNull = true });
            dt.Columns.Add(new DataColumn { ColumnName = "TotalFOC", DataType = typeof(decimal), AllowDBNull = true });
            dt.Columns.Add("DOC_STATUS", typeof(string));
            foreach (var item in data)
            {
                DataRow dr = dt.NewRow();
                dr["DOC_SFCH_ID"] = item.DOC_SFCH_ID;
                dr["DOC_SFCD_ID"] = item.DOC_SFCD_ID;
                //dr["DOC_CODE"] = item.DOC_CODE;
                //dr["DOC_VER"] = item.DOC_VER ?? 0;
                //dr["DOC_REV"] = item.DOC_REV ?? 0;
                dr["BRAND_CODE"] = item.BRAND_CODE;
                dr["BRAND_NAME"] = item.BRAND_NAME;
                dr["SIZE"] = item.SIZE;
                dr["UOM"] = item.UOM;
                dr["PACK"] = item.PACK;
                dr["PACK_NAME"] = item.PACK_NAME;
                dr["PROD_CODE"] = item.PROD_CODE;
                dr["PROD_NAME"] = item.PROD_NAME;
                dr["YEAR"] = item.YEAR ?? DateTime.Now.Year;

                dr["M1Sales"] = item.M1Sales ?? 0;
                dr["M2Sales"] = item.M2Sales ?? 0;
                dr["M3Sales"] = item.M3Sales ?? 0;
                dr["M4Sales"] = item.M4Sales ?? 0;
                dr["M5Sales"] = item.M5Sales ?? 0;
                dr["M6Sales"] = item.M6Sales ?? 0;
                dr["M7Sales"] = item.M7Sales ?? 0;
                dr["M8Sales"] = item.M8Sales ?? 0;
                dr["M9Sales"] = item.M9Sales ?? 0;
                dr["M10Sales"] = item.M10Sales ?? 0;
                dr["M11Sales"] = item.M11Sales ?? 0;
                dr["M12Sales"] = item.M12Sales ?? 0;
                dr["TotalSales"] = item.TotalSales ?? 0;
                dr["M1FOC"] = item.M1FOC ?? 0;
                dr["M2FOC"] = item.M2FOC ?? 0;
                dr["M3FOC"] = item.M3FOC ?? 0;
                dr["M4FOC"] = item.M4FOC ?? 0;
                dr["M5FOC"] = item.M5FOC ?? 0;
                dr["M6FOC"] = item.M6FOC ?? 0;
                dr["M7FOC"] = item.M7FOC ?? 0;
                dr["M8FOC"] = item.M8FOC ?? 0;
                dr["M9FOC"] = item.M9FOC ?? 0;
                dr["M10FOC"] = item.M10FOC ?? 0;
                dr["M11FOC"] = item.M11FOC ?? 0;
                dr["M12FOC"] = item.M12FOC ?? 0;
                dr["TotalFOC"] = item.TotalFOC ?? 0;
                dr["DOC_STATUS"] = item.DOC_STATUS;

                dt.Rows.Add(dr);
            }

            return dt;
        }
        public ActionResult ExportFiles(int DOC_SFCH_ID)
        {
            DateTime s = DateTime.Now;
            int intYear = 0;
            try
            {
                _biz.LogService.Debug("GetDetailItemToExport");
                var data = _biz.SaleForecastService.GetDetailItemToExport(DOC_SFCH_ID);
                //intYear = data.Count() > 0 ? Convert.ToInt32((data.FirstOrDefault().YEAR) : Convert.ToInt32(DateTime.Now.Year);
                if (data.Count() > 0)
                {
                    var e = data.ElementAt(0);
                    intYear = e.YEAR ?? DateTime.Now.Year;

                    //if (data.Select(x => x.YEAR).FirstOrDefault().HasValue)
                    //    intYear = data.Select(x => x.YEAR).FirstOrDefault().Value;
                    //else
                    //    intYear = DateTime.Now.Year;
                }
                else
                {
                    intYear = DateTime.Now.Year;
                }
                // dto to datatable
                DataTable dt = CreateDataTableDetail(data.ToList());
                //

                using (XLWorkbook workbook = new XLWorkbook())
                {
                    workbook.Worksheets.Add(dt, "Sale Forecast");

                    using (var memoryStream = new MemoryStream())
                    {
                        Response.ClearContent();
                        Response.Buffer = true;
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("content-disposition", "attachment;  filename=SaleForecast_" + intYear.ToString() + "_" + s.ToString("yyyyMMdd") + ".xlsx");
                        workbook.SaveAs(memoryStream);
                        memoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.End();
                    }
                }

                return null;
            }
            catch (IOException ex)
            {
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #region UploadFile Xlsx
        [HttpPost] //ContentResult
        public ActionResult ImportFiles(HttpPostedFileBase excelfile)
        {
            DateTime s = DateTime.Now;
            int records = 0;
            int skipDup = 0;
            foreach (string file in Request.Files)
            {
                HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                var workbook = new XLWorkbook(hpf.InputStream);
                var worksheet = workbook.Worksheet(1);
                var range = worksheet.RangeUsed();
                var table = range.AsTable();

                var cellDOC_SFCH_ID = table.HeadersRow().CellsUsed(c => c.Value.ToString() == "").FirstOrDefault();
                var cellDOC_SFCD_ID = table.HeadersRow().CellsUsed(c => c.Value.ToString() == "").FirstOrDefault();
                var cellPROD_CODE = table.HeadersRow().CellsUsed(c => c.Value.ToString() == "").FirstOrDefault();

                if ((cellDOC_SFCH_ID != null && cellPROD_CODE != null) || (cellDOC_SFCD_ID != null && cellPROD_CODE != null)) //&& cellDOC_SFCD_ID != null 
                {
                    List<SaleForecastDetailDto> RTR_Upload = new List<SaleForecastDetailDto>();
                    for (int i = 2; i <= range.RowCount(); i++)
                    {
                        try
                        {

                            string DOC_SFCH_ID = (cellDOC_SFCH_ID != null) ? worksheet.Cell(i, cellDOC_SFCH_ID.WorksheetColumn().ColumnLetter()).Value.ToString().Trim() : "";
                            string DOC_SFCD_ID = (cellDOC_SFCD_ID != null) ? worksheet.Cell(i, cellDOC_SFCD_ID.WorksheetColumn().ColumnLetter()).Value.ToString().Trim() : "";
                            string PROD_CODE = (cellPROD_CODE != null) ? worksheet.Cell(i, cellPROD_CODE.WorksheetColumn().ColumnLetter()).Value.ToString().Trim() : "";

                            if (DOC_SFCH_ID != "" || PROD_CODE != "" || DOC_SFCD_ID != "") //
                            {

                                RTR_Upload.Add(new SaleForecastDetailDto
                                {
                                    //Id = 0,
                                    ////QC_Date = DateTime.Now,
                                    //QC_User = User.Identity.Name,
                                    //SUBR_NUMB = DOC_SFCH_ID,   // RTR Code
                                    //SIM_NUMBER = DOC_SFCD_ID,   // RTR Name
                                    ////REGISTER_DATE = tryChk2 ? dFormat : t,
                                    //System_Status = PROD_CODE   // Active
                                });

                                records++;

                            }
                            else
                            {
                                skipDup++;

                            }
                        }
                        catch (Exception ex)
                        {
                            skipDup++;
                            //return Json(new JsonResultModel() { ret = false, message = "Upload fail : " + ex.Message + " with " + records + " and skip " + skipDup + " record(s)" }, JsonRequestBehavior.AllowGet);
                        }
                    }

                    // code for manage detail
                    //

                }
                else
                {
                    return Json(new JsonResultModel() { ret = true, message = "Upload Success with " + records + " record(s)\n and Skip " + skipDup + " record(s) \n [Column " + ((cellDOC_SFCD_ID == null) ? "cellDOC_SFCD_ID" : (cellDOC_SFCH_ID == null) ? "cellDOC_SFCH_ID" : "cellDate") + " not found]" }, JsonRequestBehavior.AllowGet);

                }
            }
            return Json(new JsonResultModel() { ret = true, message = "Upload Success with " + records + " and skip " + skipDup + " record(s)" }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file)
        {
            DateTime s = DateTime.Now;
            int records = 0;
            int skipDup = 0;
            int DOC_SFCH_ID = 0;
            try
            {
                if (file.ContentLength > 0)
                {
                    //string _FileName = Path.GetFileName(file.FileName);
                    //string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), _FileName);
                    //file.SaveAs(_path);
                    //foreach (string fi in Request.Files)
                    //{
                    //}
                    //return Json(new JsonResultModel() { ret = true, message = "Upload Success with " + records + " and skip " + skipDup + " record(s)" }, JsonRequestBehavior.AllowGet);
                    HttpPostedFileBase hpf = file;
                    var workbook = new XLWorkbook(hpf.InputStream);
                    var worksheet = workbook.Worksheet(1);
                    var range = worksheet.RangeUsed();
                    var rowCount = range.RangeAddress.RowSpan;

                    //var doc_sfch_id = range.Cell("A2").Value;
                    //var doc_sfcd_id = range.Cell("B2").Value;
                    //var prod_code = range.Cell("L2").Value;
                    //var table = range.AsTable();

                    //var worksheet = workbook.Worksheet(1);
                    var firstRowUsed = worksheet.FirstRowUsed();
                    var firstPossibleAddress = worksheet.Row(firstRowUsed.RowNumber()).FirstCell().Address;
                    var lastPossibleAddress = worksheet.LastCellUsed().Address;
                    if (range.ColumnCount() < 38)
                    {
                        return View();
                    }
                    //// Get a range with the remainder of the worksheet data (the range used)
                    //var range = worksheet.Range(firstPossibleAddress, lastPossibleAddress).AsRange(); //.RangeUsed();
                    //// Treat the range as a table (to be able to use the column names)
                    //var table = range.AsTable();


                    //var cellDOC_SFCH_ID = table.HeadersRow().CellsUsed(c => c.Value.ToString().ToUpper().Trim() == "DOC_SFCH_ID").FirstOrDefault();
                    //var cellDOC_SFCD_ID = table.HeadersRow().CellsUsed(c => c.Value.ToString() == "DOC_SFCD_ID").FirstOrDefault();
                    //var cellPROD_CODE = table.HeadersRow().CellsUsed(c => c.Value.ToString() == "PROD_CODE").FirstOrDefault();
                    var hdoc_sfch_id = range.Cell("A1").Value;
                    var hdoc_sfcd_id = range.Cell("B1").Value;
                    var hprod_code = range.Cell("I1").Value;
                    if (hdoc_sfch_id != null && hdoc_sfcd_id != null && hdoc_sfcd_id != null)
                    {
                        List<SaleForecastDetailDto> SF_Upload = new List<SaleForecastDetailDto>();
                        IEnumerable<SaleForecastDetailDto> data = new List<SaleForecastDetailDto>();
                        if (range.RowCount() > 1)
                        {
                            DOC_SFCH_ID = int.Parse(range.Cell($"A{2}").Value.ToString());
                            // get item list
                            data = _biz.SaleForecastService.GetDetailItemToExport(DOC_SFCH_ID);
                            //
                        }
                        for (int i = 2; i <= range.RowCount(); i++)
                        {
                            try
                            {
                                //decimal M1Sales = decimal.Parse(worksheet.Cell(i, 2).Value.ToString());
                                //decimal M1FOC = decimal.Parse(worksheet.Cell(i, 2).Value.ToString());

                                DOC_SFCH_ID = int.Parse(range.Cell($"A{i}").Value.ToString());
                                //// get item list
                                //var data = _biz.SaleForecastService.GetDetailItemToExport(DOC_SFCH_ID);
                                ////
                                string PROD_CODE = range.Cell($"I{i}").Value.ToString();
                                var prod_detail = data.Where(x => x.PROD_CODE.Equals(PROD_CODE)).FirstOrDefault();

                                int DOC_SFCD_ID = 0;
                                string DOC_CODE = "";
                                int DOC_VER = 0;
                                int DOC_REV = 0;
                                string BRAND_CODE = "";
                                string SIZE = "";
                                string UOM = "";
                                string PACK = "";
                                //DOC_SFCD_ID = int.Parse(range.Cell($"B{i}").Value.ToString());
                                //BRAND_CODE = range.Cell($"C{i}").Value.ToString();
                                //SIZE = range.Cell($"E{i}").Value.ToString();
                                //UOM = range.Cell($"F{i}").Value.ToString();
                                //PACK = range.Cell($"G{i}").Value.ToString();
                                if (prod_detail != null)
                                {
                                    DOC_SFCD_ID = prod_detail.DOC_SFCD_ID;
                                    //DOC_CODE = prod_detail.DOC_CODE;
                                    //DOC_VER = int.Parse(prod_detail.DOC_VER.ToString());
                                    //DOC_REV = int.Parse(prod_detail.DOC_REV.ToString());
                                    BRAND_CODE = prod_detail.BRAND_CODE;
                                    SIZE = prod_detail.SIZE;
                                    UOM = prod_detail.UOM;
                                    PACK = prod_detail.PACK;
                                }
                                else
                                {
                                    var e = data.ElementAt(0);
                                    DOC_SFCD_ID = e.DOC_SFCD_ID;
                                    //DOC_CODE = e.DOC_CODE;
                                    //DOC_VER = int.Parse(e.DOC_VER.ToString());
                                    //DOC_REV = int.Parse(e.DOC_REV.ToString());
                                    BRAND_CODE = e.BRAND_CODE;
                                    SIZE = e.SIZE;
                                    UOM = e.UOM;
                                    PACK = e.PACK;
                                }

                                int YEAR = int.Parse(range.Cell($"K{i}").Value.ToString());
                                decimal M1Sales = decimal.Parse(range.Cell($"L{i}").Value.ToString().Replace(",",""));
                                decimal M1FOC = decimal.Parse(range.Cell($"M{i}").Value.ToString().Replace(",", ""));
                                decimal M2Sales = decimal.Parse(range.Cell($"N{i}").Value.ToString().Replace(",", ""));
                                decimal M2FOC = decimal.Parse(range.Cell($"O{i}").Value.ToString().Replace(",", ""));
                                decimal M3Sales = decimal.Parse(range.Cell($"P{i}").Value.ToString().Replace(",", ""));
                                decimal M3FOC = decimal.Parse(range.Cell($"Q{i}").Value.ToString().Replace(",", ""));
                                decimal M4Sales = decimal.Parse(range.Cell($"R{i}").Value.ToString().Replace(",", ""));
                                decimal M4FOC = decimal.Parse(range.Cell($"S{i}").Value.ToString().Replace(",", ""));
                                decimal M5Sales = decimal.Parse(range.Cell($"T{i}").Value.ToString().Replace(",", ""));
                                decimal M5FOC = decimal.Parse(range.Cell($"U{i}").Value.ToString().Replace(",", ""));
                                decimal M6Sales = decimal.Parse(range.Cell($"V{i}").Value.ToString().Replace(",", ""));
                                decimal M6FOC = decimal.Parse(range.Cell($"W{i}").Value.ToString().Replace(",", ""));
                                decimal M7Sales = decimal.Parse(range.Cell($"X{i}").Value.ToString().Replace(",", ""));
                                decimal M7FOC = decimal.Parse(range.Cell($"Y{i}").Value.ToString().Replace(",", ""));
                                decimal M8Sales = decimal.Parse(range.Cell($"Z{i}").Value.ToString().Replace(",", ""));
                                decimal M8FOC = decimal.Parse(range.Cell($"AA{i}").Value.ToString().Replace(",", ""));
                                decimal M9Sales = decimal.Parse(range.Cell($"AB{i}").Value.ToString().Replace(",", ""));
                                decimal M9FOC = decimal.Parse(range.Cell($"AC{i}").Value.ToString().Replace(",", ""));
                                decimal M10Sales = decimal.Parse(range.Cell($"AD{i}").Value.ToString().Replace(",", ""));
                                decimal M10FOC = decimal.Parse(range.Cell($"AE{i}").Value.ToString().Replace(",", ""));
                                decimal M11Sales = decimal.Parse(range.Cell($"AF{i}").Value.ToString().Replace(",", ""));
                                decimal M11FOC = decimal.Parse(range.Cell($"AG{i}").Value.ToString().Replace(",", ""));
                                decimal M12Sales = decimal.Parse(range.Cell($"AH{i}").Value.ToString().Replace(",", ""));
                                decimal M12FOC = decimal.Parse(range.Cell($"AI{i}").Value.ToString().Replace(",", ""));

                                if (DOC_SFCH_ID != 0) //
                                {

                                    SF_Upload.Add(new SaleForecastDetailDto
                                    {
                                        DOC_SFCH_ID = DOC_SFCH_ID,  // 1
                                        DOC_SFCD_ID = DOC_SFCD_ID,  // 2
                                        DOC_CODE = DOC_CODE,
                                        DOC_VER = DOC_VER,
                                        DOC_REV = DOC_REV,
                                        BRAND_CODE = BRAND_CODE,
                                        SIZE = SIZE,
                                        UOM = UOM,
                                        PACK = PACK,
                                        PROD_CODE = PROD_CODE,  // 12
                                        YEAR = YEAR,
                                        M1Sales = M1Sales,
                                        M2Sales = M2Sales,
                                        M3Sales = M3Sales,
                                        M4Sales = M4Sales,
                                        M5Sales = M5Sales,
                                        M6Sales = M6Sales,
                                        M7Sales = M7Sales,
                                        M8Sales = M8Sales,
                                        M9Sales = M9Sales,
                                        M10Sales = M10Sales,
                                        M11Sales = M11Sales,
                                        M12Sales = M12Sales,
                                        M1FOC = M1FOC,
                                        M2FOC = M2FOC,
                                        M3FOC = M3FOC,
                                        M4FOC = M4FOC,
                                        M5FOC = M5FOC,
                                        M6FOC = M6FOC,
                                        M7FOC = M7FOC,
                                        M8FOC = M8FOC,
                                        M9FOC = M9FOC,
                                        M10FOC = M10FOC,
                                        M11FOC = M11FOC,
                                        M12FOC = M12FOC,
                                        DOC_STATUS = Domain.Enums.DOCUMENT_STATUS.NONE,
                                    });

                                    records++;

                                }
                                else
                                {
                                    skipDup++;

                                }
                            }
                            catch (Exception ex)
                            {
                                skipDup++;
                                //return Json(new JsonResultModel() { ret = false, message = "Upload fail : " + ex.Message + " with " + records + " and skip " + skipDup + " record(s)" }, JsonRequestBehavior.AllowGet);
                            }
                        }

                        if (SF_Upload.Count > 0)
                        {
                            //SaleForecastHeaderDto headerDto = _biz.SaleForecastService.GetHeaderById(SF_Upload[0].DOC_SFCH_ID);
                            //var sfdetail = _biz.SaleForecastService.GetDetailItems(SF_Upload[0].DOC_SFCH_ID);
                            // insert detail

                            CreateSaleForecastDetailList(SF_Upload);

                        }

                        // code for manage detail
                        //

                    }
                    else
                    {
                        return Json(new JsonResultModel() { ret = true, message = $"{file.FileName} : No Sale Forecast Data to Import!" }, JsonRequestBehavior.AllowGet);
                        //return View();
                    }
                }
                
                ViewBag.Message = "Sale Forecast's File Import Successfully!!";
                //return View();
            }
            catch (Exception ex)
            {
                ViewBag.Message = $"Sale Forecast's File upload failed : {ex.Message}";
                //return View();
                return Json(new JsonResultModel() { ret = true, message = "Upload Success with " + records + " and skip " + skipDup + " record(s)" }, JsonRequestBehavior.AllowGet);
            }
            //return Json(new JsonResultModel() { ret = true, message = "Upload Success with " + records + " and skip " + skipDup + " record(s)" }, JsonRequestBehavior.AllowGet);
            return RedirectToAction("SaleForecastItem", new { DOC_SFCH_ID = DOC_SFCH_ID });

        }
        #endregion

    }

}