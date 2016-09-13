namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modify_AcconttingHeaderDocument_APHeader_Table : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.CAPS_AccountingHeaderTransactions", newName: "CAPS_AccountingDocument");
            RenameTable(name: "dbo.CAPS_APHeaderTransactions", newName: "CAPS_InvoiceEntryDocument");
            RenameColumn(table: "dbo.CAPS_AccountingDocument", name: "AHTID", newName: "AccountingDocumentId");
            RenameColumn(table: "dbo.CAPS_ARInvoiceEntryDocument", name: "AHTID", newName: "AccountingDocumentId");
            RenameColumn(table: "dbo.CAPS_CashEntryDocument", name: "AHTID", newName: "AccountingDocumentId");
            RenameColumn(table: "dbo.CAPS_ChargeEntryDocument", name: "AHTID", newName: "AccountingDocumentId");
            RenameColumn(table: "dbo.CAPS_JournalEntryDocument", name: "AHTID", newName: "AccountingDocumentId");
            RenameColumn(table: "dbo.CAPS_InvoiceEntryDocument", name: "AHTID", newName: "AccountingDocumentId");
            RenameColumn(table: "dbo.CAPS_PaymentEntryDocument", name: "AHTID", newName: "AccountingDocumentId");
            RenameColumn(table: "dbo.CAPS_PayrollEntryDocument", name: "AHTID", newName: "AccountingDocumentId");
            RenameColumn(table: "dbo.CAPS_PettyCashEntryDocument", name: "AHTID", newName: "AccountingDocumentId");
            RenameColumn(table: "dbo.CAPS_PurchaseOrderEntryDocument", name: "AHTID", newName: "AccountingDocumentId");
            RenameIndex(table: "dbo.CAPS_InvoiceEntryDocument", name: "IX_AHTID", newName: "IX_AccountingDocumentId");
            RenameIndex(table: "dbo.CAPS_CashEntryDocument", name: "IX_AHTID", newName: "IX_AccountingDocumentId");
            RenameIndex(table: "dbo.CAPS_PaymentEntryDocument", name: "IX_AHTID", newName: "IX_AccountingDocumentId");
            RenameIndex(table: "dbo.CAPS_PayrollEntryDocument", name: "IX_AHTID", newName: "IX_AccountingDocumentId");
            RenameIndex(table: "dbo.CAPS_PurchaseOrderEntryDocument", name: "IX_AHTID", newName: "IX_AccountingDocumentId");
            RenameIndex(table: "dbo.CAPS_ARInvoiceEntryDocument", name: "IX_AHTID", newName: "IX_AccountingDocumentId");
            RenameIndex(table: "dbo.CAPS_ChargeEntryDocument", name: "IX_AHTID", newName: "IX_AccountingDocumentId");
            RenameIndex(table: "dbo.CAPS_JournalEntryDocument", name: "IX_AHTID", newName: "IX_AccountingDocumentId");
            RenameIndex(table: "dbo.CAPS_PettyCashEntryDocument", name: "IX_AHTID", newName: "IX_AccountingDocumentId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.CAPS_PettyCashEntryDocument", name: "IX_AccountingDocumentId", newName: "IX_AHTID");
            RenameIndex(table: "dbo.CAPS_JournalEntryDocument", name: "IX_AccountingDocumentId", newName: "IX_AHTID");
            RenameIndex(table: "dbo.CAPS_ChargeEntryDocument", name: "IX_AccountingDocumentId", newName: "IX_AHTID");
            RenameIndex(table: "dbo.CAPS_ARInvoiceEntryDocument", name: "IX_AccountingDocumentId", newName: "IX_AHTID");
            RenameIndex(table: "dbo.CAPS_PurchaseOrderEntryDocument", name: "IX_AccountingDocumentId", newName: "IX_AHTID");
            RenameIndex(table: "dbo.CAPS_PayrollEntryDocument", name: "IX_AccountingDocumentId", newName: "IX_AHTID");
            RenameIndex(table: "dbo.CAPS_PaymentEntryDocument", name: "IX_AccountingDocumentId", newName: "IX_AHTID");
            RenameIndex(table: "dbo.CAPS_CashEntryDocument", name: "IX_AccountingDocumentId", newName: "IX_AHTID");
            RenameIndex(table: "dbo.CAPS_InvoiceEntryDocument", name: "IX_AccountingDocumentId", newName: "IX_AHTID");
            RenameColumn(table: "dbo.CAPS_PurchaseOrderEntryDocument", name: "AccountingDocumentId", newName: "AHTID");
            RenameColumn(table: "dbo.CAPS_PettyCashEntryDocument", name: "AccountingDocumentId", newName: "AHTID");
            RenameColumn(table: "dbo.CAPS_PayrollEntryDocument", name: "AccountingDocumentId", newName: "AHTID");
            RenameColumn(table: "dbo.CAPS_PaymentEntryDocument", name: "AccountingDocumentId", newName: "AHTID");
            RenameColumn(table: "dbo.CAPS_InvoiceEntryDocument", name: "AccountingDocumentId", newName: "AHTID");
            RenameColumn(table: "dbo.CAPS_JournalEntryDocument", name: "AccountingDocumentId", newName: "AHTID");
            RenameColumn(table: "dbo.CAPS_ChargeEntryDocument", name: "AccountingDocumentId", newName: "AHTID");
            RenameColumn(table: "dbo.CAPS_CashEntryDocument", name: "AccountingDocumentId", newName: "AHTID");
            RenameColumn(table: "dbo.CAPS_ARInvoiceEntryDocument", name: "AccountingDocumentId", newName: "AHTID");
            RenameColumn(table: "dbo.CAPS_AccountingDocument", name: "AccountingDocumentId", newName: "AHTID");
            RenameTable(name: "dbo.CAPS_InvoiceEntryDocument", newName: "CAPS_APHeaderTransactions");
            RenameTable(name: "dbo.CAPS_AccountingDocument", newName: "CAPS_AccountingHeaderTransactions");
        }
    }
}
