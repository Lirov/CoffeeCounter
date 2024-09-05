using CoffeeCounter.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeCounter.Data
{
    class CoffeeDbContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Coffee> Coffee { get; set; }

        public CoffeeDbContext(DbContextOptions<CoffeeDbContext> options): base(options){}

        public CoffeeDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
     // Replace with your actual connection string
     //optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CoffeeDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            optionsBuilder.UseMySql("Server=localhost;Port=3306;Database=CoffeeDB;User=root;",
                new MySqlServerVersion(new Version(8, 0, 21)));  // Adjust version as per your MariaDB version
    }

}
