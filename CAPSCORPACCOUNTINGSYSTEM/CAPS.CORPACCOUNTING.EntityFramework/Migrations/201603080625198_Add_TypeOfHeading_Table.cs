namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_TypeOfHeading_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_TypeOfHeading",
                c => new
                    {
                        TypeOfHeadingID = c.Int(nullable: false, identity: true),
                        Description = c.String(maxLength: 100),
                        Caption = c.String(maxLength: 40),
                        DisplaySequence = c.Short(),
                        Notes = c.String(maxLength: 500),
                        TypeOfHeadingGroupId = c.Int(nullable: false),
                        IsDefault = c.Boolean(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    })
                .PrimaryKey(t => t.TypeOfHeadingID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CAPS_TypeOfHeading");
        }
    }
}
