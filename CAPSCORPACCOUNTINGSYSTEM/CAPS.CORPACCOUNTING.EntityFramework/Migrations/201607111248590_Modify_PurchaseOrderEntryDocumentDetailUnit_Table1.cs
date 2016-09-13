namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modify_PurchaseOrderEntryDocumentDetailUnit_Table1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CAPS_PurchaseOrderEntryDocumentDetail", "RemainingAmount", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.CAPS_PurchaseOrderEntryDocumentDetail", "PendingAmount", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.CAPS_PurchaseOrderHistory", "RemainingAmount", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.CAPS_PurchaseOrderHistory", "PendingAmount", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CAPS_PurchaseOrderHistory", "PendingAmount");
            DropColumn("dbo.CAPS_PurchaseOrderHistory", "RemainingAmount");
            DropColumn("dbo.CAPS_PurchaseOrderEntryDocumentDetail", "PendingAmount");
            DropColumn("dbo.CAPS_PurchaseOrderEntryDocumentDetail", "RemainingAmount");
        }
    }
}
