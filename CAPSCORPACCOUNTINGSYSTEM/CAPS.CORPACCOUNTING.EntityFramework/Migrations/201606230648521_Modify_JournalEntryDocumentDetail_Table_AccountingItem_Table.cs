namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modify_JournalEntryDocumentDetail_Table_AccountingItem_Table : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.CAPS_AccountingItem", name: "AccountingItemOrigId", newName: "SplitAccountingItemId");
            RenameIndex(table: "dbo.CAPS_AccountingItem", name: "IX_AccountingItemOrigId", newName: "IX_SplitAccountingItemId");
            AddColumn("dbo.CAPS_JournalEntryDocumentDetail", "DebitAccountingItemId", c => c.Long());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CAPS_JournalEntryDocumentDetail", "DebitAccountingItemId");
            RenameIndex(table: "dbo.CAPS_AccountingItem", name: "IX_SplitAccountingItemId", newName: "IX_AccountingItemOrigId");
            RenameColumn(table: "dbo.CAPS_AccountingItem", name: "SplitAccountingItemId", newName: "AccountingItemOrigId");
        }
    }
}
