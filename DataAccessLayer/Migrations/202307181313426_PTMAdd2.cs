namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PTMAdd2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PackageTrackings", "Package_PackageId", c => c.Int());
            CreateIndex("dbo.PackageTrackings", "Package_PackageId");
            AddForeignKey("dbo.PackageTrackings", "Package_PackageId", "dbo.Packages", "PackageId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PackageTrackings", "Package_PackageId", "dbo.Packages");
            DropIndex("dbo.PackageTrackings", new[] { "Package_PackageId" });
            DropColumn("dbo.PackageTrackings", "Package_PackageId");
        }
    }
}
