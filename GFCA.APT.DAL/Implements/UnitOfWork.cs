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
        private IGLAccountRepository _glaccountRepository;
        private IClientRepository _clientRepository;
        private IBudgetTypeRepository _budgetTypeRepository;
        private ITradeActivityRepository _tradeActivityRepository;
        private IChannelRepository _channelRepository;
        private ICompanyRepository _companyRepository;
        private ICostCenterRepository _costCenterRepository;
        private ICustomerRepository _customerRepository;
        private IDistributorRepository _distributorRepository;

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

        public IGLAccountRepository GLAccountRepository
        {
            get
            {
                return _glaccountRepository ?? (_glaccountRepository = new GLAccountRepository(_transaction));
            }
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
        public IBudgetTypeRepository BudgetTypeRepository
        {
            get
            {
                return _budgetTypeRepository ?? (_budgetTypeRepository = new BudgetTypeRepository(_transaction));
            }
        }
        public ITradeActivityRepository TradeActivityRepository
        {
            get
            {
                return _tradeActivityRepository ?? (_tradeActivityRepository = new TradeActivityRepository(_transaction));
            }
        }
        public IChannelRepository ChannelRepository
        {
            get
            {
                return _channelRepository ?? (_channelRepository = new ChannelRepository(_transaction));
            }
        }
        public IClientRepository ClientRepository
        {
            get
            {
                return _clientRepository ?? (_clientRepository = new ClientRepository(_transaction));
            }
        }

        public ICompanyRepository CompanyRepository
        {
            get
            {
                return _companyRepository ?? (_companyRepository = new CompanyRepository(_transaction));
            }
        }

        public ICostCenterRepository CostCenterRepository
        {
            get
            {
                return _costCenterRepository ?? (_costCenterRepository = new CostCenterRepository(_transaction));
            }
        }

        public ICustomerRepository CustomerRepository
        {
            get
            {
                return _customerRepository ?? (_customerRepository = new CustomerRepository(_transaction));
            }
        }

        public IDistributorRepository DistributorRepository
        {
            get
            {
                return _distributorRepository ?? (_distributorRepository = new DistributorRepository(_transaction));
            }
        }

        private void resetRepositories()
        {
            _brandRepository = null;
            _productRepository = null;
            _emissionRepository = null;
            _glaccountRepository = null;
            _budgetTypeRepository = null;
            _tradeActivityRepository = null;
            _channelRepository = null;
            _companyRepository = null;
            _costCenterRepository = null;
            _customerRepository = null;
            _clientRepository = null;
            _distributorRepository = null;
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
