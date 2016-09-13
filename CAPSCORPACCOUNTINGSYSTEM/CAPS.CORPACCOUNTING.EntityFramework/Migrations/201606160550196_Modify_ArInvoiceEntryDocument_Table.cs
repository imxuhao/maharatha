namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modify_ArInvoiceEntryDocument_Table : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CAPS_ARInvoiceEntryDocument", "LocationSetId", c => c.Int());
            AlterColumn("dbo.CAPS_ARInvoiceEntryDocument", "ReversalDate", c => c.DateTime(storeType: "smalldatetime"));
            CreateIndex("dbo.CAPS_ARInvoiceEntryDocument", "LocationSetId");
            AddForeignKey("dbo.CAPS_ARInvoiceEntryDocument", "LocationSetId", "dbo.CAPS_LocationSet", "LocationSetId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_ARInvoiceEntryDocument", "LocationSetId", "dbo.CAPS_LocationSet");
            DropIndex("dbo.CAPS_ARInvoiceEntryDocument", new[] { "LocationSetId" });
            AlterColumn("dbo.CAPS_ARInvoiceEntryDocument", "ReversalDate", c => c.DateTime(nullable: false, storeType: "smalldatetime"));
            DropColumn("dbo.CAPS_ARInvoiceEntryDocument", "LocationSetId");
        }
    }
}
