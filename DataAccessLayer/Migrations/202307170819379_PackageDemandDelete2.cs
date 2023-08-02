namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PackageDemandDelete2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Packages", "DemandID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Packages", "DemandID", c => c.Int());
        }
    }
}
