namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_PurchaseOrderEntryDocument_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_PurchaseOrderEntryDocument",
                c => new
                    {
                        AHTID = c.Int(nullable: false),
                        VendorId = c.Int(),
                        PaymentTermId = c.Int(),
                        BankAccountId = c.Int(),
                        IsCreditCard = c.Boolean(nullable: false),
                        IsShipping = c.Boolean(nullable: false),
                        IsPrintRequired = c.Boolean(nullable: false),
                        IsEnterable = c.Boolean(nullable: false),
                        IsHistory = c.Boolean(nullable: false),
                        IsRetired = c.Boolean(nullable: false),
                        SourcePoAccountingDocumentId = c.Long(),
                        InvoiceAccountingDocumentId = c.Long(),
                        UploadDocumentLogId = c.Long(),
                        IsWillBill = c.Boolean(),
                        IsAdditionalBill = c.Boolean(),
                        IsPettyCash = c.Boolean(),
                        IsPaymentCheck = c.Boolean(),
                        IsDepositCheck = c.Boolean(),
                        DateNeededBy = c.DateTime(),
                        TimeNeededBy = c.String(),
                        IsPartial = c.Boolean(),
                        IsOverage = c.Boolean(),
                        IsReimbursement = c.Boolean(),
                        IsReinstated = c.Boolean(),
                        ReinstatedPoDocumentId = c.Long(),
                        ControllingBankAccountId = c.Int(),
                        IsApproveEmail = c.Boolean(),
                        PoOriginalAmount = c.Decimal(precision: 18, scale: 2),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_PurchaseOrderEntryDocumentUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_PurchaseOrderEntryDocumentUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.AHTID)
                .ForeignKey("dbo.CAPS_AccountingHeaderTransactions", t => t.AHTID)
                .ForeignKey("dbo.CAPS_Vendors", t => t.VendorId)
                .ForeignKey("dbo.CAPS_VendorPaymentTerms", t => t.PaymentTermId)
                .ForeignKey("dbo.CAPS_BankAccount", t => t.BankAccountId)
                .ForeignKey("dbo.CAPS_UploadDocumentLog", t => t.UploadDocumentLogId)
                .ForeignKey("dbo.CAPS_BankAccount", t => t.ControllingBankAccountId)
                .Index(t => t.AHTID)
                .Index(t => t.VendorId)
                .Index(t => t.PaymentTermId)
                .Index(t => t.BankAccountId)
                .Index(t => t.UploadDocumentLogId)
                .Index(t => t.ControllingBankAccountId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_PurchaseOrderEntryDocument", "ControllingBankAccountId", "dbo.CAPS_BankAccount");
            DropForeignKey("dbo.CAPS_PurchaseOrderEntryDocument", "UploadDocumentLogId", "dbo.CAPS_UploadDocumentLog");
            DropForeignKey("dbo.CAPS_PurchaseOrderEntryDocument", "BankAccountId", "dbo.CAPS_BankAccount");
            DropForeignKey("dbo.CAPS_PurchaseOrderEntryDocument", "PaymentTermId", "dbo.CAPS_VendorPaymentTerms");
            DropForeignKey("dbo.CAPS_PurchaseOrderEntryDocument", "VendorId", "dbo.CAPS_Vendors");
            DropForeignKey("dbo.CAPS_PurchaseOrderEntryDocument", "AHTID", "dbo.CAPS_AccountingHeaderTransactions");
            DropIndex("dbo.CAPS_PurchaseOrderEntryDocument", new[] { "ControllingBankAccountId" });
            DropIndex("dbo.CAPS_PurchaseOrderEntryDocument", new[] { "UploadDocumentLogId" });
            DropIndex("dbo.CAPS_PurchaseOrderEntryDocument", new[] { "BankAccountId" });
            DropIndex("dbo.CAPS_PurchaseOrderEntryDocument", new[] { "PaymentTermId" });
            DropIndex("dbo.CAPS_PurchaseOrderEntryDocument", new[] { "VendorId" });
            DropIndex("dbo.CAPS_PurchaseOrderEntryDocument", new[] { "AHTID" });
            DropTable("dbo.CAPS_PurchaseOrderEntryDocument",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_PurchaseOrderEntryDocumentUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_PurchaseOrderEntryDocumentUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
