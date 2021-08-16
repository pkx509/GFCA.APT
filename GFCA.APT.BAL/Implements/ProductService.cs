using GFCA.APT.BAL.Interfaces;
using GFCA.APT.DAL.Implements;
using GFCA.APT.DAL.Interfaces;
using GFCA.APT.Domain.Dto;
using GFCA.APT.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFCA.APT.BAL.Implements
{
    public class ProductService : ServiceBase, IProductService
    {
        public static ProductService CreateInstant()
        {
            var uow = UnitOfWork.CreateInstant();
            var svc = new ProductService(uow);

            return svc;
        }
        public ProductService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public IEnumerable<ProductDto> GetAll()
        {
            var dto = _uow.ProductRepository.All();
            return dto;
        }

        public ProductDto GetByID(int Id)
        {
            throw new NotImplementedException();
        }

        public BusinessResponse Create(ProductDto model)
        {
            throw new NotImplementedException();
        }

        public BusinessResponse Edit(ProductDto model)
        {
            throw new NotImplementedException();
        }

        public BusinessResponse Delete(ProductDto model)
        {
            throw new NotImplementedException();
        }
    }
}
