namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_JobType_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_JobType",
                c => new
                    {
                        JobTypeId = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 100),
                        Caption = c.String(maxLength: 20),
                        DisplaySequence = c.Short(),
                        DefaultChartOfAccountId = c.Int(),
                        TypeOfJobId = c.Short(),
                        TypeOfFormatMaskId = c.Short(),
                        TypeOfHeadingId = c.Int(),
                        BudgetTypeOfAccountingLayoutId = c.Int(),
                        EntityId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        TypeOfInactiveStatusId = c.Int(),
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
                    { "DynamicFilter_JobTypeUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_JobTypeUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.JobTypeId)
                .ForeignKey("dbo.CAPS_AccountingLayout", t => t.BudgetTypeOfAccountingLayoutId)
                .ForeignKey("dbo.CAPS_Entity", t => t.EntityId, cascadeDelete: true)
                .ForeignKey("dbo.CAPS_TypeOfFormatMask", t => t.TypeOfFormatMaskId)
                .Index(t => t.TypeOfFormatMaskId)
                .Index(t => t.BudgetTypeOfAccountingLayoutId)
                .Index(t => t.EntityId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_JobType", "TypeOfFormatMaskId", "dbo.CAPS_TypeOfFormatMask");
            DropForeignKey("dbo.CAPS_JobType", "EntityId", "dbo.CAPS_Entity");
            DropForeignKey("dbo.CAPS_JobType", "BudgetTypeOfAccountingLayoutId", "dbo.CAPS_AccountingLayout");
            DropIndex("dbo.CAPS_JobType", new[] { "EntityId" });
            DropIndex("dbo.CAPS_JobType", new[] { "BudgetTypeOfAccountingLayoutId" });
            DropIndex("dbo.CAPS_JobType", new[] { "TypeOfFormatMaskId" });
            DropTable("dbo.CAPS_JobType",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_JobTypeUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_JobTypeUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
