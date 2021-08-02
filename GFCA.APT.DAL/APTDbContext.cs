using System.Data.SqlClient;

namespace GFCA.APT.DAL
{
    public partial class APTDbContext
    {
        public APTDbContext(string connectionString) : base(connectionString) { }
        public APTDbContext(SqlConnection connection) : base(connection, contextOwnsConnection: false) { }
    }
}
