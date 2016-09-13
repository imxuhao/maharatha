namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_TypeOfCategoryUnit_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_TypeOfCategory",
                c => new
                    {
                        TypeOfCategoryId = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 100),
                        Caption = c.String(),
                        DisplaySequence = c.Short(),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.TypeOfCategoryId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CAPS_TypeOfCategory");
        }
    }
}
