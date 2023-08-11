using ElkoodTask.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace ElkoodTask.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<BranchType> BranchType { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<ProductType> ProductType { get; set; }
        public DbSet<Branch> Branch { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Production_Operation> Production_Operation { get; set; }
        public DbSet<Distribution_Operation> Distribution_Operation { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Branch>()
                .HasMany(a => a.Production_Operation)
                .WithOne(b => b.Branch)
                .HasForeignKey(b => b.BranchId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Product>()
                .HasMany(a => a.Production_Operation)
                .WithOne(b => b.Product)
                .HasForeignKey(b => b.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Branch>()
                .HasMany(a => a.Distribution_Operation)
                .WithOne(b => b.PrimaryBranch)
                .HasForeignKey(b => b.PrimaryBranchId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Distribution_Operation>()
                .HasOne(d => d.PrimaryBranch)
                .WithMany()
                .HasForeignKey(d => d.PrimaryBranchId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Distribution_Operation>()
                .HasOne(d => d.SecondaryBranch)
                .WithMany()
                .HasForeignKey(d => d.SecondaryBranchId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Product>()
                .HasMany(a => a.Distribution_Operation)
                .WithOne(b => b.Product)
                .HasForeignKey(b => b.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

           
}
    }
}