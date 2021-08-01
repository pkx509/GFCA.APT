using System.Collections.Generic;
using System.Linq;

namespace GFCA.APT.DAL.Repositories
{
    public class CostCenterRepository : RepositoryBase, IRepository<CostCenter>
    {
        public CostCenterRepository() : base()
        {
        }
        public CostCenterRepository(APTDbContext context) : base(context)
        {
        }

        public IEnumerable<CostCenter> GetAll()
        {
            return _context.CostCenters.ToList();
        }
        public CostCenter GetById(int primaryKey)
        {
            return _context.CostCenters.Find(primaryKey);
        }
        public void Insert(CostCenter data)
        {
            _context.CostCenters.Add(data);
        }
        public void Update(CostCenter data)
        {
            _context.Entry(data).State = System.Data.Entity.EntityState.Modified;
        }
        public void Delete(int primaryKey)
        {
            CostCenter data = _context.CostCenters.Find(primaryKey);
            _context.CostCenters.Remove(data);
        }
    }
}
