namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deletedcurrentuser : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.CurrentUsers");
        }
        
        public override void Down()
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
    }
}
