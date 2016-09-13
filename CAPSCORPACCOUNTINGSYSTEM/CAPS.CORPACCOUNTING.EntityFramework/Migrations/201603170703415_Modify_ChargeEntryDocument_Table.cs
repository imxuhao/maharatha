namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modify_ChargeEntryDocument_Table : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.CAPS_ChargeEntryDocument", "UploadDocumentLogId");
            AddForeignKey("dbo.CAPS_ChargeEntryDocument", "UploadDocumentLogId", "dbo.CAPS_UploadDocumentLog", "UploadDocumentLogId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_ChargeEntryDocument", "UploadDocumentLogId", "dbo.CAPS_UploadDocumentLog");
            DropIndex("dbo.CAPS_ChargeEntryDocument", new[] { "UploadDocumentLogId" });
        }
    }
}
