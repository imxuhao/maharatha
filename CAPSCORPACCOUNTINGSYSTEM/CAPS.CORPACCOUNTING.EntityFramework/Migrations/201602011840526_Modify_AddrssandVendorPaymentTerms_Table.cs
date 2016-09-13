namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modify_AddrssandVendorPaymentTerms_Table : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CAPS_Address", "EmployeeId", "dbo.CAPS_Employee");
            DropIndex("dbo.CAPS_Address", new[] { "EmployeeId" });
            AddColumn("dbo.CAPS_Address", "ObjectId", c => c.Int(nullable: false));
            AlterColumn("dbo.CAPS_Vendors", "TypeofPaymentMethod", c => c.Int());
            AlterColumn("dbo.CAPS_Vendors", "Typeof1099Box", c => c.Int());
            DropColumn("dbo.CAPS_Address", "EmployeeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CAPS_Address", "EmployeeId", c => c.Int(nullable: false));
            AlterColumn("dbo.CAPS_Vendors", "Typeof1099Box", c => c.Int(nullable: false));
            AlterColumn("dbo.CAPS_Vendors", "TypeofPaymentMethod", c => c.Int(nullable: false));
            DropColumn("dbo.CAPS_Address", "ObjectId");
            CreateIndex("dbo.CAPS_Address", "EmployeeId");
            AddForeignKey("dbo.CAPS_Address", "EmployeeId", "dbo.CAPS_Employee", "EmployeeId", cascadeDelete: true);
        }
    }
}
