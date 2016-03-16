namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_TypeOfPayroll_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_TypeOfPayroll",
                c => new
                    {
                        TypeOfPayroll = c.Short(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 100),
                        Caption = c.String(maxLength: 20),
                        DisplaySequence = c.Short(),
                        Notes = c.String(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    })
                .PrimaryKey(t => t.TypeOfPayroll);
            
            CreateIndex("dbo.CAPS_PayrollEntryDocumentDetail", "TypeOfPayrollId");
            AddForeignKey("dbo.CAPS_PayrollEntryDocumentDetail", "TypeOfPayrollId", "dbo.CAPS_TypeOfPayroll", "TypeOfPayroll");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_PayrollEntryDocumentDetail", "TypeOfPayrollId", "dbo.CAPS_TypeOfPayroll");
            DropIndex("dbo.CAPS_PayrollEntryDocumentDetail", new[] { "TypeOfPayrollId" });
            DropTable("dbo.CAPS_TypeOfPayroll");
        }
    }
}
