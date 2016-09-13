namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_ReportDistributionList_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_ReportDistributionList",
                c => new
                    {
                        ReportDistributionListId = c.Int(nullable: false, identity: true),
                        ReportDistributionId = c.Int(),
                        TypeOfDistributionId = c.Int(),
                        RoleId = c.Int(),
                        UserId = c.Int(),
                        IsNotified = c.Boolean(),
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
                    { "DynamicFilter_ReportDistributionListUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_ReportDistributionListUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.ReportDistributionListId)
                .ForeignKey("dbo.CAPS_ReportDistribution", t => t.ReportDistributionId)
                .Index(t => t.ReportDistributionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_ReportDistributionList", "ReportDistributionId", "dbo.CAPS_ReportDistribution");
            DropIndex("dbo.CAPS_ReportDistributionList", new[] { "ReportDistributionId" });
            DropTable("dbo.CAPS_ReportDistributionList",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ReportDistributionListUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_ReportDistributionListUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
