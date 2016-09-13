namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Abp_V191 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.CAPS_Tenant", newName: "CAPS_Tenants");
            DropIndex("dbo.CAPS_UserLoginAttempts", new[] { "TenancyName", "UserNameOrEmailAddress", "Result" });
            AlterColumn("dbo.CAPS_UserLoginAttempts", "UserNameOrEmailAddress", c => c.String(maxLength: 255));
            CreateIndex("dbo.CAPS_UserLoginAttempts", new[] { "UserId", "TenantId" });
            CreateIndex("dbo.CAPS_UserLoginAttempts", new[] { "TenancyName", "UserNameOrEmailAddress", "Result" });
        }
        
        public override void Down()
        {
            DropIndex("dbo.CAPS_UserLoginAttempts", new[] { "TenancyName", "UserNameOrEmailAddress", "Result" });
            DropIndex("dbo.CAPS_UserLoginAttempts", new[] { "UserId", "TenantId" });
            AlterColumn("dbo.CAPS_UserLoginAttempts", "UserNameOrEmailAddress", c => c.String(maxLength: 256));
            CreateIndex("dbo.CAPS_UserLoginAttempts", new[] { "TenancyName", "UserNameOrEmailAddress", "Result" });
            RenameTable(name: "dbo.CAPS_Tenants", newName: "CAPS_Tenant");
        }
    }
}
