namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Modify_TaxRecovery_Tables : DbMigration
    {
        public override void Up()
        {
            AlterTableAnnotations(
                "dbo.CAPS_ValueAddedTaxRecovery",
                c => new
                    {
                        ValueAddedTaxRecoveryId = c.Int(nullable: false, identity: true),
                        LajitId = c.Int(),
                        ValueAddedTaxTypeId = c.Int(nullable: false),
                        TypeOfVatRecoveryId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_ValueAddedTaxRecoveryUnit_MustHaveTenant",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                    { 
                        "DynamicFilter_ValueAddedTaxRecoveryUnit_SoftDelete",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
            AlterTableAnnotations(
                "dbo.CAPS_ValueAddedTaxType",
                c => new
                    {
                        ValueAddedTaxTypeId = c.Int(nullable: false, identity: true),
                        LajitId = c.Int(),
                        TypeOfCountryId = c.Short(nullable: false),
                        TypeOfValueAddedTaxId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_ValueAddedTaxTypeUnit_MustHaveTenant",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                    { 
                        "DynamicFilter_ValueAddedTaxTypeUnit_SoftDelete",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
            DropColumn("dbo.CAPS_ValueAddedTaxRecovery", "TenantId");
            DropColumn("dbo.CAPS_ValueAddedTaxRecovery", "OrganizationUnitId");
            DropColumn("dbo.CAPS_ValueAddedTaxRecovery", "IsDeleted");
            DropColumn("dbo.CAPS_ValueAddedTaxRecovery", "DeleterUserId");
            DropColumn("dbo.CAPS_ValueAddedTaxRecovery", "DeletionTime");
            DropColumn("dbo.CAPS_ValueAddedTaxRecovery", "LastModificationTime");
            DropColumn("dbo.CAPS_ValueAddedTaxRecovery", "LastModifierUserId");
            DropColumn("dbo.CAPS_ValueAddedTaxRecovery", "CreationTime");
            DropColumn("dbo.CAPS_ValueAddedTaxRecovery", "CreatorUserId");
            DropColumn("dbo.CAPS_ValueAddedTaxType", "TenantId");
            DropColumn("dbo.CAPS_ValueAddedTaxType", "OrganizationUnitId");
            DropColumn("dbo.CAPS_ValueAddedTaxType", "IsDeleted");
            DropColumn("dbo.CAPS_ValueAddedTaxType", "DeleterUserId");
            DropColumn("dbo.CAPS_ValueAddedTaxType", "DeletionTime");
            DropColumn("dbo.CAPS_ValueAddedTaxType", "LastModificationTime");
            DropColumn("dbo.CAPS_ValueAddedTaxType", "LastModifierUserId");
            DropColumn("dbo.CAPS_ValueAddedTaxType", "CreationTime");
            DropColumn("dbo.CAPS_ValueAddedTaxType", "CreatorUserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CAPS_ValueAddedTaxType", "CreatorUserId", c => c.Long());
            AddColumn("dbo.CAPS_ValueAddedTaxType", "CreationTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.CAPS_ValueAddedTaxType", "LastModifierUserId", c => c.Long());
            AddColumn("dbo.CAPS_ValueAddedTaxType", "LastModificationTime", c => c.DateTime());
            AddColumn("dbo.CAPS_ValueAddedTaxType", "DeletionTime", c => c.DateTime());
            AddColumn("dbo.CAPS_ValueAddedTaxType", "DeleterUserId", c => c.Long());
            AddColumn("dbo.CAPS_ValueAddedTaxType", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.CAPS_ValueAddedTaxType", "OrganizationUnitId", c => c.Long());
            AddColumn("dbo.CAPS_ValueAddedTaxType", "TenantId", c => c.Int(nullable: false));
            AddColumn("dbo.CAPS_ValueAddedTaxRecovery", "CreatorUserId", c => c.Long());
            AddColumn("dbo.CAPS_ValueAddedTaxRecovery", "CreationTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.CAPS_ValueAddedTaxRecovery", "LastModifierUserId", c => c.Long());
            AddColumn("dbo.CAPS_ValueAddedTaxRecovery", "LastModificationTime", c => c.DateTime());
            AddColumn("dbo.CAPS_ValueAddedTaxRecovery", "DeletionTime", c => c.DateTime());
            AddColumn("dbo.CAPS_ValueAddedTaxRecovery", "DeleterUserId", c => c.Long());
            AddColumn("dbo.CAPS_ValueAddedTaxRecovery", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.CAPS_ValueAddedTaxRecovery", "OrganizationUnitId", c => c.Long());
            AddColumn("dbo.CAPS_ValueAddedTaxRecovery", "TenantId", c => c.Int(nullable: false));
            AlterTableAnnotations(
                "dbo.CAPS_ValueAddedTaxType",
                c => new
                    {
                        ValueAddedTaxTypeId = c.Int(nullable: false, identity: true),
                        LajitId = c.Int(),
                        TypeOfCountryId = c.Short(nullable: false),
                        TypeOfValueAddedTaxId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_ValueAddedTaxTypeUnit_MustHaveTenant",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                    { 
                        "DynamicFilter_ValueAddedTaxTypeUnit_SoftDelete",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            AlterTableAnnotations(
                "dbo.CAPS_ValueAddedTaxRecovery",
                c => new
                    {
                        ValueAddedTaxRecoveryId = c.Int(nullable: false, identity: true),
                        LajitId = c.Int(),
                        ValueAddedTaxTypeId = c.Int(nullable: false),
                        TypeOfVatRecoveryId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_ValueAddedTaxRecoveryUnit_MustHaveTenant",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                    { 
                        "DynamicFilter_ValueAddedTaxRecoveryUnit_SoftDelete",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
        }
    }
}
