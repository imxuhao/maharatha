namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_PettyCashEntryDocumentDetail_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_PettyCashEntryDocumentDetail",
                c => new
                    {
                        AccountingItemId = c.Long(nullable: false),
                        PettyCashId = c.Int(),
                        PurchaseOrderItemId = c.Long(),
                        PoHistoryItemId = c.Long(),
                        VendorId = c.Int(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_PettyCashEntryDocumentDetailUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_PettyCashEntryDocumentDetailUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.AccountingItemId)
                .ForeignKey("dbo.CAPS_AccountingItem", t => t.AccountingItemId)
                .ForeignKey("dbo.CAPS_Vendor", t => t.VendorId)
                .Index(t => t.AccountingItemId)
                .Index(t => t.VendorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_PettyCashEntryDocumentDetail", "VendorId", "dbo.CAPS_Vendor");
            DropForeignKey("dbo.CAPS_PettyCashEntryDocumentDetail", "AccountingItemId", "dbo.CAPS_AccountingItem");
            DropIndex("dbo.CAPS_PettyCashEntryDocumentDetail", new[] { "VendorId" });
            DropIndex("dbo.CAPS_PettyCashEntryDocumentDetail", new[] { "AccountingItemId" });
            DropTable("dbo.CAPS_PettyCashEntryDocumentDetail",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_PettyCashEntryDocumentDetailUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_PettyCashEntryDocumentDetailUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
