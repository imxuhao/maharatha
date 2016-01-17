namespace CAPS.CORPACCOUNTING.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Modify_AbpAuditLogs : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AbpAuditLogs", "CustomData", c => c.String(maxLength: 2000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AbpAuditLogs", "CustomData", c => c.String());
        }
    }
}
