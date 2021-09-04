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

        private IClientService _clientService;
        public IClientService ClientService => _clientService ?? (_clientService = Serve.ClientService.CreateInstant());

        private IGLAccountService _glaccountService;
        public IGLAccountService GLAccountService => _glaccountService ?? (_glaccountService = Serve.GLAccountService.CreateInstant());

        private IBudgetTypeService _budgetTypeService;
        public IBudgetTypeService BudgetTypeService => _budgetTypeService ?? (_budgetTypeService = Serve.BudgetTypeService.CreateInstant());

        private ITradeActivityService _tradeActivityService;
        public ITradeActivityService TradeActivityService => _tradeActivityService ?? (_tradeActivityService = Serve.TradeActivityService.CreateInstant());
        
        private IChannelService _channelService;
        public IChannelService ChannelService => _channelService ?? (_channelService = Serve.ChannelService.CreateInstant());

        private ICompanyService _companyService;
        public ICompanyService CompanyService => _companyService ?? (_companyService = Serve.CompanyService.CreateInstant());

        private ICostCenterService _costCenterService;
        public ICostCenterService CostCenterService => _costCenterService ?? (_costCenterService = Serve.CostCenterService.CreateInstant());

        private ICustomerService _customerService;
        public ICustomerService CustomerService => _customerService ?? (_customerService = Serve.CustomerService.CreateInstant());

        private IDocumentTypeService _documentTypeService;
        public IDocumentTypeService DocumentTypeService => _documentTypeService ?? (_documentTypeService = Serve.DocumentTypeService.CreateInstant());
        
        private IDistributorService _distributorService;
        public IDistributorService DistributorService => _distributorService ?? (_distributorService = Serve.DistributorService.CreateInstant());

        private IEmployeeService _employeeService;
        public IEmployeeService EmployeeService => _employeeService ?? (_employeeService = Serve.EmployeeService.CreateInstant());
    }
}
