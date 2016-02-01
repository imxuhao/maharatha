namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Vendor_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_Vendors",
                c => new
                    {
                        VendorId = c.Int(nullable: false, identity: true),
                        LastName = c.String(nullable: false, maxLength: 100),
                        FirstName = c.String(maxLength: 100),
                        PayToName = c.String(maxLength: 100),
                        DbaName = c.String(maxLength: 100),
                        VendorNumber = c.String(maxLength: 50),
                        VendorAccountInfo = c.String(maxLength: 100),
                        FedralTaxId = c.String(maxLength: 15),
                        SSNTaxId = c.String(maxLength: 15),
                        CreditLimit = c.Decimal(precision: 18, scale: 2),
                        TypeofPaymentMethod = c.Int(nullable: false),
                        PaymentTermsId = c.Int(),
                        TypeofCurrency = c.String(maxLength: 20),
                        IsCorporation = c.Boolean(nullable: false),
                        Is1099 = c.Boolean(nullable: false),
                        IsIndependentContractor = c.Boolean(nullable: false),
                        Isw9OnFile = c.Boolean(nullable: false),
                        TypeofVendorId = c.Int(nullable: false),
                        Typeof1099Box = c.Int(nullable: false),
                        EDDContractStartDate = c.DateTime(),
                        EDDContractStopDate = c.DateTime(),
                        EDDConctractAmount = c.Decimal(precision: 18, scale: 2),
                        WorkRegion = c.String(maxLength: 10),
                        IsEDDContractOnGoing = c.Boolean(nullable: false),
                        ACHBankName = c.String(maxLength: 100),
                        ACHRoutingNumber = c.String(maxLength: 20),
                        ACHAccountNumber = c.String(maxLength: 20),
                        ACHWireFromBankName = c.String(maxLength: 100),
                        ACHWireFromBankAddress = c.String(maxLength: 100),
                        ACHWireFromSwiftCode = c.String(maxLength: 12),
                        ACHWireFromAccountNumber = c.String(maxLength: 12),
                        ACHWireToBankName = c.String(maxLength: 100),
                        ACHWireToSwiftCode = c.String(maxLength: 12),
                        ACHWireToBeneficiary = c.String(maxLength: 100),
                        ACHWireToAccountNumber = c.String(maxLength: 20),
                        ACHWireToIBAN = c.String(maxLength: 30),
                        IsActive = c.Boolean(nullable: false,defaultValue:true),
                        IsApproved = c.Boolean(nullable: false,defaultValue:true),
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
                    { "DynamicFilter_VendorUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_VendorUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.VendorId)
                .ForeignKey("dbo.Caps_VendorPaymentTerms", t => t.PaymentTermsId)
                .Index(t => t.PaymentTermsId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Caps_Vendors", "PaymentTermsId", "dbo.Caps_VendorPaymentTerms");
            DropIndex("dbo.Caps_Vendors", new[] { "PaymentTermsId" });
            DropTable("dbo.Caps_Vendors",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_VendorUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_VendorUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
