namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_JobPurchaseOrderLog_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_JobPurchaseOrderLog",
                c => new
                    {
                        JobPurchaseOrderLogId = c.Int(nullable: false, identity: true),
                        JobBudgetId = c.Int(nullable: false),
                        AccountId = c.Long(),
                        VendorId = c.Int(),
                        LineInfo = c.String(maxLength: 50),
                        Description = c.String(maxLength: 500),
                        Payee = c.String(maxLength: 200),
                        PONumber = c.String(maxLength: 50),
                        Amount = c.Decimal(nullable: false, storeType: "money"),
                        AccountingTransactionId = c.String(maxLength: 50),
                        InvoiceNumber = c.String(maxLength: 50),
                        TransactionAmount = c.Decimal(nullable: false, storeType: "money"),
                        CheckNumber = c.String(maxLength: 50),
                        InvoiceDate = c.DateTime(),
                        CheckDate = c.DateTime(),
                        PostingDate = c.DateTime(),
                        IsReconciled = c.Boolean(nullable: false,defaultValue:false),
                        TransactionTypeId = c.Int(),
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
                    { "DynamicFilter_JobPurchaseOrderLogUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_JobPurchaseOrderLogUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.JobPurchaseOrderLogId)
                .ForeignKey("dbo.CAPS_Accounts", t => t.AccountId)
                .ForeignKey("dbo.CAPS_JobBudget", t => t.JobBudgetId, cascadeDelete: true)
                .ForeignKey("dbo.CAPS_Vendors", t => t.VendorId)
                .Index(t => t.JobBudgetId)
                .Index(t => t.AccountId)
                .Index(t => t.VendorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_JobPurchaseOrderLog", "VendorId", "dbo.CAPS_Vendors");
            DropForeignKey("dbo.CAPS_JobPurchaseOrderLog", "JobBudgetId", "dbo.CAPS_JobBudget");
            DropForeignKey("dbo.CAPS_JobPurchaseOrderLog", "AccountId", "dbo.CAPS_Accounts");
            DropIndex("dbo.CAPS_JobPurchaseOrderLog", new[] { "VendorId" });
            DropIndex("dbo.CAPS_JobPurchaseOrderLog", new[] { "AccountId" });
            DropIndex("dbo.CAPS_JobPurchaseOrderLog", new[] { "JobBudgetId" });
            DropTable("dbo.CAPS_JobPurchaseOrderLog",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_JobPurchaseOrderLogUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_JobPurchaseOrderLogUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
