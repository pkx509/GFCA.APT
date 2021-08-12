using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using GFCA.APT.Domain.Dto;

namespace GFCA.APT.DAL.Implements
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly APTDbContext _context;
        private readonly SqlTransaction _transaction;
        private SqlConnection _connection => new SqlConnection(ConnectionString);
        public UnitOfWork()
        {
            _context = new APTDbContext();
            _context.Configuration.LazyLoadingEnabled = false;
        }
        public static string ConnectionString => ConfigurationManager.ConnectionStrings["APTDbConnectionString"].ToString();

        public SqlTransaction Transaction => _transaction;

        public void TransactionBegin()
        {
            //_transaction = _context.Database.BeginTransaction();
        }

        private BrandRepository _brand;
        private ProductRepository _product;
        public BrandRepository Brand => _brand ?? new BrandRepository(_context);

 
        public ProductRepository Product => _product ?? new ProductRepository(_context);

        public void Commit()
        {
            _context.SaveChanges();
            //_transaction.Commit();
            /*
            bool saveFailed;
            do
            {
                saveFailed = false;
                try
                {
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    saveFailed = true;
                    // Update the values of the entity that failed to save from the store
                    ex.Entries.Single().Reload();
                }
            } while (saveFailed);
            */
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    //_transaction.Dispose();
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
