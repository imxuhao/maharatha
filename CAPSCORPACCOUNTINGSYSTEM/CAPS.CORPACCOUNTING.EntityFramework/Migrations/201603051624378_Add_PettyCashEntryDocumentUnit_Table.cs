namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_PettyCashEntryDocumentUnit_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_PettyCashEntryDocument",
                c => new
                    {
                        AHTID = c.Int(nullable: false),
                        BatchId = c.Int(),
                        PettyCashAccountId = c.Int(nullable: false),
                        UploadDocumentLogId = c.Int(),
                        ReimbursementAmount = c.Decimal(precision: 18, scale: 2),
                        BatchInfo = c.String(),
                        AdvanceAmount = c.Decimal(precision: 18, scale: 2),
                        BankAccountId = c.Int(),
                        TypeOfPaymentMethodId = c.Int(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_PettyCashEntryDocumentUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_PettyCashEntryDocumentUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.AHTID)
                .ForeignKey("dbo.CAPS_AccountingHeaderTransactions", t => t.AHTID)
                .ForeignKey("dbo.CAPS_Batch", t => t.BatchId)
                .ForeignKey("dbo.CAPS_PettyCashAccount", t => t.PettyCashAccountId, cascadeDelete: true)
                .ForeignKey("dbo.CAPS_UploadDocumentLog", t => t.UploadDocumentLogId)
                .ForeignKey("dbo.CAPS_BankAccount", t => t.BankAccountId)
                .Index(t => t.AHTID)
                .Index(t => t.BatchId)
                .Index(t => t.PettyCashAccountId)
                .Index(t => t.UploadDocumentLogId)
                .Index(t => t.BankAccountId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_PettyCashEntryDocument", "BankAccountId", "dbo.CAPS_BankAccount");
            DropForeignKey("dbo.CAPS_PettyCashEntryDocument", "UploadDocumentLogId", "dbo.CAPS_UploadDocumentLog");
            DropForeignKey("dbo.CAPS_PettyCashEntryDocument", "PettyCashAccountId", "dbo.CAPS_PettyCashAccount");
            DropForeignKey("dbo.CAPS_PettyCashEntryDocument", "BatchId", "dbo.CAPS_Batch");
            DropForeignKey("dbo.CAPS_PettyCashEntryDocument", "AHTID", "dbo.CAPS_AccountingHeaderTransactions");
            DropIndex("dbo.CAPS_PettyCashEntryDocument", new[] { "BankAccountId" });
            DropIndex("dbo.CAPS_PettyCashEntryDocument", new[] { "UploadDocumentLogId" });
            DropIndex("dbo.CAPS_PettyCashEntryDocument", new[] { "PettyCashAccountId" });
            DropIndex("dbo.CAPS_PettyCashEntryDocument", new[] { "BatchId" });
            DropIndex("dbo.CAPS_PettyCashEntryDocument", new[] { "AHTID" });
            DropTable("dbo.CAPS_PettyCashEntryDocument",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_PettyCashEntryDocumentUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_PettyCashEntryDocumentUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
