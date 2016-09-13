namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_NotedObject_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_NotedObject",
                c => new
                    {
                        NotedObjectId = c.Long(nullable: false, identity: true),
                        TypeOfObjectId = c.Int(nullable: false),
                        ObjectId = c.Long(nullable: false),
                        Notes = c.String(),
                        IsSharedUpdate = c.Boolean(nullable: false),
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
                    { "DynamicFilter_NotedObjectUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_NotedObjectUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.NotedObjectId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CAPS_NotedObject",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_NotedObjectUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_NotedObjectUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
