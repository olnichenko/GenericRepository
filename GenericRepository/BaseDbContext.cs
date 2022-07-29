using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
