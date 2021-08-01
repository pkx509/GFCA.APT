using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace GFCA.APT.DAL.Repositories
{
    public class CustomerRepository 
        : RepositoryBase
        , IRepository<Customer>
        , IDisposable
    {
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
        public Customer GetById(int primaryKey)
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
    }
}