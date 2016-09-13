namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_SalesRegion_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_SalesRegion",
                c => new
                    {
                        SalesRegionId = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 200),
                        SalesRegionAbbreviation = c.String(maxLength: 10),
                        EntityId = c.Int(nullable: false),
                        IsApproved = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        TypeOfInactiveStatusId = c.Int(),
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
                    { "DynamicFilter_SalesRegionUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_SalesRegionUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.SalesRegionId)
                .ForeignKey("dbo.CAPS_Entity", t => t.EntityId, cascadeDelete: true)
                .Index(t => t.EntityId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_SalesRegion", "EntityId", "dbo.CAPS_Entity");
            DropIndex("dbo.CAPS_SalesRegion", new[] { "EntityId" });
            DropTable("dbo.CAPS_SalesRegion",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_SalesRegionUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_SalesRegionUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
