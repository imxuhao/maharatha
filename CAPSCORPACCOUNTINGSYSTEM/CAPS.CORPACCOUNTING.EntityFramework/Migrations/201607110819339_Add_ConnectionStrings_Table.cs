namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_ConnectionStrings_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Caps_ConnectionStrings",
                c => new
                    {
                        ConnectionStringId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        ConnectionString = c.String(nullable: false, maxLength: 1024),
                    })
                .PrimaryKey(t => t.ConnectionStringId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Caps_ConnectionStrings");
        }
    }
}
