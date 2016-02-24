namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_TypeOfUploadFileUnit_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_TypeOfUploadFile",
                c => new
                    {
                        TypeOfUploadFileId = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 100),
                        DisplaySequence = c.Int(),
                        Notes = c.String(maxLength: 500),
                        UploadFileName = c.String(maxLength: 100),
                        UploadOptionA = c.Boolean(),
                        UploadOptionB = c.Boolean(),
                        UploadOptionC = c.Boolean(),
                        UploadOptionD = c.Boolean(),
                        OverrideJobId = c.Int(),
                        SecureAccessCategoryIdAssignedByUser = c.Int(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    })
                .PrimaryKey(t => t.TypeOfUploadFileId)
                .ForeignKey("dbo.CAPS_Job", t => t.OverrideJobId)
                .Index(t => t.OverrideJobId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_TypeOfUploadFile", "OverrideJobId", "dbo.CAPS_Job");
            DropIndex("dbo.CAPS_TypeOfUploadFile", new[] { "OverrideJobId" });
            DropTable("dbo.CAPS_TypeOfUploadFile");
        }
    }
}
