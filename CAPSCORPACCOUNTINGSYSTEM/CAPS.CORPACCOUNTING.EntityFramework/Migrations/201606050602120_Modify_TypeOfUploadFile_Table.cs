namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modify_TypeOfUploadFile_Table : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CAPS_TypeOfUploadFile", "TypeofUploadId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CAPS_TypeOfUploadFile", "TypeofUploadId");
        }
    }
}
