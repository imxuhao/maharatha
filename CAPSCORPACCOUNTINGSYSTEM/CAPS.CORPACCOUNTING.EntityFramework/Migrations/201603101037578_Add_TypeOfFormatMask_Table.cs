namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_TypeOfFormatMask_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_TypeOfFormatMask",
                c => new
                    {
                        TypeOfFormatMaskId = c.Short(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 100),
                        Caption = c.String(maxLength: 20),
                        DisplaySequence = c.Short(),
                        Notes = c.String(),
                        FormatMask = c.String(maxLength: 100),
                        IsIntegerRequired = c.Boolean(nullable: false),
                        IsNumberRequired = c.Boolean(nullable: false),
                        TypeOfMaskId = c.Int(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    })
                .PrimaryKey(t => t.TypeOfFormatMaskId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CAPS_TypeOfFormatMask");
        }
    }
}
