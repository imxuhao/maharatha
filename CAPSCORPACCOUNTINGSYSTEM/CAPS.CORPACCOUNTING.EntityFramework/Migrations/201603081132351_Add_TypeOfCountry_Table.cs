namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_TypeOfCountry_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_TypeOfCountry",
                c => new
                    {
                        TypeOfCountryId = c.Short(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 100),
                        Caption = c.String(maxLength: 20),
                        DisplaySequence = c.Short(),
                        Notes = c.String(),
                        TwoLetterAbbreviation = c.String(maxLength: 2),
                        ThreeLetterAbbreviation = c.String(maxLength: 3),
                        IsoNumber = c.String(maxLength: 3),
                    })
                .PrimaryKey(t => t.TypeOfCountryId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CAPS_TypeOfCountry");
        }
    }
}
