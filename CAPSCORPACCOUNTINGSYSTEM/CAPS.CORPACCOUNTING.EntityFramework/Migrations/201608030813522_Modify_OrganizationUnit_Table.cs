namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modify_OrganizationUnit_Table : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CAPS_OrganizationUnits", "EntityClassificationId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CAPS_OrganizationUnits", "EntityClassificationId");
        }
    }
}