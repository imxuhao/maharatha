namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_BatchExecutionResult_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_BatchExecutionResult",
                c => new
                    {
                        BatchExecutionResultId = c.Int(nullable: false, identity: true),
                        BatchReportId = c.Int(nullable: false),
                        ExecutionDate = c.DateTime(nullable: false, storeType: "smalldatetime"),
                        ExecutedBy = c.Int(nullable: false),
                        IsSuccess = c.Boolean(nullable: false),
                        ExecutionResult = c.String(nullable: false),
                        ReportsPath = c.String(nullable: false),
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
                    { "DynamicFilter_BatchExecutionResultUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_BatchExecutionResultUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.BatchExecutionResultId)
                .ForeignKey("dbo.CAPS_BatchReport", t => t.BatchReportId, cascadeDelete: true)
                .Index(t => t.BatchReportId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_BatchExecutionResult", "BatchReportId", "dbo.CAPS_BatchReport");
            DropIndex("dbo.CAPS_BatchExecutionResult", new[] { "BatchReportId" });
            DropTable("dbo.CAPS_BatchExecutionResult",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_BatchExecutionResultUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_BatchExecutionResultUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
