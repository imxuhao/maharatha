namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_TypeOfOtherName_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_TypeOfOtherName",
                c => new
                    {
                        TypeOfOtherNameId = c.Short(nullable: false, identity: true),
                        Description = c.String(maxLength: 100),
                        DisplaySequence = c.Short(),
                        Notes = c.String(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    })
                .PrimaryKey(t => t.TypeOfOtherNameId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CAPS_TypeOfOtherName");
        }
    }
}
