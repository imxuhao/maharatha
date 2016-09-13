namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Modify_GridList_Table : DbMigration
    {
        public override void Up()
        {
            AlterTableAnnotations(
                "dbo.CAPS_GridList",
                c => new
                    {
                        GridId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 300),
                        Description = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_GridListUnit_MustHaveTenant",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
            AddColumn("dbo.CAPS_GridList", "IsActive", c => c.Boolean(nullable: false));
            AlterColumn("dbo.CAPS_GridList", "Name", c => c.String(nullable: false, maxLength: 300));
            DropColumn("dbo.CAPS_GridList", "TenantId");
            DropColumn("dbo.CAPS_GridList", "CreationTime");
            DropColumn("dbo.CAPS_GridList", "CreatorUserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CAPS_GridList", "CreatorUserId", c => c.Long());
            AddColumn("dbo.CAPS_GridList", "CreationTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.CAPS_GridList", "TenantId", c => c.Int(nullable: false));
            AlterColumn("dbo.CAPS_GridList", "Name", c => c.String(nullable: false));
            DropColumn("dbo.CAPS_GridList", "IsActive");
            AlterTableAnnotations(
                "dbo.CAPS_GridList",
                c => new
                    {
                        GridId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 300),
                        Description = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_GridListUnit_MustHaveTenant",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
        }
    }
}
