namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_WorkFlow_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_WorkFlow",
                c => new
                    {
                        WorkFlowId = c.Long(nullable: false, identity: true),
                        Description = c.String(maxLength: 400),
                        TypeOfWorkFlowId = c.Int(nullable: false),
                        TypeOfWorkFlowStatusId = c.Int(nullable: false),
                        WorkFlowDocument = c.String(storeType: "xml"),
                        CompletedByUserId = c.Int(),
                        DateCompleted = c.DateTime(storeType: "smalldatetime"),
                        RetryCount = c.Short(),
                        EntityId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                        IsUrgent = c.Boolean(nullable: false),
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
                    { "DynamicFilter_WorkFlowUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_WorkFlowUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.WorkFlowId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CAPS_WorkFlow",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_WorkFlowUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_WorkFlowUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
