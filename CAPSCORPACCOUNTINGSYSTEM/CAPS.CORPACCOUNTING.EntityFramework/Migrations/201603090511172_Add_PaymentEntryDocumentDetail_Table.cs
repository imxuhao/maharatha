namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_PaymentEntryDocumentDetail_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_PaymentEntryDocumentDetail",
                c => new
                    {
                        AccountingItemId = c.Long(nullable: false),
                        PurchaseOrderItemId = c.Long(),
                        PoHistoryItemId = c.Long(),
                        VendorId = c.Int(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_PaymentEntryDocumentDetailUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_PaymentEntryDocumentDetailUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.AccountingItemId)
                .ForeignKey("dbo.CAPS_AccountingItem", t => t.AccountingItemId)
                .ForeignKey("dbo.CAPS_Vendor", t => t.VendorId)
                .Index(t => t.AccountingItemId)
                .Index(t => t.VendorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_PaymentEntryDocumentDetail", "VendorId", "dbo.CAPS_Vendor");
            DropForeignKey("dbo.CAPS_PaymentEntryDocumentDetail", "AccountingItemId", "dbo.CAPS_AccountingItem");
            DropIndex("dbo.CAPS_PaymentEntryDocumentDetail", new[] { "VendorId" });
            DropIndex("dbo.CAPS_PaymentEntryDocumentDetail", new[] { "AccountingItemId" });
            DropTable("dbo.CAPS_PaymentEntryDocumentDetail",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_PaymentEntryDocumentDetailUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_PaymentEntryDocumentDetailUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
