namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_TypeOfAccountingLayout_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_TypeOfAccountingLayout",
                c => new
                    {
                        TypeOfAccountingLayoutID = c.Int(nullable: false, identity: true),
                        TypeOfHeadingGroupId = c.Int(),
                        TypeOfEntryLayoutHeadingId = c.Short(),
                        TypeOfAccountingLayoutHeadingId = c.Short(),
                        DescriptionInternalUseOnly = c.String(maxLength: 100),
                        DisplaySequence = c.Short(nullable: false),
                        Notes = c.String(maxLength: 500),
                        IsDisplayedOnFirstPage = c.Boolean(nullable: false),
                        IsHidden = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    })
                .PrimaryKey(t => t.TypeOfAccountingLayoutID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CAPS_TypeOfAccountingLayout");
        }
    }
}
