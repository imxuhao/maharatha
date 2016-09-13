namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modify_TenantExtended_Table : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CAPS_TenantExtended", "CompanyLogoId", c => c.Guid());
            DropColumn("dbo.CAPS_TenantExtended", "Logo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CAPS_TenantExtended", "Logo", c => c.Binary());
            DropColumn("dbo.CAPS_TenantExtended", "CompanyLogoId");
        }
    }
}
