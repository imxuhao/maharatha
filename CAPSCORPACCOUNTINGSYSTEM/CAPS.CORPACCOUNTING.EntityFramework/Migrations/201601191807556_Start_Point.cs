namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Start_Point : DbMigration
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
