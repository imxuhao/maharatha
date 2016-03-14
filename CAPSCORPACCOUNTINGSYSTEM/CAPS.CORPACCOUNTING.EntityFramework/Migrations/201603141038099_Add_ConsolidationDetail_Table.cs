namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_ConsolidationDetail_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_ConsolidationDetail",
                c => new
                    {
                        ConsolidationDetailId = c.Int(nullable: false, identity: true),
                        ConsolidationGroupId = c.Int(nullable: false),
                        Description = c.String(),
                        Caption = c.String(),
                        DisplaySequence = c.Int(),
                        LinkCompanyId = c.Int(),
                        LinkCostCenterId = c.Int(),
                        LinkJobId = c.Int(),
                        LinkSubAccountId1 = c.Int(),
                        LinkSubAccountId2 = c.Int(),
                        LinkSubAccountId3 = c.Int(),
                        LinkSubAccountId4 = c.Int(),
                        LinkSubAccountId5 = c.Int(),
                        LinkSubAccountId6 = c.Int(),
                        LinkSubAccountId7 = c.Int(),
                        LinkSubAccountId8 = c.Int(),
                        LinkSubAccountId9 = c.Int(),
                        LinkSubAccountId10 = c.Int(),
                        IsActive = c.Boolean(nullable: false),
                        TypeOfInactiveStatusId = c.Int(),
                        TypeOfAccountId = c.Int(),
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
                    { "DynamicFilter_ConsolidationDetailUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_ConsolidationDetailUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.ConsolidationDetailId)
                .ForeignKey("dbo.CAPS_ConsolidationGroup", t => t.ConsolidationGroupId, cascadeDelete: true)
                .ForeignKey("dbo.CAPS_TypeOfAccount", t => t.TypeOfAccountId)
                .Index(t => t.ConsolidationGroupId)
                .Index(t => t.TypeOfAccountId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_ConsolidationDetail", "TypeOfAccountId", "dbo.CAPS_TypeOfAccount");
            DropForeignKey("dbo.CAPS_ConsolidationDetail", "ConsolidationGroupId", "dbo.CAPS_ConsolidationGroup");
            DropIndex("dbo.CAPS_ConsolidationDetail", new[] { "TypeOfAccountId" });
            DropIndex("dbo.CAPS_ConsolidationDetail", new[] { "ConsolidationGroupId" });
            DropTable("dbo.CAPS_ConsolidationDetail",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ConsolidationDetailUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_ConsolidationDetailUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
