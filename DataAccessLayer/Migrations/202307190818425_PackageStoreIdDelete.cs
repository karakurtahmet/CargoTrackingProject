namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PackageStoreIdDelete : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Packages", "StoreId", "dbo.Stores");
            DropIndex("dbo.Packages", new[] { "StoreId" });
            AddColumn("dbo.Packages", "StoreCode", c => c.String());
            DropColumn("dbo.Packages", "StoreId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Packages", "StoreId", c => c.Int(nullable: false));
            DropColumn("dbo.Packages", "StoreCode");
            CreateIndex("dbo.Packages", "StoreId");
            AddForeignKey("dbo.Packages", "StoreId", "dbo.Stores", "StoreId", cascadeDelete: true);
        }
    }
}
