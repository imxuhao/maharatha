namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_GridList_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_GridList",
                c => new
                    {
                        GridId = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        TenantId = c.Int(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_GridListUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.GridId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CAPS_GridList",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_GridListUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
