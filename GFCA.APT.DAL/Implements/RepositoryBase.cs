using System.Data;

namespace GFCA.APT.DAL.Implements
{
    public abstract class RepositoryBase
    {
        protected IDbTransaction Transaction { get; private set; }
        protected IDbConnection Connection => Transaction.Connection;

        public RepositoryBase(IDbTransaction transaction)
        {
            Transaction = transaction;
        }
    }
}
