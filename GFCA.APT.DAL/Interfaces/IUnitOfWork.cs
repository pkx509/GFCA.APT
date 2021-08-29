using System;

namespace GFCA.APT.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
	{
		IBrandRepository BrandRepository { get; }
		IProductRepository ProductRepository { get; }
		IEmissionRepository EmissionRepository { get; }
		IGLAccountRepository GLAccountRepository { get; }
		IBudgetTypeRepository BudgetTypeRepository { get; }
		ITradeActivityRepository TradeActivityRepository { get; }
		IChannelRepository ChannelRepository { get; }
		ICompanyRepository CompanyRepository { get; }
		ICostCenterRepository CostCenterRepository { get; }
		ICustomerRepository CustomerRepository { get; }
		IClientRepository ClientRepository { get; }
		IEmployeeRepository EmployeeRepository { get; }

		void Commit();
		//bool LazyLoadingEnabled { get; set; }
		//bool ProxyCreationEnabled { get; set; }
		//string ConnectionString { get; set; }
	}
}
