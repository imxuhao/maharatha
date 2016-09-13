namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modify_ChartofAccount_Table : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CAPS_ChartOfAccount", "IsCorporate", c => c.Boolean(nullable: false));
            AddColumn("dbo.CAPS_ChartOfAccount", "IsNumeric", c => c.Boolean(nullable: false,defaultValue:true));
            AddColumn("dbo.CAPS_ChartOfAccount", "LinkChartOfAccountID", c => c.Int());
            AddColumn("dbo.CAPS_ChartOfAccount", "StandardGroupTotalId", c => c.Int());          
            DropColumn("dbo.CAPS_ChartOfAccount", "ChartofAccountsType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CAPS_ChartOfAccount", "ChartofAccountsType", c => c.Int(nullable: false));                  
            DropColumn("dbo.CAPS_ChartOfAccount", "StandardGroupTotalId");
            DropColumn("dbo.CAPS_ChartOfAccount", "LinkChartOfAccountID");
            DropColumn("dbo.CAPS_ChartOfAccount", "IsNumeric");
            DropColumn("dbo.CAPS_ChartOfAccount", "IsCorporate");
        }
    }
}
