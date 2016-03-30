namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_UserViewSettings_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_UserViewSettings",
                c => new
                    {
                        UserViewId = c.Int(nullable: false, identity: true),
                        GridId = c.Int(nullable: false),
                        UserId = c.Long(nullable: false),
                        ViewSettings = c.String(nullable: false),
                        IsDefault = c.Boolean(),
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
                    { "DynamicFilter_UserViewSettingsUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_UserViewSettingsUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.UserViewId)
                .ForeignKey("dbo.CAPS_GridList", t => t.GridId, cascadeDelete: true)
                .ForeignKey("dbo.CAPS_Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.GridId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_UserViewSettings", "UserId", "dbo.CAPS_Users");
            DropForeignKey("dbo.CAPS_UserViewSettings", "GridId", "dbo.CAPS_GridList");
            DropIndex("dbo.CAPS_UserViewSettings", new[] { "UserId" });
            DropIndex("dbo.CAPS_UserViewSettings", new[] { "GridId" });
            DropTable("dbo.CAPS_UserViewSettings",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_UserViewSettingsUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_UserViewSettingsUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
