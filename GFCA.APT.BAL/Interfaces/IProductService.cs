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
        ProductDto GetById(int Id);
        BusinessResponse Create(ProductDto model);
        BusinessResponse Edit(ProductDto model);
        BusinessResponse Remove(ProductDto model);



       //IEnumerable<ProductDto> GetAll();
       
        IEnumerable<APTProduct> GetMatGroup();
        IEnumerable<APTProduct> GetMatGroup1();
        IEnumerable<APTProduct> GetMatGroup2();
        IEnumerable<APTProduct> GetMatGroup3();
        IEnumerable<APTProduct> GetMatGroup4();
        IEnumerable<APTProduct> GetFormula();
        IEnumerable<APTProduct> GetPack();


        IEnumerable<CustomerDto> GetCustomer();


        // BusinessResponse Create(ProductDto model);
        //   BusinessResponse Edit(ProductDto model);
        //   BusinessResponse Remove(ProductDto model);

    }
}
