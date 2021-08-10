using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace GFCA.APT.DAL.Repositories
{
    public class CustomerRepository 
        : RepositoryBase
        , IRepository<Customer>
        , IDisposable
    {
        public IQueryable<Customer> Table => throw new NotImplementedException();

        public IQueryable<Customer> TableNoTracking => throw new NotImplementedException();

        public CustomerRepository() : base()
        {
        }
        public CustomerRepository(APTDbContext context) : base(context)
        {
        }

        public IEnumerable<Customer> GetAll()
        {
            return _context.Customers.ToList();
        }
        public Customer GetByID(int primaryKey)
        {
            return _context.Customers.Find(primaryKey);
        }
        public void Insert(Customer data)
        {
            _context.Customers.Add(data);
        }
        public void Update(Customer data)
        {
            _context.Entry(data).State = EntityState.Modified;
        }
        public void Delete(int primaryKey)
        {
            Customer data = _context.Customers.Find(primaryKey);
            _context.Customers.Remove(data);
        }

        public Customer GetById(int primaryKey)
        {
            throw new NotImplementedException();
        }

        public void Update(IEnumerable<Customer> entities)
        {
            throw new NotImplementedException();
        }

        public void Delete(Customer entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(IEnumerable<Customer> entities)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Customer> Where(Expression<Func<Customer, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}