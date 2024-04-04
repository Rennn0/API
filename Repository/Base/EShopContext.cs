using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository.Base
{
    public class EShopContext : DbContext
    {
        public EShopContext(DbContextOptions<EShopContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public EShopContext() : base()
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Product>(p =>
            {
                p.Property(p => p.Id).ValueGeneratedOnAdd();
                p.Property(p => p.Price).HasPrecision(12, 4);
            });

            builder.Entity<User>(u =>
            {
                u.Property(u => u.Id).ValueGeneratedOnAdd();
            });
            builder.Entity<Category>(c =>
            {
                c.Property(c => c.Id).ValueGeneratedOnAdd();
            });

            builder.Entity<Category>(c =>
            {
                c.HasMany(c => c.Products).WithOne(p => p.Category).HasForeignKey(p => p.CategoryId);
            });
        }
    }
}