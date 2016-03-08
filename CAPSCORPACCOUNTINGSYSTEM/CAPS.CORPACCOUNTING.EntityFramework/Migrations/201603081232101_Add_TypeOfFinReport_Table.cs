namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_TypeOfFinReport_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_TypeOfFinReport",
                c => new
                    {
                        TypeOfReportId = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 100),
                        Caption = c.String(maxLength: 20),
                        DisplaySequence = c.Short(),
                        Notes = c.String(),
                        ReportBpgid = c.Int(),
                        TypeOfCategoryId_Id = c.Short(),
                    })
                .PrimaryKey(t => t.TypeOfReportId)
                .ForeignKey("dbo.CAPS_TypeOfCategory", t => t.TypeOfCategoryId_Id)
                .Index(t => t.TypeOfCategoryId_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_TypeOfFinReport", "TypeOfCategoryId_Id", "dbo.CAPS_TypeOfCategory");
            DropIndex("dbo.CAPS_TypeOfFinReport", new[] { "TypeOfCategoryId_Id" });
            DropTable("dbo.CAPS_TypeOfFinReport");
        }
    }
}
