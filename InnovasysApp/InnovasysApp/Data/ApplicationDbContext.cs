using InnovasysApp.Models;
using Microsoft.EntityFrameworkCore;

namespace InnovasysApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(u => u.IsActive)
                .HasConversion<byte>();

            modelBuilder.Entity<User>()
                .Property(u => u.NotUsername)
                .HasColumnName("NotUsername");

            base.OnModelCreating(modelBuilder);
        }
    }
}
