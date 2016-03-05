namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modify_POEntryDocument_Table : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.CAPS_PurchaseOrderEntryDocument", "UploadDocumentLogId");
            AddForeignKey("dbo.CAPS_PurchaseOrderEntryDocument", "UploadDocumentLogId", "dbo.CAPS_UploadDocumentLog", "UploadDocumentLogId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_PurchaseOrderEntryDocument", "UploadDocumentLogId", "dbo.CAPS_UploadDocumentLog");
            DropIndex("dbo.CAPS_PurchaseOrderEntryDocument", new[] { "UploadDocumentLogId" });
        }
    }
}
