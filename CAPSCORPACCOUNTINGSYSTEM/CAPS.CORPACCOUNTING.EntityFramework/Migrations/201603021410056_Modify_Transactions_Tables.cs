namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Modify_Transactions_Tables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_InvoiceEntryDocument",
                c => new
                    {
                        AHTID = c.Int(nullable: false),
                        BatchId = c.Int(),
                        VendorId = c.Int(),
                        TypeOfInvoiceId = c.Int(nullable: false),
                        PettyCashAccountId = c.Int(),
                        PaymentTermId = c.Int(),
                        TypeOfCheckGroupId = c.Int(),
                        BankAccountId = c.Int(),
                        PaymentDate = c.DateTime(storeType: "smalldatetime"),
                        PaymentNumber = c.String(),
                        PurchaseOrderReference = c.String(maxLength: 100),
                        ReversedByUserId = c.Int(),
                        ReversalDate = c.DateTime(storeType: "smalldatetime"),
                        IsInvoiceHistory = c.Boolean(nullable: false),
                        IsEnterable = c.Boolean(nullable: false),
                        GeneratedAccountingDocumentId = c.Long(),
                        UploadDocumentLogID = c.Int(),
                        BatchInfo = c.String(),
                        PaymentSelectedByUserId = c.Int(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_InvoiceEntryDocumentUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_InvoiceEntryDocumentUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.AHTID)
                .ForeignKey("dbo.CAPS_AccountingHeaderTransactions", t => t.AHTID)
                .Index(t => t.AHTID);
            
            CreateTable(
                "dbo.CAPS_PayrollEntryDocument",
                c => new
                    {
                        AHTID = c.Int(nullable: false),
                        BatchId = c.Int(),
                        VendorId = c.Int(),
                        TypeOfInvoiceId = c.Int(nullable: false),
                        IsEnterable = c.Boolean(nullable: false),
                        APInvoiceAccountingDocId = c.Long(),
                        UploadDocumentLogId = c.Int(),
                        IsReversed = c.Boolean(),
                        ReversedByUserId = c.Int(),
                        ReversalDate = c.DateTime(storeType: "smalldatetime"),
                        IsVoid = c.Boolean(),
                        IsVoidDateOriginal = c.Boolean(),
                        LinkedAccountingDocumentId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_PayrollEntryDocumentUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_PayrollEntryDocumentUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.AHTID)
                .ForeignKey("dbo.CAPS_AccountingHeaderTransactions", t => t.AHTID)
                .Index(t => t.AHTID);
            
            AddColumn("dbo.CAPS_AccountingHeaderTransactions", "TypeOfAccountingDocumentId", c => c.Int(nullable: false));
            AddColumn("dbo.CAPS_AccountingHeaderTransactions", "TypeOfObjectId", c => c.Int());
            AddColumn("dbo.CAPS_AccountingHeaderTransactions", "RecurDocId", c => c.Long());
            AddColumn("dbo.CAPS_AccountingHeaderTransactions", "ReverseDocId", c => c.Long());
            AddColumn("dbo.CAPS_AccountingHeaderTransactions", "OriginalDocumentId", c => c.Long());
            AddColumn("dbo.CAPS_AccountingHeaderTransactions", "ControlTotal", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.CAPS_AccountingHeaderTransactions", "DocumentReference", c => c.String(maxLength: 100));
            AddColumn("dbo.CAPS_AccountingHeaderTransactions", "VoucherReference", c => c.String(maxLength: 100));
            AddColumn("dbo.CAPS_AccountingHeaderTransactions", "TypeOfCurrencyId", c => c.Short());
            AddColumn("dbo.CAPS_AccountingHeaderTransactions", "CurrencyAdjustmentId", c => c.Int());
            AddColumn("dbo.CAPS_AccountingHeaderTransactions", "PostBatchDescription", c => c.String(maxLength: 100));
            AddColumn("dbo.CAPS_AccountingHeaderTransactions", "IsPosted", c => c.Boolean(nullable: false));
            AddColumn("dbo.CAPS_AccountingHeaderTransactions", "IsAutoPosted", c => c.Boolean(nullable: false));
            AddColumn("dbo.CAPS_AccountingHeaderTransactions", "IsChanged", c => c.Boolean(nullable: false));
            AddColumn("dbo.CAPS_AccountingHeaderTransactions", "PostedByUserId", c => c.Int());
            AddColumn("dbo.CAPS_AccountingHeaderTransactions", "BankRecControlId", c => c.Int());
            AddColumn("dbo.CAPS_AccountingHeaderTransactions", "IsSelected", c => c.Boolean(nullable: false));
            AddColumn("dbo.CAPS_AccountingHeaderTransactions", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.CAPS_AccountingHeaderTransactions", "IsApproved", c => c.Boolean(nullable: false));
            AddColumn("dbo.CAPS_AccountingHeaderTransactions", "TypeOfInactiveStatusId", c => c.Int());
            AddColumn("dbo.CAPS_AccountingHeaderTransactions", "IsBankRecOmitted", c => c.Boolean());
            AddColumn("dbo.CAPS_AccountingHeaderTransactions", "IsICTJournal", c => c.Boolean());
            AddColumn("dbo.CAPS_AccountingHeaderTransactions", "ICTCompanyId", c => c.Int());
            AddColumn("dbo.CAPS_AccountingHeaderTransactions", "ICTAccountingDocumentId", c => c.Long());
            AddColumn("dbo.CAPS_AccountingHeaderTransactions", "CurrencyOverrideRate", c => c.Double());
            AddColumn("dbo.CAPS_AccountingHeaderTransactions", "FunctionalCurrencyControlTotal", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.CAPS_AccountingHeaderTransactions", "TypeOfCurrencyRateId", c => c.Short());
            AddColumn("dbo.CAPS_AccountingHeaderTransactions", "MemoLine", c => c.String(maxLength: 100));
            AddColumn("dbo.CAPS_AccountingHeaderTransactions", "Is13Period", c => c.Boolean());
            AddColumn("dbo.CAPS_AccountingHeaderTransactions", "HomeCurrencyAmount", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.CAPS_AccountingHeaderTransactions", "CustomForexRate", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.CAPS_AccountingHeaderTransactions", "IsPOSubmitForApproval", c => c.Boolean(nullable: false));
            AddColumn("dbo.CAPS_AccountingHeaderTransactions", "IsCPASTran", c => c.Boolean());
            AddColumn("dbo.CAPS_AccountingHeaderTransactions", "CPASProjCloseId", c => c.Int());
            AddColumn("dbo.CAPS_AccountingHeaderTransactions", "CPASProjId", c => c.Int());
            AddColumn("dbo.CAPS_APHeaderTransactions", "BatchId", c => c.Int());
            AddColumn("dbo.CAPS_APHeaderTransactions", "VendorId", c => c.Int());
            AddColumn("dbo.CAPS_APHeaderTransactions", "TypeOfInvoiceId", c => c.Int(nullable: false));
            AddColumn("dbo.CAPS_APHeaderTransactions", "PettyCashAccountId", c => c.Long());
            AddColumn("dbo.CAPS_APHeaderTransactions", "PaymentTermId", c => c.Int());
            AddColumn("dbo.CAPS_APHeaderTransactions", "TypeOfCheckGroupId", c => c.Int());
            AddColumn("dbo.CAPS_APHeaderTransactions", "BankAccountId", c => c.Int());
            AddColumn("dbo.CAPS_APHeaderTransactions", "PaymentNumber", c => c.String());
            AddColumn("dbo.CAPS_APHeaderTransactions", "PurchaseOrderReference", c => c.String(maxLength: 100));
            AddColumn("dbo.CAPS_APHeaderTransactions", "ReversedByUserId", c => c.Int());
            AddColumn("dbo.CAPS_APHeaderTransactions", "IsInvoiceHistory", c => c.Boolean(nullable: false));
            AddColumn("dbo.CAPS_APHeaderTransactions", "IsEnterable", c => c.Boolean(nullable: false));
            AddColumn("dbo.CAPS_APHeaderTransactions", "GeneratedAccountingDocumentId", c => c.Long());
            AddColumn("dbo.CAPS_APHeaderTransactions", "UploadDocumentLogID", c => c.Int());
            AddColumn("dbo.CAPS_APHeaderTransactions", "BatchInfo", c => c.String());
            AddColumn("dbo.CAPS_APHeaderTransactions", "PaymentSelectedByUserId", c => c.Int());
            CreateIndex("dbo.CAPS_APHeaderTransactions", "BatchId");
            CreateIndex("dbo.CAPS_APHeaderTransactions", "VendorId");
            CreateIndex("dbo.CAPS_APHeaderTransactions", "PettyCashAccountId");
            CreateIndex("dbo.CAPS_APHeaderTransactions", "PaymentTermId");
            AddForeignKey("dbo.CAPS_APHeaderTransactions", "BatchId", "dbo.CAPS_Batch", "BatchId");
            AddForeignKey("dbo.CAPS_APHeaderTransactions", "VendorId", "dbo.CAPS_Vendors", "VendorId");
            AddForeignKey("dbo.CAPS_APHeaderTransactions", "PettyCashAccountId", "dbo.CAPS_Accounts", "AccountId");
            AddForeignKey("dbo.CAPS_APHeaderTransactions", "PaymentTermId", "dbo.CAPS_VendorPaymentTerms", "PaymentTermsId");
            DropColumn("dbo.CAPS_AccountingHeaderTransactions", "PostingDate");
            DropColumn("dbo.CAPS_APHeaderTransactions", "CheckDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CAPS_APHeaderTransactions", "CheckDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.CAPS_AccountingHeaderTransactions", "PostingDate", c => c.DateTime(nullable: false));
            DropForeignKey("dbo.CAPS_PayrollEntryDocument", "AHTID", "dbo.CAPS_AccountingHeaderTransactions");
            DropForeignKey("dbo.CAPS_InvoiceEntryDocument", "AHTID", "dbo.CAPS_AccountingHeaderTransactions");
            DropForeignKey("dbo.CAPS_APHeaderTransactions", "PaymentTermId", "dbo.CAPS_VendorPaymentTerms");
            DropForeignKey("dbo.CAPS_APHeaderTransactions", "PettyCashAccountId", "dbo.CAPS_Accounts");
            DropForeignKey("dbo.CAPS_APHeaderTransactions", "VendorId", "dbo.CAPS_Vendors");
            DropForeignKey("dbo.CAPS_APHeaderTransactions", "BatchId", "dbo.CAPS_Batch");
            DropIndex("dbo.CAPS_PayrollEntryDocument", new[] { "AHTID" });
            DropIndex("dbo.CAPS_InvoiceEntryDocument", new[] { "AHTID" });
            DropIndex("dbo.CAPS_APHeaderTransactions", new[] { "PaymentTermId" });
            DropIndex("dbo.CAPS_APHeaderTransactions", new[] { "PettyCashAccountId" });
            DropIndex("dbo.CAPS_APHeaderTransactions", new[] { "VendorId" });
            DropIndex("dbo.CAPS_APHeaderTransactions", new[] { "BatchId" });
            DropColumn("dbo.CAPS_APHeaderTransactions", "PaymentSelectedByUserId");
            DropColumn("dbo.CAPS_APHeaderTransactions", "BatchInfo");
            DropColumn("dbo.CAPS_APHeaderTransactions", "UploadDocumentLogID");
            DropColumn("dbo.CAPS_APHeaderTransactions", "GeneratedAccountingDocumentId");
            DropColumn("dbo.CAPS_APHeaderTransactions", "IsEnterable");
            DropColumn("dbo.CAPS_APHeaderTransactions", "IsInvoiceHistory");
            DropColumn("dbo.CAPS_APHeaderTransactions", "ReversedByUserId");
            DropColumn("dbo.CAPS_APHeaderTransactions", "PurchaseOrderReference");
            DropColumn("dbo.CAPS_APHeaderTransactions", "PaymentNumber");
            DropColumn("dbo.CAPS_APHeaderTransactions", "BankAccountId");
            DropColumn("dbo.CAPS_APHeaderTransactions", "TypeOfCheckGroupId");
            DropColumn("dbo.CAPS_APHeaderTransactions", "PaymentTermId");
            DropColumn("dbo.CAPS_APHeaderTransactions", "PettyCashAccountId");
            DropColumn("dbo.CAPS_APHeaderTransactions", "TypeOfInvoiceId");
            DropColumn("dbo.CAPS_APHeaderTransactions", "VendorId");
            DropColumn("dbo.CAPS_APHeaderTransactions", "BatchId");
            DropColumn("dbo.CAPS_AccountingHeaderTransactions", "CPASProjId");
            DropColumn("dbo.CAPS_AccountingHeaderTransactions", "CPASProjCloseId");
            DropColumn("dbo.CAPS_AccountingHeaderTransactions", "IsCPASTran");
            DropColumn("dbo.CAPS_AccountingHeaderTransactions", "IsPOSubmitForApproval");
            DropColumn("dbo.CAPS_AccountingHeaderTransactions", "CustomForexRate");
            DropColumn("dbo.CAPS_AccountingHeaderTransactions", "HomeCurrencyAmount");
            DropColumn("dbo.CAPS_AccountingHeaderTransactions", "Is13Period");
            DropColumn("dbo.CAPS_AccountingHeaderTransactions", "MemoLine");
            DropColumn("dbo.CAPS_AccountingHeaderTransactions", "TypeOfCurrencyRateId");
            DropColumn("dbo.CAPS_AccountingHeaderTransactions", "FunctionalCurrencyControlTotal");
            DropColumn("dbo.CAPS_AccountingHeaderTransactions", "CurrencyOverrideRate");
            DropColumn("dbo.CAPS_AccountingHeaderTransactions", "ICTAccountingDocumentId");
            DropColumn("dbo.CAPS_AccountingHeaderTransactions", "ICTCompanyId");
            DropColumn("dbo.CAPS_AccountingHeaderTransactions", "IsICTJournal");
            DropColumn("dbo.CAPS_AccountingHeaderTransactions", "IsBankRecOmitted");
            DropColumn("dbo.CAPS_AccountingHeaderTransactions", "TypeOfInactiveStatusId");
            DropColumn("dbo.CAPS_AccountingHeaderTransactions", "IsApproved");
            DropColumn("dbo.CAPS_AccountingHeaderTransactions", "IsActive");
            DropColumn("dbo.CAPS_AccountingHeaderTransactions", "IsSelected");
            DropColumn("dbo.CAPS_AccountingHeaderTransactions", "BankRecControlId");
            DropColumn("dbo.CAPS_AccountingHeaderTransactions", "PostedByUserId");
            DropColumn("dbo.CAPS_AccountingHeaderTransactions", "IsChanged");
            DropColumn("dbo.CAPS_AccountingHeaderTransactions", "IsAutoPosted");
            DropColumn("dbo.CAPS_AccountingHeaderTransactions", "IsPosted");
            DropColumn("dbo.CAPS_AccountingHeaderTransactions", "PostBatchDescription");
            DropColumn("dbo.CAPS_AccountingHeaderTransactions", "CurrencyAdjustmentId");
            DropColumn("dbo.CAPS_AccountingHeaderTransactions", "TypeOfCurrencyId");
            DropColumn("dbo.CAPS_AccountingHeaderTransactions", "VoucherReference");
            DropColumn("dbo.CAPS_AccountingHeaderTransactions", "DocumentReference");
            DropColumn("dbo.CAPS_AccountingHeaderTransactions", "ControlTotal");
            DropColumn("dbo.CAPS_AccountingHeaderTransactions", "OriginalDocumentId");
            DropColumn("dbo.CAPS_AccountingHeaderTransactions", "ReverseDocId");
            DropColumn("dbo.CAPS_AccountingHeaderTransactions", "RecurDocId");
            DropColumn("dbo.CAPS_AccountingHeaderTransactions", "TypeOfObjectId");
            DropColumn("dbo.CAPS_AccountingHeaderTransactions", "TypeOfAccountingDocumentId");
            DropTable("dbo.CAPS_PayrollEntryDocument",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_PayrollEntryDocumentUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_PayrollEntryDocumentUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.CAPS_InvoiceEntryDocument",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_InvoiceEntryDocumentUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_InvoiceEntryDocumentUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
