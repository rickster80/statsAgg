namespace StatsAggregator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Journeys : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Journeys",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        From = c.String(),
                        To = c.String(),
                        DateAdded = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Log_Journey",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Jid = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Log_Journey");
            DropTable("dbo.Journeys");
        }
    }
}
