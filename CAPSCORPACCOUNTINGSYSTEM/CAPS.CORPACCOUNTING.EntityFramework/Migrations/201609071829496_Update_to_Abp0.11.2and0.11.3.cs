namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_to_Abp0112and0113 : DbMigration
    {
        public override void Up()
        {
           // AlterColumn("dbo.CAPS_OrganizationUnits", "Code", c => c.String(nullable: false, maxLength: 95));
            DropIndex("dbo.CAPS_OrganizationUnits", new[] { "TenantId", "Code" });
            AlterColumn("dbo.CAPS_OrganizationUnits", "Code", c => c.String(nullable: false, maxLength: 95));
            CreateIndex("dbo.CAPS_OrganizationUnits", new[] { "TenantId", "Code" });
        }
        
        public override void Down()
        {
            //AlterColumn("dbo.CAPS_OrganizationUnits", "Code", c => c.String(nullable: false, maxLength: 128));
            DropIndex("dbo.CAPS_OrganizationUnits", new[] { "TenantId", "Code" });
            AlterColumn("dbo.CAPS_OrganizationUnits", "Code", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.CAPS_OrganizationUnits", new[] { "TenantId", "Code" });
        }
    }
}
