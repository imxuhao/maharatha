namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Fix_PettyCashAccount_DataType : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_ARInvoiceEntryDocument",
                c => new
                    {
                        AHTID = c.Int(nullable: false),
                        BatchId = c.Int(),
                        CustomerId = c.Int(nullable: false),
                        ArytdInvoiceId = c.Int(),
                        InvoiceNotes = c.String(),
                        SalesRepId = c.Int(),
                        ArPaymentTermId = c.Int(),
                        ContactAddressId = c.Int(),
                        TypeOfInvoiceId = c.Int(),
                        IsInvoiceHistory = c.Boolean(nullable: false),
                        IsEnterable = c.Boolean(nullable: false),
                        ReversedByUserId = c.Int(),
                        GroupBillingAccountingDocumentId = c.Long(),
                        GroupBillingSequence = c.Short(),
                        IsProductionDetailsPrinted = c.Boolean(nullable: false),
                        BatchInfo = c.String(),
                        PurchaseOrderReference = c.String(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ArInvoiceEntryDocumentUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_ArInvoiceEntryDocumentUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.AHTID)
                .ForeignKey("dbo.CAPS_AccountingHeaderTransactions", t => t.AHTID)
                .ForeignKey("dbo.CAPS_Batch", t => t.BatchId)
                .Index(t => t.AHTID)
                .Index(t => t.BatchId);
            
            CreateTable(
                "dbo.CAPS_CashEntryDocument",
                c => new
                    {
                        AHTID = c.Int(nullable: false),
                        BatchId = c.Int(),
                        BankAccountId = c.Int(nullable: false),
                        ReconciliationId = c.Int(),
                        IsEnterable = c.Boolean(nullable: false),
                        SendingBankAccountId = c.Int(),
                        PettyCashAccountId = c.Long(nullable: false),
                        BatchInfo = c.String(),
                        IsReversed = c.Boolean(),
                        ReversedByUserId = c.Int(),
                        IsVoid = c.Boolean(),
                        IsVoidDateOriginal = c.Boolean(),
                        LinkedAccountingDocumentId = c.Long(),
                        ReissueBatchId = c.Int(),
                        ReissueVoidDate = c.Int(),
                        DepositTypeOfCategoryId = c.Int(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_CashEntryDocumentUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_CashEntryDocumentUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.AHTID)
                .ForeignKey("dbo.CAPS_AccountingHeaderTransactions", t => t.AHTID)
                .ForeignKey("dbo.CAPS_Batch", t => t.BatchId)
                .ForeignKey("dbo.CAPS_BankAccount", t => t.BankAccountId, cascadeDelete: true)
                .ForeignKey("dbo.CAPS_BankAccount", t => t.SendingBankAccountId)
                .ForeignKey("dbo.CAPS_Accounts", t => t.PettyCashAccountId, cascadeDelete: true)
                .ForeignKey("dbo.CAPS_Batch", t => t.ReissueBatchId)
                .Index(t => t.AHTID)
                .Index(t => t.BatchId)
                .Index(t => t.BankAccountId)
                .Index(t => t.SendingBankAccountId)
                .Index(t => t.PettyCashAccountId)
                .Index(t => t.ReissueBatchId);
            
            CreateTable(
                "dbo.CAPS_ChargeEntryDocumentUnit",
                c => new
                    {
                        AHTID = c.Int(nullable: false),
                        BatchId = c.Int(),
                        VendorId = c.Int(),
                        TypeOfInvoiceId = c.Int(nullable: false),
                        BankAccountId = c.Int(),
                        IsEnterable = c.Boolean(nullable: false),
                        ApInvoiceAccountingDocId = c.Long(),
                        UploadDocumentLogId = c.Int(),
                        IsApInvoiceGenSelected = c.Boolean(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ChargeEntryDocumentUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_ChargeEntryDocumentUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.AHTID)
                .ForeignKey("dbo.CAPS_AccountingHeaderTransactions", t => t.AHTID)
                .ForeignKey("dbo.CAPS_Batch", t => t.BatchId)
                .ForeignKey("dbo.CAPS_Vendors", t => t.VendorId)
                .ForeignKey("dbo.CAPS_BankAccount", t => t.BankAccountId)
                .Index(t => t.AHTID)
                .Index(t => t.BatchId)
                .Index(t => t.VendorId)
                .Index(t => t.BankAccountId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_ChargeEntryDocumentUnit", "BankAccountId", "dbo.CAPS_BankAccount");
            DropForeignKey("dbo.CAPS_ChargeEntryDocumentUnit", "VendorId", "dbo.CAPS_Vendors");
            DropForeignKey("dbo.CAPS_ChargeEntryDocumentUnit", "BatchId", "dbo.CAPS_Batch");
            DropForeignKey("dbo.CAPS_ChargeEntryDocumentUnit", "AHTID", "dbo.CAPS_AccountingHeaderTransactions");
            DropForeignKey("dbo.CAPS_CashEntryDocument", "ReissueBatchId", "dbo.CAPS_Batch");
            DropForeignKey("dbo.CAPS_CashEntryDocument", "PettyCashAccountId", "dbo.CAPS_Accounts");
            DropForeignKey("dbo.CAPS_CashEntryDocument", "SendingBankAccountId", "dbo.CAPS_BankAccount");
            DropForeignKey("dbo.CAPS_CashEntryDocument", "BankAccountId", "dbo.CAPS_BankAccount");
            DropForeignKey("dbo.CAPS_CashEntryDocument", "BatchId", "dbo.CAPS_Batch");
            DropForeignKey("dbo.CAPS_CashEntryDocument", "AHTID", "dbo.CAPS_AccountingHeaderTransactions");
            DropForeignKey("dbo.CAPS_ARInvoiceEntryDocument", "BatchId", "dbo.CAPS_Batch");
            DropForeignKey("dbo.CAPS_ARInvoiceEntryDocument", "AHTID", "dbo.CAPS_AccountingHeaderTransactions");
            DropIndex("dbo.CAPS_ChargeEntryDocumentUnit", new[] { "BankAccountId" });
            DropIndex("dbo.CAPS_ChargeEntryDocumentUnit", new[] { "VendorId" });
            DropIndex("dbo.CAPS_ChargeEntryDocumentUnit", new[] { "BatchId" });
            DropIndex("dbo.CAPS_ChargeEntryDocumentUnit", new[] { "AHTID" });
            DropIndex("dbo.CAPS_CashEntryDocument", new[] { "ReissueBatchId" });
            DropIndex("dbo.CAPS_CashEntryDocument", new[] { "PettyCashAccountId" });
            DropIndex("dbo.CAPS_CashEntryDocument", new[] { "SendingBankAccountId" });
            DropIndex("dbo.CAPS_CashEntryDocument", new[] { "BankAccountId" });
            DropIndex("dbo.CAPS_CashEntryDocument", new[] { "BatchId" });
            DropIndex("dbo.CAPS_CashEntryDocument", new[] { "AHTID" });
            DropIndex("dbo.CAPS_ARInvoiceEntryDocument", new[] { "BatchId" });
            DropIndex("dbo.CAPS_ARInvoiceEntryDocument", new[] { "AHTID" });
            DropTable("dbo.CAPS_ChargeEntryDocumentUnit",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ChargeEntryDocumentUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_ChargeEntryDocumentUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.CAPS_CashEntryDocument",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_CashEntryDocumentUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_CashEntryDocumentUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.CAPS_ARInvoiceEntryDocument",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ArInvoiceEntryDocumentUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_ArInvoiceEntryDocumentUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
