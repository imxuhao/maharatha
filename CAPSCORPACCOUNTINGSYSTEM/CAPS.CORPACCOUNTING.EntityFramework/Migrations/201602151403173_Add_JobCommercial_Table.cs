namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_JobCommercial_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_JobCommercial",
                c => new
                    {
                        JobCommertialId = c.Int(nullable: false, identity: true),
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
                        IsBudgetLocked = c.Boolean(nullable: false,defaultValue:false),
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
                        IsCostPlus = c.Boolean(nullable: false, defaultValue: false),
                        IsWrapUpInsurance = c.Boolean(nullable: false, defaultValue: false),
                        IsFringeAccountSeparate = c.Boolean(nullable: false, defaultValue: false),
                        OrganizationUnitId = c.Long(),
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
                    { "DynamicFilter_JobCommercialUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_JobCommercialUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.JobCommertialId)
                .ForeignKey("dbo.CAPS_Customers", t => t.AgencyId)
                .ForeignKey("dbo.CAPS_Customers", t => t.AgencyClientCustomerId)
                .ForeignKey("dbo.CAPS_Employee", t => t.ArtDirectorEmployeeId)
                .ForeignKey("dbo.CAPS_Employee", t => t.DirectorEmployeeId)
                .ForeignKey("dbo.CAPS_Employee", t => t.DirOfPhotoEmployeeId)
                .ForeignKey("dbo.CAPS_Employee", t => t.EditorEmployeeId)
                .ForeignKey("dbo.CAPS_Employee", t => t.ExecutiveProducerId)
                .ForeignKey("dbo.CAPS_Job", t => t.JobId, cascadeDelete: true)
                .ForeignKey("dbo.CAPS_Employee", t => t.ProducerEmployeeId)
                .ForeignKey("dbo.CAPS_SalesRep", t => t.SalesRepId)
                .ForeignKey("dbo.CAPS_Employee", t => t.SetDesignerEmployeeId)
                .ForeignKey("dbo.CAPS_Customers", t => t.ThirdPartyCustomerId)
                .Index(t => t.JobId)
                .Index(t => t.ExecutiveProducerId)
                .Index(t => t.DirectorEmployeeId)
                .Index(t => t.ProducerEmployeeId)
                .Index(t => t.DirOfPhotoEmployeeId)
                .Index(t => t.SetDesignerEmployeeId)
                .Index(t => t.EditorEmployeeId)
                .Index(t => t.ArtDirectorEmployeeId)
                .Index(t => t.SalesRepId)
                .Index(t => t.AgencyId)
                .Index(t => t.AgencyClientCustomerId)
                .Index(t => t.ThirdPartyCustomerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_JobCommercial", "ThirdPartyCustomerId", "dbo.CAPS_Customers");
            DropForeignKey("dbo.CAPS_JobCommercial", "SetDesignerEmployeeId", "dbo.CAPS_Employee");
            DropForeignKey("dbo.CAPS_JobCommercial", "SalesRepId", "dbo.CAPS_SalesRep");
            DropForeignKey("dbo.CAPS_JobCommercial", "ProducerEmployeeId", "dbo.CAPS_Employee");
            DropForeignKey("dbo.CAPS_JobCommercial", "JobId", "dbo.CAPS_Job");
            DropForeignKey("dbo.CAPS_JobCommercial", "ExecutiveProducerId", "dbo.CAPS_Employee");
            DropForeignKey("dbo.CAPS_JobCommercial", "EditorEmployeeId", "dbo.CAPS_Employee");
            DropForeignKey("dbo.CAPS_JobCommercial", "DirOfPhotoEmployeeId", "dbo.CAPS_Employee");
            DropForeignKey("dbo.CAPS_JobCommercial", "DirectorEmployeeId", "dbo.CAPS_Employee");
            DropForeignKey("dbo.CAPS_JobCommercial", "ArtDirectorEmployeeId", "dbo.CAPS_Employee");
            DropForeignKey("dbo.CAPS_JobCommercial", "AgencyClientCustomerId", "dbo.CAPS_Customers");
            DropForeignKey("dbo.CAPS_JobCommercial", "AgencyId", "dbo.CAPS_Customers");
            DropIndex("dbo.CAPS_JobCommercial", new[] { "ThirdPartyCustomerId" });
            DropIndex("dbo.CAPS_JobCommercial", new[] { "AgencyClientCustomerId" });
            DropIndex("dbo.CAPS_JobCommercial", new[] { "AgencyId" });
            DropIndex("dbo.CAPS_JobCommercial", new[] { "SalesRepId" });
            DropIndex("dbo.CAPS_JobCommercial", new[] { "ArtDirectorEmployeeId" });
            DropIndex("dbo.CAPS_JobCommercial", new[] { "EditorEmployeeId" });
            DropIndex("dbo.CAPS_JobCommercial", new[] { "SetDesignerEmployeeId" });
            DropIndex("dbo.CAPS_JobCommercial", new[] { "DirOfPhotoEmployeeId" });
            DropIndex("dbo.CAPS_JobCommercial", new[] { "ProducerEmployeeId" });
            DropIndex("dbo.CAPS_JobCommercial", new[] { "DirectorEmployeeId" });
            DropIndex("dbo.CAPS_JobCommercial", new[] { "ExecutiveProducerId" });
            DropIndex("dbo.CAPS_JobCommercial", new[] { "JobId" });
            DropTable("dbo.CAPS_JobCommercial",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_JobCommercialUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_JobCommercialUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
