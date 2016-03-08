namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_GroupTotal_Table : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.CAPS_TypeOfCategory");
            CreateTable(
                "dbo.CAPS_GroupTotal",
                c => new
                    {
                        GroupTotalId = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false),
                        TypeOfCategoryId = c.Short(nullable: false),
                        TypeOfGroupId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        TypeOfInactiveStatusId = c.Int(),
                        EntityId = c.Long(),
                        ObjectId = c.Long(),
                        LinkChartOfAccountId = c.Int(),
                        IsDefaultFormatUsed = c.Boolean(),
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
                    { "DynamicFilter_GroupTotalUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_GroupTotalUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.GroupTotalId)
                .ForeignKey("dbo.CAPS_ChartOfAccount", t => t.LinkChartOfAccountId)
                .ForeignKey("dbo.CAPS_TypeOfCategory", t => t.TypeOfCategoryId, cascadeDelete: true)
                .Index(t => t.TypeOfCategoryId)
                .Index(t => t.LinkChartOfAccountId);
            
            AlterColumn("dbo.CAPS_TypeOfCategory", "TypeOfCategoryId", c => c.Short(nullable: false, identity: true));
            AddPrimaryKey("dbo.CAPS_TypeOfCategory", "TypeOfCategoryId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_GroupTotal", "TypeOfCategoryId", "dbo.CAPS_TypeOfCategory");
            DropForeignKey("dbo.CAPS_GroupTotal", "LinkChartOfAccountId", "dbo.CAPS_ChartOfAccount");
            DropIndex("dbo.CAPS_GroupTotal", new[] { "LinkChartOfAccountId" });
            DropIndex("dbo.CAPS_GroupTotal", new[] { "TypeOfCategoryId" });
            DropPrimaryKey("dbo.CAPS_TypeOfCategory");
            AlterColumn("dbo.CAPS_TypeOfCategory", "TypeOfCategoryId", c => c.Int(nullable: false, identity: true));
            DropTable("dbo.CAPS_GroupTotal",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_GroupTotalUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_GroupTotalUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            AddPrimaryKey("dbo.CAPS_TypeOfCategory", "TypeOfCategoryId");
        }
    }
}
