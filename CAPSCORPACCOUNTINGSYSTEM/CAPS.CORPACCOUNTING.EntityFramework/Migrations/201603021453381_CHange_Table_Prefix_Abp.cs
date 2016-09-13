namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class CHange_Table_Prefix_Abp : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.AbpAuditLogs", newName: "CAPS_AuditLogs");
            RenameTable(name: "dbo.AbpBackgroundJobs", newName: "CAPS_BackgroundJobs");
            RenameTable(name: "dbo.AbpFeatures", newName: "CAPS_Features");
            RenameTable(name: "dbo.AbpEditions", newName: "CAPS_Editions");
            RenameTable(name: "dbo.AbpOrganizationUnits", newName: "CAPS_OrganizationUnits");
            RenameTable(name: "dbo.AbpLanguages", newName: "CAPS_Languages");
            RenameTable(name: "dbo.AbpLanguageTexts", newName: "CAPS_LanguageTexts");
            RenameTable(name: "dbo.AbpNotifications", newName: "CAPS_Notifications");
            RenameTable(name: "dbo.AbpNotificationSubscriptions", newName: "CAPS_NotificationSubscriptions");
            RenameTable(name: "dbo.AbpPermissions", newName: "CAPS_Permissions");
            RenameTable(name: "dbo.AbpRoles", newName: "CAPS_Roles");
            RenameTable(name: "dbo.AbpUsers", newName: "CAPS_Users");
            RenameTable(name: "dbo.AbpUserLogins", newName: "CAPS_UserLogins");
            RenameTable(name: "dbo.AbpUserRoles", newName: "CAPS_UserRoles");
            RenameTable(name: "dbo.AbpSettings", newName: "CAPS_Settings");
            RenameTable(name: "dbo.AbpTenants", newName: "CAPS_Tenant");
            RenameTable(name: "dbo.AbpUserNotifications", newName: "CAPS_UserNotifications");
            RenameTable(name: "dbo.AbpUserOrganizationUnits", newName: "CAPS_UserOrganizationUnits");
            DropForeignKey("dbo.CAPS_InvoiceEntryDocument", "AHTID", "dbo.CAPS_AccountingHeaderTransactions");
            DropIndex("dbo.CAPS_InvoiceEntryDocument", new[] { "AHTID" });
            CreateIndex("dbo.CAPS_PayrollEntryDocument", "BatchId");
            CreateIndex("dbo.CAPS_PayrollEntryDocument", "VendorId");
            AddForeignKey("dbo.CAPS_PayrollEntryDocument", "BatchId", "dbo.CAPS_Batch", "BatchId");
            AddForeignKey("dbo.CAPS_PayrollEntryDocument", "VendorId", "dbo.CAPS_Vendors", "VendorId");
            DropColumn("dbo.CAPS_PayrollEntryDocument", "ReversalDate");
            DropTable("dbo.CAPS_InvoiceEntryDocument",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_InvoiceEntryDocumentUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_InvoiceEntryDocumentUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CAPS_InvoiceEntryDocument",
                c => new
                    {
                        AHTID = c.Int(nullable: false),
                        BatchId = c.Int(),
                        VendorId = c.Int(),
                        TypeOfInvoiceId = c.Int(nullable: false),
                        PettyCashAccountId = c.Int(),
                        PaymentTermId = c.Int(),
                        TypeOfCheckGroupId = c.Int(),
                        BankAccountId = c.Int(),
                        PaymentDate = c.DateTime(storeType: "smalldatetime"),
                        PaymentNumber = c.String(),
                        PurchaseOrderReference = c.String(maxLength: 100),
                        ReversedByUserId = c.Int(),
                        ReversalDate = c.DateTime(storeType: "smalldatetime"),
                        IsInvoiceHistory = c.Boolean(nullable: false),
                        IsEnterable = c.Boolean(nullable: false),
                        GeneratedAccountingDocumentId = c.Long(),
                        UploadDocumentLogID = c.Int(),
                        BatchInfo = c.String(),
                        PaymentSelectedByUserId = c.Int(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_InvoiceEntryDocumentUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_InvoiceEntryDocumentUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.AHTID);
            
            AddColumn("dbo.CAPS_PayrollEntryDocument", "ReversalDate", c => c.DateTime(storeType: "smalldatetime"));
            DropForeignKey("dbo.CAPS_PayrollEntryDocument", "VendorId", "dbo.CAPS_Vendors");
            DropForeignKey("dbo.CAPS_PayrollEntryDocument", "BatchId", "dbo.CAPS_Batch");
            DropIndex("dbo.CAPS_PayrollEntryDocument", new[] { "VendorId" });
            DropIndex("dbo.CAPS_PayrollEntryDocument", new[] { "BatchId" });
            CreateIndex("dbo.CAPS_InvoiceEntryDocument", "AHTID");
            AddForeignKey("dbo.CAPS_InvoiceEntryDocument", "AHTID", "dbo.CAPS_AccountingHeaderTransactions", "AHTID");
            RenameTable(name: "dbo.CAPS_UserOrganizationUnits", newName: "AbpUserOrganizationUnits");
            RenameTable(name: "dbo.CAPS_UserNotifications", newName: "AbpUserNotifications");
            RenameTable(name: "dbo.CAPS_Tenant", newName: "AbpTenants");
            RenameTable(name: "dbo.CAPS_Settings", newName: "AbpSettings");
            RenameTable(name: "dbo.CAPS_UserRoles", newName: "AbpUserRoles");
            RenameTable(name: "dbo.CAPS_UserLogins", newName: "AbpUserLogins");
            RenameTable(name: "dbo.CAPS_Users", newName: "AbpUsers");
            RenameTable(name: "dbo.CAPS_Roles", newName: "AbpRoles");
            RenameTable(name: "dbo.CAPS_Permissions", newName: "AbpPermissions");
            RenameTable(name: "dbo.CAPS_NotificationSubscriptions", newName: "AbpNotificationSubscriptions");
            RenameTable(name: "dbo.CAPS_Notifications", newName: "AbpNotifications");
            RenameTable(name: "dbo.CAPS_LanguageTexts", newName: "AbpLanguageTexts");
            RenameTable(name: "dbo.CAPS_Languages", newName: "AbpLanguages");
            RenameTable(name: "dbo.CAPS_OrganizationUnits", newName: "AbpOrganizationUnits");
            RenameTable(name: "dbo.CAPS_Editions", newName: "AbpEditions");
            RenameTable(name: "dbo.CAPS_Features", newName: "AbpFeatures");
            RenameTable(name: "dbo.CAPS_BackgroundJobs", newName: "AbpBackgroundJobs");
            RenameTable(name: "dbo.CAPS_AuditLogs", newName: "AbpAuditLogs");
        }
    }
}
