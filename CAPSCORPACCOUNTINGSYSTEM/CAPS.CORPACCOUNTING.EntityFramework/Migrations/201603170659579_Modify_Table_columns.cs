namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modify_Table_columns : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CAPS_ChargeEntryDocument", "UploadDocumentLog_Id", "dbo.CAPS_UploadDocumentLog");
            DropForeignKey("dbo.CAPS_CashEntryDocument", "PettyCashAccountId", "dbo.CAPS_PettyCashAccount");
            DropIndex("dbo.CAPS_CashEntryDocument", new[] { "PettyCashAccountId" });
            DropIndex("dbo.CAPS_ChargeEntryDocument", new[] { "UploadDocumentLog_Id" });
            DropPrimaryKey("dbo.CAPS_ApprovedSOX");
            DropPrimaryKey("dbo.CAPS_AttachedObject");
            AddColumn("dbo.CAPS_AccountingDocument", "DocumentDate", c => c.DateTime(storeType: "smalldatetime"));
            AddColumn("dbo.CAPS_AccountingDocument", "TransactionDate", c => c.DateTime(nullable: false, storeType: "smalldatetime"));
            AddColumn("dbo.CAPS_AccountingDocument", "DatePosted", c => c.DateTime(storeType: "smalldatetime"));
            AddColumn("dbo.CAPS_ARInvoiceEntryDocument", "ReversalDate", c => c.DateTime(nullable: false, storeType: "smalldatetime"));
            AddColumn("dbo.CAPS_CashEntryDocument", "ReversalDate", c => c.DateTime(storeType: "smalldatetime"));
            AddColumn("dbo.CAPS_JournalEntryDocument", "DateOfReversal", c => c.DateTime(storeType: "smalldatetime"));
            AddColumn("dbo.CAPS_JournalEntryDocument", "DateToRecur", c => c.DateTime(storeType: "smalldatetime"));
            AddColumn("dbo.CAPS_JournalEntryDocument", "FinalDate", c => c.DateTime(storeType: "smalldatetime"));
            AddColumn("dbo.CAPS_JournalEntryDocument", "LastPostDate", c => c.DateTime(storeType: "smalldatetime"));
            AddColumn("dbo.CAPS_InvoiceEntryDocument", "PaymentDate", c => c.DateTime(storeType: "smalldatetime"));
            AddColumn("dbo.CAPS_InvoiceEntryDocument", "ReversalDate", c => c.DateTime(storeType: "smalldatetime"));
            AddColumn("dbo.CAPS_PayrollEntryDocument", "ReversalDate", c => c.DateTime(storeType: "smalldatetime"));
            AlterColumn("dbo.CAPS_CashEntryDocument", "PettyCashAccountId", c => c.Long());
            AlterColumn("dbo.CAPS_ChargeEntryDocument", "UploadDocumentLogId", c => c.Long());
            AlterColumn("dbo.CAPS_BankAccount", "BankAccountNumber", c => c.String(maxLength: 200));
            AlterColumn("dbo.CAPS_BankAccount", "RoutingNumber", c => c.String(maxLength: 200));
            AlterColumn("dbo.CAPS_BankAccount", "CCFootNote", c => c.String());
            AlterColumn("dbo.CAPS_TypeOfUploadFile", "SecureAccessCategoryIdAssignedByUser", c => c.Short());
            AlterColumn("dbo.CAPS_TypeOfCheckStock", "Notes", c => c.String());
            AlterColumn("dbo.CAPS_PettyCashAccount", "PayToName", c => c.String(maxLength: 400));
            AlterColumn("dbo.CAPS_SubAccount", "Description", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.CAPS_TypeOfAccountingLayout", "DescriptionInternalUseOnly", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.CAPS_TypeOfAccountingLayout", "Notes", c => c.String());
            AlterColumn("dbo.CAPS_TypeOfHeading", "Description", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.CAPS_AccountingLayout", "Description", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("dbo.CAPS_ApprovedSOX", "ApprovedSOXID", c => c.Long(nullable: false, identity: true));
            AlterColumn("dbo.CAPS_AttachedObject", "AttachedObjectId", c => c.Long(nullable: false, identity: true));
            AlterColumn("dbo.CAPS_AttachedObject", "AttachedDate", c => c.DateTime(nullable: false, storeType: "smalldatetime"));
            AlterColumn("dbo.CAPS_BankRecCleared", "UploadInfo", c => c.String());
            AlterColumn("dbo.CAPS_TypeOfProfitCalc", "Description", c => c.String(maxLength: 400));
            AlterColumn("dbo.CAPS_TaxRebate", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.CAPS_TypeOfSeverityLevel", "Description", c => c.String(maxLength: 50));
            AlterColumn("dbo.CAPS_WorkSheet1099", "SsnTaxId", c => c.String(maxLength: 50));
            AddPrimaryKey("dbo.CAPS_ApprovedSOX", "ApprovedSOXID");
            AddPrimaryKey("dbo.CAPS_AttachedObject", "AttachedObjectId");
            CreateIndex("dbo.CAPS_CashEntryDocument", "PettyCashAccountId");
            AddForeignKey("dbo.CAPS_CashEntryDocument", "PettyCashAccountId", "dbo.CAPS_PettyCashAccount", "PettyCashAccountId");
            DropColumn("dbo.CAPS_ChargeEntryDocument", "UploadDocumentLog_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CAPS_ChargeEntryDocument", "UploadDocumentLog_Id", c => c.Long());
            DropForeignKey("dbo.CAPS_CashEntryDocument", "PettyCashAccountId", "dbo.CAPS_PettyCashAccount");
            DropIndex("dbo.CAPS_CashEntryDocument", new[] { "PettyCashAccountId" });
            DropPrimaryKey("dbo.CAPS_AttachedObject");
            DropPrimaryKey("dbo.CAPS_ApprovedSOX");
            AlterColumn("dbo.CAPS_WorkSheet1099", "SsnTaxId", c => c.String());
            AlterColumn("dbo.CAPS_TypeOfSeverityLevel", "Description", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.CAPS_TaxRebate", "Description", c => c.String());
            AlterColumn("dbo.CAPS_TypeOfProfitCalc", "Description", c => c.String(maxLength: 100));
            AlterColumn("dbo.CAPS_BankRecCleared", "UploadInfo", c => c.String(maxLength: 400));
            AlterColumn("dbo.CAPS_AttachedObject", "AttachedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.CAPS_AttachedObject", "AttachedObjectId", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.CAPS_ApprovedSOX", "ApprovedSOXID", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.CAPS_AccountingLayout", "Description", c => c.String(maxLength: 500));
            AlterColumn("dbo.CAPS_TypeOfHeading", "Description", c => c.String(maxLength: 100));
            AlterColumn("dbo.CAPS_TypeOfAccountingLayout", "Notes", c => c.String(maxLength: 500));
            AlterColumn("dbo.CAPS_TypeOfAccountingLayout", "DescriptionInternalUseOnly", c => c.String(maxLength: 100));
            AlterColumn("dbo.CAPS_SubAccount", "Description", c => c.String(maxLength: 100));
            AlterColumn("dbo.CAPS_PettyCashAccount", "PayToName", c => c.String(maxLength: 200));
            AlterColumn("dbo.CAPS_TypeOfCheckStock", "Notes", c => c.String(maxLength: 500));
            AlterColumn("dbo.CAPS_TypeOfUploadFile", "SecureAccessCategoryIdAssignedByUser", c => c.Int());
            AlterColumn("dbo.CAPS_BankAccount", "CCFootNote", c => c.String(maxLength: 200));
            AlterColumn("dbo.CAPS_BankAccount", "RoutingNumber", c => c.String(maxLength: 100));
            AlterColumn("dbo.CAPS_BankAccount", "BankAccountNumber", c => c.String(maxLength: 100));
            AlterColumn("dbo.CAPS_ChargeEntryDocument", "UploadDocumentLogId", c => c.Int());
            AlterColumn("dbo.CAPS_CashEntryDocument", "PettyCashAccountId", c => c.Long(nullable: false));
            DropColumn("dbo.CAPS_PayrollEntryDocument", "ReversalDate");
            DropColumn("dbo.CAPS_InvoiceEntryDocument", "ReversalDate");
            DropColumn("dbo.CAPS_InvoiceEntryDocument", "PaymentDate");
            DropColumn("dbo.CAPS_JournalEntryDocument", "LastPostDate");
            DropColumn("dbo.CAPS_JournalEntryDocument", "FinalDate");
            DropColumn("dbo.CAPS_JournalEntryDocument", "DateToRecur");
            DropColumn("dbo.CAPS_JournalEntryDocument", "DateOfReversal");
            DropColumn("dbo.CAPS_CashEntryDocument", "ReversalDate");
            DropColumn("dbo.CAPS_ARInvoiceEntryDocument", "ReversalDate");
            DropColumn("dbo.CAPS_AccountingDocument", "DatePosted");
            DropColumn("dbo.CAPS_AccountingDocument", "TransactionDate");
            DropColumn("dbo.CAPS_AccountingDocument", "DocumentDate");
            AddPrimaryKey("dbo.CAPS_AttachedObject", "AttachedObjectId");
            AddPrimaryKey("dbo.CAPS_ApprovedSOX", "ApprovedSOXID");
            CreateIndex("dbo.CAPS_ChargeEntryDocument", "UploadDocumentLog_Id");
            CreateIndex("dbo.CAPS_CashEntryDocument", "PettyCashAccountId");
            AddForeignKey("dbo.CAPS_CashEntryDocument", "PettyCashAccountId", "dbo.CAPS_PettyCashAccount", "PettyCashAccountId", cascadeDelete: true);
            AddForeignKey("dbo.CAPS_ChargeEntryDocument", "UploadDocumentLog_Id", "dbo.CAPS_UploadDocumentLog", "UploadDocumentLogId");
        }
    }
}
