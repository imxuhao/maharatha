namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modify_SubAccountUnit_Table : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CAPS_SubAccount", "TypeofSubAccountId", c => c.Int(nullable: false));
            AlterColumn("dbo.CAPS_SubAccount", "TypeOfInactiveStatusId", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CAPS_SubAccount", "TypeOfInactiveStatusId", c => c.Short());
            DropColumn("dbo.CAPS_SubAccount", "TypeofSubAccountId");
        }
    }
}
