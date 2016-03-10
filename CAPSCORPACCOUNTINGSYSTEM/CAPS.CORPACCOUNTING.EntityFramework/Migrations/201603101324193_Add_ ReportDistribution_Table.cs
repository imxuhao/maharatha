namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_ReportDistribution_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_ReportDistribution",
                c => new
                    {
                        ReportDistributionId = c.Int(nullable: false, identity: true),
                        TypeOfReportDistributionId = c.Int(nullable: false),
                        DisplaySequence = c.Short(),
                        EntityId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
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
                    { "DynamicFilter_ReportDistributionUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_ReportDistributionUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.ReportDistributionId)
                .ForeignKey("dbo.CAPS_Entity", t => t.EntityId, cascadeDelete: true)
                .Index(t => t.EntityId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_ReportDistribution", "EntityId", "dbo.CAPS_Entity");
            DropIndex("dbo.CAPS_ReportDistribution", new[] { "EntityId" });
            DropTable("dbo.CAPS_ReportDistribution",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ReportDistributionUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_ReportDistributionUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
