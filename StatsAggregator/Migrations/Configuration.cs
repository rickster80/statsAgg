namespace StatsAggregator.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Models;

    internal sealed class Configuration : DbMigrationsConfiguration<StatsAggregator.EF.StatsContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(StatsAggregator.EF.StatsContext context)
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
            //var day = 1;
            //var month = 1;
            //for (int i = 0; i < 100; i++) {                
            //    context.Log_Universal.Add(new Log_Universal()
            //    {
            //        ActionType = ActionType.Created,
            //        Datestamp = new DateTime(2015, month, day),
            //        SubscriberId = i,
            //        EntityKey
            //    });
            //    day = day < 27 ? ++day : 1;
            //    month = month < 11 ? ++month : 1;
            //}
        }
    }
}
