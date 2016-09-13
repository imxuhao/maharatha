namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Organization_POHistory_Tables : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.CAPS_PurchaseOrderHistory", name: "CAPS_PurchaseOrderHistoryId", newName: "PurchaseOrderHistoryId");
            AddColumn("dbo.CAPS_OrganizationUnits", "ConnectionStringId", c => c.Int());
            CreateIndex("dbo.CAPS_OrganizationUnits", "ConnectionStringId");
            AddForeignKey("dbo.CAPS_OrganizationUnits", "ConnectionStringId", "dbo.Caps_ConnectionStrings", "ConnectionStringId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_OrganizationUnits", "ConnectionStringId", "dbo.Caps_ConnectionStrings");
            DropIndex("dbo.CAPS_OrganizationUnits", new[] { "ConnectionStringId" });
            DropColumn("dbo.CAPS_OrganizationUnits", "ConnectionStringId");
            RenameColumn(table: "dbo.CAPS_PurchaseOrderHistory", name: "PurchaseOrderHistoryId", newName: "CAPS_PurchaseOrderHistoryId");
        }
    }
}
