namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_ConsolidationGroup_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_ConsolidationGroup",
                c => new
                    {
                        ConsolidationGroupId = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        TypeOfCategoryId = c.Short(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        TypeOfInactiveStatusId = c.Int(),
                        IsYtdIncluded = c.Boolean(),
                        IsQuarterly = c.Boolean(),
                        IsEliminationIncluded = c.Boolean(),
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
                    { "DynamicFilter_ConsolidationGroupUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_ConsolidationGroupUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.ConsolidationGroupId)
                .ForeignKey("dbo.CAPS_TypeOfCategory", t => t.TypeOfCategoryId, cascadeDelete: true)
                .Index(t => t.TypeOfCategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_ConsolidationGroup", "TypeOfCategoryId", "dbo.CAPS_TypeOfCategory");
            DropIndex("dbo.CAPS_ConsolidationGroup", new[] { "TypeOfCategoryId" });
            DropTable("dbo.CAPS_ConsolidationGroup",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ConsolidationGroupUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_ConsolidationGroupUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
