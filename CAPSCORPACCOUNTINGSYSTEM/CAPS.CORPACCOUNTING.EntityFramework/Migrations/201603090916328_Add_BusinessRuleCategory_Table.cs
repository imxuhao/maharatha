namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_BusinessRuleCategory_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_BusinessRuleCategory",
                c => new
                    {
                        BusinessRuleCategoryId = c.Short(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 100),
                        Caption = c.String(maxLength: 20),
                        DisplaySequence = c.Short(),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.BusinessRuleCategoryId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CAPS_BusinessRuleCategory");
        }
    }
}
