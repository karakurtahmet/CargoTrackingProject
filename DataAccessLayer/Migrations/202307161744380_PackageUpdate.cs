namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PackageUpdate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Packages", "DemandId", "dbo.Demands");
            DropIndex("dbo.Packages", new[] { "DemandId" });
            AlterColumn("dbo.Packages", "DemandId", c => c.Int());
            CreateIndex("dbo.Packages", "DemandId");
            AddForeignKey("dbo.Packages", "DemandId", "dbo.Demands", "DemandId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Packages", "DemandId", "dbo.Demands");
            DropIndex("dbo.Packages", new[] { "DemandId" });
            AlterColumn("dbo.Packages", "DemandId", c => c.Int(nullable: false));
            CreateIndex("dbo.Packages", "DemandId");
            AddForeignKey("dbo.Packages", "DemandId", "dbo.Demands", "DemandId", cascadeDelete: true);
        }
    }
}
