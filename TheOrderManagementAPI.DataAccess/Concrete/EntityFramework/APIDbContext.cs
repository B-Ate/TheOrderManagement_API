using Microsoft.EntityFrameworkCore;
using TheOrderManagementAPI.Entities;

namespace TheOrderManagementAPI.DataAccess.Concrete.EntityFramework
{
    public class APIDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\Local;Database=Testdb;Trusted_Connection=true");
        }

        public DbSet<Product> Product { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<CustomerOrder> CustomerOrder { get; set; }
    }
}
