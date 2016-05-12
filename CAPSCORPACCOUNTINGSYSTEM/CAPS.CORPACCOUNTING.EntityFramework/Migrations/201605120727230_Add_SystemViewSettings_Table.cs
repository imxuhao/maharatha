namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_SystemViewSettings_Table : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.CAPS_GridList", newName: "CAPS_SystemViewList");
            DropForeignKey("dbo.CAPS_UserViewSettings", "UserId", "dbo.CAPS_Users");
            DropIndex("dbo.CAPS_UserViewSettings", new[] { "UserId" });
            RenameColumn(table: "dbo.CAPS_SystemViewList", name: "GridId", newName: "ViewId");
            RenameColumn(table: "dbo.CAPS_UserViewSettings", name: "GridId", newName: "ViewId");
            RenameIndex(table: "dbo.CAPS_UserViewSettings", name: "IX_GridId", newName: "IX_ViewId");
            CreateTable(
                "dbo.CAPS_SystemViewSettings",
                c => new
                    {
                        SystemViewId = c.Int(nullable: false, identity: true),
                        ViewId = c.Int(nullable: false),
                        ViewName = c.String(nullable: false, maxLength: 300),
                        ViewSettings = c.String(nullable: false),
                        IsDefault = c.Boolean(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    })
                .PrimaryKey(t => t.SystemViewId)
                .ForeignKey("dbo.CAPS_SystemViewList", t => t.ViewId, cascadeDelete: true)
                .Index(t => t.ViewId);
            
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
            
            AddColumn("dbo.CAPS_UserViewSettings", "ViewName", c => c.String(nullable: false, maxLength: 300));
            AddColumn("dbo.CAPS_UserViewSettings", "OrganizationUnitId", c => c.Long());
            AlterColumn("dbo.CAPS_UserViewSettings", "UserId", c => c.Long(nullable: false));
            AlterColumn("dbo.CAPS_UserViewSettings", "TenantId", c => c.Int(nullable: false));
            CreateIndex("dbo.CAPS_UserViewSettings", "UserId");
            AddForeignKey("dbo.CAPS_UserViewSettings", "UserId", "dbo.CAPS_Users", "Id", cascadeDelete: true);
            DropColumn("dbo.CAPS_UserViewSettings", "ViewSettingName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CAPS_UserViewSettings", "ViewSettingName", c => c.String(nullable: false, maxLength: 300));
            DropForeignKey("dbo.CAPS_UserViewSettings", "UserId", "dbo.CAPS_Users");
            DropForeignKey("dbo.CAPS_SystemViewSettings", "ViewId", "dbo.CAPS_SystemViewList");
            DropIndex("dbo.CAPS_UserViewSettings", new[] { "UserId" });
            DropIndex("dbo.CAPS_SystemViewSettings", new[] { "ViewId" });
            AlterColumn("dbo.CAPS_UserViewSettings", "TenantId", c => c.Int());
            AlterColumn("dbo.CAPS_UserViewSettings", "UserId", c => c.Long());
            DropColumn("dbo.CAPS_UserViewSettings", "OrganizationUnitId");
            DropColumn("dbo.CAPS_UserViewSettings", "ViewName");
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
            
            DropTable("dbo.CAPS_SystemViewSettings");
            RenameIndex(table: "dbo.CAPS_UserViewSettings", name: "IX_ViewId", newName: "IX_GridId");
            RenameColumn(table: "dbo.CAPS_UserViewSettings", name: "ViewId", newName: "GridId");
            RenameColumn(table: "dbo.CAPS_SystemViewList", name: "ViewId", newName: "GridId");
            CreateIndex("dbo.CAPS_UserViewSettings", "UserId");
            AddForeignKey("dbo.CAPS_UserViewSettings", "UserId", "dbo.CAPS_Users", "Id");
            RenameTable(name: "dbo.CAPS_SystemViewList", newName: "CAPS_GridList");
        }
    }
}
