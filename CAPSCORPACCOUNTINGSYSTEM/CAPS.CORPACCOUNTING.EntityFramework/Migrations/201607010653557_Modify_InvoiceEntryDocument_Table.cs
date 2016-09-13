namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modify_InvoiceEntryDocument_Table : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CAPS_InvoiceEntryDocument", "DueDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CAPS_InvoiceEntryDocument", "DueDate");
        }
    }
}
