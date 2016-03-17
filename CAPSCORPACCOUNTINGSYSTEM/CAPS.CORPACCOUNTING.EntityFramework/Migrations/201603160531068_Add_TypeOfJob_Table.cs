namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_TypeOfJob_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_TypeOfJob",
                c => new
                    {
                        TypeOfPayroll = c.Short(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 100),
                        Caption = c.String(maxLength: 20),
                        DisplaySequence = c.Short(),
                        Notes = c.String(),
                        IsCorporateLedger = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false,defaultValue:true),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    })
                .PrimaryKey(t => t.TypeOfPayroll);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CAPS_TypeOfJob");
        }
    }
}
