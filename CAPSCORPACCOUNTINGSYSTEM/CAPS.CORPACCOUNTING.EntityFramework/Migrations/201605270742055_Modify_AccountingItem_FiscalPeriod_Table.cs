namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modify_AccountingItem_FiscalPeriod_Table : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CAPS_AccountingItem", "IsAccountingItemSplit", c => c.Boolean(nullable: false));
            AddColumn("dbo.CAPS_FiscalPeriod", "MonthYear", c => c.String());
            CreateIndex("dbo.CAPS_AccountingItem", "AccountingItemOrigId");
            AddForeignKey("dbo.CAPS_AccountingItem", "AccountingItemOrigId", "dbo.CAPS_AccountingItem", "AccountingItemId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_AccountingItem", "AccountingItemOrigId", "dbo.CAPS_AccountingItem");
            DropIndex("dbo.CAPS_AccountingItem", new[] { "AccountingItemOrigId" });
            DropColumn("dbo.CAPS_FiscalPeriod", "MonthYear");
            DropColumn("dbo.CAPS_AccountingItem", "IsAccountingItemSplit");
        }
    }
}
