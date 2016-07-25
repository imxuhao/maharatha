namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Modify_AccountingItemUnit_Table : DbMigration
    {
        public override void Up()
        {
            AlterTableAnnotations(
                "dbo.CAPS_PurchaseOrderHistory",
                c => new
                    {
                        PurchaseOrderHistoryId = c.Long(nullable: false, identity: true),
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
                        OverRelieveAmount = c.Decimal(precision: 18, scale: 2),
                        RemainingAmount = c.Decimal(precision: 18, scale: 2),
                        PendingAmount = c.Decimal(precision: 18, scale: 2),
                        CheckTypeId = c.Int(),
                        ChangeInAmount = c.Decimal(precision: 18, scale: 2),
                        RowNumber = c.Long(),
                        SourceTypeId = c.Int(),
                        DocumentReference = c.String(maxLength: 100),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_PurchaseOrderHistory_MustHaveTenant",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                    { 
                        "DynamicFilter_PurchaseOrderHistory_SoftDelete",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                    { 
                        "DynamicFilter_PurchaseOrderHistoryUnit_MustHaveTenant",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            AddColumn("dbo.CAPS_AccountingItem", "PoAccountingItemId", c => c.Long());
            AddColumn("dbo.CAPS_AccountingItem", "CheckTypeId", c => c.Int());
            AddColumn("dbo.CAPS_AccountingItem", "RowNumber", c => c.Long());
            AddColumn("dbo.CAPS_PurchaseOrderEntryDocumentDetail", "IsClose", c => c.Boolean());
            AddColumn("dbo.CAPS_PurchaseOrderEntryDocumentDetail", "CloseDate", c => c.DateTime());
            AddColumn("dbo.CAPS_PurchaseOrderEntryDocumentDetail", "SourceTypeId", c => c.Int());
            AddColumn("dbo.CAPS_PurchaseOrderEntryDocumentDetail", "DocumentReference", c => c.String(maxLength: 100));
            AddColumn("dbo.CAPS_PurchaseOrderEntryDocumentDetail", "SourceId", c => c.Long());
            AddColumn("dbo.CAPS_PurchaseOrderHistory", "CheckTypeId", c => c.Int());
            AddColumn("dbo.CAPS_PurchaseOrderHistory", "ChangeInAmount", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.CAPS_PurchaseOrderHistory", "RowNumber", c => c.Long());
            AddColumn("dbo.CAPS_PurchaseOrderHistory", "SourceTypeId", c => c.Int());
            AddColumn("dbo.CAPS_PurchaseOrderHistory", "DocumentReference", c => c.String(maxLength: 100));
            CreateIndex("dbo.CAPS_PurchaseOrderEntryDocumentDetail", "SourceId");
            AddForeignKey("dbo.CAPS_PurchaseOrderEntryDocumentDetail", "SourceId", "dbo.CAPS_AccountingDocument", "AccountingDocumentId");
            DropColumn("dbo.CAPS_AccountingItem", "OriginalItemId");
            DropColumn("dbo.CAPS_PurchaseOrderHistory", "OriginalItemId");
            DropColumn("dbo.CAPS_PurchaseOrderHistory", "IsDeleted");
            DropColumn("dbo.CAPS_PurchaseOrderHistory", "DeleterUserId");
            DropColumn("dbo.CAPS_PurchaseOrderHistory", "DeletionTime");
            DropColumn("dbo.CAPS_PurchaseOrderHistory", "LastModificationTime");
            DropColumn("dbo.CAPS_PurchaseOrderHistory", "LastModifierUserId");
        }
        
        public override void Down()
        {
            
                  Sql(@"ALTER TABLE [dbo].[CAPS_PurchaseOrderHistory] DROP CONSTRAINT [FK_CAPS_PurchaseOrderHistory_SourceTypeId]");
            Sql(@"ALTER TABLE [dbo].[CAPS_PurchaseOrderHistory] DROP CONSTRAINT [FK_CAPS_PurchaseOrderHistory_CheckTypeId]");
            Sql(@"ALTER TABLE [dbo].[CAPS_AccountingItem] DROP CONSTRAINT [FK_CAPS_AccountingItem_CheckTypeId]");
            AddColumn("dbo.CAPS_PurchaseOrderHistory", "LastModifierUserId", c => c.Long());
            AddColumn("dbo.CAPS_PurchaseOrderHistory", "LastModificationTime", c => c.DateTime());
            AddColumn("dbo.CAPS_PurchaseOrderHistory", "DeletionTime", c => c.DateTime());
            AddColumn("dbo.CAPS_PurchaseOrderHistory", "DeleterUserId", c => c.Long());
            AddColumn("dbo.CAPS_PurchaseOrderHistory", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.CAPS_PurchaseOrderHistory", "OriginalItemId", c => c.Long());
            AddColumn("dbo.CAPS_AccountingItem", "OriginalItemId", c => c.Long());
            DropForeignKey("dbo.CAPS_PurchaseOrderEntryDocumentDetail", "SourceId", "dbo.CAPS_AccountingDocument");
            DropIndex("dbo.CAPS_PurchaseOrderEntryDocumentDetail", new[] { "SourceId" });
            DropColumn("dbo.CAPS_PurchaseOrderHistory", "DocumentReference");
            DropColumn("dbo.CAPS_PurchaseOrderHistory", "SourceTypeId");
            DropColumn("dbo.CAPS_PurchaseOrderHistory", "RowNumber");
            DropColumn("dbo.CAPS_PurchaseOrderHistory", "ChangeInAmount");
            DropColumn("dbo.CAPS_PurchaseOrderHistory", "CheckTypeId");
            DropColumn("dbo.CAPS_PurchaseOrderEntryDocumentDetail", "SourceId");
            DropColumn("dbo.CAPS_PurchaseOrderEntryDocumentDetail", "DocumentReference");
            DropColumn("dbo.CAPS_PurchaseOrderEntryDocumentDetail", "SourceTypeId");
            DropColumn("dbo.CAPS_PurchaseOrderEntryDocumentDetail", "CloseDate");
            DropColumn("dbo.CAPS_PurchaseOrderEntryDocumentDetail", "IsClose");
            DropColumn("dbo.CAPS_AccountingItem", "RowNumber");
            DropColumn("dbo.CAPS_AccountingItem", "CheckTypeId");
            DropColumn("dbo.CAPS_AccountingItem", "PoAccountingItemId");
            AlterTableAnnotations(
                "dbo.CAPS_PurchaseOrderHistory",
                c => new
                    {
                        PurchaseOrderHistoryId = c.Long(nullable: false, identity: true),
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
                        OverRelieveAmount = c.Decimal(precision: 18, scale: 2),
                        RemainingAmount = c.Decimal(precision: 18, scale: 2),
                        PendingAmount = c.Decimal(precision: 18, scale: 2),
                        CheckTypeId = c.Int(),
                        ChangeInAmount = c.Decimal(precision: 18, scale: 2),
                        RowNumber = c.Long(),
                        SourceTypeId = c.Int(),
                        DocumentReference = c.String(maxLength: 100),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_PurchaseOrderHistory_MustHaveTenant",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                    { 
                        "DynamicFilter_PurchaseOrderHistory_SoftDelete",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                    { 
                        "DynamicFilter_PurchaseOrderHistoryUnit_MustHaveTenant",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
        }
    }
}
