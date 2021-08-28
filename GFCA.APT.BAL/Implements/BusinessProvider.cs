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



        private TB_M_CHANNELService _tb_m_channelService;
        public ITB_M_CHANNELService ITB_M_CHANNELService => _tb_m_channelService ?? (_tb_m_channelService = Serve.TB_M_CHANNELService.CreateInstant());



        private TB_M_CLIENTService _tb_m_clientService;
        public ITB_M_CLIENTService TB_M_CLIENTService => _tb_m_clientService ?? (_tb_m_clientService = Serve.TB_M_CLIENTService.CreateInstant());


        private TB_M_COST_CENTERService _tb_m_cost_centerService;
        public ITB_M_COST_CENTERService TB_M_COST_CENTERService => _tb_m_cost_centerService ?? (_tb_m_cost_centerService = Serve.TB_M_COST_CENTERService.CreateInstant());


        private ITB_M_CUSTOMERService _tb_m_customerService;
        public ITB_M_CUSTOMERService TB_M_CUSTOMERService => _tb_m_customerService ?? (_tb_m_customerService = Serve.TB_M_CUSTOMERService.CreateInstant());

        


        private ITB_M_UNITService _tb_m_unitService;
        public ITB_M_UNITService TB_M_UNITService => _tb_m_unitService ?? (_tb_m_unitService = Serve.TB_M_UNITService.CreateInstant());

     


        private ITB_M_ACCOUNT_PAYABLEService _tb_m_account_paybleService;
        public ITB_M_ACCOUNT_PAYABLEService TB_M_ACCOUNT_PAYABLEService => _tb_m_account_paybleService ?? (_tb_m_account_paybleService = Serve.TB_M_ACCOUNT_PAYABLEService.CreateInstant());



    }
}
