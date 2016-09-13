namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_TypeOfArInvoiceBuild_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_TypeOfARInvoiceBuild",
                c => new
                    {
                        TypeOfArInvoiceBuildId = c.Short(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 100),
                        Caption = c.String(),
                        DisplaySequence = c.Short(),
                        Notes = c.String(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    })
                .PrimaryKey(t => t.TypeOfArInvoiceBuildId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CAPS_TypeOfARInvoiceBuild");
        }
    }
}
