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
    public class ProductService : ServiceBase, IProductService
    {

        private readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        internal static ProductService CreateInstant()
        {
            var uow = UnitOfWork.CreateInstant();
            var svc = new ProductService(uow);

            return svc;
        }

        public ProductService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

     
        public IEnumerable<ProductDto> GetAll()
        {
            var dto = _uow.ProductRepository.All();
            return dto;
        }
        public ProductDto GetByCode(string code)
        {

            var dto = _uow.ProductRepository.GetByCode(code);
            return dto;
        }

        public BusinessResponse Create(ProductDto model)
        {
            var response = new BusinessResponse();
            try
            {
                var dto = new ProductDto();
                // dto.PROD_ID = 0;
                dto.PROD_CODE = model.PROD_CODE;
                dto.PROD_NAME = model.PROD_NAME;
                dto.CUST_CODE = model.CUST_CODE;
                dto.MAT_CODE = model.MAT_CODE;
                dto.ORG_CODE = model.ORG_CODE;
                dto.DIV_CODE = model.DIV_CODE;
                dto.EMIS_CODE = model.EMIS_CODE;
                dto.MAT_GROUP = model.MAT_GROUP;
                dto.MAT_GROUP_DESC = model.MAT_GROUP_DESC;
                dto.MAT_GROUP1 = model.MAT_GROUP1;
                dto.MAT_GROUP1_DESC = model.MAT_GROUP1_DESC;
                dto.MAT_GROUP2 = model.MAT_GROUP2;
                dto.MAT_GROUP2_DESC = model.MAT_GROUP2_DESC;
                dto.MAT_GROUP3 = model.MAT_GROUP3;
                dto.MAT_GROUP3_DESC = model.MAT_GROUP3_DESC;
                dto.FORMULA = model.FORMULA;
                dto.PACK = model.PACK;
                dto.PACK_DESC = model.PACK_DESC;
              //  dto. = model.UNIT_CODE;
                dto.FLAG_ROW = model.FLAG_ROW;
                dto.CREATED_BY = _currentUser.UserName ?? "System";
                dto.CREATED_DATE = System.DateTime.Now;
                dto.UPDATED_BY = model.UPDATED_BY ?? "";
                dto.UPDATED_DATE = model.UPDATED_DATE ?? null;
                _uow.ProductRepository.Insert(dto);
                _uow.Commit();
                response.Message = $"{typeof(ProductService)} has been created";
                response.Success = true;
            }
            catch (Exception ex)
            {

                response.Message = ex.Message.ToString();
            }
            finally
            {

                // base.Dispose();
            }
            return response;


        }


        public BusinessResponse Remove(ProductDto model)
        {
            var response = new BusinessResponse();
            try
            {
                if (string.IsNullOrEmpty(model.PROD_CODE))
                    throw new Exception("not existing PROD_ID");

                string code = model.PROD_CODE;
                var dto = _uow.ProductRepository.GetByCode(code);

                dto.FLAG_ROW = FLAG_ROW.DELETE;
                dto.UPDATED_BY = _currentUser.UserName ?? "System";
                dto.UPDATED_DATE = DateTime.UtcNow;

                _uow.ProductRepository.Update(dto);
                //_unitOfWork.Commit();

                response.Message = $"{typeof(ProductService)} has been deleted";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message.ToString();
                //_logger.Error($"{ex.Message}");
            }
            finally
            {
                base.Dispose();
            }

            return response;
        }

        public BusinessResponse Edit(ProductDto model)
        {
            var response = new BusinessResponse();
            try
            {

                if (string.IsNullOrEmpty(model.PROD_CODE))
                    throw new Exception("not existing PROD ID");

                string code = model.PROD_CODE;
                var dto = _uow.ProductRepository.GetByCode(code);



              
                dto.CUST_CODE = model.CUST_CODE;

                dto.EMIS_CODE = model.EMIS_CODE;
                dto.UPDATED_BY = model.UPDATED_BY;
                dto.UPDATED_DATE = DateTime.UtcNow;

                _uow.ProductRepository.Update(dto);
                _uow.Commit();

                response.Success = true;
                response.MessageType = TOAST_TYPE.SUCCESS;
                response.Message = $"Product ({model.PROD_CODE}) has been changed";

 
            }
            catch (Exception ex)
            {
                response.Message = ex.Message.ToString();

            }
            finally
            {
                base.Dispose();
            }

            return response;


        }

      


        /*
        public IEnumerable<APTProduct> GetMatGroup()
        {
            var dto = _uow.ProductRepository.GetMatGroup();
            return dto;
        }

        public IEnumerable<APTProduct> GetMatGroup1()
        {
            var dto = _uow.ProductRepository.GetMatGroup1();
            return dto;
        }

        public IEnumerable<APTProduct> GetMatGroup2()
        {
           var dto = _uow.ProductRepository.GetMatGroup2();
            return dto;
        }

        public IEnumerable<APTProduct> GetMatGroup3()
        {
            var dto = _uow.ProductRepository.GetMatGroup3();
            return dto;
        }

        public IEnumerable<APTProduct> GetMatGroup4()
        {

            var dto = _uow.ProductRepository.GetMatGroup4();
            return dto;
        }

        public IEnumerable<APTProduct> GetFormula()
        {
            var dto = _uow.ProductRepository.GetFormula();
            return dto;
        }

        public IEnumerable<APTProduct> GetPack()
        {
            var dto = _uow.ProductRepository.GetPack();
            return dto;
        }

        public IEnumerable<CustomerDto> GetCustomer()
        {
            var dto = _uow.ProductRepository.GetCustomer();
            return dto;
        }

      */

      


    }
}
