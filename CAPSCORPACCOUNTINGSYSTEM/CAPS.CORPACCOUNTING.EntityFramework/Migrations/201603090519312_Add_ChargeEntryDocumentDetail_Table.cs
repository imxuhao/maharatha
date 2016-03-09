namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_ChargeEntryDocumentDetail_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_ChargeEntryDocumentDetail",
                c => new
                    {
                        AccountingItemId = c.Long(nullable: false),
                        PurchaseOrderItemId = c.Long(),
                        PoHistoryItemId = c.Long(),
                        VendorId = c.Int(),
                        IsPrePaid = c.Boolean(nullable: false),
                        BankAccountId = c.Long(),
                        TypeOfChargeId = c.Short(),
                        ChargeReferenceNumber = c.String(),
                        ChargeDate = c.DateTime(),
                        ChargeSicCode = c.String(),
                        ChargeSeNumber = c.String(),
                        ChargeOtherInfo = c.String(),
                        ChargeOriginalAmount = c.Decimal(precision: 18, scale: 2),
                        ChargeRetiredAmount = c.Decimal(precision: 18, scale: 2),
                        IsChargeDisputed = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ChargeEntryDocumentDetailUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_ChargeEntryDocumentDetailUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
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
            DropForeignKey("dbo.CAPS_ChargeEntryDocumentDetail", "BankAccountId", "dbo.CAPS_BankAccount");
            DropForeignKey("dbo.CAPS_ChargeEntryDocumentDetail", "VendorId", "dbo.CAPS_Vendor");
            DropForeignKey("dbo.CAPS_ChargeEntryDocumentDetail", "AccountingItemId", "dbo.CAPS_AccountingItem");
            DropIndex("dbo.CAPS_ChargeEntryDocumentDetail", new[] { "BankAccountId" });
            DropIndex("dbo.CAPS_ChargeEntryDocumentDetail", new[] { "VendorId" });
            DropIndex("dbo.CAPS_ChargeEntryDocumentDetail", new[] { "AccountingItemId" });
            DropTable("dbo.CAPS_ChargeEntryDocumentDetail",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ChargeEntryDocumentDetailUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_ChargeEntryDocumentDetailUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
