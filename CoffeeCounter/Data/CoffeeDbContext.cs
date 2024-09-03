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
    optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CoffeeDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

    }

    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }
    public class Coffee
    {
        public int Id { get; set; }
        public string? Kind { get; set; }
        public string? Volume { get; set; }
        public string? Time { get; set; }
        public string? Date { get; set; }
        public string? Location { get; set; }
    }
}
