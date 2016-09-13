namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_JobPORangeAllocationUnit_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_JobPORangeAllocation",
                c => new
                    {
                        PORangeAllocationId = c.Int(nullable: false, identity: true),
                        JobId = c.Int(nullable: false),
                        PoRangeStartNumber = c.String(nullable: false, maxLength: 50),
                        PoRangeEndNumber = c.String(nullable: false, maxLength: 50),
                        OrganizationUnitId = c.Long(nullable: false),
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
                    { "DynamicFilter_JobPORangeAllocationUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_JobPORangeAllocationUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.PORangeAllocationId)
                .ForeignKey("dbo.CAPS_Job", t => t.JobId, cascadeDelete: true)
                .Index(t => t.JobId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_JobPORangeAllocation", "JobId", "dbo.CAPS_Job");
            DropIndex("dbo.CAPS_JobPORangeAllocation", new[] { "JobId" });
            DropTable("dbo.CAPS_JobPORangeAllocation",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_JobPORangeAllocationUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_JobPORangeAllocationUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
