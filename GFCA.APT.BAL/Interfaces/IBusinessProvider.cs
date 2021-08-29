
namespace GFCA.APT.BAL.Interfaces
{
    public interface IBusinessProvider
    {
        ILogService LogService { get; }
        IBrandService BrandService { get; }
        IEmissionService EmissionService { get; }
        IProductService ProductService { get; }
        IClientService ClientService { get; }
        IGLAccountService GLAccountService { get; }
        IBudgetTypeService BudgetTypeService { get; }
        ITradeActivityService TradeActivityService { get; }
        IChannelService ChannelService { get; }
        ICompanyService CompanyService { get; }
        ICostCenterService CostCenterService { get; }
        ICustomerService CustomerService { get; }
        IOrganizationService OrganizationService { get; }
    }
}
