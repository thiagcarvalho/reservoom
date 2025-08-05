using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservoom.DbContexts
{
    public class ReservoomDesignTimeDbContextFactory : IDesignTimeDbContextFactory<ReservoomDbContext>
    {
        public ReservoomDbContext CreateDbContext(string[] args)
        {
            Batteries_V2.Init();
            DbContextOptions options = new DbContextOptionsBuilder().UseSqlite("Data Source=reservoom.db").Options;

            return new ReservoomDbContext(options);
        }
    }
}
