namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PackageAddDemand1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Packages", "DemandID", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Packages", "DemandID", c => c.Short());
        }
    }
}
