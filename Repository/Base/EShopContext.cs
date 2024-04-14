using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository.Base
{
	public class EShopContext : DbContext
	{
		public DbSet<User> Users { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<PurchaseHistory> PurchaseHistories { get; set; }
		public DbSet<UserVerification> UserVerifications { get; set; }

		public EShopContext(DbContextOptions<EShopContext> options) : base(options)
		{
		}

		public EShopContext() : base()
		{
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<User>(u =>
			{
				u.Property(u => u.Id).ValueGeneratedOnAdd();
				u.HasIndex(u => u.Email).IsUnique();
				u.HasMany(u => u.PurchaseHistories).WithOne(ph => ph.User).HasForeignKey(ph => ph.UserId);
			});

			builder.Entity<Product>(p =>
			{
				p.Property(p => p.Id).ValueGeneratedOnAdd();
				p.Property(p => p.Price).HasPrecision(12, 2);
				p.HasOne(p => p.Category).WithMany(c => c.Products).HasForeignKey(p => p.CategoryId);
				p.HasMany(p => p.PurchaseHistories).WithOne(ph => ph.Product).HasForeignKey(p => p.ProductId);
			});

			builder.Entity<Category>(c =>
			{
				c.Property(c => c.Id).ValueGeneratedOnAdd();
				c.HasMany(c => c.Products).WithOne(p => p.Category).HasForeignKey(p => p.CategoryId);
			});

			builder.Entity<PurchaseHistory>(ph =>
			{
				ph.Property(ph => ph.Id).ValueGeneratedOnAdd();
				ph.Property(ph => ph.Price).HasPrecision(18, 4);
				ph.HasOne(ph => ph.User).WithMany(u => u.PurchaseHistories).HasForeignKey(ph => ph.UserId);
				ph.HasOne(ph => ph.Product).WithMany(p => p.PurchaseHistories).HasForeignKey(ph => ph.ProductId);
				ph.HasIndex(ph => ph.Correlation);
			});

			builder.Entity<UserVerification>(uv =>
			{
				uv.Property(uv => uv.Id).ValueGeneratedOnAdd();
				uv.HasIndex(uv => uv.Hash).IsUnique();
			});
		}
	}
}