namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_TypeOfCreditRating_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_TypeOfCreditRating",
                c => new
                    {
                        TypeOfCreditRatingId = c.Short(nullable: false, identity: true),
                        Description = c.String(maxLength: 100),
                        Caption = c.String(maxLength: 20),
                        DisplaySequence = c.Short(),
                        Notes = c.String(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    })
                .PrimaryKey(t => t.TypeOfCreditRatingId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CAPS_TypeOfCreditRating");
        }
    }
}
