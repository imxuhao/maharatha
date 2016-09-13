namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_TypeOfChart_Enum : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CAPS_ChartOfAccount", "TypeOfChartId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CAPS_ChartOfAccount", "TypeOfChartId");
        }
    }
}
