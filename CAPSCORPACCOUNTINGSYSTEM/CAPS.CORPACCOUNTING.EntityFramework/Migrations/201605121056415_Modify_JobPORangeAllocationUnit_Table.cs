namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modify_JobPORangeAllocationUnit_Table : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CAPS_JobPORangeAllocation", "PoRangeStartNumber", c => c.Long(nullable: false));
            AlterColumn("dbo.CAPS_JobPORangeAllocation", "PoRangeEndNumber", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CAPS_JobPORangeAllocation", "PoRangeEndNumber", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.CAPS_JobPORangeAllocation", "PoRangeStartNumber", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
