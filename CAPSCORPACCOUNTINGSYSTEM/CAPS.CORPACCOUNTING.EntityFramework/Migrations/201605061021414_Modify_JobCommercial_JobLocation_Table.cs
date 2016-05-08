namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Modify_JobCommercial_JobLocation_Table : DbMigration
    {
        public override void Up()
        {
            Sql("alter table dbo.CAPS_JobLocation DROP CONSTRAINT[FK_dbo.CAPS_JobLocation_dbo.CAPS_JobDetail_JobDetailId]");
            DropForeignKey("dbo.CAPS_JobLocation", "JobDetailId", "dbo.CAPS_JobCommercial");
            DropForeignKey("dbo.CAPS_JobCommercial", "JobId", "dbo.CAPS_Job");
            DropIndex("dbo.CAPS_JobCommercial", new[] { "JobId" });
            DropIndex("dbo.CAPS_JobLocation", new[] { "JobDetailId" });
            DropPrimaryKey("dbo.CAPS_JobCommercial");
            AlterTableAnnotations(
                "dbo.CAPS_JobCommercial",
                c => new
                    {
                        JobCommercialId = c.Int(nullable: false,identity:false),
                        BidDate = c.DateTime(),
                        AwardDate = c.DateTime(),
                        ShootingDate = c.DateTime(),
                        WrapDate = c.DateTime(),
                        RoughCutDate = c.DateTime(),
                        AirDate = c.DateTime(),
                        DateClosed = c.DateTime(),FinalShootDate = c.DateTime(),
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
            
            AlterTableAnnotations(
                "dbo.CAPS_JobLocation",
                c => new
                    {
                        JobLocationId = c.Int(nullable: false, identity: true),
                        JobId = c.Int(nullable: false),
                        LocationId = c.Int(nullable: false),
                        LocationSiteDate = c.DateTime(),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_JobLocationUnit_MustHaveTenant",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                    { 
                        "DynamicFilter_JobLocationUnit_SoftDelete",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
            AddColumn("dbo.CAPS_JobCommercial", "ContractExecutionDate", c => c.DateTime());
            AddColumn("dbo.CAPS_JobCommercial", "DeliveryDate", c => c.DateTime());
            AlterColumn("dbo.CAPS_JobCommercial", "JobCommercialId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.CAPS_JobCommercial", "JobCommercialId");
            DropColumn("dbo.CAPS_JobCommercial", "LajitId");
            DropColumn("dbo.CAPS_JobCommercial", "JobId");
            DropColumn("dbo.CAPS_JobCommercial", "OrganizationUnitId");
            DropColumn("dbo.CAPS_JobCommercial", "TenantId");
            DropColumn("dbo.CAPS_JobCommercial", "IsDeleted");
            DropColumn("dbo.CAPS_JobCommercial", "DeleterUserId");
            DropColumn("dbo.CAPS_JobCommercial", "DeletionTime");
            DropColumn("dbo.CAPS_JobCommercial", "LastModificationTime");
            DropColumn("dbo.CAPS_JobCommercial", "LastModifierUserId");
            DropColumn("dbo.CAPS_JobCommercial", "CreationTime");
            DropColumn("dbo.CAPS_JobCommercial", "CreatorUserId");
            DropColumn("dbo.CAPS_JobLocation", "JobDetailId");
            DropColumn("dbo.CAPS_JobLocation", "OrganizationUnitId");
            DropColumn("dbo.CAPS_JobLocation", "TenantId");
            DropColumn("dbo.CAPS_JobLocation", "IsDeleted");
            DropColumn("dbo.CAPS_JobLocation", "DeleterUserId");
            DropColumn("dbo.CAPS_JobLocation", "DeletionTime");
            DropColumn("dbo.CAPS_JobLocation", "LastModificationTime");
            DropColumn("dbo.CAPS_JobLocation", "LastModifierUserId");
            DropColumn("dbo.CAPS_JobLocation", "CreationTime");
            DropColumn("dbo.CAPS_JobLocation", "CreatorUserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CAPS_JobLocation", "CreatorUserId", c => c.Long());
            AddColumn("dbo.CAPS_JobLocation", "CreationTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.CAPS_JobLocation", "LastModifierUserId", c => c.Long());
            AddColumn("dbo.CAPS_JobLocation", "LastModificationTime", c => c.DateTime());
            AddColumn("dbo.CAPS_JobLocation", "DeletionTime", c => c.DateTime());
            AddColumn("dbo.CAPS_JobLocation", "DeleterUserId", c => c.Long());
            AddColumn("dbo.CAPS_JobLocation", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.CAPS_JobLocation", "TenantId", c => c.Int(nullable: false));
            AddColumn("dbo.CAPS_JobLocation", "OrganizationUnitId", c => c.Long());
            AddColumn("dbo.CAPS_JobLocation", "JobDetailId", c => c.Int(nullable: false));
            AddColumn("dbo.CAPS_JobCommercial", "CreatorUserId", c => c.Long());
            AddColumn("dbo.CAPS_JobCommercial", "CreationTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.CAPS_JobCommercial", "LastModifierUserId", c => c.Long());
            AddColumn("dbo.CAPS_JobCommercial", "LastModificationTime", c => c.DateTime());
            AddColumn("dbo.CAPS_JobCommercial", "DeletionTime", c => c.DateTime());
            AddColumn("dbo.CAPS_JobCommercial", "DeleterUserId", c => c.Long());
            AddColumn("dbo.CAPS_JobCommercial", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.CAPS_JobCommercial", "TenantId", c => c.Int(nullable: false));
            AddColumn("dbo.CAPS_JobCommercial", "OrganizationUnitId", c => c.Long());
            AddColumn("dbo.CAPS_JobCommercial", "JobId", c => c.Int(nullable: false));
            AddColumn("dbo.CAPS_JobCommercial", "LajitId", c => c.Int());
            DropPrimaryKey("dbo.CAPS_JobCommercial");
            AlterColumn("dbo.CAPS_JobCommercial", "JobCommercialId", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.CAPS_JobCommercial", "DeliveryDate");
            DropColumn("dbo.CAPS_JobCommercial", "ContractExecutionDate");
            AlterTableAnnotations(
                "dbo.CAPS_JobLocation",
                c => new
                    {
                        JobLocationId = c.Int(nullable: false, identity: true),
                        JobId = c.Int(nullable: false),
                        LocationId = c.Int(nullable: false),
                        LocationSiteDate = c.DateTime(),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_JobLocationUnit_MustHaveTenant",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                    { 
                        "DynamicFilter_JobLocationUnit_SoftDelete",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
            AlterTableAnnotations(
                "dbo.CAPS_JobCommercial",
                c => new
                    {
                        JobCommercialId = c.Int(nullable: false),
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
            
            AddPrimaryKey("dbo.CAPS_JobCommercial", "JobCommercialId");
            CreateIndex("dbo.CAPS_JobLocation", "JobDetailId");
            CreateIndex("dbo.CAPS_JobCommercial", "JobId");
            AddForeignKey("dbo.CAPS_JobCommercial", "JobId", "dbo.CAPS_Job", "JobId", cascadeDelete: true);
            AddForeignKey("dbo.CAPS_JobLocation", "JobDetailId", "dbo.CAPS_JobCommercial", "JobCommercialId", cascadeDelete: true);
        }
    }
}
