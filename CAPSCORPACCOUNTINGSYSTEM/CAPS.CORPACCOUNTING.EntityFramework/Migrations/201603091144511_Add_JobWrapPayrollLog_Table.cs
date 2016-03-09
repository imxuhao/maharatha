namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_JobWrapPayrollLog_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_JobWrapPayrollLog",
                c => new
                    {
                        JobWrapPayrollLogId = c.Int(nullable: false, identity: true),
                        JobWrapDocumentLogId = c.Int(),
                        JobBudgetId = c.Int(nullable: false),
                        AccountId = c.Long(),
                        LineInfo = c.String(),
                        Payee = c.String(),
                        PoNumber = c.String(),
                        TaxRate = c.Decimal(storeType: "money"),
                        OTbase = c.Decimal(storeType: "money"),
                        Days = c.Decimal(storeType: "money"),
                        Rate = c.Decimal(storeType: "money"),
                        Ot1Hours = c.Decimal(storeType: "money"),
                        Ot2Hours = c.Decimal(storeType: "money"),
                        Ot3Hours = c.Decimal(storeType: "money"),
                        OtherTaxable = c.Decimal(precision: 18, scale: 2),
                        OtherNonTaxable = c.Decimal(precision: 18, scale: 2),
                        TotalSt = c.Decimal(precision: 18, scale: 2),
                        TotalOt = c.Decimal(precision: 18, scale: 2),
                        TotalPay = c.Decimal(precision: 18, scale: 2),
                        Description = c.String(maxLength: 200),
                        TotalTax = c.Decimal(precision: 18, scale: 2),
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
                    { "DynamicFilter_JobWrapPayrollLogUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_JobWrapPayrollLogUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.JobWrapPayrollLogId)
                .ForeignKey("dbo.CAPS_Account", t => t.AccountId)
                .ForeignKey("dbo.CAPS_JobBudget", t => t.JobBudgetId, cascadeDelete: true)
                .Index(t => t.JobBudgetId)
                .Index(t => t.AccountId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_JobWrapPayrollLog", "JobBudgetId", "dbo.CAPS_JobBudget");
            DropForeignKey("dbo.CAPS_JobWrapPayrollLog", "AccountId", "dbo.CAPS_Account");
            DropIndex("dbo.CAPS_JobWrapPayrollLog", new[] { "AccountId" });
            DropIndex("dbo.CAPS_JobWrapPayrollLog", new[] { "JobBudgetId" });
            DropTable("dbo.CAPS_JobWrapPayrollLog",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_JobWrapPayrollLogUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_JobWrapPayrollLogUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
