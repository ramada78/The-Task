using ElkoodTask.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.IO;

namespace ElkoodTask
{
    public static class TaskInitializer
    {//this WebApplication app

        //public static void Seed(IApplicationBuilder applicationBuilder)
        public static WebApplication Seed(this WebApplication app)
        {

            //using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            using (var scope = app.Services.CreateScope())
            {
                //var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                using var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                context.Database.EnsureCreated();

                if (!context.BranchType.Any())
                 
                    {
                        context.Company.AddRange(
                             new Company { Id = 1, Name = "IceCube", Location = "Aleppo, Syria", EstablishmentDate = DateTime.Parse("2007 - 07 - 07"), Activity = "Clothes" },
                             new Company { Id = 2, Name = "Zara", Location = "Istanbul, Turkey", EstablishmentDate = DateTime.Parse("1975 - 05 - 24"), Activity = "Clothes" }
                        );
                        context.BranchType.AddRange(
                            new BranchType { Id = 1, Name = "Primary" },
                            new BranchType {Id = 2, Name = "Secondary" }
                        );
                        context.ProductType.AddRange(
                           new ProductType { Id = 1, Name = "Dress" },
                           new ProductType { Id = 2, Name = "T - Shirt" },
                           new ProductType { Id = 3, Name = "Jeans" }
                        );
                        context.Branch.AddRange(
                           new Branch { Id = 1, Name = "AlNil Street", BranchTypeId = 1, CompanyId = 1, Location = "Aleppo" },
                           new Branch { Id = 2, Name = "AlFardos Street", BranchTypeId = 2, CompanyId = 2, Location = "Istanbul" },
                           new Branch { Id = 3, Name = "AlMajd Street", BranchTypeId = 2, CompanyId = 2, Location = "Stockholm" }
                        );
                        context.Product.AddRange(
                           new Product { Id = 1, Name = "Yellow Dress", ProductTypeId = 1 },
                           new Product { Id = 2, Name = "Red T - Shirt", ProductTypeId = 2 },
                           new Product { Id = 3, Name = "Blue Jeans", ProductTypeId = 3 }
                        );
                        
                        context.Production_Operation.AddRange(
                           new Production_Operation { Id = 1, BranchId = 1, ProductId = 1, Quantity = 50000, RemainingQuantity = 10000, Date = DateTime.Parse("2012 - 06 - 22") },
                           new Production_Operation { Id = 2, BranchId = 2, ProductId = 2, Quantity = 10000, RemainingQuantity = 2000, Date = DateTime.Parse("2013 - 07 - 23") },
                           new Production_Operation { Id = 3, BranchId = 3, ProductId = 3, Quantity = 20000, RemainingQuantity = 5000, Date = DateTime.Parse("2014 - 08 - 24") }
                        );
                        context.Distribution_Operation.AddRange(
                          new Distribution_Operation { Id = 1, PrimaryBranchId = 1, PrimaryBranch = null, SecondaryBranchId = 2, SecondaryBranch = null, ProductId = 1, Product = null, Quantity = 1000, Date = DateTime.Parse("2012 - 06 - 22") },
                          new Distribution_Operation { Id = 2, PrimaryBranchId = 3, PrimaryBranch = null, SecondaryBranchId = 3, SecondaryBranch = null, ProductId = 2, Product = null, Quantity = 2000, Date = DateTime.Parse("2013 - 07 - 23") },
                          new Distribution_Operation { Id = 3, PrimaryBranchId = 2, PrimaryBranch = null, SecondaryBranchId = 1, SecondaryBranch = null, ProductId = 3, Product = null, Quantity = 3000, Date = DateTime.Parse("2014 - 08 - 24") }
                        );
                        context.SaveChanges();
                    }
                
                
            }
            return app;
        }

     
    }
}
