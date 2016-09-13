namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_User_OrganizationID : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.CAPS_Users", name: "DefaultOrganization_Id", newName: "DefaultOrganizationId");
            RenameIndex(table: "dbo.CAPS_Users", name: "IX_DefaultOrganization_Id", newName: "IX_DefaultOrganizationId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.CAPS_Users", name: "IX_DefaultOrganizationId", newName: "IX_DefaultOrganization_Id");
            RenameColumn(table: "dbo.CAPS_Users", name: "DefaultOrganizationId", newName: "DefaultOrganization_Id");
        }
    }
}
