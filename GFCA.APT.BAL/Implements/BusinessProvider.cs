using Serve = GFCA.APT.BAL.Implements;
using GFCA.APT.BAL.Interfaces;

namespace GFCA.APT.BAL.Implements
{
    public class BusinessProvider : IBusinessProvider
    {
        private ILogService _logService;
        public ILogService LogService => _logService ?? (_logService = Serve.LogService.CreateInstant());

        private IBrandService _brandService;
        public IBrandService BrandService => _brandService ?? (_brandService = Serve.BrandService.CreateInstant());

        private IEmissionService _emissionService;
        public IEmissionService EmissionService => _emissionService ?? (_emissionService = Serve.EmissionService.CreateInstant());

        private IProductService _productService;
        public IProductService ProductService => _productService ?? (_productService = Serve.ProductService.CreateInstant());

        private IGLAccountService _glaccountService;
        public IGLAccountService GLAccountService => _glaccountService ?? (_glaccountService = Serve.GLAccountService.CreateInstant());

        private IBudgetTypeService _budgetTypeService;
        public IBudgetTypeService BudgetTypeService => _budgetTypeService ?? (_budgetTypeService = Serve.BudgetTypeService.CreateInstant());
    }
}
