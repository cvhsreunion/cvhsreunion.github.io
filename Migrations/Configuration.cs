namespace CVHSReunion.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using CVHSReunion.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<CVHSReunion.Models.CVHSReunionContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(CVHSReunion.Models.CVHSReunionContext context)
        {
            //context.Users.AddOrUpdate(
            //    new Users { firstName="brett", lastName="reinhard",emailAddress="brett@reinhards.us",status="attending"}
            //    );

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
