namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_CashEntryDocumentDetail_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_CashEntryDocumentDetail",
                c => new
                    {
                        AccountingItemId = c.Long(nullable: false),
                        VendorId = c.Int(),
                        BankAccountId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_CashEntryDocumentDetailUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_CashEntryDocumentDetailUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.AccountingItemId)
                .ForeignKey("dbo.CAPS_AccountingItem", t => t.AccountingItemId)
                .ForeignKey("dbo.CAPS_Vendor", t => t.VendorId)
                .ForeignKey("dbo.CAPS_BankAccount", t => t.BankAccountId)
                .Index(t => t.AccountingItemId)
                .Index(t => t.VendorId)
                .Index(t => t.BankAccountId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_CashEntryDocumentDetail", "BankAccountId", "dbo.CAPS_BankAccount");
            DropForeignKey("dbo.CAPS_CashEntryDocumentDetail", "VendorId", "dbo.CAPS_Vendor");
            DropForeignKey("dbo.CAPS_CashEntryDocumentDetail", "AccountingItemId", "dbo.CAPS_AccountingItem");
            DropIndex("dbo.CAPS_CashEntryDocumentDetail", new[] { "BankAccountId" });
            DropIndex("dbo.CAPS_CashEntryDocumentDetail", new[] { "VendorId" });
            DropIndex("dbo.CAPS_CashEntryDocumentDetail", new[] { "AccountingItemId" });
            DropTable("dbo.CAPS_CashEntryDocumentDetail",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_CashEntryDocumentDetailUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_CashEntryDocumentDetailUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
