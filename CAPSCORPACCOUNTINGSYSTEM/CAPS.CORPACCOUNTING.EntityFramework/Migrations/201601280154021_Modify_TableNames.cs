namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modify_TableNames : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Accounts", newName: "CAPS_Accounts");
            RenameTable(name: "dbo.ChartOfAccounts", newName: "CAPS_COA");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.CAPS_COA", newName: "ChartOfAccounts");
            RenameTable(name: "dbo.CAPS_Accounts", newName: "Accounts");
        }
    }
}
