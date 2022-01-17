using GFCA.APT.BAL.Interfaces;
using GFCA.APT.DAL.Implements;
using GFCA.APT.DAL.Interfaces;
using GFCA.APT.Domain.Dto;
using GFCA.APT.Domain.Enums;
using GFCA.APT.Domain.HTTP.Controls;
using GFCA.APT.Domain.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GFCA.APT.BAL.Implements
{
    public class SaleForecastService: ServiceBase, ISaleForecastService
    {
        private readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        #region [ constructor ]
        internal static SaleForecastService CreateInstant()
        {
            var uow = UnitOfWork.CreateInstant();
            var svc = new SaleForecastService(uow);

            return svc;
        }
        public SaleForecastService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
        #endregion [ constructor ]

        #region [ header ]
        public IEnumerable<SaleForecastHeaderDto> GetHeaderAll()
        {
            try
            {
                var doch = _uow.SaleForecastRepository.GetSaleForecastAll();
                return doch;
            }
            catch (Exception ex)
            {
                _uow.Dispose();
                throw ex;
            }
        }
        public SaleForecastHeaderDto GetHeaderById(int id)
        {
            try
            {
                var doch = _uow.SaleForecastRepository.GetSaleForecastAll()
                    .Where(w => w.DOC_SFCH_ID == id);
                return doch.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _uow.Dispose();
                throw ex;
            }
        }
        public SaleForecastHeaderDto GetHeaderByCode(string code, int ver = -1, int rev = -1)
        {
            try
            {
                
                //FC-YYYYMM-VVRR
                string docCode = code.Substring(0, 9);
                var doch = _uow.SaleForecastRepository.GetSaleForecastAll()
                    .Where(w => w.DOC_CODE.Contains(code));


                if (ver > 0) //get by specify version/revision
                {
                    string docVersion = $"{docCode}-{ver.ToString("00")}";
                    doch = doch.Where(w => w.DOC_CODE.Contains(docVersion));

                    if (rev > 0)
                    {
                        string docRevision = $"{docCode}-{docVersion}{rev.ToString("00")}";
                        doch = doch.Where(w => w.DOC_CODE.Contains(docRevision));

                    }
                }

                return doch.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _uow.Dispose();
                throw ex;
            }
        }

        public BusinessResponse CreateHeader(SaleForecastHeaderDto model)
        {
            BusinessResponse response = new BusinessResponse(false, MESSAGE_TYPE.WARNING, string.Empty);
            try
            {
                var doch = model;

                var doc = _uow.DocumentRepository.GenerateDocNo(doch.DOC_TYPE_CODE, doch.CUST_CODE, doch.DOC_YEAR, doch.DOC_MONTH);
                //generate document no

                doc.DOC_STATUS = DOCUMENT_STATUS.DRAFT;
                doc.FLOW_CURRENT = "";
                doc.FLOW_NEXT = "";
                doc.COMP_CODE = doch.COMP_CODE;
                doc.COMP_NAME = doch.COMP_NAME;
                doc.CUST_CODE = doch.CUST_CODE;
                doc.CUST_NAME = doch.CUST_NAME;
                doc.ORG_CODE = doch.ORG_CODE;
                doc.ORG_NAME = doch.ORG_NAME;
                doc.REQUESTER = "System";
                _uow.DocumentRepository.Insert(doc);
                doch.DOC_VER = doc.DOC_VER;
                doch.DOC_CODE = doc.DOC_CODE;
                /*
                doch.DOC_TYPE_CODE = "";
                //doch.DOC_CODE      = "";
                doch.DOC_VER       = doc.DOC_VER.ToString();
                doch.DOC_REV       = doc.DOC_REV.ToString();
                doch.DOC_MONTH     = "";
                doch.DOC_YEAR      = "";
                doch.DOC_STATUS    = "";
                doch.FLOW_CURRENT  = "";
                doch.FLOW_NEXT     = "";
                //doch.REQUESTER     = "";
                //doch.DOC_FCH_ID    = 0;
                doch.CLIENT_CODE   = "";
                doch.CLIENT_NAME   = "";
                doch.CUST_CODE     = "";
                doch.CHANNEL_CODE  = "";
                doch.CHANNEL_NAME  = "";
                doch.FLAG_ROW      = "S";
                */
                doch.CREATED_BY = doc.REQUESTER;
                doch.CREATED_DATE = DateTime.UtcNow;
                _uow.SaleForecastRepository.InsertSaleForecastHeader(doch);

                _uow.Commit();
                response.Success = true;
                response.MessageType = MESSAGE_TYPE.SUCCESS;
                response.Message = string.Empty;
                response.Data = doch;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.MessageType = MESSAGE_TYPE.ERROR;
                response.Message = ex.Message;
                _logger.Error("CreateHeader", ex);
                _uow.Dispose();
            }

            return response;
        }
        public BusinessResponse EditHeader(SaleForecastHeaderDto model)
        {
            BusinessResponse response = new BusinessResponse(false, MESSAGE_TYPE.WARNING, string.Empty);
            try
            {
                var doch = model;

                var doc = _uow.DocumentRepository.GenerateDocNo(doch.DOC_TYPE_CODE, doch.DOC_CODE);
                //generate document no

                GenerateNextState(doch.DOC_STATUS, doch.COMMAND_TYPE, ref doc);

                doc.DOC_STATUS = DOCUMENT_STATUS.DRAFT;
                doc.FLOW_CURRENT = "";
                doc.FLOW_NEXT = "";
                doc.REQUESTER = "System";

                _uow.DocumentRepository.Insert(doc);
                doch.DOC_SFCH_ID = doc.DOC_VER;
                doch.DOC_CODE = doc.DOC_CODE;
                
                doch.REQUESTER = doc.REQUESTER;
                //doch.CREATED_BY = doc.REQUESTER;
                //doch.CREATED_DATE = DateTime.UtcNow;
                doch.UPDATED_BY = doc.REQUESTER;
                doch.UPDATED_DATE = DateTime.UtcNow;
                _uow.SaleForecastRepository.InsertSaleForecastHeader(doch);

                _uow.Commit();
                response.Success = true;
                response.MessageType = MESSAGE_TYPE.SUCCESS;
                response.Message = string.Empty;

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.MessageType = MESSAGE_TYPE.ERROR;
                response.Message = ex.Message;
                _uow.Dispose();
            }

            return response;
        }
        public BusinessResponse RemoveHeader(SaleForecastHeaderDto model)
        {
            BusinessResponse response = new BusinessResponse();
            try
            {
                _uow.SaleForecastRepository.DeleteSaleForecastHeader(model.DOC_SFCH_ID);
                
                _uow.Commit();
                response.Success = true;
                response.MessageType = MESSAGE_TYPE.SUCCESS;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.MessageType = MESSAGE_TYPE.ERROR;
                _logger.Error("RemoveHeader : ", ex);
                _uow.Dispose();
            }

            return response;
        }
        #endregion [ header ]

        #region [ detail ]
        public SaleForecastDetailDto GetDetailItem(int DOC_SFCD_ID)
        {
            var documentType = "SF";
            SaleForecastDto dto = new SaleForecastDto(DOC_SFCD_ID);
            try
            {
                if (dto.DataMode == PAGE_MODE.CREATING)
                    return dto.DetailItem;

                var docd = _uow.SaleForecastRepository.GetDetailItem(DOC_SFCD_ID);
                //dto. = docd;
                var doch = _uow.SaleForecastRepository.GetSaleForecastByItemID(docd.DOC_SFCH_ID);
                dto.HeaderData = doch;
                dto.DocumentData = _uow.DocumentRepository.GetDocumentStateFlow(documentType, doch.DOC_SFCH_ID);
                dto.HistoryData = _uow.DocumentRepository.GetDocumentHistories(documentType, doch.DOC_SFCH_ID);

                return docd;
            }
            catch (Exception ex)
            {
                _uow.Dispose();
                throw ex;
            }
        }
        public IEnumerable<SaleForecastDetailDto> GetDetailItems(int DOC_SFCH_ID)
        {
            try
            {
                var docd = _uow.SaleForecastRepository.GetDetailItems(DOC_SFCH_ID);
                return docd;
            }
            catch (Exception ex)
            {
                _uow.Dispose();
                throw ex;
            }
        }
        public IEnumerable<SaleForecastDetailDto> GetDetailItems(string code, int ver = -1, int rev = -1)
        {
            try
            {
                /*
                string docCode = code.Substring(0, 9);

                //FC-YYYYMM-VVRR
                var docd = _uow.SaleForecastRepository.GetDetailAll()
                    .Where(w => w.DOC_CODE.Contains(docCode));
                

                if (ver > 0) 
                {
                    string docVersion = $"{docCode}-{ver.ToString("00")}";
                    docd = docd.Where(w => w.DOC_CODE.Contains(docVersion));

                    if (rev > 0)
                    {
                        string docRevision = $"{docCode}-{docVersion}{rev.ToString("00")}";
                        docd = docd.Where(w => w.DOC_CODE.Contains(docRevision));

                    }
                }
                */
                var docd = _uow.SaleForecastRepository.GetDetailItems(7);
                return docd;
            }
            catch (Exception ex)
            {
                _uow.Dispose();
                throw ex;
            }
        }
        
        public BusinessResponse CreateDetail(SaleForecastDetailDto model)
        {
            BusinessResponse response = new BusinessResponse(false, MESSAGE_TYPE.WARNING, string.Empty);
            try
            {
                
                var docd = model;
                var doch = _uow.SaleForecastRepository.GetSaleForecastByItemID(docd.DOC_SFCH_ID);
                //validate new document
                //docd.DOC_SFCD_ID = 1;
                docd.DOC_SFCH_ID = doch.DOC_SFCH_ID;
                docd.DOC_CODE = doch.DOC_CODE;
                docd.DOC_VER = doch.DOC_VER;
                docd.DOC_REV = doch.DOC_REV;
                //docd.ACC_CODE = docd.ACC_CODE;
                //docd.CONDITION_TYPE = CONDITION_TYPE.PLANNING;
                //docd.CONTRACT_CATE = docd.CONTRACT_CATE;

                docd.CREATED_BY = "System";
                docd.CREATED_DATE = DateTime.UtcNow;
                _uow.SaleForecastRepository.InsertSaleForecastDetail(docd);

                
                _uow.Commit();
                response.Success = true;
                response.MessageType = MESSAGE_TYPE.SUCCESS;
                response.Message = string.Empty;
                response.Data = docd;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.MessageType = MESSAGE_TYPE.ERROR;
                response.Message = ex.Message;
                _uow.Dispose();
            }

            return response;
        }
        public BusinessResponse EditDetail(SaleForecastDetailDto model)
        {
            BusinessResponse response = new BusinessResponse(false, MESSAGE_TYPE.WARNING, string.Empty);
            try
            {
                var docd = model;

                //validate new document
                docd.DOC_SFCD_ID = docd.DOC_SFCD_ID;
                docd.DOC_SFCH_ID = docd.DOC_SFCH_ID;
                docd.DOC_CODE = docd.DOC_CODE;
                //docd.CONDITION_TYPE = CONDITION_TYPE.PLANNING;
                //docd.DOC_VER = doch.DOC_VER;
                //docd.DOC_REV = doch.DOC_REV;

                //docd.CREATED_BY = "System";
                //docd.CREATED_DATE = DateTime.UtcNow;
                docd.UPDATED_BY = "System";
                docd.UPDATED_DATE = DateTime.UtcNow;
                _uow.SaleForecastRepository.UpdateSaleForecastDetail(docd);

                _uow.Commit();
                response.Success = true;
                response.MessageType = MESSAGE_TYPE.SUCCESS;
                response.Message = string.Empty;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.MessageType = MESSAGE_TYPE.ERROR;
                response.Message = ex.Message;
                _logger.Error("EditDetail : ", ex);
                _uow.Dispose();
            }

            return response;
        }
        public BusinessResponse RemoveDetail(SaleForecastDetailDto model)
        {
            BusinessResponse response = new BusinessResponse();
            try
            {
                _uow.SaleForecastRepository.DeleteSaleForecastDetail(model.DOC_SFCD_ID);
                
                _uow.Commit();
                response.Success = true;
                response.MessageType = MESSAGE_TYPE.SUCCESS;
                response.Message = string.Empty;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                response.MessageType = MESSAGE_TYPE.ERROR;
                _logger.Error("RemoveDetail : ", ex);
                _uow.Dispose();
            }

            return response;
        }
        public BusinessResponse CreateDetailList(List<SaleForecastDetailDto> model)
        {
            BusinessResponse response = new BusinessResponse(false, MESSAGE_TYPE.WARNING, string.Empty);
            try
            {

                var docd = model;
                var doch = _uow.SaleForecastRepository.GetSaleForecastByItemID(docd[0].DOC_SFCH_ID);
                var sfdetail = _uow.SaleForecastRepository.GetDetailItems(docd[0].DOC_SFCH_ID);
                //validate new document
                //docd.DOC_SFCD_ID = 1;
                //
                foreach (var item in model)
                {
                    item.DOC_SFCH_ID = doch.DOC_SFCH_ID;
                    item.DOC_CODE = doch.DOC_CODE;
                    item.DOC_VER = doch.DOC_VER;
                    item.DOC_REV = doch.DOC_REV;
                    //docd.ACC_CODE = docd.ACC_CODE;
                    //docd.CONDITION_TYPE = CONDITION_TYPE.PLANNING;
                    //docd.CONTRACT_CATE = docd.CONTRACT_CATE;

                    var chkExist = sfdetail.Where(x => x.PROD_CODE.Equals(item.PROD_CODE)).FirstOrDefault();
                    if (chkExist == null)
                    {
                        item.CREATED_BY = "System";
                        item.CREATED_DATE = DateTime.UtcNow;
                        _uow.SaleForecastRepository.InsertSaleForecastDetail(item);
                    }
                    else
                    {
                        item.UPDATED_BY = "System";
                        item.UPDATED_DATE = DateTime.UtcNow;
                        _uow.SaleForecastRepository.UpdateSaleForecastDetail(item);
                    }

                    _uow.Commit();
                }
                response.Success = true;
                response.MessageType = MESSAGE_TYPE.SUCCESS;
                response.Message = string.Empty;
                response.Data = docd;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.MessageType = MESSAGE_TYPE.ERROR;
                response.Message = ex.Message;
                _uow.Dispose();
            }

            return response;
        }
        #endregion [ detail ]

        #region Export
        public IEnumerable<SaleForecastDetailDto> GetDetailItemToExport(int DOC_FCH_ID)
        {
            try
            {
                var docd = _uow.SaleForecastRepository.GetDetailItemToExport(DOC_FCH_ID);
                return docd;
            }
            catch (Exception ex)
            {
                _uow.Dispose();
                throw ex;
            }
        }
        #endregion

    }
}
