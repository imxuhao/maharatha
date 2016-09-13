namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Organization_Roles : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AbpRoles", "OrganizationUnitId", c => c.Long());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AbpRoles", "OrganizationUnitId");
        }
    }
}
