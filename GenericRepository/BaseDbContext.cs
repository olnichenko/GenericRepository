using Microsoft.EntityFrameworkCore;

namespace GenericRepository
{
    public class BaseDbContext : DbContext
    {
        private string _connectionString;
        public BaseDbContext(string connectionString)
        {
            _connectionString = connectionString;
            Database.EnsureCreated();
        }
    }
}
