namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_ErrorMessage_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_ErrorMessage",
                c => new
                    {
                        ErrorMessageId = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false),
                        Caption = c.String(maxLength: 20),
                        TypeOfErrorId = c.Int(nullable: false),
                        ErrorCategoryId = c.Int(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    })
                .PrimaryKey(t => t.ErrorMessageId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CAPS_ErrorMessage");
        }
    }
}
