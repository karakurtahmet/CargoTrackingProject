namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DemandStatusUpdate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DemandStatus",
                c => new
                    {
                        DemandStatusId = c.Int(nullable: false, identity: true),
                        DemandStatusName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.DemandStatusId);
            
            AddColumn("dbo.Demands", "StatusId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Demands", "StatusId");
            DropTable("dbo.DemandStatus");
        }
    }
}
