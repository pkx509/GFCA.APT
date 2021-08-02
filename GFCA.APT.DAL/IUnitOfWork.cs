namespace GFCA.APT.DAL
{
    public interface IUnitOfWork
    {
        Repository<Department> Departments { get; }
        Repository<TB_M_BRAND> Brands { get; }
        Repository<TB_M_CHANNEL> Channels { get; }
        Repository<TB_M_CUSTOMER> Customers { get; }
        void Save();
        void Dispose();
    }
}
