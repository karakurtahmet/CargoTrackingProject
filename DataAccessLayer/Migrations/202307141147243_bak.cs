namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bak : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Demands", "PackageId", "dbo.Packages");
            DropIndex("dbo.Demands", new[] { "PackageId" });
            AddColumn("dbo.Packages", "DemandId", c => c.Int(nullable: false));
            CreateIndex("dbo.Packages", "DemandId");
            AddForeignKey("dbo.Packages", "DemandId", "dbo.Demands", "DemandId", cascadeDelete: true);
            DropColumn("dbo.Demands", "PackageId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Demands", "PackageId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Packages", "DemandId", "dbo.Demands");
            DropIndex("dbo.Packages", new[] { "DemandId" });
            DropColumn("dbo.Packages", "DemandId");
            CreateIndex("dbo.Demands", "PackageId");
            AddForeignKey("dbo.Demands", "PackageId", "dbo.Packages", "PackageId", cascadeDelete: true);
        }
    }
}
