namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using WebApplication1.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<WebApplication1.Models.WebApplication1Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(WebApplication1.Models.WebApplication1Context context)
        {

            context.Users.AddOrUpdate(
              new User {
                  Id = 1,
                  Email = "jsobyra@gmail.com",
                  Surname = "Sobyra",
                  Forename = "Jakub",
                  Password = "admin",
                  Username = "jsobyra"
              });

            context.Roles.AddOrUpdate(
              new Role
              {
                  Id = 1,
                  Name = "Admin",
                  ManageUsers = true,
                  CanExport = true,
                  AverageTemperatures = true,
                  YearTemperatures = true
              });

            context.UserRoles.AddOrUpdate(
              new UserRole
              {
                  Id = 1,
                  UserId = 1,
                  RoleId = 1
              });


            context.SaveChanges();
        }
    }
}
