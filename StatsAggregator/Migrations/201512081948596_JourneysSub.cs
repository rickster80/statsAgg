namespace StatsAggregator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class JourneysSub : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Journeys", "SubscriberId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Journeys", "SubscriberId");
        }
    }
}
