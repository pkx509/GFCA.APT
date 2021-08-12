using GFCA.APT.BAL.Log;
using GFCA.APT.DAL.Implements;
using GFCA.APT.DAL.Interfaces;
using GFCA.APT.Domain.Dto;
using GFCA.APT.Domain.Models;
using System;
using System.Collections.Generic;

namespace GFCA.APT.BAL.Warehouses
{
    public class ProductService : BusinessBase, IProductService
    {
        public static ProductService CreateInstant(ILogService log)
        {
            var uow = UnitOfWork.CreateInstant();
            var svc = new ProductService(uow, log);

            return svc;
        }
        public ProductService(IUnitOfWork unitOfWork, ILogService log) : base(unitOfWork, log) { }

        public IEnumerable<ProductDto> GetAll()
        {
            var dto = _uow.ProductRepository.All();
            return dto;
        }

        public BusinessResponse Create(ProductDto model)
        {
            throw new NotImplementedException();
        }

        public BusinessResponse Delete(ProductDto model)
        {
            throw new NotImplementedException();
        }

        public BusinessResponse Edit(ProductDto model)
        {
            throw new NotImplementedException();
        }

   

        public ProductDto GetByID(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
