namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_TypeOfCurrencyRate_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_TypeOfCurrencyRate",
                c => new
                    {
                        TypeOfCurrencyRateId = c.Short(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 100),
                        Caption = c.String(maxLength: 20),
                        DisplaySequence = c.Short(),
                        Notes = c.String(),
                        TypeOfUploadFileId = c.Int(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    })
                .PrimaryKey(t => t.TypeOfCurrencyRateId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CAPS_TypeOfCurrencyRate");
        }
    }
}
