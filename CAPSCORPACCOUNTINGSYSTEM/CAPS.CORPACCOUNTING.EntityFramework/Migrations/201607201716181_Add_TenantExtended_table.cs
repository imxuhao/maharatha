namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_TenantExtended_table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_TenantExtended",
                c => new
                    {
                        TenantExtendedId = c.Int(nullable: false, identity: true),
                        TransmitterContactName = c.String(maxLength: 1000),
                        TransmitterEmailAddress = c.String(maxLength: 1000),
                        TransmitterCode = c.String(maxLength: 1000),
                        TransmitterControlCode = c.String(maxLength: 1000),
                        FederalTaxId = c.String(maxLength: 15),
                        Logo = c.Binary(),
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
                    { "DynamicFilter_TenantExtendedUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_TenantExtendedUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.TenantExtendedId);
            
            AlterTableAnnotations(
                "dbo.CAPS_Settings",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TenantId = c.Int(),
                        UserId = c.Long(),
                        Name = c.String(nullable: false, maxLength: 256),
                        Value = c.String(maxLength: 2000),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_SettingExtended_MayHaveTenant",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
            DropColumn("dbo.CAPS_Settings", "OrganizationUnitId");
            DropColumn("dbo.CAPS_Settings", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CAPS_Settings", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.CAPS_Settings", "OrganizationUnitId", c => c.Long());
            AlterTableAnnotations(
                "dbo.CAPS_Settings",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TenantId = c.Int(),
                        UserId = c.Long(),
                        Name = c.String(nullable: false, maxLength: 256),
                        Value = c.String(maxLength: 2000),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_SettingExtended_MayHaveTenant",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            DropTable("dbo.CAPS_TenantExtended",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_TenantExtendedUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_TenantExtendedUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
