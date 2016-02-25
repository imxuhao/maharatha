namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_BankAccountUser_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_BankAccountUser",
                c => new
                    {
                        BankAccountUserId = c.Int(nullable: false, identity: true),
                        BankAccountId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
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
                    { "DynamicFilter_BankAccountUserUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_BankAccountUserUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.BankAccountUserId)
                .ForeignKey("dbo.CAPS_BankAccount", t => t.BankAccountId, cascadeDelete: true)
                .Index(t => t.BankAccountId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_BankAccountUser", "BankAccountId", "dbo.CAPS_BankAccount");
            DropIndex("dbo.CAPS_BankAccountUser", new[] { "BankAccountId" });
            DropTable("dbo.CAPS_BankAccountUser",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_BankAccountUserUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_BankAccountUserUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
