namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_TypeOfPreference_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_TypeOfPreference",
                c => new
                    {
                        TypeOfPreferenceId = c.Short(nullable: false, identity: true),
                        TypeOfPreferenceCategoryId = c.Int(nullable: false),
                        PreferenceChoiceGroupId = c.Int(),
                        Description = c.String(nullable: false, maxLength: 100),
                        Caption = c.String(maxLength: 20),
                        DisplaySequence = c.Short(),
                        Notes = c.String(),
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
                    { "DynamicFilter_TypeOfPreferenceUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_TypeOfPreferenceUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.TypeOfPreferenceId)
                .ForeignKey("dbo.CAPS_PreferenceChoiceGroup", t => t.PreferenceChoiceGroupId)
                .Index(t => t.PreferenceChoiceGroupId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_TypeOfPreference", "PreferenceChoiceGroupId", "dbo.CAPS_PreferenceChoiceGroup");
            DropIndex("dbo.CAPS_TypeOfPreference", new[] { "PreferenceChoiceGroupId" });
            DropTable("dbo.CAPS_TypeOfPreference",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_TypeOfPreferenceUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_TypeOfPreferenceUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
