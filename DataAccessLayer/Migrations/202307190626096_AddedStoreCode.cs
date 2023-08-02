namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedStoreCode : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Stores", "StoreCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Stores", "StoreCode");
        }
    }
}
