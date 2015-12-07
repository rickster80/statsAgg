namespace StatsAggregator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updates : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Log_Universal", "CommunityId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Log_Universal", "CommunityId");
        }
    }
}
