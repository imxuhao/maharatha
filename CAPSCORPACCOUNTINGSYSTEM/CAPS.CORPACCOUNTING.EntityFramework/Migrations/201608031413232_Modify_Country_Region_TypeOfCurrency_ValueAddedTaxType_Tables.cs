namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Modify_Country_Region_TypeOfCurrency_ValueAddedTaxType_Tables : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CAPS_Country", "TypeOfCountryId", "dbo.CAPS_TypeOfCountry");
            DropForeignKey("dbo.CAPS_Region", "TypeOfCountryId", "dbo.CAPS_TypeOfCountry");
            DropForeignKey("dbo.CAPS_ValueAddedTaxType", "TypeOfCountryId", "dbo.CAPS_TypeOfCountry");
            DropIndex("dbo.CAPS_Country", new[] { "TypeOfCountryId" });
            DropIndex("dbo.CAPS_Region", new[] { "TypeOfCountryId" });
            DropIndex("dbo.CAPS_ValueAddedTaxType", new[] { "TypeOfCountryId" });
            AlterTableAnnotations(
                "dbo.CAPS_Country",
                c => new
                    {
                        CountryID = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 100),
                        Caption = c.String(maxLength: 20),
                        DisplaySequence = c.Short(),
                        Notes = c.String(),
                        TwoLetterAbbreviation = c.String(maxLength: 2),
                        ThreeLetterAbbreviation = c.String(maxLength: 3),
                        IsoNumber = c.String(maxLength: 3),
                        TenantId = c.Int(),
                        OrganizationUnitId = c.Long(),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_CountryUnit_MayHaveTenant",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                    { 
                        "DynamicFilter_CountryUnit_MustHaveTenant",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
            AlterTableAnnotations(
                "dbo.CAPS_Region",
                c => new
                    {
                        RegionId = c.Int(nullable: false, identity: true),
                        LajitId = c.Int(),
                        Description = c.String(nullable: false, maxLength: 200),
                        RegionAbbreviation = c.String(maxLength: 10),
                        CountryID = c.Int(),
                        StateCode = c.String(maxLength: 2),
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
                        "DynamicFilter_RegionUnit_MayHaveTenant",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                    { 
                        "DynamicFilter_RegionUnit_MustHaveTenant",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
            AlterTableAnnotations(
                "dbo.CAPS_TypeOfCurrency",
                c => new
                    {
                        TypeOfCurrencyId = c.Short(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 100),
                        Caption = c.String(maxLength: 10),
                        ISOCurrencyCode = c.String(nullable: false, maxLength: 20),
                        CurrencySymbol = c.String(maxLength: 20),
                        TenantId = c.Int(),
                        OrganizationUnitId = c.Long(),
                        CountryID = c.Int(),
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
                        "DynamicFilter_TypeOfCurrencyUnit_MayHaveTenant",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                    { 
                        "DynamicFilter_TypeOfCurrencyUnit_MustHaveTenant",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
            AddColumn("dbo.CAPS_Country", "Description", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.CAPS_Country", "Caption", c => c.String(maxLength: 20));
            AddColumn("dbo.CAPS_Country", "DisplaySequence", c => c.Short());
            AddColumn("dbo.CAPS_Country", "Notes", c => c.String());
            AddColumn("dbo.CAPS_Country", "TwoLetterAbbreviation", c => c.String(maxLength: 2));
            AddColumn("dbo.CAPS_Country", "ThreeLetterAbbreviation", c => c.String(maxLength: 3));
            AddColumn("dbo.CAPS_Country", "IsoNumber", c => c.String(maxLength: 3));
            AddColumn("dbo.CAPS_Country", "OrganizationUnitId", c => c.Long());
            AddColumn("dbo.CAPS_Region", "CountryID", c => c.Int());
            AddColumn("dbo.CAPS_TypeOfCurrency", "CountryID", c => c.Int());
            AddColumn("dbo.CAPS_ValueAddedTaxType", "CountryID", c => c.Int());
            AlterColumn("dbo.CAPS_Country", "TenantId", c => c.Int());
            AlterColumn("dbo.CAPS_Region", "TenantId", c => c.Int());
            AlterColumn("dbo.CAPS_TypeOfCurrency", "TenantId", c => c.Int());
            CreateIndex("dbo.CAPS_Region", "CountryID");
            AddForeignKey("dbo.CAPS_Region", "CountryID", "dbo.CAPS_Country", "CountryID");
            DropColumn("dbo.CAPS_Country", "TypeOfCountryId");
            DropColumn("dbo.CAPS_Region", "TypeOfCountryId");
            DropColumn("dbo.CAPS_ValueAddedTaxType", "TypeOfCountryId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CAPS_ValueAddedTaxType", "TypeOfCountryId", c => c.Short(nullable: false));
            AddColumn("dbo.CAPS_Region", "TypeOfCountryId", c => c.Short());
            AddColumn("dbo.CAPS_Country", "TypeOfCountryId", c => c.Short());
            DropForeignKey("dbo.CAPS_Region", "CountryID", "dbo.CAPS_Country");
            DropIndex("dbo.CAPS_Region", new[] { "CountryID" });
            AlterColumn("dbo.CAPS_TypeOfCurrency", "TenantId", c => c.Int(nullable: false));
            AlterColumn("dbo.CAPS_Region", "TenantId", c => c.Int(nullable: false));
            AlterColumn("dbo.CAPS_Country", "TenantId", c => c.Int(nullable: false));
            DropColumn("dbo.CAPS_ValueAddedTaxType", "CountryID");
            DropColumn("dbo.CAPS_TypeOfCurrency", "CountryID");
            DropColumn("dbo.CAPS_Region", "CountryID");
            DropColumn("dbo.CAPS_Country", "OrganizationUnitId");
            DropColumn("dbo.CAPS_Country", "IsoNumber");
            DropColumn("dbo.CAPS_Country", "ThreeLetterAbbreviation");
            DropColumn("dbo.CAPS_Country", "TwoLetterAbbreviation");
            DropColumn("dbo.CAPS_Country", "Notes");
            DropColumn("dbo.CAPS_Country", "DisplaySequence");
            DropColumn("dbo.CAPS_Country", "Caption");
            DropColumn("dbo.CAPS_Country", "Description");
            AlterTableAnnotations(
                "dbo.CAPS_TypeOfCurrency",
                c => new
                    {
                        TypeOfCurrencyId = c.Short(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 100),
                        Caption = c.String(maxLength: 10),
                        ISOCurrencyCode = c.String(nullable: false, maxLength: 20),
                        CurrencySymbol = c.String(maxLength: 20),
                        TenantId = c.Int(),
                        OrganizationUnitId = c.Long(),
                        CountryID = c.Int(),
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
                        "DynamicFilter_TypeOfCurrencyUnit_MayHaveTenant",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                    { 
                        "DynamicFilter_TypeOfCurrencyUnit_MustHaveTenant",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            AlterTableAnnotations(
                "dbo.CAPS_Region",
                c => new
                    {
                        RegionId = c.Int(nullable: false, identity: true),
                        LajitId = c.Int(),
                        Description = c.String(nullable: false, maxLength: 200),
                        RegionAbbreviation = c.String(maxLength: 10),
                        CountryID = c.Int(),
                        StateCode = c.String(maxLength: 2),
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
                        "DynamicFilter_RegionUnit_MayHaveTenant",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                    { 
                        "DynamicFilter_RegionUnit_MustHaveTenant",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            AlterTableAnnotations(
                "dbo.CAPS_Country",
                c => new
                    {
                        CountryID = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 100),
                        Caption = c.String(maxLength: 20),
                        DisplaySequence = c.Short(),
                        Notes = c.String(),
                        TwoLetterAbbreviation = c.String(maxLength: 2),
                        ThreeLetterAbbreviation = c.String(maxLength: 3),
                        IsoNumber = c.String(maxLength: 3),
                        TenantId = c.Int(),
                        OrganizationUnitId = c.Long(),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_CountryUnit_MayHaveTenant",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                    { 
                        "DynamicFilter_CountryUnit_MustHaveTenant",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            CreateIndex("dbo.CAPS_ValueAddedTaxType", "TypeOfCountryId");
            CreateIndex("dbo.CAPS_Region", "TypeOfCountryId");
            CreateIndex("dbo.CAPS_Country", "TypeOfCountryId");
            AddForeignKey("dbo.CAPS_ValueAddedTaxType", "TypeOfCountryId", "dbo.CAPS_TypeOfCountry", "TypeOfCountryId", cascadeDelete: true);
            AddForeignKey("dbo.CAPS_Region", "TypeOfCountryId", "dbo.CAPS_TypeOfCountry", "TypeOfCountryId");
            AddForeignKey("dbo.CAPS_Country", "TypeOfCountryId", "dbo.CAPS_TypeOfCountry", "TypeOfCountryId");
        }
    }
}
