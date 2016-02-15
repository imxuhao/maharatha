namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_LinkJobs_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_JobLinks",
                c => new
                    {
                        JobLinkId = c.Int(nullable: false, identity: true),
                        JobId = c.Int(nullable: false),
                        LinkJobId = c.Int(nullable: false),
                        LinkCompanyId = c.Long(nullable: false),
                        IsApproved = c.Boolean(nullable: false,defaultValue:false),
                        IsActive = c.Boolean(nullable: false,defaultValue:true),
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
                    { "DynamicFilter_LinkJobsUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_LinkJobsUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.JobLinkId)
                .ForeignKey("dbo.CAPS_Job", t => t.JobId, cascadeDelete: true)
                .ForeignKey("dbo.CAPS_Job", t => t.LinkJobId)
                .ForeignKey("dbo.AbpOrganizationUnits", t => t.LinkCompanyId, cascadeDelete: true)
                .Index(t => t.JobId)
                .Index(t => t.LinkJobId)
                .Index(t => t.LinkCompanyId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_JobLinks", "LinkCompanyId", "dbo.AbpOrganizationUnits");
            DropForeignKey("dbo.CAPS_JobLinks", "LinkJobId", "dbo.CAPS_Job");
            DropForeignKey("dbo.CAPS_JobLinks", "JobId", "dbo.CAPS_Job");
            DropIndex("dbo.CAPS_JobLinks", new[] { "LinkCompanyId" });
            DropIndex("dbo.CAPS_JobLinks", new[] { "LinkJobId" });
            DropIndex("dbo.CAPS_JobLinks", new[] { "JobId" });
            DropTable("dbo.CAPS_JobLinks",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_LinkJobsUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_LinkJobsUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
