using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using GFCA.APT.DAL.Interfaces;

namespace GFCA.APT.DAL.Implements
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private IBrandRepository _brandRepository;
        private IProductRepository _productRepository;
        private IEmissionRepository _emissionRepository;

        private bool _disposed = false;
        public static IUnitOfWork CreateInstant()
        {
            var uow = new UnitOfWork("APTDbConnectionString");
            return uow;
        }

        public UnitOfWork(string connectionName)
        {
            string connString = ConfigurationManager.ConnectionStrings[connectionName].ToString();
            Initial(connString);
        }

        private void Initial(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }
        
        public IBrandRepository BrandRepository
        {
            get
            {
                return _brandRepository ?? (_brandRepository = new BrandRepository(_transaction));
            }
        }
        public IProductRepository ProductRepository
        {
            get
            {
                return _productRepository ?? (_productRepository = new ProductRepository(_transaction));
            }
        }
        public IEmissionRepository EmissionRepository
        {
            get
            {
                return _emissionRepository ?? (_emissionRepository = new EmissionRepository(_transaction));
            }
        }

        private void resetRepositories()
        {
            _brandRepository = null;
            _productRepository = null;
            _emissionRepository = null;
        }

        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch //(Exception ex)
            {
                _transaction.Rollback();
                throw;
            }
            finally 
            {
                _transaction.Dispose();
                _transaction = _connection.BeginTransaction();
                resetRepositories();
            }
        }

        #region [ Grabrage Collection ]
        
        private void Dispose(bool disposing)
        {

            if (!_disposed)
            {
                if (disposing)
                {
                    if (_transaction != null)
                    {
                        _transaction.Dispose();
                        _transaction = null;
                    }
                    if (_connection != null)
                    {
                        _connection.Dispose();
                        _connection = null;
                    }
                }
                _disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        ~UnitOfWork()
        {
            Dispose(false);
        }

        #endregion [ Grabrage Collection ]
    }

}
