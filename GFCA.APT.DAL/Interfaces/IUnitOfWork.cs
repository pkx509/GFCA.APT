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
		IDistributorRepository DistributorRepository { get; }
		IDocumentTypeRepository DocumentTypeRepository { get; }
		IEmployeeRepository EmployeeRepository { get; }
		IInternalOrderRepository InternalOrderRepository { get; }
		IOrganizationRepository OrganizationRepository { get; }
		IPackRepository PackRepository { get; }
		IUnitRepository UnitRepository { get; }
		ISizeRepository SizeRepository { get; }
		ICustomerPartyRepository CustomerPartyRepository { get; }
		IPromotionGroupRepository PromotionGroupRepository { get; }
		IDocumentRepository DocumentRepository { get; }
		IFixedContractRepository FixedContractRepository { get; }
		IPromotionRepository PromotionRepository { get; }
		IBudgetPlanRepository BudgetPlanRepository { get; }
		void Commit();
		//bool LazyLoadingEnabled { get; set; }
		//bool ProxyCreationEnabled { get; set; }
		//string ConnectionString { get; set; }
	}
}
