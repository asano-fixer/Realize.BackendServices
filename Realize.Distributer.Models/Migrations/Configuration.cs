namespace Realize.Distributer.Models.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class MaintenanceDbContextConfiguration : DbMigrationsConfiguration<Realize.Distributer.Models.DbContexts.MaintenanceDbContext>
    {
        public MaintenanceDbContextConfiguration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Realize.Distributer.Models.DbContexts.MaintenanceDbContext context)
        {
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
    internal sealed class MasterDbContextConfiguration : DbMigrationsConfiguration<Realize.Distributer.Models.DbContexts.MasterDbContext>
    {
        public MasterDbContextConfiguration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Realize.Distributer.Models.DbContexts.MasterDbContext context)
        {
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
