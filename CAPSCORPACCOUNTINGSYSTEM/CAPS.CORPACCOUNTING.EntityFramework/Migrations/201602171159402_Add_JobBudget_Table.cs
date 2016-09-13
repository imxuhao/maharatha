namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_JobBudget_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_JobBudget",
                c => new
                    {
                        JobBudgetId = c.Int(nullable: false, identity: true),
                        JobId = c.Int(),
                        Description = c.String(nullable: false, maxLength: 500),
                        TypeofBudgetId = c.Int(nullable: false),
                        TypeofBudgetSoftwareId = c.Int(nullable: false),
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
                    { "DynamicFilter_JobBudgetUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_JobBudgetUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.JobBudgetId)
                .ForeignKey("dbo.CAPS_Job", t => t.JobId)
                .Index(t => t.JobId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_JobBudget", "JobId", "dbo.CAPS_Job");
            DropIndex("dbo.CAPS_JobBudget", new[] { "JobId" });
            DropTable("dbo.CAPS_JobBudget",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_JobBudgetUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_JobBudgetUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
