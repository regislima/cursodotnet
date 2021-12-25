using System;
using api.Domain.Models;
using api.Util.Extensions;
using Microsoft.EntityFrameworkCore;

namespace api.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Category>().ToTable("categories");
            builder.Entity<Category>().HasKey(c => c.Id);
            builder.Entity<Category>().Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Category>().Property(c => c.Name).IsRequired().HasMaxLength(50);
            builder.Entity<Category>().Property(c => c.CreateDate).ValueGeneratedOnAdd().HasDefaultValue(DateTime.Now);
            builder.Entity<Category>().Property(c => c.UpdateDate).ValueGeneratedOnUpdate().HasDefaultValue(DateTime.Now);
            builder.Entity<Category>().HasMany(c => c.Products).WithOne(p => p.Category).HasForeignKey(p => p.CategoryId);

            builder.Entity<Product>().ToTable("products");
            builder.Entity<Product>().HasKey(p => p.Id);
            builder.Entity<Product>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Product>().Property(p => p.Name).IsRequired().HasMaxLength(50);
            builder.Entity<Product>().Property(p => p.QuantityInPackage).IsRequired();
            builder.Entity<Product>().Property(p => p.UnitOfMeasurement).IsRequired();
            builder.Entity<Product>().Property(c => c.CreateDate).ValueGeneratedOnAdd().HasDefaultValue(DateTime.Now);
            builder.Entity<Product>().Property(c => c.UpdateDate).ValueGeneratedOnUpdate().HasDefaultValue(DateTime.Now);

            builder.Entity<User>().ToTable("users");
            builder.Entity<User>().HasKey(u => u.Id);
            builder.Entity<User>().Property(u => u.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<User>().Property(u => u.Login).IsRequired().HasMaxLength(50);
            builder.Entity<User>().Property(u => u.Password).IsRequired().HasMaxLength(10);
            builder.Entity<User>().Property(c => c.CreateDate).ValueGeneratedOnAdd().HasDefaultValue(DateTime.Now);
            builder.Entity<User>().Property(c => c.UpdateDate).ValueGeneratedOnUpdate().HasDefaultValue(DateTime.Now);
            builder.Entity<User>().HasData
            (
                new User
                {
                    Id = 1,
                    Login = "admin",
                    Password = "admin"
                }
            );
        }
    }
}