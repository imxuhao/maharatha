namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_JobWrapCheckLog_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_JobWrapCheckLog",
                c => new
                    {
                        JobWrapCheckLogId = c.Int(nullable: false, identity: true),
                        JobWrapDocumentLogId = c.Int(),
                        JobBudgetId = c.Int(nullable: false),
                        AccountId = c.Long(),
                        LineInfo = c.String(maxLength: 50),
                        Payee = c.String(maxLength: 300),
                        Description = c.String(maxLength: 200),
                        CheckInfo = c.String(maxLength: 50),
                        CheckDate = c.DateTime(storeType: "smalldatetime"),
                        PoNumber = c.String(maxLength: 50),
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
                    { "DynamicFilter_JobWrapCheckLogUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_JobWrapCheckLogUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.JobWrapCheckLogId)
                .ForeignKey("dbo.CAPS_Account", t => t.AccountId)
                .ForeignKey("dbo.CAPS_JobBudget", t => t.JobBudgetId, cascadeDelete: true)
                .Index(t => t.JobBudgetId)
                .Index(t => t.AccountId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_JobWrapCheckLog", "JobBudgetId", "dbo.CAPS_JobBudget");
            DropForeignKey("dbo.CAPS_JobWrapCheckLog", "AccountId", "dbo.CAPS_Account");
            DropIndex("dbo.CAPS_JobWrapCheckLog", new[] { "AccountId" });
            DropIndex("dbo.CAPS_JobWrapCheckLog", new[] { "JobBudgetId" });
            DropTable("dbo.CAPS_JobWrapCheckLog",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_JobWrapCheckLogUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_JobWrapCheckLogUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
