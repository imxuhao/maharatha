namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_GroupItemRange_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_GroupItemRange",
                c => new
                    {
                        GroupItemRangeId = c.Int(nullable: false, identity: true),
                        GroupItemId = c.Int(nullable: false),
                        StartingRange = c.String(maxLength: 40),
                        EndingRange = c.String(maxLength: 40),
                        IsNegative = c.Boolean(nullable: false),
                        DivisionName = c.String(),
                        SelectDivisionJobId = c.Int(),
                        SelectControlCenterId = c.Int(),
                        SelectStartRange = c.String(),
                        SelectEndRange = c.String(),
                        TypeOfAccountId = c.Int(),
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
                    { "DynamicFilter_GroupItemRangeUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_GroupItemRangeUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.GroupItemRangeId)
                .ForeignKey("dbo.CAPS_GroupItem", t => t.GroupItemId, cascadeDelete: true)
                .Index(t => t.GroupItemId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_GroupItemRange", "GroupItemId", "dbo.CAPS_GroupItem");
            DropIndex("dbo.CAPS_GroupItemRange", new[] { "GroupItemId" });
            DropTable("dbo.CAPS_GroupItemRange",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_GroupItemRangeUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_GroupItemRangeUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
