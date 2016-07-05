namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOrganizationColumnTenantTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CAPS_Tenants", "OrganizationUnitId", c => c.Long());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CAPS_Tenants", "OrganizationUnitId");
        }
    }
}
