namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_ReceiptHistoryDetail_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_ReceiptHistoryDetail",
                c => new
                    {
                        AccountingItemId = c.Long(nullable: false),
                        CustomerInvoiceId = c.Int(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ReceiptHistoryDetailUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_ReceiptHistoryDetailUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.AccountingItemId)
                .ForeignKey("dbo.CAPS_AccountingItem", t => t.AccountingItemId)
                .Index(t => t.AccountingItemId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_ReceiptHistoryDetail", "AccountingItemId", "dbo.CAPS_AccountingItem");
            DropIndex("dbo.CAPS_ReceiptHistoryDetail", new[] { "AccountingItemId" });
            DropTable("dbo.CAPS_ReceiptHistoryDetail",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ReceiptHistoryDetailUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_ReceiptHistoryDetailUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
