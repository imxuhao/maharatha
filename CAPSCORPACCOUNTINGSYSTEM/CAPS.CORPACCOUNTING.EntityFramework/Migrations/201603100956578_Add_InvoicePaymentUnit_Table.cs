namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_InvoicePaymentUnit_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_InvoicePayment",
                c => new
                    {
                        InvoicePaymentId = c.Long(nullable: false, identity: true),
                        VendorId = c.Int(),
                        InvoiceAccountingDocumentId = c.Long(nullable: false),
                        PaymentAccountingDocumentId = c.Long(),
                        PaymentRequestId = c.Int(),
                        TypeOfCheckGroupId = c.Int(),
                        TypeOfCurrencyId = c.Short(),
                        TypeOfPaymentMethodId = c.Int(),
                        IsPaymentSelected = c.Boolean(nullable: false),
                        InvoicePaymentAmount = c.Decimal(precision: 18, scale: 2),
                        SelectedPaymentAmount = c.Decimal(precision: 18, scale: 2),
                        PaymentNumber = c.String(maxLength: 50),
                        DiscountDate = c.DateTime(storeType: "smalldatetime"),
                        DiscountAmount = c.Decimal(precision: 18, scale: 2),
                        ProcessDate = c.DateTime(storeType: "smalldatetime"),
                        IsGeneratedDiscount = c.Boolean(),
                        DiscountLinkInvoicePaymentId = c.Long(),
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
                    { "DynamicFilter_InvoicePaymentUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_InvoicePaymentUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.InvoicePaymentId)
                .ForeignKey("dbo.CAPS_Vendor", t => t.VendorId)
                .Index(t => t.VendorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_InvoicePayment", "VendorId", "dbo.CAPS_Vendor");
            DropIndex("dbo.CAPS_InvoicePayment", new[] { "VendorId" });
            DropTable("dbo.CAPS_InvoicePayment",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_InvoicePaymentUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_InvoicePaymentUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
