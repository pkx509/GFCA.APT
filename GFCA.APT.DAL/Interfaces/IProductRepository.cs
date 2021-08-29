using GFCA.APT.Domain.Dto;
using System.Collections.Generic;

namespace GFCA.APT.DAL.Interfaces
{
    public interface IProductRepository : IRepositories<ProductDto>
    {
        ProductDto GetById(int Id);


        void Update(ProductDto _ProductDto);

        IEnumerable<APTProduct> GetMatGroup();
        IEnumerable<APTProduct> GetMatGroup1();
        IEnumerable<APTProduct> GetMatGroup2();
        IEnumerable<APTProduct> GetMatGroup3();
        IEnumerable<APTProduct> GetMatGroup4();

        IEnumerable<APTProduct> GetFormula();
        IEnumerable<APTProduct> GetPack();

        IEnumerable<APTProduct> GetSize();
        IEnumerable<APTProduct> GetSixeUOM();
        IEnumerable<APTProduct> GetCONV_FCL();
        IEnumerable<APTProduct> GetCONV_L();

        IEnumerable<CustomerDto> GetCustomer();

 
    }
}
