namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_JournalEntryDocument_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_JournalEntryDocument",
                c => new
                    {
                        AHTID = c.Int(nullable: false),
                        BatchId = c.Int(),
                        IsReversingEntry = c.Boolean(nullable: false),
                        IsRecurringEntry = c.Boolean(nullable: false),
                        BatchInfo = c.String(),
                        IsBatchRemoved = c.Boolean(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_JournalEntryDocumentUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_JournalEntryDocumentUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.AHTID)
                .ForeignKey("dbo.CAPS_AccountingHeaderTransactions", t => t.AHTID)
                .ForeignKey("dbo.CAPS_Batch", t => t.BatchId)
                .Index(t => t.AHTID)
                .Index(t => t.BatchId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_JournalEntryDocument", "BatchId", "dbo.CAPS_Batch");
            DropForeignKey("dbo.CAPS_JournalEntryDocument", "AHTID", "dbo.CAPS_AccountingHeaderTransactions");
            DropIndex("dbo.CAPS_JournalEntryDocument", new[] { "BatchId" });
            DropIndex("dbo.CAPS_JournalEntryDocument", new[] { "AHTID" });
            DropTable("dbo.CAPS_JournalEntryDocument",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_JournalEntryDocumentUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_JournalEntryDocumentUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
