namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modify_UserViewSettings_Table : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CAPS_UserViewSettings", "ViewSettingName", c => c.String(nullable: false, maxLength: 300));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CAPS_UserViewSettings", "ViewSettingName");
        }
    }
}
