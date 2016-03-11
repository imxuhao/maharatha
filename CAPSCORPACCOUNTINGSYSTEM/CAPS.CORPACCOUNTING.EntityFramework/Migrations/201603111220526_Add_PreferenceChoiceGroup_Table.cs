namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_PreferenceChoiceGroup_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_PreferenceChoiceGroup",
                c => new
                    {
                        PreferenceChoiceGroupId = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 100),
                        Caption = c.String(maxLength: 20),
                        DisplaySequence = c.Short(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    })
                .PrimaryKey(t => t.PreferenceChoiceGroupId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CAPS_PreferenceChoiceGroup");
        }
    }
}
