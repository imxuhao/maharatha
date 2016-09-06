namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modify_TypeOfAccountUnit_Table1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CAPS_TypeOfAccount", "IsEditable", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CAPS_TypeOfAccount", "IsEditable");
        }
    }
}
