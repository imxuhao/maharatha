namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_ChargeEntryDocument_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_ChargeEntryDocument",
                c => new
                    {
                        AHTID = c.Int(nullable: false),
                        UploadDocumentLog_Id = c.Long(),
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
                .ForeignKey("dbo.CAPS_UploadDocumentLog", t => t.UploadDocumentLog_Id)
                .ForeignKey("dbo.CAPS_Batch", t => t.BatchId)
                .ForeignKey("dbo.CAPS_Vendors", t => t.VendorId)
                .ForeignKey("dbo.CAPS_BankAccount", t => t.BankAccountId)
                .Index(t => t.AHTID)
                .Index(t => t.UploadDocumentLog_Id)
                .Index(t => t.BatchId)
                .Index(t => t.VendorId)
                .Index(t => t.BankAccountId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_ChargeEntryDocument", "BankAccountId", "dbo.CAPS_BankAccount");
            DropForeignKey("dbo.CAPS_ChargeEntryDocument", "VendorId", "dbo.CAPS_Vendors");
            DropForeignKey("dbo.CAPS_ChargeEntryDocument", "BatchId", "dbo.CAPS_Batch");
            DropForeignKey("dbo.CAPS_ChargeEntryDocument", "UploadDocumentLog_Id", "dbo.CAPS_UploadDocumentLog");
            DropForeignKey("dbo.CAPS_ChargeEntryDocument", "AHTID", "dbo.CAPS_AccountingHeaderTransactions");
            DropIndex("dbo.CAPS_ChargeEntryDocument", new[] { "BankAccountId" });
            DropIndex("dbo.CAPS_ChargeEntryDocument", new[] { "VendorId" });
            DropIndex("dbo.CAPS_ChargeEntryDocument", new[] { "BatchId" });
            DropIndex("dbo.CAPS_ChargeEntryDocument", new[] { "UploadDocumentLog_Id" });
            DropIndex("dbo.CAPS_ChargeEntryDocument", new[] { "AHTID" });
            DropTable("dbo.CAPS_ChargeEntryDocument",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ChargeEntryDocumentUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_ChargeEntryDocumentUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
