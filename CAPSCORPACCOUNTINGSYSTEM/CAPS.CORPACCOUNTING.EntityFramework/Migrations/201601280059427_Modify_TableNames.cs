using System.Data.Entity.Migrations;

namespace CAPS.CORPACCOUNTING.Migrations
{
    public partial class Modify_TableNames : DbMigration
    {
        public override void Up()
        {
            RenameTable("dbo.Accounts", "CAPS_Accounts");
            RenameTable("dbo.ChartOfAccounts", "CAPS_COA");
        }

        public override void Down()
        {
            RenameTable("dbo.CAPS_COA", "ChartOfAccounts");
            RenameTable("dbo.CAPS_Accounts", "Accounts");
        }
    }
}