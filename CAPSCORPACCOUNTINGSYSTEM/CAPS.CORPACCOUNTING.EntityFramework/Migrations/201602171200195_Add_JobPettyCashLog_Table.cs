namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_JobPettyCashLog_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_JobPettyCashLog",
                c => new
                    {
                        JobPettyCashLogId = c.Int(nullable: false, identity: true),
                        JobBudgetId = c.Int(nullable: false),
                        AccountId = c.Long(),
                        LineInfo = c.String(maxLength: 50),
                        Description = c.String(nullable: false, maxLength: 500),
                        Amount = c.Decimal(nullable: false, storeType: "money"),
                        AccountingTransactionId = c.String(),
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
                    { "DynamicFilter_JobPettyCashLogUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_JobPettyCashLogUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.JobPettyCashLogId)
                .ForeignKey("dbo.CAPS_Accounts", t => t.AccountId)
                .ForeignKey("dbo.CAPS_JobBudget", t => t.JobBudgetId, cascadeDelete: true)
                .Index(t => t.JobBudgetId)
                .Index(t => t.AccountId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_JobPettyCashLog", "JobBudgetId", "dbo.CAPS_JobBudget");
            DropForeignKey("dbo.CAPS_JobPettyCashLog", "AccountId", "dbo.CAPS_Accounts");
            DropIndex("dbo.CAPS_JobPettyCashLog", new[] { "AccountId" });
            DropIndex("dbo.CAPS_JobPettyCashLog", new[] { "JobBudgetId" });
            DropTable("dbo.CAPS_JobPettyCashLog",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_JobPettyCashLogUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_JobPettyCashLogUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
