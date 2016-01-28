namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modify_Address_Table : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CAPS_Address", "EmployeeId", "dbo.CAPS_Employee");
            DropIndex("dbo.CAPS_Address", new[] { "EmployeeId" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.CAPS_Address", "EmployeeId");
            AddForeignKey("dbo.CAPS_Address", "EmployeeId", "dbo.CAPS_Employee", "EmployeeId", cascadeDelete: true);
        }
    }
}
