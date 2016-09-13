namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_ARInvoiceEntryDocument_Table : DbMigration
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_ARInvoiceEntryDocument", "BatchId", "dbo.CAPS_Batch");
            DropForeignKey("dbo.CAPS_ARInvoiceEntryDocument", "AHTID", "dbo.CAPS_AccountingHeaderTransactions");
            DropIndex("dbo.CAPS_ARInvoiceEntryDocument", new[] { "BatchId" });
            DropIndex("dbo.CAPS_ARInvoiceEntryDocument", new[] { "AHTID" });
            DropTable("dbo.CAPS_ARInvoiceEntryDocument",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ArInvoiceEntryDocumentUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_ArInvoiceEntryDocumentUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
