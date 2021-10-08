using GFCA.APT.BAL.Interfaces;
using GFCA.APT.DAL.Implements;
using GFCA.APT.DAL.Interfaces;
using GFCA.APT.Domain.Dto;
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
        internal static FixedContractService CreateInstant()
        {
            var uow = UnitOfWork.CreateInstant();
            var svc = new FixedContractService(uow);

            return svc;
        }

        public FixedContractService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public FixedContractDto GetHeaderById(int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FixedContractDto> GetDetailByHeaderId(int Id)
        {
            throw new NotImplementedException();
        }

        public BusinessResponse Create(FixedContractDto model)
        {
            BusinessResponse response = new BusinessResponse(false, TOAST_TYPE.WARNING, string.Empty);
            try
            {
                var doch = model.Header;

                if (!_uow.DocumentRepository.ValidateFixedContract(doch.DOC_TYPE_CODE, doch.DOC_YEAR, doch.DOC_MONTH))
                    throw new Exception("Document is invalid!!");
                
                var doc = _uow.DocumentRepository.GenerateDocNo(doch.DOC_TYPE_CODE, doch.DOC_YEAR, doch.DOC_MONTH);

                doc.DOC_STATUS = "Draft";
                doc.FLOW_CURRENT = "";
                doc.FLOW_NEXT = "";
                doc.REQUESTER = "System";

                _uow.DocumentRepository.Insert(doc);

                doch.DOC_FCH_ID = doc.DOC_VER;
                doch.DOC_CODE = doc.DOC_CODE;
                doch.CREATED_BY = doc.REQUESTER;
                doch.CREATED_DATE = DateTime.UtcNow;
                _uow.FixedContractRepository.InsertHeader(doch);

                var docd = model.Detail;
                docd.DOC_FCH_ID = doc.DOC_VER;
                docd.DOC_FCD_ID = doc.DOC_REV;
                docd.DOC_CODE = doc.DOC_CODE;
                docd.CREATED_BY = doc.REQUESTER;
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

        public BusinessResponse Edit(FixedContractDto model)
        {
            try
            {

                throw new NotImplementedException();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IEnumerable<FixedContractDto> GetHeaderAll()
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public BusinessResponse Remove(FixedContractDto model)
        {
            try
            {

                throw new NotImplementedException();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
