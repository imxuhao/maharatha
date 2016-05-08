namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Modify_JobCommercials_Table : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.CAPS_JobCommercial", name: "JobCommercialId", newName: "JobId");
            AlterTableAnnotations(
                "dbo.CAPS_JobCommercial",
                c => new
                    {
                        JobId = c.Int(nullable: false,identity:false),
                        BidDate = c.DateTime(),
                        AwardDate = c.DateTime(),
                        ShootingDate = c.DateTime(),
                        WrapDate = c.DateTime(),
                        RoughCutDate = c.DateTime(),
                        AirDate = c.DateTime(),
                        DateClosed = c.DateTime(),
                        FinalShootDate = c.DateTime(),
                        ProductOwner = c.String(maxLength: 500),
                        ProductName = c.String(maxLength: 200),
                        ExecutiveProducerId = c.Int(),
                        DirectorEmployeeId = c.Int(),
                        ProducerEmployeeId = c.Int(),
                        DirOfPhotoEmployeeId = c.Int(),
                        SetDesignerEmployeeId = c.Int(),
                        EditorEmployeeId = c.Int(),
                        ArtDirectorEmployeeId = c.Int(),
                        SalesRepId = c.Int(),
                        AgencyId = c.Int(),
                        AgencyClientCustomerId = c.Int(),
                        ThirdPartyCustomerId = c.Int(),
                        AgencyProducer = c.String(maxLength: 200),
                        AgencyProducerContactInfo = c.String(maxLength: 500),
                        AgencyArtDirector = c.String(maxLength: 200),
                        AgencyArtDirContactInfo = c.String(maxLength: 500),
                        AgencyWriter = c.String(maxLength: 200),
                        AgencyWriterContactInfo = c.String(maxLength: 500),
                        AgencyBusinessManager = c.String(maxLength: 200),
                        AgencyBusMgrContactInfo = c.String(maxLength: 500),
                        AgencyJobNumber = c.String(maxLength: 50),
                        AgencyPONumber = c.String(maxLength: 50),
                        AgencyName = c.String(maxLength: 200),
                        AgencyAddress = c.String(maxLength: 500),
                        AgencyPhone = c.String(maxLength: 50),
                        CommercialTitle1 = c.String(maxLength: 200),
                        CommercialTitle2 = c.String(maxLength: 200),
                        CommercialTitle3 = c.String(maxLength: 200),
                        CommercialTitle4 = c.String(maxLength: 200),
                        CommercialTitle5 = c.String(maxLength: 200),
                        CommercialTitle6 = c.String(maxLength: 200),
                        ProjectTotal = c.Decimal(precision: 18, scale: 2),
                        CGITotal = c.Decimal(precision: 18, scale: 2),
                        MarkupPercent = c.Decimal(precision: 18, scale: 2),
                        MarkupTotal = c.Decimal(precision: 18, scale: 2),
                        RDARevenue = c.Decimal(precision: 18, scale: 2),
                        IncomeAccrual = c.Decimal(precision: 18, scale: 2),
                        CostAccrual = c.Decimal(precision: 18, scale: 2),
                        PostProductionCompany = c.String(maxLength: 200),
                        DubbingHouse = c.String(maxLength: 200),
                        StorageHouse = c.String(maxLength: 200),
                        IsBudgetLocked = c.Boolean(nullable: false),
                        CommercialNumber = c.String(maxLength: 50),
                        CommercialLength = c.String(maxLength: 50),
                        PreProductionDays = c.Int(),
                        StrikeDays = c.Int(),
                        PreLightDays = c.Int(),
                        PreLightHours = c.Int(),
                        StrikeHours = c.Int(),
                        StudioShootDays = c.Int(),
                        ShootHours = c.Int(),
                        LocationDays = c.Int(),
                        LocationHours = c.Int(),
                        IsCostPlus = c.Boolean(nullable: false),
                        IsWrapUpInsurance = c.Boolean(nullable: false),
                        IsFringeAccountSeparate = c.Boolean(nullable: false),
                        IsOTon = c.Boolean(nullable: false),
                        AgencyEmail = c.String(maxLength: 500),
                        ContractExecutionDate = c.DateTime(),
                        DeliveryDate = c.DateTime(),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_JobCommercialUnit_MustHaveTenant",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                    { 
                        "DynamicFilter_JobCommercialUnit_SoftDelete",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            CreateIndex("dbo.CAPS_JobCommercial", "JobId");
            AddForeignKey("dbo.CAPS_JobCommercial", "JobId", "dbo.CAPS_Job", "JobId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_JobCommercial", "JobId", "dbo.CAPS_Job");
            DropIndex("dbo.CAPS_JobCommercial", new[] { "JobId" });
            AlterTableAnnotations(
                "dbo.CAPS_JobCommercial",
                c => new
                    {
                        JobId = c.Int(nullable: false),
                        BidDate = c.DateTime(),
                        AwardDate = c.DateTime(),
                        ShootingDate = c.DateTime(),
                        WrapDate = c.DateTime(),
                        RoughCutDate = c.DateTime(),
                        AirDate = c.DateTime(),
                        DateClosed = c.DateTime(),
                        FinalShootDate = c.DateTime(),
                        ProductOwner = c.String(maxLength: 500),
                        ProductName = c.String(maxLength: 200),
                        ExecutiveProducerId = c.Int(),
                        DirectorEmployeeId = c.Int(),
                        ProducerEmployeeId = c.Int(),
                        DirOfPhotoEmployeeId = c.Int(),
                        SetDesignerEmployeeId = c.Int(),
                        EditorEmployeeId = c.Int(),
                        ArtDirectorEmployeeId = c.Int(),
                        SalesRepId = c.Int(),
                        AgencyId = c.Int(),
                        AgencyClientCustomerId = c.Int(),
                        ThirdPartyCustomerId = c.Int(),
                        AgencyProducer = c.String(maxLength: 200),
                        AgencyProducerContactInfo = c.String(maxLength: 500),
                        AgencyArtDirector = c.String(maxLength: 200),
                        AgencyArtDirContactInfo = c.String(maxLength: 500),
                        AgencyWriter = c.String(maxLength: 200),
                        AgencyWriterContactInfo = c.String(maxLength: 500),
                        AgencyBusinessManager = c.String(maxLength: 200),
                        AgencyBusMgrContactInfo = c.String(maxLength: 500),
                        AgencyJobNumber = c.String(maxLength: 50),
                        AgencyPONumber = c.String(maxLength: 50),
                        AgencyName = c.String(maxLength: 200),
                        AgencyAddress = c.String(maxLength: 500),
                        AgencyPhone = c.String(maxLength: 50),
                        CommercialTitle1 = c.String(maxLength: 200),
                        CommercialTitle2 = c.String(maxLength: 200),
                        CommercialTitle3 = c.String(maxLength: 200),
                        CommercialTitle4 = c.String(maxLength: 200),
                        CommercialTitle5 = c.String(maxLength: 200),
                        CommercialTitle6 = c.String(maxLength: 200),
                        ProjectTotal = c.Decimal(precision: 18, scale: 2),
                        CGITotal = c.Decimal(precision: 18, scale: 2),
                        MarkupPercent = c.Decimal(precision: 18, scale: 2),
                        MarkupTotal = c.Decimal(precision: 18, scale: 2),
                        RDARevenue = c.Decimal(precision: 18, scale: 2),
                        IncomeAccrual = c.Decimal(precision: 18, scale: 2),
                        CostAccrual = c.Decimal(precision: 18, scale: 2),
                        PostProductionCompany = c.String(maxLength: 200),
                        DubbingHouse = c.String(maxLength: 200),
                        StorageHouse = c.String(maxLength: 200),
                        IsBudgetLocked = c.Boolean(nullable: false),
                        CommercialNumber = c.String(maxLength: 50),
                        CommercialLength = c.String(maxLength: 50),
                        PreProductionDays = c.Int(),
                        StrikeDays = c.Int(),
                        PreLightDays = c.Int(),
                        PreLightHours = c.Int(),
                        StrikeHours = c.Int(),
                        StudioShootDays = c.Int(),
                        ShootHours = c.Int(),
                        LocationDays = c.Int(),
                        LocationHours = c.Int(),
                        IsCostPlus = c.Boolean(nullable: false),
                        IsWrapUpInsurance = c.Boolean(nullable: false),
                        IsFringeAccountSeparate = c.Boolean(nullable: false),
                        IsOTon = c.Boolean(nullable: false),
                        AgencyEmail = c.String(maxLength: 500),
                        ContractExecutionDate = c.DateTime(),
                        DeliveryDate = c.DateTime(),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_JobCommercialUnit_MustHaveTenant",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                    { 
                        "DynamicFilter_JobCommercialUnit_SoftDelete",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
            RenameColumn(table: "dbo.CAPS_JobCommercial", name: "JobId", newName: "JobCommercialId");
        }
    }
}
