namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_JobWrapSalesLog_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_JobWrapSalesLog",
                c => new
                    {
                        JobWrapSalesLogId = c.Long(nullable: false, identity: true),
                        JobWrapDocumentLogId = c.Int(),
                        JobBudgetId = c.Int(nullable: false),
                        AccountId = c.Long(),
                        LineInfo = c.String(maxLength: 50),
                        Payor = c.String(maxLength: 300),
                        Description = c.String(maxLength: 300),
                        Notes = c.String(),
                        ReceiptInfo = c.String(maxLength: 50),
                        Amount = c.Decimal(precision: 18, scale: 2),
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
                    { "DynamicFilter_JobWrapSalesLogUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_JobWrapSalesLogUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.JobWrapSalesLogId)
                .ForeignKey("dbo.CAPS_Account", t => t.AccountId)
                .ForeignKey("dbo.CAPS_JobBudget", t => t.JobBudgetId, cascadeDelete: true)
                .Index(t => t.JobBudgetId)
                .Index(t => t.AccountId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_JobWrapSalesLog", "JobBudgetId", "dbo.CAPS_JobBudget");
            DropForeignKey("dbo.CAPS_JobWrapSalesLog", "AccountId", "dbo.CAPS_Account");
            DropIndex("dbo.CAPS_JobWrapSalesLog", new[] { "AccountId" });
            DropIndex("dbo.CAPS_JobWrapSalesLog", new[] { "JobBudgetId" });
            DropTable("dbo.CAPS_JobWrapSalesLog",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_JobWrapSalesLogUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_JobWrapSalesLogUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
