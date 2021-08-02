using System;

namespace GFCA.APT.DAL.Implements
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private APTDbContext _context = new APTDbContext();
        
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

        private Repository<Brand> _brand;
        public Repository<Brand> Brands
        {
            get
            {
                if (this._brand == null)
                {
                    this._brand = new Repository<Brand>(_context);
                }
                return _brand;
            }
        }

        private Repository<Channel> _channel;
        public Repository<Channel> Channels
        {
            get
            {
                if (this._channel == null)
                {
                    this._channel = new Repository<Channel>(_context);
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
