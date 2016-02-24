namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_TypeOfCheckStock_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_TypeOfCheckStock",
                c => new
                    {
                        TypeOfCheckStockId = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 100),
                        DisplaySequence = c.Int(),
                        Notes = c.String(maxLength: 500),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    })
                .PrimaryKey(t => t.TypeOfCheckStockId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CAPS_TypeOfCheckStock");
        }
    }
}
