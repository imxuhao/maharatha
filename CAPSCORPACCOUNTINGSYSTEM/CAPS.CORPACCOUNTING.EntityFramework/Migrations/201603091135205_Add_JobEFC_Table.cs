namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_JobEFC_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_JobEFC",
                c => new
                    {
                        JobEfcId = c.Int(nullable: false, identity: true),
                        JobBudgetId = c.Int(nullable: false),
                        AccountId = c.Long(nullable: false),
                        SubAccountId = c.Long(),
                        EfcAdjustment = c.Decimal(precision: 18, scale: 2),
                        ProjectControlPeriodId = c.Int(),
                        Comment = c.String(),
                        SubAccountId1 = c.Long(),
                        SubAccountId2 = c.Long(),
                        SubAccountId3 = c.Long(),
                        SubAccountId4 = c.Long(),
                        SubAccountId5 = c.Long(),
                        SubAccountId6 = c.Long(),
                        SubAccountId7 = c.Long(),
                        SubAccountId8 = c.Long(),
                        SubAccountId9 = c.Long(),
                        SubAccountId10 = c.Long(),
                        BudgetAdjustment = c.Decimal(precision: 18, scale: 2),
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
                    { "DynamicFilter_JobEFCUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_JobEFCUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.JobEfcId)
                .ForeignKey("dbo.CAPS_Account", t => t.AccountId, cascadeDelete: true)
                .ForeignKey("dbo.CAPS_JobBudget", t => t.JobBudgetId, cascadeDelete: true)
                .ForeignKey("dbo.CAPS_ProjectControlPeriod", t => t.ProjectControlPeriodId)
                .ForeignKey("dbo.CAPS_SubAccount", t => t.SubAccountId)
                .ForeignKey("dbo.CAPS_SubAccount", t => t.SubAccountId1)
                .ForeignKey("dbo.CAPS_SubAccount", t => t.SubAccountId10)
                .ForeignKey("dbo.CAPS_SubAccount", t => t.SubAccountId2)
                .ForeignKey("dbo.CAPS_SubAccount", t => t.SubAccountId3)
                .ForeignKey("dbo.CAPS_SubAccount", t => t.SubAccountId4)
                .ForeignKey("dbo.CAPS_SubAccount", t => t.SubAccountId5)
                .ForeignKey("dbo.CAPS_SubAccount", t => t.SubAccountId6)
                .ForeignKey("dbo.CAPS_SubAccount", t => t.SubAccountId7)
                .ForeignKey("dbo.CAPS_SubAccount", t => t.SubAccountId8)
                .ForeignKey("dbo.CAPS_SubAccount", t => t.SubAccountId9)
                .Index(t => t.JobBudgetId)
                .Index(t => t.AccountId)
                .Index(t => t.SubAccountId)
                .Index(t => t.ProjectControlPeriodId)
                .Index(t => t.SubAccountId1)
                .Index(t => t.SubAccountId2)
                .Index(t => t.SubAccountId3)
                .Index(t => t.SubAccountId4)
                .Index(t => t.SubAccountId5)
                .Index(t => t.SubAccountId6)
                .Index(t => t.SubAccountId7)
                .Index(t => t.SubAccountId8)
                .Index(t => t.SubAccountId9)
                .Index(t => t.SubAccountId10);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_JobEFC", "SubAccountId9", "dbo.CAPS_SubAccount");
            DropForeignKey("dbo.CAPS_JobEFC", "SubAccountId8", "dbo.CAPS_SubAccount");
            DropForeignKey("dbo.CAPS_JobEFC", "SubAccountId7", "dbo.CAPS_SubAccount");
            DropForeignKey("dbo.CAPS_JobEFC", "SubAccountId6", "dbo.CAPS_SubAccount");
            DropForeignKey("dbo.CAPS_JobEFC", "SubAccountId5", "dbo.CAPS_SubAccount");
            DropForeignKey("dbo.CAPS_JobEFC", "SubAccountId4", "dbo.CAPS_SubAccount");
            DropForeignKey("dbo.CAPS_JobEFC", "SubAccountId3", "dbo.CAPS_SubAccount");
            DropForeignKey("dbo.CAPS_JobEFC", "SubAccountId2", "dbo.CAPS_SubAccount");
            DropForeignKey("dbo.CAPS_JobEFC", "SubAccountId10", "dbo.CAPS_SubAccount");
            DropForeignKey("dbo.CAPS_JobEFC", "SubAccountId1", "dbo.CAPS_SubAccount");
            DropForeignKey("dbo.CAPS_JobEFC", "SubAccountId", "dbo.CAPS_SubAccount");
            DropForeignKey("dbo.CAPS_JobEFC", "ProjectControlPeriodId", "dbo.CAPS_ProjectControlPeriod");
            DropForeignKey("dbo.CAPS_JobEFC", "JobBudgetId", "dbo.CAPS_JobBudget");
            DropForeignKey("dbo.CAPS_JobEFC", "AccountId", "dbo.CAPS_Account");
            DropIndex("dbo.CAPS_JobEFC", new[] { "SubAccountId10" });
            DropIndex("dbo.CAPS_JobEFC", new[] { "SubAccountId9" });
            DropIndex("dbo.CAPS_JobEFC", new[] { "SubAccountId8" });
            DropIndex("dbo.CAPS_JobEFC", new[] { "SubAccountId7" });
            DropIndex("dbo.CAPS_JobEFC", new[] { "SubAccountId6" });
            DropIndex("dbo.CAPS_JobEFC", new[] { "SubAccountId5" });
            DropIndex("dbo.CAPS_JobEFC", new[] { "SubAccountId4" });
            DropIndex("dbo.CAPS_JobEFC", new[] { "SubAccountId3" });
            DropIndex("dbo.CAPS_JobEFC", new[] { "SubAccountId2" });
            DropIndex("dbo.CAPS_JobEFC", new[] { "SubAccountId1" });
            DropIndex("dbo.CAPS_JobEFC", new[] { "ProjectControlPeriodId" });
            DropIndex("dbo.CAPS_JobEFC", new[] { "SubAccountId" });
            DropIndex("dbo.CAPS_JobEFC", new[] { "AccountId" });
            DropIndex("dbo.CAPS_JobEFC", new[] { "JobBudgetId" });
            DropTable("dbo.CAPS_JobEFC",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_JobEFCUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_JobEFCUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
