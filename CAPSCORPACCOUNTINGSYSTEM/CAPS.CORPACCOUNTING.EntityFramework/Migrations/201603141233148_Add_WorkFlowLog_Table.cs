namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_WorkFlowLog_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_WorkFlowLog",
                c => new
                    {
                        WorkFlowLogId = c.Long(nullable: false, identity: true),
                        WorkFlowId = c.Long(nullable: false),
                        WorkFlowStepId = c.Int(),
                        TypeOfWorkFlowStatusId = c.Short(nullable: false),
                        WorkFlowMessageId = c.Int(nullable: false),
                        ReferenceInformation = c.String(),
                        LogDateTime = c.DateTime(nullable: false),
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
                    { "DynamicFilter_WorkFlowLogUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_WorkFlowLogUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.WorkFlowLogId)
                .ForeignKey("dbo.CAPS_WorkFlow", t => t.WorkFlowId, cascadeDelete: true)
                .Index(t => t.WorkFlowId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_WorkFlowLog", "WorkFlowId", "dbo.CAPS_WorkFlow");
            DropIndex("dbo.CAPS_WorkFlowLog", new[] { "WorkFlowId" });
            DropTable("dbo.CAPS_WorkFlowLog",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_WorkFlowLogUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_WorkFlowLogUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
