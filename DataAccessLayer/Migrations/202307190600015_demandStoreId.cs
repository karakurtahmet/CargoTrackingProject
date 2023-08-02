namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class demandStoreId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Demands", "StoreCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Demands", "StoreCode");
        }
    }
}
