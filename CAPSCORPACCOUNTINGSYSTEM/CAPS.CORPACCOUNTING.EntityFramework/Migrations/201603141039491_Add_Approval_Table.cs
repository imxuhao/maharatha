namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Approval_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_Approval",
                c => new
                    {
                        ApprovalId = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 100),
                        Caption = c.String(maxLength: 20),
                        DisplaySequence = c.Short(),
                        Notes = c.String(),
                        TypeOfApprovalId = c.Int(nullable: false),
                        IsRequestUserSpecific = c.Boolean(nullable: false),
                        IsApprovalUserSpecific = c.Boolean(nullable: false),
                        NumberOfApprovalsRequired = c.Short(),
                        RerouteTimeoutDdhhmm = c.Short(),
                        RerouteApprovalId = c.Int(),
                        IsActive = c.Boolean(),
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
                    { "DynamicFilter_ApprovalUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_ApprovalUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.ApprovalId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CAPS_Approval",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ApprovalUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_ApprovalUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
