using GFCA.APT.BAL.Log;
using GFCA.APT.DAL;
using GFCA.APT.Domain.Dto;
using GFCA.APT.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFCA.APT.BAL.Warehouses
{
    public class ProductService : BusinessBase, IProductService
    {
        public ProductService(IUnitOfWork unitOfWork, ILogService log) : base(unitOfWork, log) { }

        public IEnumerable<ProductDto> GetAll()
        {
            var dto = _unitOfWork.Product.GetAll();
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
