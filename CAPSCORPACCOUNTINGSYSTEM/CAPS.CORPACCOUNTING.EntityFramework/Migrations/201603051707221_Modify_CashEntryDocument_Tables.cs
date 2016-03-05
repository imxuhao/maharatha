namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modify_CashEntryDocument_Tables : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.CAPS_CashEntryDocument", "ReconciliationId");
            AddForeignKey("dbo.CAPS_CashEntryDocument", "ReconciliationId", "dbo.CAPS_BankRecControl", "BankRecControlId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_CashEntryDocument", "ReconciliationId", "dbo.CAPS_BankRecControl");
            DropIndex("dbo.CAPS_CashEntryDocument", new[] { "ReconciliationId" });
        }
    }
}
