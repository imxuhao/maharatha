namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Accounting_AP_Tables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_AccountingHeaderTransactions",
                c => new
                    {
                        AHTID = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 100),
                        PostingDate = c.DateTime(nullable: false),
                        TenantId = c.Int(nullable: false),
                        OrganizationUnitId = c.Long(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_AccountingHeaderTransactionsUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_AccountingHeaderTransactionsUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.AHTID);
            
            CreateTable(
                "dbo.CAPS_APHeaderTransactions",
                c => new
                    {
                        AHTID = c.Int(nullable: false),
                        CheckDate = c.DateTime(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ApHeaderTransactions_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_ApHeaderTransactions_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.AHTID)
                .ForeignKey("dbo.CAPS_AccountingHeaderTransactions", t => t.AHTID)
                .Index(t => t.AHTID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_APHeaderTransactions", "AHTID", "dbo.CAPS_AccountingHeaderTransactions");
            DropIndex("dbo.CAPS_APHeaderTransactions", new[] { "AHTID" });
            DropTable("dbo.CAPS_APHeaderTransactions",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ApHeaderTransactions_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_ApHeaderTransactions_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.CAPS_AccountingHeaderTransactions",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_AccountingHeaderTransactionsUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_AccountingHeaderTransactionsUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
