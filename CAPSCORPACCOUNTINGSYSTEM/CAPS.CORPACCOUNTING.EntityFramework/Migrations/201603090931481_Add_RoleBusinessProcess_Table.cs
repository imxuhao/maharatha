namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_RoleBusinessProcess_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_RoleBusinessProcess",
                c => new
                    {
                        RoleBusinessProcessId = c.Int(nullable: false, identity: true),
                        RoleId = c.Int(nullable: false),
                        BusinessProcessGroupId = c.Int(nullable: false),
                        RerouteBusinessProcessGroupId = c.Int(),
                        DashboardMenuSequence = c.Int(),
                        IsOptionalProcessRequired = c.Boolean(nullable: false),
                        IsNotificationRequired = c.Boolean(nullable: false),
                        TimeOutPeriodBeforeRoleBroadCastDayHourMin = c.Int(),
                        IsApprovalRequired = c.Boolean(nullable: false),
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
                    { "DynamicFilter_RoleBusinessProcessUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_RoleBusinessProcessUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.RoleBusinessProcessId)
                .ForeignKey("dbo.CAPS_BusinessProcessGroup", t => t.BusinessProcessGroupId, cascadeDelete: true)
                .Index(t => t.BusinessProcessGroupId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_RoleBusinessProcess", "BusinessProcessGroupId", "dbo.CAPS_BusinessProcessGroup");
            DropIndex("dbo.CAPS_RoleBusinessProcess", new[] { "BusinessProcessGroupId" });
            DropTable("dbo.CAPS_RoleBusinessProcess",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_RoleBusinessProcessUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_RoleBusinessProcessUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
