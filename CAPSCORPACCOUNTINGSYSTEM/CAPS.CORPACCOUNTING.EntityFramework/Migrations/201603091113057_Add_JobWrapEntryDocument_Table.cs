namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_JobWrapEntryDocument_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_JobWrapEntryDocument",
                c => new
                    {
                        JobWrapDocumentLogId = c.Long(nullable: false, identity: true),
                        JobBudgetId = c.Int(nullable: false),
                        VendorId = c.Int(),
                        PaymentTermId = c.Int(),
                        BankAccountId = c.Long(),
                        BatchId = c.Int(),
                        TypeOfInvoiceId = c.Int(),
                        PaymentDate = c.DateTime(storeType: "smalldatetime"),
                        PaymentNumber = c.String(),
                        PurchaseOrderReference = c.String(),
                        PettyCashAccountId = c.Long(),
                        IsCreditCard = c.Boolean(nullable: false),
                        IsPrintRequired = c.Boolean(nullable: false),
                        IsEnterable = c.Boolean(nullable: false),
                        IsHistory = c.Boolean(nullable: false),
                        IsRetired = c.Boolean(nullable: false),
                        SourcePoAccountingDocumentId = c.Long(),
                        InvoiceAccountingDocumentId = c.Long(),
                        TenantId = c.Int(nullable: false),
                        OrganizationUnitId = c.Long(),
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
                    { "DynamicFilter_JobWrapEntryDocumentUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_JobWrapEntryDocumentUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.JobWrapDocumentLogId)
                .ForeignKey("dbo.CAPS_BankAccount", t => t.BankAccountId)
                .ForeignKey("dbo.CAPS_Batch", t => t.BatchId)
                .ForeignKey("dbo.CAPS_JobBudget", t => t.JobBudgetId, cascadeDelete: true)
                .ForeignKey("dbo.CAPS_PettyCashAccount", t => t.PettyCashAccountId)
                .ForeignKey("dbo.CAPS_Vendor", t => t.VendorId)
                .Index(t => t.JobBudgetId)
                .Index(t => t.VendorId)
                .Index(t => t.BankAccountId)
                .Index(t => t.BatchId)
                .Index(t => t.PettyCashAccountId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_JobWrapEntryDocument", "VendorId", "dbo.CAPS_Vendor");
            DropForeignKey("dbo.CAPS_JobWrapEntryDocument", "PettyCashAccountId", "dbo.CAPS_PettyCashAccount");
            DropForeignKey("dbo.CAPS_JobWrapEntryDocument", "JobBudgetId", "dbo.CAPS_JobBudget");
            DropForeignKey("dbo.CAPS_JobWrapEntryDocument", "BatchId", "dbo.CAPS_Batch");
            DropForeignKey("dbo.CAPS_JobWrapEntryDocument", "BankAccountId", "dbo.CAPS_BankAccount");
            DropIndex("dbo.CAPS_JobWrapEntryDocument", new[] { "PettyCashAccountId" });
            DropIndex("dbo.CAPS_JobWrapEntryDocument", new[] { "BatchId" });
            DropIndex("dbo.CAPS_JobWrapEntryDocument", new[] { "BankAccountId" });
            DropIndex("dbo.CAPS_JobWrapEntryDocument", new[] { "VendorId" });
            DropIndex("dbo.CAPS_JobWrapEntryDocument", new[] { "JobBudgetId" });
            DropTable("dbo.CAPS_JobWrapEntryDocument",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_JobWrapEntryDocumentUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_JobWrapEntryDocumentUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
