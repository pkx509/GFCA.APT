
namespace GFCA.APT.BAL.Interfaces
{
    public interface IBusinessProvider
    {
        ILogService LogService { get; }
        IBrandService BrandService { get; }
        IEmissionService EmissionService { get; }
        IProductService ProductService { get; }
        IGLAccountService GLAccountService { get; }
        IBudgetTypeService BudgetTypeService { get; }
        ITradeActivityService TradeActivityService { get; }
    }
}
