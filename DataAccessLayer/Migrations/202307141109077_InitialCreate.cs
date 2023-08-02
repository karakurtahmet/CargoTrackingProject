namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Demands",
                c => new
                    {
                        DemandId = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        FirmID = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        DemandDate = c.DateTime(nullable: false),
                        PackageId = c.Int(nullable: false),
                        RejectionReason = c.String(),
                    })
                .PrimaryKey(t => t.DemandId)
                .ForeignKey("dbo.Firms", t => t.FirmID, cascadeDelete: true)
                .ForeignKey("dbo.Packages", t => t.PackageId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.FirmID)
                .Index(t => t.UserId)
                .Index(t => t.PackageId);
            
            CreateTable(
                "dbo.Firms",
                c => new
                    {
                        FirmId = c.Int(nullable: false, identity: true),
                        FirmName = c.String(maxLength: 50),
                        FirmAddres = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.FirmId);
            
            CreateTable(
                "dbo.Stores",
                c => new
                    {
                        StoreId = c.Int(nullable: false, identity: true),
                        StoreName = c.String(),
                        FirmId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StoreId)
                .ForeignKey("dbo.Firms", t => t.FirmId, cascadeDelete: true)
                .Index(t => t.FirmId);
            
            CreateTable(
                "dbo.Packages",
                c => new
                    {
                        PackageId = c.Int(nullable: false, identity: true),
                        Barcode = c.Int(nullable: false),
                        PStatusID = c.Int(nullable: false),
                        WarehouseId = c.Int(nullable: false),
                        StoreId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PackageId)
                .ForeignKey("dbo.PackageStatus", t => t.PStatusID, cascadeDelete: true)
                .ForeignKey("dbo.Stores", t => t.StoreId, cascadeDelete: false)
                .ForeignKey("dbo.Warehouses", t => t.WarehouseId, cascadeDelete: true)
                .Index(t => t.PStatusID)
                .Index(t => t.WarehouseId)
                .Index(t => t.StoreId);
            
            CreateTable(
                "dbo.PackageStatus",
                c => new
                    {
                        PStatusId = c.Int(nullable: false, identity: true),
                        PStatusName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.PStatusId);
            
            CreateTable(
                "dbo.Warehouses",
                c => new
                    {
                        WarehouseId = c.Int(nullable: false, identity: true),
                        WarehouseName = c.String(),
                        Capacity = c.Int(nullable: false),
                        CurrentOccupancy = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.WarehouseId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 100),
                        Password = c.String(nullable: false, maxLength: 100),
                        Name = c.String(maxLength: 50),
                        FirmId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Firms", t => t.FirmId)
                .Index(t => t.FirmId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "FirmId", "dbo.Firms");
            DropForeignKey("dbo.Demands", "UserId", "dbo.Users");
            DropForeignKey("dbo.Packages", "WarehouseId", "dbo.Warehouses");
            DropForeignKey("dbo.Packages", "StoreId", "dbo.Stores");
            DropForeignKey("dbo.Packages", "PStatusID", "dbo.PackageStatus");
            DropForeignKey("dbo.Demands", "PackageId", "dbo.Packages");
            DropForeignKey("dbo.Stores", "FirmId", "dbo.Firms");
            DropForeignKey("dbo.Demands", "FirmID", "dbo.Firms");
            DropIndex("dbo.Users", new[] { "FirmId" });
            DropIndex("dbo.Packages", new[] { "StoreId" });
            DropIndex("dbo.Packages", new[] { "WarehouseId" });
            DropIndex("dbo.Packages", new[] { "PStatusID" });
            DropIndex("dbo.Stores", new[] { "FirmId" });
            DropIndex("dbo.Demands", new[] { "PackageId" });
            DropIndex("dbo.Demands", new[] { "UserId" });
            DropIndex("dbo.Demands", new[] { "FirmID" });
            DropTable("dbo.Users");
            DropTable("dbo.Warehouses");
            DropTable("dbo.PackageStatus");
            DropTable("dbo.Packages");
            DropTable("dbo.Stores");
            DropTable("dbo.Firms");
            DropTable("dbo.Demands");
        }
    }
}
