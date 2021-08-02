using System;

namespace GFCA.APT.DAL.Implements
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly APTDbContext _context;

        public UnitOfWork()
        {
            _context = new APTDbContext();
            _context.Configuration.LazyLoadingEnabled = false;
        }

        private Repository<Department> department;
        public Repository<Department> Departments
        {
            get
            {
                if (this.department == null)
                {
                    this.department = new Repository<Department>(_context);
                }
                return department;
            }
        }

        private Repository<TB_M_BRAND> _brand;
        public Repository<TB_M_BRAND> Brands
        {
            get
            {
                if (this._brand == null)
                {
                    this._brand = new Repository<TB_M_BRAND>(_context);
                }
                return _brand;
            }
        }

        private Repository<TB_M_CHANNEL> _channel;
        public Repository<TB_M_CHANNEL> Channels
        {
            get
            {
                if (this._channel == null)
                {
                    this._channel = new Repository<TB_M_CHANNEL>(_context);
                }
                return _channel;
            }
        }
        private Repository<TB_M_CUSTOMER> _customers;
        public Repository<TB_M_CUSTOMER> Customers
        {
            get
            {
                if (this._customers == null)
                {
                    this._customers = new Repository<TB_M_CUSTOMER>(_context);
                }
                return _customers;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
