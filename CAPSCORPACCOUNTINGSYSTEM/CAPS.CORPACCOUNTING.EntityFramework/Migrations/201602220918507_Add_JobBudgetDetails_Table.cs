namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_JobBudgetDetails_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_JobBudgetDetails",
                c => new
                    {
                        JobBudgetDetailId = c.Long(nullable: false, identity: true),
                        JobBudgetId = c.Int(nullable: false),
                        AccountId = c.Long(nullable: false),
                        SubAccountId = c.Long(),
                        LocationId = c.Int(),
                        SetId = c.Int(),
                        Amount = c.Decimal(storeType: "money"),
                        IsFringeLine = c.Boolean(),
                        OrganizationUnitId = c.Long(),
                        TenantId = c.Int(nullable: false),
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
                    { "DynamicFilter_JobBudgetDetailsUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_JobBudgetDetailsUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.JobBudgetDetailId)
                .ForeignKey("dbo.CAPS_Accounts", t => t.AccountId, cascadeDelete: true)
                .ForeignKey("dbo.CAPS_JobBudget", t => t.JobBudgetId, cascadeDelete: true)
                .ForeignKey("dbo.CAPS_LocationSet", t => t.LocationId)
                .ForeignKey("dbo.CAPS_LocationSet", t => t.SetId)
                .ForeignKey("dbo.CAPS_Accounts", t => t.SubAccountId)
                .Index(t => t.JobBudgetId)
                .Index(t => t.AccountId)
                .Index(t => t.SubAccountId)
                .Index(t => t.LocationId)
                .Index(t => t.SetId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_JobBudgetDetails", "SubAccountId", "dbo.CAPS_Accounts");
            DropForeignKey("dbo.CAPS_JobBudgetDetails", "SetId", "dbo.CAPS_LocationSet");
            DropForeignKey("dbo.CAPS_JobBudgetDetails", "LocationId", "dbo.CAPS_LocationSet");
            DropForeignKey("dbo.CAPS_JobBudgetDetails", "JobBudgetId", "dbo.CAPS_JobBudget");
            DropForeignKey("dbo.CAPS_JobBudgetDetails", "AccountId", "dbo.CAPS_Accounts");
            DropIndex("dbo.CAPS_JobBudgetDetails", new[] { "SetId" });
            DropIndex("dbo.CAPS_JobBudgetDetails", new[] { "LocationId" });
            DropIndex("dbo.CAPS_JobBudgetDetails", new[] { "SubAccountId" });
            DropIndex("dbo.CAPS_JobBudgetDetails", new[] { "AccountId" });
            DropIndex("dbo.CAPS_JobBudgetDetails", new[] { "JobBudgetId" });
            DropTable("dbo.CAPS_JobBudgetDetails",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_JobBudgetDetailsUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_JobBudgetDetailsUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
