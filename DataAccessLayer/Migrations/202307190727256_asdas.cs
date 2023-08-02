namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class asdas : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DemandTrackings", "DemandId", "dbo.Demands");
            DropIndex("dbo.DemandTrackings", new[] { "DemandId" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.DemandTrackings", "DemandId");
            AddForeignKey("dbo.DemandTrackings", "DemandId", "dbo.Demands", "DemandId", cascadeDelete: true);
        }
    }
}
