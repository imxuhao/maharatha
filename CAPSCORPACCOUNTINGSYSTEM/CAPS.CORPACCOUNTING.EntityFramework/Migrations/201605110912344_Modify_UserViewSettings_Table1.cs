namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modify_UserViewSettings_Table1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CAPS_UserViewSettings", "UserId", "dbo.CAPS_Users");
            DropIndex("dbo.CAPS_UserViewSettings", new[] { "UserId" });
            AlterColumn("dbo.CAPS_UserViewSettings", "UserId", c => c.Long());
            CreateIndex("dbo.CAPS_UserViewSettings", "UserId");
            AddForeignKey("dbo.CAPS_UserViewSettings", "UserId", "dbo.CAPS_Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_UserViewSettings", "UserId", "dbo.CAPS_Users");
            DropIndex("dbo.CAPS_UserViewSettings", new[] { "UserId" });
            AlterColumn("dbo.CAPS_UserViewSettings", "UserId", c => c.Long(nullable: false));
            CreateIndex("dbo.CAPS_UserViewSettings", "UserId");
            AddForeignKey("dbo.CAPS_UserViewSettings", "UserId", "dbo.CAPS_Users", "Id", cascadeDelete: true);
        }
    }
}
