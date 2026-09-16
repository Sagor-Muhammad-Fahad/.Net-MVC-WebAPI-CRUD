namespace API_Prctice_6.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<API_Prctice_6.Models.OrderDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(API_Prctice_6.Models.OrderDBContext context)
        {
            context.Users.AddOrUpdate(u => u.UserId,
                new Models.User { UserId = 1, UserName = "admin", Password = "1234", Email = "admin@gmail.com", Roles = "admin" },
                new Models.User { UserId = 2, UserName = "sagor", Password = "1111", Email = "sagor@gmail.com", Roles = "user" });
        }
    }
}
