namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DemandStatusUpdate2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Demands", "StatusId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Demands", "StatusId", c => c.Int());
        }
    }
}
