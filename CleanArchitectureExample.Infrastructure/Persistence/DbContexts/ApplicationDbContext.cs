using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitectureExample.Domain.Entities;
using CleanArchitectureExample.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureExample.Infrastructure.Persistence.DbContexts
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Market> Markets { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<PageSale> PageSales { get; set; }
        public DbSet<ShippingPartner> ShippingPartners { get; set; }
        public DbSet<Order> Orders { get; set; }

        public DbSet<Department> Departments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Cấu hình quan hệ N-N giữa Page và User (Sale)

            modelBuilder.Entity<PageSale>()
                .HasKey(ps => new { ps.PageId, ps.SaleUserId });

            modelBuilder.Entity<PageSale>()
                .HasOne(ps => ps.Page)
                .WithMany(p => p.PageSales)
                .HasForeignKey(ps => ps.PageId);

            modelBuilder.Entity<PageSale>()
                .HasOne(ps => ps.UserProfile)
                .WithMany()
                .HasForeignKey(ps => ps.SaleUserId);
            base.OnModelCreating(modelBuilder);

            //Ràng buộc của Page
            modelBuilder.Entity<Page>()
               .HasOne(ps => ps.Creator)
               .WithMany()
               .HasForeignKey(ps => ps.CreateBy);
            base.OnModelCreating(modelBuilder);

            //Ràng buộc của Order
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Market)
                .WithMany()
                .HasForeignKey(o => o.MarketId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Product)
                .WithMany()
                .HasForeignKey(o => o.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<Order>()
            //    .HasOne(o => o.ShippingPartner)
            //    .WithMany()
            //    .HasForeignKey(o => new {o.MarketId,o.ShippingPartnerId})
            //    .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Order>()
                   .HasOne(o => o.ShippingPartner)
                   .WithMany()
                   .HasForeignKey(o => o.ShippingPartnerId);

            //modelBuilder.Entity<Page>()
            //    .HasOne<Product>()
            //    .WithMany()
            //    .HasForeignKey(p => p.ProductId);

            modelBuilder.Entity<UserProfile>()
                .HasOne(up => up.User)
                .WithOne()
                .HasForeignKey<UserProfile>(up => up.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Department>()
                .HasMany(d => d.UserProfiles)
                .WithOne(u => u.Department)
                .HasForeignKey(u => u.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserProfile>()
                .HasOne(u => u.Department)
                .WithMany(d => d.UserProfiles)
                .HasForeignKey(u => u.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            //
            //modelBuilder.Entity<Market>()
            //    .HasOne(m => m.ShippingPartner)
            //    .WithOne(s => s.Market)
            //    .HasForeignKey<ShippingPartner>(s => s.MarketId);


        }
    }
}
