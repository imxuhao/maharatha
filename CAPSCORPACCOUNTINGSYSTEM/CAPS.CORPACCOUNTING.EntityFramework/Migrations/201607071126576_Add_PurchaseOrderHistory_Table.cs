namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_PurchaseOrderHistory_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_PurchaseOrderHistory",
                c => new
                    {
                        CAPS_PurchaseOrderHistoryId = c.Long(nullable: false, identity: true),
                        AccountingItemId = c.Long(nullable: false),
                        AccountingDocumentId = c.Long(),
                        TypeOfTransactionId = c.Int(nullable: false),
                        TypeOfAmountId = c.Int(nullable: false),
                        ItemLinkId = c.Long(),
                        LedgerReference = c.String(),
                        LedgerDate = c.DateTime(storeType: "smalldatetime"),
                        LedgerYyyymm = c.Int(),
                        AccountId = c.Long(nullable: false),
                        JobId = c.Int(nullable: false),
                        ItemMemo = c.String(),
                        IsAsset = c.Boolean(nullable: false),
                        AccountRef1 = c.String(),
                        AccountRef2 = c.String(),
                        AccountRef3 = c.String(),
                        AccountRef4 = c.String(),
                        AccountRef5 = c.String(),
                        AccountRef6 = c.String(),
                        AccountRef7 = c.String(),
                        AccountRef8 = c.String(),
                        AccountRef9 = c.String(),
                        AccountRef10 = c.String(),
                        SubAccountId1 = c.Long(),
                        SubAccountId2 = c.Long(),
                        SubAccountId3 = c.Long(),
                        SubAccountId4 = c.Long(),
                        SubAccountId5 = c.Long(),
                        SubAccountId6 = c.Long(),
                        SubAccountId7 = c.Long(),
                        SubAccountId8 = c.Long(),
                        SubAccountId9 = c.Long(),
                        SubAccountId10 = c.Long(),
                        TypeOf1099T4Id = c.Int(),
                        Amount = c.Decimal(precision: 18, scale: 2),
                        CompanyCurrencyAmount = c.Decimal(precision: 18, scale: 2),
                        CurrencyAdjustmentAmount = c.Decimal(precision: 18, scale: 2),
                        OriginalItemId = c.Long(),
                        AccountingItemIdLink = c.Long(),
                        IsChanged = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        TypeOfInactiveStatusId = c.Int(),
                        ReconciliationId = c.Int(),
                        IsEnterable = c.Boolean(nullable: false),
                        AccountingItemOrigAmount = c.Decimal(precision: 18, scale: 2),
                        AccountingItemTypeOfModificationId = c.Int(),
                        SplitAccountingItemId = c.Long(),
                        IctJobId = c.Int(),
                        IctAccountingItemId = c.Long(),
                        TaxRebateId = c.Int(),
                        CurrencyOverrideRate = c.Double(),
                        FunctionalCurrencyAmount = c.Decimal(precision: 18, scale: 2),
                        TypeOfCurrencyRateId = c.Short(),
                        TypeOfCurrencyId = c.Short(),
                        HomeCurAmount = c.Decimal(precision: 18, scale: 2),
                        CustomForexRate = c.Decimal(precision: 18, scale: 2),
                        TenantId = c.Int(nullable: false),
                        OrganizationUnitId = c.Long(nullable: false),
                        IsAccountingItemSplit = c.Boolean(nullable: false),
                        IsPrePaid = c.Boolean(nullable: false),
                        IsPoPurchase = c.Boolean(),
                        IsPoRental = c.Boolean(),
                        VendorId = c.Int(),
                        ModificationTypeId = c.Int(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_PurchaseOrderHistory_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_PurchaseOrderHistory_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.CAPS_PurchaseOrderHistoryId)
                .ForeignKey("dbo.CAPS_Account", t => t.AccountId, cascadeDelete: true)
                .ForeignKey("dbo.CAPS_AccountingDocument", t => t.AccountingDocumentId)
                .ForeignKey("dbo.CAPS_AccountingItem", t => t.SplitAccountingItemId)
                .ForeignKey("dbo.CAPS_BankRecControl", t => t.ReconciliationId)
                .ForeignKey("dbo.CAPS_Job", t => t.JobId, cascadeDelete: true)
                .ForeignKey("dbo.CAPS_PurchaseOrderEntryDocumentDetail", t => t.AccountingItemId)
                .ForeignKey("dbo.CAPS_SubAccount", t => t.SubAccountId1)
                .ForeignKey("dbo.CAPS_SubAccount", t => t.SubAccountId10)
                .ForeignKey("dbo.CAPS_SubAccount", t => t.SubAccountId2)
                .ForeignKey("dbo.CAPS_SubAccount", t => t.SubAccountId3)
                .ForeignKey("dbo.CAPS_SubAccount", t => t.SubAccountId4)
                .ForeignKey("dbo.CAPS_SubAccount", t => t.SubAccountId5)
                .ForeignKey("dbo.CAPS_SubAccount", t => t.SubAccountId6)
                .ForeignKey("dbo.CAPS_SubAccount", t => t.SubAccountId7)
                .ForeignKey("dbo.CAPS_SubAccount", t => t.SubAccountId8)
                .ForeignKey("dbo.CAPS_SubAccount", t => t.SubAccountId9)
                .ForeignKey("dbo.CAPS_Vendor", t => t.VendorId)
                .Index(t => t.AccountingItemId)
                .Index(t => t.AccountingDocumentId)
                .Index(t => t.AccountId)
                .Index(t => t.JobId)
                .Index(t => t.SubAccountId1)
                .Index(t => t.SubAccountId2)
                .Index(t => t.SubAccountId3)
                .Index(t => t.SubAccountId4)
                .Index(t => t.SubAccountId5)
                .Index(t => t.SubAccountId6)
                .Index(t => t.SubAccountId7)
                .Index(t => t.SubAccountId8)
                .Index(t => t.SubAccountId9)
                .Index(t => t.SubAccountId10)
                .Index(t => t.ReconciliationId)
                .Index(t => t.SplitAccountingItemId)
                .Index(t => t.VendorId);
            
            AddColumn("dbo.CAPS_PurchaseOrderEntryDocument", "IsClose", c => c.Boolean());
            AddColumn("dbo.CAPS_PurchaseOrderEntryDocument", "CloseDate", c => c.DateTime());
            DropColumn("dbo.CAPS_PurchaseOrderEntryDocument", "IsColse");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CAPS_PurchaseOrderEntryDocument", "IsColse", c => c.Boolean());
            DropForeignKey("dbo.CAPS_PurchaseOrderHistory", "VendorId", "dbo.CAPS_Vendor");
            DropForeignKey("dbo.CAPS_PurchaseOrderHistory", "SubAccountId9", "dbo.CAPS_SubAccount");
            DropForeignKey("dbo.CAPS_PurchaseOrderHistory", "SubAccountId8", "dbo.CAPS_SubAccount");
            DropForeignKey("dbo.CAPS_PurchaseOrderHistory", "SubAccountId7", "dbo.CAPS_SubAccount");
            DropForeignKey("dbo.CAPS_PurchaseOrderHistory", "SubAccountId6", "dbo.CAPS_SubAccount");
            DropForeignKey("dbo.CAPS_PurchaseOrderHistory", "SubAccountId5", "dbo.CAPS_SubAccount");
            DropForeignKey("dbo.CAPS_PurchaseOrderHistory", "SubAccountId4", "dbo.CAPS_SubAccount");
            DropForeignKey("dbo.CAPS_PurchaseOrderHistory", "SubAccountId3", "dbo.CAPS_SubAccount");
            DropForeignKey("dbo.CAPS_PurchaseOrderHistory", "SubAccountId2", "dbo.CAPS_SubAccount");
            DropForeignKey("dbo.CAPS_PurchaseOrderHistory", "SubAccountId10", "dbo.CAPS_SubAccount");
            DropForeignKey("dbo.CAPS_PurchaseOrderHistory", "SubAccountId1", "dbo.CAPS_SubAccount");
            DropForeignKey("dbo.CAPS_PurchaseOrderHistory", "AccountingItemId", "dbo.CAPS_PurchaseOrderEntryDocumentDetail");
            DropForeignKey("dbo.CAPS_PurchaseOrderHistory", "JobId", "dbo.CAPS_Job");
            DropForeignKey("dbo.CAPS_PurchaseOrderHistory", "ReconciliationId", "dbo.CAPS_BankRecControl");
            DropForeignKey("dbo.CAPS_PurchaseOrderHistory", "SplitAccountingItemId", "dbo.CAPS_AccountingItem");
            DropForeignKey("dbo.CAPS_PurchaseOrderHistory", "AccountingDocumentId", "dbo.CAPS_AccountingDocument");
            DropForeignKey("dbo.CAPS_PurchaseOrderHistory", "AccountId", "dbo.CAPS_Account");
            DropIndex("dbo.CAPS_PurchaseOrderHistory", new[] { "VendorId" });
            DropIndex("dbo.CAPS_PurchaseOrderHistory", new[] { "SplitAccountingItemId" });
            DropIndex("dbo.CAPS_PurchaseOrderHistory", new[] { "ReconciliationId" });
            DropIndex("dbo.CAPS_PurchaseOrderHistory", new[] { "SubAccountId10" });
            DropIndex("dbo.CAPS_PurchaseOrderHistory", new[] { "SubAccountId9" });
            DropIndex("dbo.CAPS_PurchaseOrderHistory", new[] { "SubAccountId8" });
            DropIndex("dbo.CAPS_PurchaseOrderHistory", new[] { "SubAccountId7" });
            DropIndex("dbo.CAPS_PurchaseOrderHistory", new[] { "SubAccountId6" });
            DropIndex("dbo.CAPS_PurchaseOrderHistory", new[] { "SubAccountId5" });
            DropIndex("dbo.CAPS_PurchaseOrderHistory", new[] { "SubAccountId4" });
            DropIndex("dbo.CAPS_PurchaseOrderHistory", new[] { "SubAccountId3" });
            DropIndex("dbo.CAPS_PurchaseOrderHistory", new[] { "SubAccountId2" });
            DropIndex("dbo.CAPS_PurchaseOrderHistory", new[] { "SubAccountId1" });
            DropIndex("dbo.CAPS_PurchaseOrderHistory", new[] { "JobId" });
            DropIndex("dbo.CAPS_PurchaseOrderHistory", new[] { "AccountId" });
            DropIndex("dbo.CAPS_PurchaseOrderHistory", new[] { "AccountingDocumentId" });
            DropIndex("dbo.CAPS_PurchaseOrderHistory", new[] { "AccountingItemId" });
            DropColumn("dbo.CAPS_PurchaseOrderEntryDocument", "CloseDate");
            DropColumn("dbo.CAPS_PurchaseOrderEntryDocument", "IsClose");
            DropTable("dbo.CAPS_PurchaseOrderHistory",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_PurchaseOrderHistory_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_PurchaseOrderHistory_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
