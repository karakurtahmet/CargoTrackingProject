namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PackageAddDemand : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Packages", "DemandID", c => c.Short(nullable: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Packages", "DemandID");
        }
    }
}
