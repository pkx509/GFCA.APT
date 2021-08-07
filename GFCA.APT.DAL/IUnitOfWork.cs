using GFCA.APT.DAL.Implements;

namespace GFCA.APT.DAL
{
    public interface IUnitOfWork
    {
        BrandRepository Brand { get; }

        void Commit();
        void Dispose();
    }
}
