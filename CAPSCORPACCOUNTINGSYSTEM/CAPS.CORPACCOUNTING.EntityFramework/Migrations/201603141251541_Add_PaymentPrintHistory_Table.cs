namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_PaymentPrintHistory_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_PaymentPrintHistory",
                c => new
                    {
                        PaymentPrintHistoryId = c.Long(nullable: false, identity: true),
                        PaymentAccountingDocumentId = c.Long(),
                        PaymentRequestId = c.Int(),
                        BankAccountId = c.Long(),
                        VendorId = c.Int(),
                        PayToName = c.String(maxLength: 500),
                        PaymentNumber = c.String(maxLength: 50),
                        PaymentDate = c.DateTime(storeType: "smalldatetime"),
                        Amount = c.Decimal(precision: 18, scale: 2),
                        PrintDate = c.DateTime(storeType: "smalldatetime"),
                        UserId = c.Int(),
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
                    { "DynamicFilter_PaymentPrintHistoryUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_PaymentPrintHistoryUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.PaymentPrintHistoryId)
                .ForeignKey("dbo.CAPS_BankAccount", t => t.BankAccountId)
                .ForeignKey("dbo.CAPS_Vendor", t => t.VendorId)
                .Index(t => t.BankAccountId)
                .Index(t => t.VendorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_PaymentPrintHistory", "VendorId", "dbo.CAPS_Vendor");
            DropForeignKey("dbo.CAPS_PaymentPrintHistory", "BankAccountId", "dbo.CAPS_BankAccount");
            DropIndex("dbo.CAPS_PaymentPrintHistory", new[] { "VendorId" });
            DropIndex("dbo.CAPS_PaymentPrintHistory", new[] { "BankAccountId" });
            DropTable("dbo.CAPS_PaymentPrintHistory",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_PaymentPrintHistoryUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_PaymentPrintHistoryUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
