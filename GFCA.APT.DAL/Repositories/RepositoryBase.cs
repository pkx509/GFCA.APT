using System;
using GFCA.APT.DAL;

namespace GFCA.APT.DAL.Repositories
{
    public abstract class RepositoryBase : IDisposable
    {
        protected readonly APTDbContext _context;
        protected RepositoryBase() => _context = new APTDbContext();
        protected RepositoryBase(APTDbContext context) => _context = context;

        protected bool _disposed = false;

        public virtual void Save()
        {
            _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this._disposed = true;
        }
        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
