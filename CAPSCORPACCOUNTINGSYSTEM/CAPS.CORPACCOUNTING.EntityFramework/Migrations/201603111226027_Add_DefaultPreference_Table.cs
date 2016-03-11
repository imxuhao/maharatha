namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_DefaultPreference_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_DefaultPreference",
                c => new
                    {
                        DefaultPreferenceId = c.Int(nullable: false, identity: true),
                        TypeOfPreferenceCategoryId = c.Int(nullable: false),
                        TypeOfPreferenceId = c.Short(nullable: false),
                        IsRequired = c.Boolean(nullable: false),
                        IsPrivate = c.Boolean(nullable: false),
                        PreferenceValue = c.String(nullable: false, maxLength: 200),
                        IsActive = c.Boolean(nullable: false),
                        TemplateTenantId = c.Int(),
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
                    { "DynamicFilter_DefaultPreferenceUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_DefaultPreferenceUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.DefaultPreferenceId)
                .ForeignKey("dbo.CAPS_TypeOfPreference", t => t.TypeOfPreferenceId, cascadeDelete: true)
                .Index(t => t.TypeOfPreferenceId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_DefaultPreference", "TypeOfPreferenceId", "dbo.CAPS_TypeOfPreference");
            DropIndex("dbo.CAPS_DefaultPreference", new[] { "TypeOfPreferenceId" });
            DropTable("dbo.CAPS_DefaultPreference",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_DefaultPreferenceUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_DefaultPreferenceUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
