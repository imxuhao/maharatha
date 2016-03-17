namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatingAbpzerotoV190 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.CAPS_Roles", "OrganizationUnitId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CAPS_Roles", "OrganizationUnitId", c => c.Long());
        }
    }
}
