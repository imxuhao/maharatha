namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Report_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_Report",
                c => new
                    {
                        ReportId = c.Long(nullable: false, identity: true),
                        PageInfo = c.String(maxLength: 100),
                        FormId = c.Int(),
                        ReportIdentification = c.String(nullable: false, maxLength: 100),
                        ReportTitle = c.String(nullable: false, maxLength: 100),
                        TypeOfReportId = c.Int(nullable: false),
                        TypeOfReportCategoryId = c.Int(),
                        ReportDistributionId = c.Int(),
                        ReportParameters = c.String(storeType: "xml"),
                        ReportOrderBy = c.String(maxLength: 100),
                        ReportSort = c.String(maxLength: 100),
                        BpeDocument = c.String(storeType: "xml"),
                        ReportSize = c.Long(),
                        EntityId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
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
                    { "DynamicFilter_ReportUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_ReportUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.ReportId)
                .ForeignKey("dbo.CAPS_Entity", t => t.EntityId, cascadeDelete: true)
                .Index(t => t.EntityId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_Report", "EntityId", "dbo.CAPS_Entity");
            DropIndex("dbo.CAPS_Report", new[] { "EntityId" });
            DropTable("dbo.CAPS_Report",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ReportUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_ReportUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
