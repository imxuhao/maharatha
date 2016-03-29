namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modify_Address_Column : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CAPS_Address", "ObjectId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CAPS_Address", "ObjectId", c => c.Long(nullable: false));
        }
    }
}
