namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_PaymentRequestHistoty_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_PaymentRequestHistory",
                c => new
                    {
                        PaymentRequestId = c.Int(nullable: false, identity: true),
                        TypeOfAccountingDocumentId = c.Int(nullable: false),
                        BankAccountId = c.Int(),
                        VendorId = c.Int(),
                        TypeOfPaymentMethodId = c.Int(),
                        PaymentDate = c.DateTime(),
                        StartingPaymentNumber = c.String(maxLength: 50),
                        IsCheckPrinted = c.Boolean(nullable: false),
                        IsRegisterPrinted = c.Boolean(nullable: false),
                        IsPosted = c.Boolean(nullable: false),
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
                    { "DynamicFilter_PaymentRequestHistotyUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_PaymentRequestHistotyUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.PaymentRequestId)
                .ForeignKey("dbo.CAPS_BankAccount", t => t.BankAccountId)
                .ForeignKey("dbo.CAPS_Vendors", t => t.VendorId)
                .Index(t => t.BankAccountId)
                .Index(t => t.VendorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_PaymentRequestHistory", "VendorId", "dbo.CAPS_Vendors");
            DropForeignKey("dbo.CAPS_PaymentRequestHistory", "BankAccountId", "dbo.CAPS_BankAccount");
            DropIndex("dbo.CAPS_PaymentRequestHistory", new[] { "VendorId" });
            DropIndex("dbo.CAPS_PaymentRequestHistory", new[] { "BankAccountId" });
            DropTable("dbo.CAPS_PaymentRequestHistory",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_PaymentRequestHistotyUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_PaymentRequestHistotyUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
