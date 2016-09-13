namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_LocationSet_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_LocationSet",
                c => new
                    {
                        LocationSetId = c.Int(nullable: false, identity: true),
                        TypeOfLocationSetId = c.Int(nullable: false),
                        Number = c.String(maxLength: 100),
                        Description = c.String(maxLength: 500),
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
                    { "DynamicFilter_LocationSetUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_LocationSetUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.LocationSetId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CAPS_LocationSet",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_LocationSetUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_LocationSetUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
