namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modify_JournalEntryDocument_Table : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CAPS_JournalEntryDocument", "JournalTypeId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CAPS_JournalEntryDocument", "JournalTypeId");
        }
    }
}
