namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Modify_UserViewSettingsUnit_Table : DbMigration
    {
        public override void Up()
        {
            AlterTableAnnotations(
                "dbo.CAPS_UserViewSettings",
                c => new
                    {
                        UserViewId = c.Int(nullable: false, identity: true),
                        ViewId = c.Int(nullable: false),
                        UserId = c.Long(nullable: false),
                        ViewName = c.String(nullable: false, maxLength: 300),
                        ViewSettings = c.String(nullable: false),
                        IsDefault = c.Boolean(),
                        TenantId = c.Int(),
                        OrganizationUnitId = c.Long(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_UserViewSettingsUnit_MayHaveTenant",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                    { 
                        "DynamicFilter_UserViewSettingsUnit_MustHaveTenant",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
            AlterColumn("dbo.CAPS_UserViewSettings", "TenantId", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CAPS_UserViewSettings", "TenantId", c => c.Int(nullable: false));
            AlterTableAnnotations(
                "dbo.CAPS_UserViewSettings",
                c => new
                    {
                        UserViewId = c.Int(nullable: false, identity: true),
                        ViewId = c.Int(nullable: false),
                        UserId = c.Long(nullable: false),
                        ViewName = c.String(nullable: false, maxLength: 300),
                        ViewSettings = c.String(nullable: false),
                        IsDefault = c.Boolean(),
                        TenantId = c.Int(),
                        OrganizationUnitId = c.Long(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_UserViewSettingsUnit_MayHaveTenant",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                    { 
                        "DynamicFilter_UserViewSettingsUnit_MustHaveTenant",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
        }
    }
}
