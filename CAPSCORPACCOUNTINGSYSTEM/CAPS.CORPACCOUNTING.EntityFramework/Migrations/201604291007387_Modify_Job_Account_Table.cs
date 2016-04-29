namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modify_Job_Account_Table : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CAPS_Job", "IsDivision", c => c.Boolean(nullable: false));
            AlterColumn("dbo.CAPS_Account", "RollupAccountId", c => c.Long());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CAPS_Account", "RollupAccountId", c => c.Int());
            DropColumn("dbo.CAPS_Job", "IsDivision");
        }
    }
}
