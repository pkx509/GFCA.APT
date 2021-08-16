using GFCA.APT.Domain.Dto;
using GFCA.APT.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFCA.APT.BAL.Interfaces
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
