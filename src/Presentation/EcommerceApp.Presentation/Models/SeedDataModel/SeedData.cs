using Ecommerce.Infrastructure.Context;
using EcommerceApp.Domain.Entities;
using EcommerceApp.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApp.Presentation.Models.SeedDataModel
{
    public static class SeedData
    {
        public static void Seed(IApplicationBuilder app)
        {
            var scope = app.ApplicationServices.CreateScope();
            var dbContext = scope.ServiceProvider.GetService<ECommerceAppDbContext>();
            dbContext.Database.Migrate();
            if (dbContext.Categories.Count() == 0)
            {
                dbContext.Categories.Add(new Category()
                {
                    Id = Guid.NewGuid(),
                    Name = "Home Appliances",
                    CreateDate = DateTime.Now,
                    Status = Status.Active

                });
                dbContext.Categories.Add(new Category()
                {
                    Id = Guid.NewGuid(),
                    Name = "Electronics",
                    CreateDate = DateTime.Now,
                    Status = Status.Active

                });
                dbContext.Categories.Add(new Category()
                {
                    Id = Guid.NewGuid(),
                    Name = "Textile",
                    CreateDate = DateTime.Now,
                    Status = Status.Active

                });

            }
            if (dbContext.Employees.Count() == 0)
            {
                dbContext.Employees.Add(new Employee()
                {
                    Id = Guid.NewGuid(),
                    Name = "Ayşenur",
                    Surname = "Altınsoy",
                    EmailAddress= "aysenur.altinsoy@bilgeadamboost.com",
                    Status = Status.Active,
                    Password = "1234",
                    CreateDate = DateTime.Now,
                    Roles = Roles.Admin,
                    BirthDate=new DateTime(1997, 9, 9)

                });

            }
            dbContext.SaveChanges();
        }
    }
}
