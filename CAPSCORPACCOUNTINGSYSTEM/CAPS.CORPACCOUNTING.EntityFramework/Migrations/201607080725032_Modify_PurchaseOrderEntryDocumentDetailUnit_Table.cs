namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modify_PurchaseOrderEntryDocumentDetailUnit_Table : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CAPS_PurchaseOrderEntryDocumentDetail", "OverRelieveAmount", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.CAPS_PurchaseOrderHistory", "OverRelieveAmount", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CAPS_PurchaseOrderHistory", "OverRelieveAmount");
            DropColumn("dbo.CAPS_PurchaseOrderEntryDocumentDetail", "OverRelieveAmount");
        }
    }
}
