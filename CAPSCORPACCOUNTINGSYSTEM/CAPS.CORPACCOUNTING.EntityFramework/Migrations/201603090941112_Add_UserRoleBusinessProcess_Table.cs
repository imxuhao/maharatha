namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_UserRoleBusinessProcess_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_UserRoleBusinessProcess",
                c => new
                    {
                        UserRoleId = c.Int(nullable: false, identity: true),
                        BusinessProcessGroupId = c.Int(nullable: false),
                        RerouteBusinessProcessGroupId = c.Int(),
                        DashBoardMenuSequence = c.Int(),
                        IsBusinessProcessDenied = c.Boolean(nullable: false),
                        IsOptionalProcessRequired = c.Boolean(nullable: false),
                        IsNotificationRequired = c.Boolean(nullable: false),
                        IsApprovalRequired = c.Boolean(nullable: false),
                        TimeOutPeriodBeforeRoleBroadCastDayHourMin = c.Int(),
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
                    { "DynamicFilter_UserRoleBusinessProcessUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_UserRoleBusinessProcessUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.UserRoleId)
                .ForeignKey("dbo.CAPS_BusinessProcessGroup", t => t.BusinessProcessGroupId, cascadeDelete: true)
                .Index(t => t.BusinessProcessGroupId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_UserRoleBusinessProcess", "BusinessProcessGroupId", "dbo.CAPS_BusinessProcessGroup");
            DropIndex("dbo.CAPS_UserRoleBusinessProcess", new[] { "BusinessProcessGroupId" });
            DropTable("dbo.CAPS_UserRoleBusinessProcess",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_UserRoleBusinessProcessUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_UserRoleBusinessProcessUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
