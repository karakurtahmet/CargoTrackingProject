namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCurrentUser : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CurrentUsers",
                c => new
                    {
                        CurrentUserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.CurrentUserId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CurrentUsers");
        }
    }
}
