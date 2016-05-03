namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modify_ChartofAccount_Teble : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CAPS_ChartOfAccount", "RollupAccountId", c => c.Long());
            AddColumn("dbo.CAPS_ChartOfAccount", "RollupDivisionId", c => c.Int());
            AlterColumn("dbo.CAPS_ChartOfAccount", "OrganizationUnitId", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CAPS_ChartOfAccount", "OrganizationUnitId", c => c.Long());
            DropColumn("dbo.CAPS_ChartOfAccount", "RollupDivisionId");
            DropColumn("dbo.CAPS_ChartOfAccount", "RollupAccountId");
        }
    }
}
