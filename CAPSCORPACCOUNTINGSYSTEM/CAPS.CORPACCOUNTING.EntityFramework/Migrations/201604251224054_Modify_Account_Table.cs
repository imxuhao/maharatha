namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modify_Account_Table : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CAPS_Account", "TypeOfCurrencyId", c => c.Int());
            AddColumn("dbo.CAPS_Account", "TypeofConsolidationId", c => c.Int());
            AddColumn("dbo.CAPS_Account", "TypeOfCurrencyRateId", c => c.Short());
            AddColumn("dbo.CAPS_Account", "IsAccountRevalued", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CAPS_Account", "IsAccountRevalued");
            DropColumn("dbo.CAPS_Account", "TypeOfCurrencyRateId");
            DropColumn("dbo.CAPS_Account", "TypeofConsolidationId");
            DropColumn("dbo.CAPS_Account", "TypeOfCurrencyId");
        }
    }
}
