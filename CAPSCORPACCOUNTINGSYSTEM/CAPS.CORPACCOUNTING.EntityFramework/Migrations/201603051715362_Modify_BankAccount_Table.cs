namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modify_BankAccount_Table : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CAPS_BankAccount", "PettyCashAccountId", "dbo.CAPS_Accounts");
            DropIndex("dbo.CAPS_BankAccount", new[] { "PettyCashAccountId" });
            AlterColumn("dbo.CAPS_BankAccount", "ControllingBankAccountId", c => c.Int());
            AlterColumn("dbo.CAPS_BankAccount", "PettyCashAccountId", c => c.Int());
            CreateIndex("dbo.CAPS_BankAccount", "ControllingBankAccountId");
            CreateIndex("dbo.CAPS_BankAccount", "PettyCashAccountId");
            AddForeignKey("dbo.CAPS_BankAccount", "ControllingBankAccountId", "dbo.CAPS_BankAccount", "BankAccountId");
            AddForeignKey("dbo.CAPS_BankAccount", "PettyCashAccountId", "dbo.CAPS_PettyCashAccount", "PettyCashAccountId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_BankAccount", "PettyCashAccountId", "dbo.CAPS_PettyCashAccount");
            DropForeignKey("dbo.CAPS_BankAccount", "ControllingBankAccountId", "dbo.CAPS_BankAccount");
            DropIndex("dbo.CAPS_BankAccount", new[] { "PettyCashAccountId" });
            DropIndex("dbo.CAPS_BankAccount", new[] { "ControllingBankAccountId" });
            AlterColumn("dbo.CAPS_BankAccount", "PettyCashAccountId", c => c.Long());
            AlterColumn("dbo.CAPS_BankAccount", "ControllingBankAccountId", c => c.Long());
            CreateIndex("dbo.CAPS_BankAccount", "PettyCashAccountId");
            AddForeignKey("dbo.CAPS_BankAccount", "PettyCashAccountId", "dbo.CAPS_Accounts", "AccountId");
        }
    }
}
