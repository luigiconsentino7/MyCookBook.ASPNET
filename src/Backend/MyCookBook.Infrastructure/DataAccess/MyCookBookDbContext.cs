using Microsoft.EntityFrameworkCore;
using MyCookBook.Domain.Entities;

namespace MyCookBook.Infrastructure.DataAccess
{
    public class MyCookBookDbContext : DbContext
    {
        public MyCookBookDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MyCookBookDbContext).Assembly);
        }
    }
}
