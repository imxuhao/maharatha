namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_CustomerGroup_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_CustomerGroup",
                c => new
                    {
                        CustomerGroupId = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 50),
                        EntityId = c.Int(nullable: false),
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
                    { "DynamicFilter_CustomerGroupUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_CustomerGroupUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.CustomerGroupId)
                .ForeignKey("dbo.CAPS_Entity", t => t.EntityId, cascadeDelete: true)
                .Index(t => t.EntityId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_CustomerGroup", "EntityId", "dbo.CAPS_Entity");
            DropIndex("dbo.CAPS_CustomerGroup", new[] { "EntityId" });
            DropTable("dbo.CAPS_CustomerGroup",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_CustomerGroupUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_CustomerGroupUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
