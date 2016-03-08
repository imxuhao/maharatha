namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_UploadAddress_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_UploadAddress",
                c => new
                    {
                        AddressId = c.Int(nullable: false, identity: true),
                        TypeOfUploadFileId = c.Int(nullable: false),
                        TypeOfAddressId = c.Int(nullable: false),
                        TypeOfObjectId = c.Int(nullable: false),
                        ObjectId = c.Int(nullable: false),
                        ContactInfo = c.String(),
                        FirstAddress = c.String(),
                        SecondAddress = c.String(),
                        ThirdAddress = c.String(),
                        FourthAddress = c.String(),
                        City = c.String(),
                        Area = c.String(),
                        RegionId = c.Int(),
                        PostalCode = c.String(),
                        TypeOfCountryId = c.Short(),
                        IsPrimary = c.Boolean(nullable: false),
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
                    { "DynamicFilter_UploadAddressUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_UploadAddressUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.AddressId)
                .ForeignKey("dbo.CAPS_Region", t => t.RegionId)
                .ForeignKey("dbo.CAPS_TypeOfCountry", t => t.TypeOfCountryId)
                .ForeignKey("dbo.CAPS_TypeOfUploadFile", t => t.TypeOfUploadFileId, cascadeDelete: true)
                .Index(t => t.TypeOfUploadFileId)
                .Index(t => t.RegionId)
                .Index(t => t.TypeOfCountryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_UploadAddress", "TypeOfUploadFileId", "dbo.CAPS_TypeOfUploadFile");
            DropForeignKey("dbo.CAPS_UploadAddress", "TypeOfCountryId", "dbo.CAPS_TypeOfCountry");
            DropForeignKey("dbo.CAPS_UploadAddress", "RegionId", "dbo.CAPS_Region");
            DropIndex("dbo.CAPS_UploadAddress", new[] { "TypeOfCountryId" });
            DropIndex("dbo.CAPS_UploadAddress", new[] { "RegionId" });
            DropIndex("dbo.CAPS_UploadAddress", new[] { "TypeOfUploadFileId" });
            DropTable("dbo.CAPS_UploadAddress",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_UploadAddressUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_UploadAddressUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
