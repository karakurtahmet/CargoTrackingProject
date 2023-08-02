namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DTMAdd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DemandTrackings",
                c => new
                    {
                        DemandTrackingId = c.Int(nullable: false, identity: true),
                        DemandId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        DemandStatusId = c.Int(nullable: false),
                        IndexDate = c.DateTime(nullable: false),
                        ChangeDescription = c.String(),
                    })
                .PrimaryKey(t => t.DemandTrackingId)
                .ForeignKey("dbo.Demands", t => t.DemandId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: false)
                .Index(t => t.DemandId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DemandTrackings", "UserId", "dbo.Users");
            DropForeignKey("dbo.DemandTrackings", "DemandId", "dbo.Demands");
            DropIndex("dbo.DemandTrackings", new[] { "UserId" });
            DropIndex("dbo.DemandTrackings", new[] { "DemandId" });
            DropTable("dbo.DemandTrackings");
        }
    }
}
