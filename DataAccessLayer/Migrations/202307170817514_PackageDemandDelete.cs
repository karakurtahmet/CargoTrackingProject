namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PackageDemandDelete : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Packages", "DemandId", "dbo.Demands");
            DropIndex("dbo.Packages", new[] { "DemandId" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Packages", "DemandId");
            AddForeignKey("dbo.Packages", "DemandId", "dbo.Demands", "DemandId");
        }
    }
}
