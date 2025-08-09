using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservoom.DbContexts
{
    public class ReservoomDbContextFactory
    {
        private readonly string _connectionString;

        public ReservoomDbContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ReservoomDbContext CreateDbContext()
        {
            // Initialize SQLite
            SQLitePCL.Batteries_V2.Init();
            // Create DbContextOptions with SQLite connection string
            DbContextOptions options = new DbContextOptionsBuilder().UseSqlite(_connectionString).Options;

            return new ReservoomDbContext(options);
        }
    }
}
