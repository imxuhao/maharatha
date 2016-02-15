namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_JobLocation_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_JobLocation",
                c => new
                    {
                        JobLocationId = c.Int(nullable: false, identity: true),
                        JobId = c.Int(nullable: false),
                        LocationSiteDate = c.DateTime(),
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
                    { "DynamicFilter_JobLocationUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_JobLocationUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.JobLocationId)
                .ForeignKey("dbo.CAPS_Job", t => t.JobId, cascadeDelete: true)
                .Index(t => t.JobId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_JobLocation", "JobId", "dbo.CAPS_Job");
            DropIndex("dbo.CAPS_JobLocation", new[] { "JobId" });
            DropTable("dbo.CAPS_JobLocation",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_JobLocationUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_JobLocationUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
