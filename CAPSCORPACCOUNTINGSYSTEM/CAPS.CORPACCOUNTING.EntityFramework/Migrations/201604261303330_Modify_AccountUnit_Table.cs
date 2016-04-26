namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modify_AccountUnit_Table : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CAPS_Account", "LinkAccountId", c => c.Long());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CAPS_Account", "LinkAccountId", c => c.Int());
        }
    }
}
