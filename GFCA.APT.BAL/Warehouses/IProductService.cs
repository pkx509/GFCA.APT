using GFCA.APT.Domain.Dto;
using GFCA.APT.Domain.Models;
using System.Collections.Generic;

namespace GFCA.APT.BAL.Warehouses
{
    public interface IProductService
    {
        IEnumerable<ProductDto> GetAll();
        ProductDto GetByID(int Id);
        BusinessResponse Create(ProductDto model);
        BusinessResponse Edit(ProductDto model);
        BusinessResponse Delete(ProductDto model);

    }
}
