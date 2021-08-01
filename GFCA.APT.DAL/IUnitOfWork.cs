namespace GFCA.APT.DAL
{
    public interface IUnitOfWork
    {
        Repository<Department> Departments { get; }
        Repository<Brand> Brands { get; }
        Repository<Channel> Channels { get; }
        void Save();
        void Dispose();
    }
}
