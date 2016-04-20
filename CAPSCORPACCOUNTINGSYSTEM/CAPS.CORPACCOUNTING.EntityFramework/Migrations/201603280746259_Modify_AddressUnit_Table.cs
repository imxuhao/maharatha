namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modify_AddressUnit_Table : DbMigration
    {
        public override void Up()
        {
            this.DeleteDefaultContraint("dbo.CAPS_Address", "ObjectId");
            AlterColumn("dbo.CAPS_Address", "ObjectId", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CAPS_Address", "ObjectId", c => c.Int(nullable: false));
        }
    }
}
