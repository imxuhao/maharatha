namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Adding_SubAccount_restriction_AccountID : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CAPS_SubAccountAccessControl", "AccountId", c => c.Long(nullable: false));
            CreateIndex("dbo.CAPS_SubAccountAccessControl", "AccountId");
            AddForeignKey("dbo.CAPS_SubAccountAccessControl", "AccountId", "dbo.CAPS_Account", "AccountId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_SubAccountAccessControl", "AccountId", "dbo.CAPS_Account");
            DropIndex("dbo.CAPS_SubAccountAccessControl", new[] { "AccountId" });
            DropColumn("dbo.CAPS_SubAccountAccessControl", "AccountId");
        }
    }
}
