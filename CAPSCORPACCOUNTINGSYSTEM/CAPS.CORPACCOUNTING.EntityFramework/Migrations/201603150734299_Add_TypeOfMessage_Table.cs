namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_TypeOfMessage_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_TypeOfMessage",
                c => new
                    {
                        TypeOfMessageId = c.Short(nullable: false, identity: true),
                        Description = c.String(nullable: false),
                        Caption = c.String(maxLength: 20),
                        DisplaySequence = c.Short(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    })
                .PrimaryKey(t => t.TypeOfMessageId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CAPS_TypeOfMessage");
        }
    }
}
