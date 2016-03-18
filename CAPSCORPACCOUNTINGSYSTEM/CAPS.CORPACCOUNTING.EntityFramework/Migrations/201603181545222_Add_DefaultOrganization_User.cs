namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_DefaultOrganization_User : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CAPS_Users", "DefaultOrganization_Id", c => c.Long());
            CreateIndex("dbo.CAPS_Users", "DefaultOrganization_Id");
            AddForeignKey("dbo.CAPS_Users", "DefaultOrganization_Id", "dbo.CAPS_OrganizationUnits", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_Users", "DefaultOrganization_Id", "dbo.CAPS_OrganizationUnits");
            DropIndex("dbo.CAPS_Users", new[] { "DefaultOrganization_Id" });
            DropColumn("dbo.CAPS_Users", "DefaultOrganization_Id");
        }
    }
}
