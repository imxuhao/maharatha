namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modify_JobAccount_Table : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CAPS_JobAccount", "RollupAccountDescription", c => c.String(maxLength: 400));
            AddColumn("dbo.CAPS_JobAccount", "RollupJobDescription", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CAPS_JobAccount", "RollupJobDescription");
            DropColumn("dbo.CAPS_JobAccount", "RollupAccountDescription");
        }
    }
}
