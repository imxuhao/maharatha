namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modify_Customer_Table : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CAPS_Customers", "TypeofPaymentMethodId", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CAPS_Customers", "TypeofPaymentMethodId", c => c.Int(nullable: false));
        }
    }
}
