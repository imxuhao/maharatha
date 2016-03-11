namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_SystemPreference_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_SystemPreference",
                c => new
                    {
                        SystemPreferenceId = c.Int(nullable: false, identity: true),
                        DefaultPreferenceId = c.Int(nullable: false),
                        PreferenceValue = c.String(nullable: false, maxLength: 200),
                        IsActive = c.Boolean(nullable: false),
                        EntityId = c.Int(),
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
                    { "DynamicFilter_SystemPreferenceUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_SystemPreferenceUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.SystemPreferenceId)
                .ForeignKey("dbo.CAPS_DefaultPreference", t => t.DefaultPreferenceId, cascadeDelete: true)
                .Index(t => t.DefaultPreferenceId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_SystemPreference", "DefaultPreferenceId", "dbo.CAPS_DefaultPreference");
            DropIndex("dbo.CAPS_SystemPreference", new[] { "DefaultPreferenceId" });
            DropTable("dbo.CAPS_SystemPreference",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_SystemPreferenceUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_SystemPreferenceUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
