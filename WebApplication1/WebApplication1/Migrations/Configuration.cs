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
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WebApplication1.Models.WebApplication1Context context)
        {
            context.Roles.AddOrUpdate(x => x.Id,
                new Role()
                {
                    Id = 1,
                    Name = "Admin",
                    ManageUsers = true,
                    AverageTemperatures = true,
                    YearTemperatures = true
                }
                );
            context.Users.AddOrUpdate(x => x.Id,
                new User()
                {
                    Id = 1,
                    Forename = "Jakub",
                    Surname = "Sobyra",
                    Username = "jsobyra",
                    Email = "jakub.sobyra@gmail.com",
                    Password = "admin"
                }
                );
            context.UserRoles.AddOrUpdate(x => x.Id,
                 new UserRole()
                 {
                     Id = 1,
                     UserId = 1,
                     RoleId = 1
                 }
                 );
        }
    }
}