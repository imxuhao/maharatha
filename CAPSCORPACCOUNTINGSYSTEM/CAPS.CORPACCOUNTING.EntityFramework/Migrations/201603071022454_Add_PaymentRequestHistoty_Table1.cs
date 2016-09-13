namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_PaymentRequestHistoty_Table1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_PaymentEntryDocument",
                c => new
                    {
                        AHTID = c.Int(nullable: false),
                        BankRecControl_Id = c.Int(),
                        PaymentRequestId = c.Int(),
                        PayToName = c.String(maxLength: 200),
                        VendorId = c.Int(),
                        BatchId = c.Int(),
                        PettyCashAccountId = c.Long(),
                        BankAccountId = c.Int(),
                        TypeOfPaymentMethodId = c.Int(nullable: false),
                        PaymentNumber = c.String(maxLength: 200),
                        PaymentAmount = c.Decimal(precision: 18, scale: 2),
                        PaymentDate = c.DateTime(),
                        PurchaseOrderReference = c.String(maxLength: 400),
                        IsCheckPrintRequired = c.Boolean(nullable: false),
                        IsRegisterPrinted = c.Boolean(nullable: false),
                        IsReversed = c.Boolean(nullable: false),
                        ReversedByUserId = c.Int(),
                        ReversalDate = c.DateTime(),
                        IsVoid = c.Boolean(nullable: false),
                        IsVoidDateOriginal = c.Boolean(nullable: false),
                        LinkedAccountingDocumentId = c.Long(),
                        ReconciliationId = c.Int(),
                        ReissueBatchId = c.Int(),
                        ReissueVoidDate = c.DateTime(),
                        IsEnterable = c.Boolean(nullable: false),
                        UploadDocumentLogId = c.Long(),
                        ArAccountingDocId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_PaymentEntryDocumentUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_PaymentEntryDocumentUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.AHTID)
                .ForeignKey("dbo.CAPS_AccountingHeaderTransactions", t => t.AHTID)
                .ForeignKey("dbo.CAPS_BankRecControl", t => t.BankRecControl_Id)
                .ForeignKey("dbo.CAPS_PaymentRequestHistory", t => t.PaymentRequestId)
                .ForeignKey("dbo.CAPS_Vendors", t => t.VendorId)
                .ForeignKey("dbo.CAPS_Batch", t => t.BatchId)
                .ForeignKey("dbo.CAPS_PettyCashAccount", t => t.PettyCashAccountId)
                .ForeignKey("dbo.CAPS_BankAccount", t => t.BankAccountId)
                .ForeignKey("dbo.CAPS_UploadDocumentLog", t => t.UploadDocumentLogId)
                .Index(t => t.AHTID)
                .Index(t => t.BankRecControl_Id)
                .Index(t => t.PaymentRequestId)
                .Index(t => t.VendorId)
                .Index(t => t.BatchId)
                .Index(t => t.PettyCashAccountId)
                .Index(t => t.BankAccountId)
                .Index(t => t.UploadDocumentLogId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_PaymentEntryDocument", "UploadDocumentLogId", "dbo.CAPS_UploadDocumentLog");
            DropForeignKey("dbo.CAPS_PaymentEntryDocument", "BankAccountId", "dbo.CAPS_BankAccount");
            DropForeignKey("dbo.CAPS_PaymentEntryDocument", "PettyCashAccountId", "dbo.CAPS_PettyCashAccount");
            DropForeignKey("dbo.CAPS_PaymentEntryDocument", "BatchId", "dbo.CAPS_Batch");
            DropForeignKey("dbo.CAPS_PaymentEntryDocument", "VendorId", "dbo.CAPS_Vendors");
            DropForeignKey("dbo.CAPS_PaymentEntryDocument", "PaymentRequestId", "dbo.CAPS_PaymentRequestHistory");
            DropForeignKey("dbo.CAPS_PaymentEntryDocument", "BankRecControl_Id", "dbo.CAPS_BankRecControl");
            DropForeignKey("dbo.CAPS_PaymentEntryDocument", "AHTID", "dbo.CAPS_AccountingHeaderTransactions");
            DropIndex("dbo.CAPS_PaymentEntryDocument", new[] { "UploadDocumentLogId" });
            DropIndex("dbo.CAPS_PaymentEntryDocument", new[] { "BankAccountId" });
            DropIndex("dbo.CAPS_PaymentEntryDocument", new[] { "PettyCashAccountId" });
            DropIndex("dbo.CAPS_PaymentEntryDocument", new[] { "BatchId" });
            DropIndex("dbo.CAPS_PaymentEntryDocument", new[] { "VendorId" });
            DropIndex("dbo.CAPS_PaymentEntryDocument", new[] { "PaymentRequestId" });
            DropIndex("dbo.CAPS_PaymentEntryDocument", new[] { "BankRecControl_Id" });
            DropIndex("dbo.CAPS_PaymentEntryDocument", new[] { "AHTID" });
            DropTable("dbo.CAPS_PaymentEntryDocument",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_PaymentEntryDocumentUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_PaymentEntryDocumentUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
