namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_TypeOfBusinessProcessControl_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_TypeOfBusinessProcessControl",
                c => new
                    {
                        TypeOfBusinessProcessControlId = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 100),
                        Caption = c.String(maxLength: 20),
                        DisplaySequence = c.Short(),
                        ControlValue = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.TypeOfBusinessProcessControlId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CAPS_TypeOfBusinessProcessControl");
        }
    }
}
