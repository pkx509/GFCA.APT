
namespace GFCA.APT.BAL.Interfaces
{
    public interface IBusinessProvider
    {
        ILogService LogService { get; }
        IBrandService BrandService { get; }
        IEmissionService EmissionService { get; }
        IProductService ProductService { get; }
        ITB_M_CHANNELService ITB_M_CHANNELService { get; }
        ITB_M_CLIENTService TB_M_CLIENTService { get; }
        ITB_M_COST_CENTERService TB_M_COST_CENTERService { get; }
        ITB_M_CUSTOMERService TB_M_CUSTOMERService { get; }
        ITB_M_UNITService TB_M_UNITService { get; }
        ITB_M_ACCOUNT_PAYABLEService TB_M_ACCOUNT_PAYABLEService { get; }
    }
}
