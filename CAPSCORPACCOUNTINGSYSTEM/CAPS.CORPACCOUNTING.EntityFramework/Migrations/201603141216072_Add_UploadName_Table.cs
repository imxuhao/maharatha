namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_UploadName_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_UploadName",
                c => new
                    {
                        UploadNameId = c.Int(nullable: false, identity: true),
                        SelectedVendorId = c.Int(),
                        TypeOfUploadFileId = c.Int(nullable: false),
                        FirstName = c.String(maxLength: 100),
                        LastName = c.String(nullable: false, maxLength: 400),
                        PayToName = c.String(maxLength: 400),
                        DbaName = c.String(maxLength: 400),
                        TypeOfVendorId = c.Int(nullable: false),
                        WireInfo = c.String(maxLength: 50),
                        UploadIdNumber = c.String(maxLength: 50),
                        VendorAccount = c.String(maxLength: 50),
                        StudioVendorInfo = c.String(maxLength: 50),
                        TypeOfCurrencyId = c.Short(),
                        CreditLimit = c.Decimal(precision: 18, scale: 2),
                        PaymentTermId = c.Int(),
                        TypeOfPaymentMethodId = c.Int(),
                        FederalTaxId = c.String(maxLength: 50),
                        SsnTaxId = c.String(maxLength: 50),
                        IsCorporation = c.Boolean(nullable: false),
                        Is1099 = c.Boolean(nullable: false),
                        IsIndependantContractor = c.Boolean(nullable: false),
                        IsW9OnFIle = c.Boolean(nullable: false),
                        EddContractStartDate = c.DateTime(storeType: "smalldatetime"),
                        EddContractStopDate = c.DateTime(storeType: "smalldatetime"),
                        EddConctractAmount = c.Decimal(precision: 18, scale: 2),
                        IsEddContractOnGoing = c.Boolean(nullable: false),
                        WorkRegionId = c.Int(),
                        IsEnterable = c.Boolean(nullable: false),
                        TypeOf1099T4Id = c.Int(),
                        EntityId = c.Int(),
                        IsApproved = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        TypeOfInActiveStatusId = c.Int(),
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
                    { "DynamicFilter_UploadNameUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_UploadNameUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.UploadNameId)
                .ForeignKey("dbo.CAPS_TypeOfUploadFile", t => t.TypeOfUploadFileId, cascadeDelete: true)
                .Index(t => t.TypeOfUploadFileId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_UploadName", "TypeOfUploadFileId", "dbo.CAPS_TypeOfUploadFile");
            DropIndex("dbo.CAPS_UploadName", new[] { "TypeOfUploadFileId" });
            DropTable("dbo.CAPS_UploadName",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_UploadNameUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_UploadNameUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
