namespace GFCA.APT.DAL
{
    public interface IUnitOfWork
    {
        Repository<Department> Departments { get; }
        Repository<Brand> Brands { get; }
        Repository<Channel> Channels { get; }
        Repository<TB_M_CUSTOMER> Customers { get; }
        void Save();
        void Dispose();
    }
}
