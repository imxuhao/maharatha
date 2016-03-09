namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_BatchReportItem_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_BatchReportItem",
                c => new
                    {
                        BatchReportItemId = c.Int(nullable: false, identity: true),
                        BatchReportId = c.Int(nullable: false),
                        BusinessProcessGroupId = c.Int(nullable: false),
                        UserReportId = c.Long(nullable: false),
                        ReportName = c.String(maxLength: 500),
                        IsActive = c.Boolean(nullable: false),
                        IsSavedView = c.Boolean(),
                        StartDate = c.DateTime(storeType: "smalldatetime"),
                        EndDate = c.DateTime(storeType: "smalldatetime"),
                        CompareSDate = c.DateTime(storeType: "smalldatetime"),
                        CompareEDate = c.DateTime(storeType: "smalldatetime"),
                        ReportStyle = c.Int(),
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
                    { "DynamicFilter_BatchReportItemUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_BatchReportItemUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.BatchReportItemId)
                .ForeignKey("dbo.CAPS_BatchReport", t => t.BatchReportId, cascadeDelete: true)
                .ForeignKey("dbo.CAPS_BusinessProcessGroup", t => t.BusinessProcessGroupId, cascadeDelete: true)
                .Index(t => t.BatchReportId)
                .Index(t => t.BusinessProcessGroupId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_BatchReportItem", "BusinessProcessGroupId", "dbo.CAPS_BusinessProcessGroup");
            DropForeignKey("dbo.CAPS_BatchReportItem", "BatchReportId", "dbo.CAPS_BatchReport");
            DropIndex("dbo.CAPS_BatchReportItem", new[] { "BusinessProcessGroupId" });
            DropIndex("dbo.CAPS_BatchReportItem", new[] { "BatchReportId" });
            DropTable("dbo.CAPS_BatchReportItem",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_BatchReportItemUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_BatchReportItemUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
