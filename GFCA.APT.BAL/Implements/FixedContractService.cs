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
using System.Text;
using System.Threading.Tasks;

namespace GFCA.APT.BAL.Implements
{
    public class FixedContractService: ServiceBase, IFixedContractService
    {
        private readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        #region [ constructor ]
        internal static FixedContractService CreateInstant()
        {
            var uow = UnitOfWork.CreateInstant();
            var svc = new FixedContractService(uow);

            return svc;
        }
        public FixedContractService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
        #endregion [ constructor ]

        #region [ header ]
        public IEnumerable<FixedContractHeaderDto> GetHeaderAll()
        {
            try
            {
                var doch = _uow.FixedContractRepository.GetHeaderAll();
                return doch;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public FixedContractHeaderDto GetHeaderById(int id)
        {
            try
            {
                var doch = _uow.FixedContractRepository.GetHeaderAll()
                    .Where(w => w.DOC_FCH_ID == id);
                return doch.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public FixedContractHeaderDto GetHeaderByCode(string code, int ver = -1, int rev = -1)
        {
            try
            {
                
                //FC-YYYYMM-VVRR
                string docCode = code.Substring(0, 9);
                var doch = _uow.FixedContractRepository.GetHeaderAll()
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
                throw ex;
            }
        }

        public BusinessResponse CreateHeader(FixedContractHeaderDto model)
        {
            BusinessResponse response = new BusinessResponse(false, TOAST_TYPE.WARNING, string.Empty);
            try
            {
                var doch = model;

                var doc = _uow.DocumentRepository.GenerateDocNo(doch.DOC_TYPE_CODE, int.Parse(doch.DOC_YEAR), int.Parse(doch.DOC_MONTH), doch.CLIENT_CODE, doch.CHANNEL_CODE, doch.CUST_CODE);
                //generate document no

                doc.DOC_STATUS = DOCUMENT_STATUS.DRAFT;
                doc.FLOW_CURRENT = "";
                doc.FLOW_NEXT = "";

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
                _uow.FixedContractRepository.InsertHeader(doch);

                _uow.Commit();
                response.Success = true;
                response.MessageType = TOAST_TYPE.SUCCESS;
                response.Message = string.Empty;

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.MessageType = TOAST_TYPE.ERROR;
                response.Message = ex.Message;
            }

            return response;
        }
        public BusinessResponse EditHeader(FixedContractHeaderDto model)
        {
            BusinessResponse response = new BusinessResponse(false, TOAST_TYPE.WARNING, string.Empty);
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
                doch.DOC_FCH_ID = doc.DOC_VER;
                doch.DOC_CODE = doc.DOC_CODE;
                
                doch.REQUESTER = doc.REQUESTER;
                doch.CREATED_BY = doc.REQUESTER;
                doch.CREATED_DATE = DateTime.UtcNow;
                _uow.FixedContractRepository.InsertHeader(doch);

                _uow.Commit();
                response.Success = true;
                response.MessageType = TOAST_TYPE.SUCCESS;
                response.Message = string.Empty;

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.MessageType = TOAST_TYPE.ERROR;
                response.Message = ex.Message;
            }

            return response;
        }
        #endregion [ header ]

        #region [ detail ]
        public FixedContractDto GetDetailItem(int DOC_FCD_ID)
        {
            FixedContractDto dto = new FixedContractDto();
            dto.Detail = new FixedContractDetailDto();
            dto.Header = new FixedContractHeaderDto();
            dto.Stateflow = new DocumentStateFlowDto();
            dto.Histories = new List<DocumentHistoryDto>();
            try
            {
                dto.DataMode = Domain.Enums.PAGE_MODE.EDITING;
                if (DOC_FCD_ID == 0) 
                {
                    dto.DataMode = Domain.Enums.PAGE_MODE.CREATING;
                    return dto;
                }

                var docd = _uow.FixedContractRepository.GetDetailItem(DOC_FCD_ID);
                dto.Detail = docd;
                var doch = _uow.FixedContractRepository.GetHeaderById(docd.DOC_FCH_ID);
                dto.Header = doch;
                //dto.Stateflow = _uow.DocumentRepository.GetDocumentStateFlow(doch.DOC_FCH_ID);
                dto.Histories = _uow.DocumentRepository.GetDocumentHistories(doch.DOC_FCH_ID);

                return dto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<FixedContractDetailDto> GetDetailItems(int DOC_FCH_ID)
        {
            try
            {
                var docd = _uow.FixedContractRepository.GetDetailItems(DOC_FCH_ID);
                return docd;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<FixedContractDetailDto> GetDetailItems(string code, int ver = -1, int rev = -1)
        {
            try
            {
                /*
                string docCode = code.Substring(0, 9);

                //FC-YYYYMM-VVRR
                var docd = _uow.FixedContractRepository.GetDetailAll()
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
                var docd = _uow.FixedContractRepository.GetDetailItems(code);
                return docd;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public BusinessResponse CreateDetail(FixedContractDetailDto model)
        {
            BusinessResponse response = new BusinessResponse(false, TOAST_TYPE.WARNING, string.Empty);
            try
            {
                
                var docd = model;

                //validate new document
                //docd.DOC_FCD_ID = 1;
                docd.DOC_FCH_ID = docd.DOC_FCH_ID;
                docd.DOC_CODE = docd.DOC_CODE;
                //docd.DOC_VER = doch.DOC_VER;
                //docd.DOC_REV = doch.DOC_REV;

                docd.CREATED_BY = "System";
                docd.CREATED_DATE = DateTime.UtcNow;
                _uow.FixedContractRepository.InsertDetail(docd);

                
                _uow.Commit();
                response.Success = true;
                response.MessageType = TOAST_TYPE.SUCCESS;
                response.Message = string.Empty;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.MessageType = TOAST_TYPE.ERROR;
                response.Message = ex.Message;
            }

            return response;
        }
        public BusinessResponse EditDetail(FixedContractDetailDto model)
        {
            BusinessResponse response = new BusinessResponse(false, TOAST_TYPE.WARNING, string.Empty);
            try
            {
                var docd = model;

                //validate new document
                docd.DOC_FCD_ID = docd.DOC_FCD_ID;
                docd.DOC_FCH_ID = docd.DOC_FCH_ID;
                docd.DOC_CODE = docd.DOC_CODE;
                //docd.DOC_VER = doch.DOC_VER;
                //docd.DOC_REV = doch.DOC_REV;

                docd.CREATED_BY = "System";
                docd.CREATED_DATE = DateTime.UtcNow;
                _uow.FixedContractRepository.UpdateDetail(docd);


                _uow.Commit();
                response.Success = true;
                response.MessageType = TOAST_TYPE.SUCCESS;
                response.Message = string.Empty;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.MessageType = TOAST_TYPE.ERROR;
                response.Message = ex.Message;
            }

            return response;
        }
        public BusinessResponse RemoveDetail(FixedContractDetailDto model)
        {
            throw new NotImplementedException();
        }

        #endregion [ detail ]
    }
}
