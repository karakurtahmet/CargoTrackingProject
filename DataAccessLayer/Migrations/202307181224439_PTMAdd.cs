namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PTMAdd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PackageTrackings",
                c => new
                    {
                        TrackingId = c.Int(nullable: false, identity: true),
                        Barcode = c.String(),
                        Description = c.String(),
                        IndexDate = c.DateTime(nullable: true),
                    })
                .PrimaryKey(t => t.TrackingId);
            
            AlterColumn("dbo.Packages", "Barcode", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Packages", "Barcode", c => c.Int(nullable: false));
            DropTable("dbo.PackageTrackings");
        }
    }
}
