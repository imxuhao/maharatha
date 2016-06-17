namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modify_PayrollEntry_PurchaseOrderEntry_Tables : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CAPS_JobLocation", "LocationId", "dbo.CAPS_LocationSet");
            DropForeignKey("dbo.CAPS_ARInvoiceEntryDocument", "LocationSetId", "dbo.CAPS_LocationSet");
            DropIndex("dbo.CAPS_JobLocation", new[] { "LocationId" });
            DropIndex("dbo.CAPS_ARInvoiceEntryDocument", new[] { "LocationSetId" });
            AddColumn("dbo.CAPS_PurchaseOrderEntryDocument", "IsColse", c => c.Boolean());
            AddColumn("dbo.CAPS_PayrollEntryDocumentDetail", "OriginalAccountId", c => c.Long());
            AddColumn("dbo.CAPS_PayrollEntryDocumentDetail", "OriginalJobId", c => c.Int());
            CreateIndex("dbo.CAPS_PayrollEntryDocumentDetail", "OriginalAccountId");
            CreateIndex("dbo.CAPS_PayrollEntryDocumentDetail", "OriginalJobId");
            AddForeignKey("dbo.CAPS_PayrollEntryDocumentDetail", "OriginalAccountId", "dbo.CAPS_Account", "AccountId");
            AddForeignKey("dbo.CAPS_PayrollEntryDocumentDetail", "OriginalJobId", "dbo.CAPS_Job", "JobId");
            DropColumn("dbo.CAPS_ARInvoiceEntryDocument", "LocationSetId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CAPS_ARInvoiceEntryDocument", "LocationSetId", c => c.Int());
            DropForeignKey("dbo.CAPS_PayrollEntryDocumentDetail", "OriginalJobId", "dbo.CAPS_Job");
            DropForeignKey("dbo.CAPS_PayrollEntryDocumentDetail", "OriginalAccountId", "dbo.CAPS_Account");
            DropIndex("dbo.CAPS_PayrollEntryDocumentDetail", new[] { "OriginalJobId" });
            DropIndex("dbo.CAPS_PayrollEntryDocumentDetail", new[] { "OriginalAccountId" });
            DropColumn("dbo.CAPS_PayrollEntryDocumentDetail", "OriginalJobId");
            DropColumn("dbo.CAPS_PayrollEntryDocumentDetail", "OriginalAccountId");
            DropColumn("dbo.CAPS_PurchaseOrderEntryDocument", "IsColse");
            CreateIndex("dbo.CAPS_ARInvoiceEntryDocument", "LocationSetId");
            CreateIndex("dbo.CAPS_JobLocation", "LocationId");
            AddForeignKey("dbo.CAPS_ARInvoiceEntryDocument", "LocationSetId", "dbo.CAPS_LocationSet", "LocationSetId");
            AddForeignKey("dbo.CAPS_JobLocation", "LocationId", "dbo.CAPS_LocationSet", "LocationSetId", cascadeDelete: true);
        }
    }
}
