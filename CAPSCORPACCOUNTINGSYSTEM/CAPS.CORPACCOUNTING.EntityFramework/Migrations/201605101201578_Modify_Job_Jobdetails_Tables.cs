namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Modify_Job_Jobdetails_Tables : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CAPS_Job", "ChartOfAccountId", "dbo.CAPS_ChartOfAccount");
            DropIndex("dbo.CAPS_Job", new[] { "ChartOfAccountId" });
            AlterTableAnnotations(
                "dbo.CAPS_UserViewSettings",
                c => new
                    {
                        UserViewId = c.Int(nullable: false, identity: true),
                        GridId = c.Int(nullable: false),
                        UserId = c.Long(nullable: false),
                        ViewSettingName = c.String(nullable: false, maxLength: 300),
                        ViewSettings = c.String(nullable: false),
                        IsDefault = c.Boolean(),
                        TenantId = c.Int(),
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
            
            AddColumn("dbo.CAPS_JobCommercial", "LocationNames", c => c.String());
            AlterColumn("dbo.CAPS_Job", "ChartOfAccountId", c => c.Int(nullable: false));
            AlterColumn("dbo.CAPS_UserViewSettings", "TenantId", c => c.Int());
            CreateIndex("dbo.CAPS_Job", "ChartOfAccountId");
            AddForeignKey("dbo.CAPS_Job", "ChartOfAccountId", "dbo.CAPS_ChartOfAccount", "ChartOfAccountId", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_Job", "ChartOfAccountId", "dbo.CAPS_ChartOfAccount");
            DropIndex("dbo.CAPS_Job", new[] { "ChartOfAccountId" });
            AlterColumn("dbo.CAPS_UserViewSettings", "TenantId", c => c.Int(nullable: false));
            AlterColumn("dbo.CAPS_Job", "ChartOfAccountId", c => c.Int());
            DropColumn("dbo.CAPS_JobCommercial", "LocationNames");
            AlterTableAnnotations(
                "dbo.CAPS_UserViewSettings",
                c => new
                    {
                        UserViewId = c.Int(nullable: false, identity: true),
                        GridId = c.Int(nullable: false),
                        UserId = c.Long(nullable: false),
                        ViewSettingName = c.String(nullable: false, maxLength: 300),
                        ViewSettings = c.String(nullable: false),
                        IsDefault = c.Boolean(),
                        TenantId = c.Int(),
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
            
            CreateIndex("dbo.CAPS_Job", "ChartOfAccountId");
            AddForeignKey("dbo.CAPS_Job", "ChartOfAccountId", "dbo.CAPS_ChartOfAccount", "ChartOfAccountId");
        }
    }
}
