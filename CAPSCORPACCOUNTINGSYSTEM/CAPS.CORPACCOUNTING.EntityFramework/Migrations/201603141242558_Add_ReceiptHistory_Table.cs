namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_ReceiptHistory_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_ReceiptHistory",
                c => new
                    {
                        ReceiptHistoryId = c.Int(nullable: false, identity: true),
                        ReceiptAccountingItemId = c.Long(),
                        Description = c.String(maxLength: 400),
                        DepositReference = c.String(maxLength: 100),
                        DepositDate = c.DateTime(storeType: "smalldatetime"),
                        CustomerId = c.Int(nullable: false),
                        BankAccountId = c.Long(),
                        ArytdInvoiceId = c.Int(),
                        ArBillingTypeId = c.Int(),
                        AccountId = c.Long(),
                        JobId = c.Int(),
                        ReceiptAmount = c.Decimal(precision: 18, scale: 2),
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
                    { "DynamicFilter_ReceiptHistoryUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_ReceiptHistoryUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.ReceiptHistoryId)
                .ForeignKey("dbo.CAPS_Account", t => t.AccountId)
                .ForeignKey("dbo.CAPS_ARBillingType", t => t.ArBillingTypeId)
                .ForeignKey("dbo.CAPS_BankAccount", t => t.BankAccountId)
                .ForeignKey("dbo.CAPS_Customer", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.CAPS_Job", t => t.JobId)
                .Index(t => t.CustomerId)
                .Index(t => t.BankAccountId)
                .Index(t => t.ArBillingTypeId)
                .Index(t => t.AccountId)
                .Index(t => t.JobId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_ReceiptHistory", "JobId", "dbo.CAPS_Job");
            DropForeignKey("dbo.CAPS_ReceiptHistory", "CustomerId", "dbo.CAPS_Customer");
            DropForeignKey("dbo.CAPS_ReceiptHistory", "BankAccountId", "dbo.CAPS_BankAccount");
            DropForeignKey("dbo.CAPS_ReceiptHistory", "ArBillingTypeId", "dbo.CAPS_ARBillingType");
            DropForeignKey("dbo.CAPS_ReceiptHistory", "AccountId", "dbo.CAPS_Account");
            DropIndex("dbo.CAPS_ReceiptHistory", new[] { "JobId" });
            DropIndex("dbo.CAPS_ReceiptHistory", new[] { "AccountId" });
            DropIndex("dbo.CAPS_ReceiptHistory", new[] { "ArBillingTypeId" });
            DropIndex("dbo.CAPS_ReceiptHistory", new[] { "BankAccountId" });
            DropIndex("dbo.CAPS_ReceiptHistory", new[] { "CustomerId" });
            DropTable("dbo.CAPS_ReceiptHistory",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ReceiptHistoryUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_ReceiptHistoryUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
