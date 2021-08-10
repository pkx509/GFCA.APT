using GFCA.APT.DAL.Implements;

namespace GFCA.APT.DAL
{
    public interface IUnitOfWork
    {
        BrandRepository Brand { get; }

        void Commit();
        void Dispose();
        //bool LazyLoadingEnabled { get; set; }
        //bool ProxyCreationEnabled { get; set; }
        //string ConnectionString { get; set; }
    }
}
