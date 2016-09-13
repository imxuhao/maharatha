namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_BankAccountPaymentRange_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_BankAccountPaymentRange",
                c => new
                    {
                        BankAccountPaymentRangeId = c.Int(nullable: false, identity: true),
                        BankAccountId = c.Int(nullable: false),
                        StartingPaymentNumber = c.Int(nullable: false),
                        EndingPaymentNumber = c.Int(nullable: false),
                        TenantId = c.Int(nullable: false),
                        OrganizationUnitId = c.Long(),
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
                    { "DynamicFilter_BankAccountPaymentRangeUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_BankAccountPaymentRangeUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.BankAccountPaymentRangeId)
                .ForeignKey("dbo.CAPS_BankAccount", t => t.BankAccountId, cascadeDelete: true)
                .Index(t => t.BankAccountId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_BankAccountPaymentRange", "BankAccountId", "dbo.CAPS_BankAccount");
            DropIndex("dbo.CAPS_BankAccountPaymentRange", new[] { "BankAccountId" });
            DropTable("dbo.CAPS_BankAccountPaymentRange",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_BankAccountPaymentRangeUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_BankAccountPaymentRangeUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
