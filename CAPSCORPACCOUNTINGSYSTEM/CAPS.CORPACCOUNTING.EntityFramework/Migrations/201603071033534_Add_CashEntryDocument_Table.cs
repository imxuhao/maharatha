namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_CashEntryDocument_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_CashEntryDocument",
                c => new
                    {
                        AHTID = c.Int(nullable: false),
                        BatchId = c.Int(),
                        BankAccountId = c.Int(nullable: false),
                        ReconciliationId = c.Int(),
                        IsEnterable = c.Boolean(nullable: false),
                        SendingBankAccountId = c.Int(),
                        PettyCashAccountId = c.Long(nullable: false),
                        BatchInfo = c.String(),
                        IsReversed = c.Boolean(),
                        ReversedByUserId = c.Int(),
                        IsVoid = c.Boolean(),
                        IsVoidDateOriginal = c.Boolean(),
                        LinkedAccountingDocumentId = c.Long(),
                        ReissueBatchId = c.Int(),
                        ReissueVoidDate = c.Int(),
                        DepositTypeOfCategoryId = c.Int(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_CashEntryDocumentUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_CashEntryDocumentUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.AHTID)
                .ForeignKey("dbo.CAPS_AccountingHeaderTransactions", t => t.AHTID)
                .ForeignKey("dbo.CAPS_Batch", t => t.BatchId)
                .ForeignKey("dbo.CAPS_BankAccount", t => t.BankAccountId, cascadeDelete: true)
                .ForeignKey("dbo.CAPS_BankRecControl", t => t.ReconciliationId)
                .ForeignKey("dbo.CAPS_BankAccount", t => t.SendingBankAccountId)
                .ForeignKey("dbo.CAPS_PettyCashAccount", t => t.PettyCashAccountId, cascadeDelete: true)
                .ForeignKey("dbo.CAPS_Batch", t => t.ReissueBatchId)
                .Index(t => t.AHTID)
                .Index(t => t.BatchId)
                .Index(t => t.BankAccountId)
                .Index(t => t.ReconciliationId)
                .Index(t => t.SendingBankAccountId)
                .Index(t => t.PettyCashAccountId)
                .Index(t => t.ReissueBatchId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_CashEntryDocument", "ReissueBatchId", "dbo.CAPS_Batch");
            DropForeignKey("dbo.CAPS_CashEntryDocument", "PettyCashAccountId", "dbo.CAPS_PettyCashAccount");
            DropForeignKey("dbo.CAPS_CashEntryDocument", "SendingBankAccountId", "dbo.CAPS_BankAccount");
            DropForeignKey("dbo.CAPS_CashEntryDocument", "ReconciliationId", "dbo.CAPS_BankRecControl");
            DropForeignKey("dbo.CAPS_CashEntryDocument", "BankAccountId", "dbo.CAPS_BankAccount");
            DropForeignKey("dbo.CAPS_CashEntryDocument", "BatchId", "dbo.CAPS_Batch");
            DropForeignKey("dbo.CAPS_CashEntryDocument", "AHTID", "dbo.CAPS_AccountingHeaderTransactions");
            DropIndex("dbo.CAPS_CashEntryDocument", new[] { "ReissueBatchId" });
            DropIndex("dbo.CAPS_CashEntryDocument", new[] { "PettyCashAccountId" });
            DropIndex("dbo.CAPS_CashEntryDocument", new[] { "SendingBankAccountId" });
            DropIndex("dbo.CAPS_CashEntryDocument", new[] { "ReconciliationId" });
            DropIndex("dbo.CAPS_CashEntryDocument", new[] { "BankAccountId" });
            DropIndex("dbo.CAPS_CashEntryDocument", new[] { "BatchId" });
            DropIndex("dbo.CAPS_CashEntryDocument", new[] { "AHTID" });
            DropTable("dbo.CAPS_CashEntryDocument",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_CashEntryDocumentUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_CashEntryDocumentUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
