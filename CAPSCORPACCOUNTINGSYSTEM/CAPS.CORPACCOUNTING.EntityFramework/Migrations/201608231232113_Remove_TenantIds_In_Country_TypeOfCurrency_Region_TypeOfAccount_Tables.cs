namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Remove_TenantIds_In_Country_TypeOfCurrency_Region_TypeOfAccount_Tables : DbMigration
    {
        public override void Up()
        {
            AlterTableAnnotations(
                "dbo.CAPS_TypeOfAccount",
                c => new
                    {
                        TypeOfAccountId = c.Int(nullable: false, identity: true),
                        LajitId = c.Int(),
                        Description = c.String(nullable: false, maxLength: 100),
                        Caption = c.String(maxLength: 20),
                        DisplaySequence = c.Short(),
                        Notes = c.String(),
                        TypeOfAccountClassificationId = c.Short(),
                        IsCurrencyCodeRequired = c.Boolean(nullable: false),
                        IsPaymentType = c.Boolean(nullable: false),
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
                        "DynamicFilter_TypeOfAccountUnit_MayHaveTenant",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
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
                        OrganizationUnitId = c.Long(),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_CountryUnit_MayHaveTenant",
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
                });
            
            DropColumn("dbo.CAPS_TypeOfAccount", "TenantId");
            DropColumn("dbo.CAPS_Country", "TenantId");
            DropColumn("dbo.CAPS_Region", "TenantId");
            DropColumn("dbo.CAPS_TypeOfCurrency", "TenantId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CAPS_TypeOfCurrency", "TenantId", c => c.Int());
            AddColumn("dbo.CAPS_Region", "TenantId", c => c.Int());
            AddColumn("dbo.CAPS_Country", "TenantId", c => c.Int());
            AddColumn("dbo.CAPS_TypeOfAccount", "TenantId", c => c.Int());
            AlterTableAnnotations(
                "dbo.CAPS_TypeOfCurrency",
                c => new
                    {
                        TypeOfCurrencyId = c.Short(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 100),
                        Caption = c.String(maxLength: 10),
                        ISOCurrencyCode = c.String(nullable: false, maxLength: 20),
                        CurrencySymbol = c.String(maxLength: 20),
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
                        OrganizationUnitId = c.Long(),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_CountryUnit_MayHaveTenant",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            AlterTableAnnotations(
                "dbo.CAPS_TypeOfAccount",
                c => new
                    {
                        TypeOfAccountId = c.Int(nullable: false, identity: true),
                        LajitId = c.Int(),
                        Description = c.String(nullable: false, maxLength: 100),
                        Caption = c.String(maxLength: 20),
                        DisplaySequence = c.Short(),
                        Notes = c.String(),
                        TypeOfAccountClassificationId = c.Short(),
                        IsCurrencyCodeRequired = c.Boolean(nullable: false),
                        IsPaymentType = c.Boolean(nullable: false),
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
                        "DynamicFilter_TypeOfAccountUnit_MayHaveTenant",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
        }
    }
}
