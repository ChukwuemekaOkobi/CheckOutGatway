using Entities;
using Microsoft.EntityFrameworkCore;

namespace Services.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            
        }



        public DbSet<Merchant> Merchants { get; set; }

        public DbSet<Transaction> Transactions { get; set; }
    }
}
