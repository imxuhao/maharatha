namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_TypeOfAllocationStatus_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_TypeOfAllocationStatus",
                c => new
                    {
                        TypeOfAllocationStatusId = c.Short(nullable: false, identity: true),
                        Description = c.String(maxLength: 100),
                        Caption = c.String(maxLength: 20),
                        DisplaySequence = c.Short(),
                        Notes = c.String(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    })
                .PrimaryKey(t => t.TypeOfAllocationStatusId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CAPS_TypeOfAllocationStatus");
        }
    }
}
