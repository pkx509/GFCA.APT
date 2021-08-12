using System;

namespace GFCA.APT.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
	{
		IBrandRepository BrandRepository { get; }
		IProductRepository ProductRepository { get; }
		void Commit();
		//bool LazyLoadingEnabled { get; set; }
		//bool ProxyCreationEnabled { get; set; }
		//string ConnectionString { get; set; }
	}
}
