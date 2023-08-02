namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class demandUpdate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Demands", "FirmID", "dbo.Firms");
            DropIndex("dbo.Demands", new[] { "FirmID" });
            DropColumn("dbo.Demands", "FirmID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Demands", "FirmID", c => c.Int(nullable: false));
            CreateIndex("dbo.Demands", "FirmID");
            AddForeignKey("dbo.Demands", "FirmID", "dbo.Firms", "FirmId", cascadeDelete: true);
        }
    }
}
