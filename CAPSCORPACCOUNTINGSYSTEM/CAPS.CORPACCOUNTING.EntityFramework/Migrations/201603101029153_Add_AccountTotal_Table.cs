namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_AccountTotal_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_AccountTotal",
                c => new
                    {
                        AccountTotalId = c.Int(nullable: false, identity: true),
                        TypeOfCategoryId = c.Short(nullable: false),
                        FiscalPeriodId = c.Int(nullable: false),
                        AccountId = c.Long(nullable: false),
                        JobId = c.Int(),
                        ConsolCompanyId = c.Int(),
                        ToCompanyId = c.Int(),
                        SubAccountId1 = c.Long(),
                        SubAccountId2 = c.Long(),
                        SubAccountId3 = c.Long(),
                        SubAccountId4 = c.Long(),
                        SubAccountId5 = c.Long(),
                        SubAccountId6 = c.Long(),
                        SubAccountId7 = c.Long(),
                        SubAccountId8 = c.Long(),
                        SubAccountId9 = c.Long(),
                        SubAccountId10 = c.Long(),
                        Amount1 = c.Decimal(precision: 18, scale: 2),
                        Amount2 = c.Decimal(precision: 18, scale: 2),
                        Amount3 = c.Decimal(precision: 18, scale: 2),
                        Amount4 = c.Decimal(precision: 18, scale: 2),
                        Amount5 = c.Decimal(precision: 18, scale: 2),
                        Amount6 = c.Decimal(precision: 18, scale: 2),
                        Amount7 = c.Decimal(precision: 18, scale: 2),
                        Amount8 = c.Decimal(precision: 18, scale: 2),
                        Amount9 = c.Decimal(precision: 18, scale: 2),
                        Amount10 = c.Decimal(precision: 18, scale: 2),
                        Amount11 = c.Decimal(precision: 18, scale: 2),
                        Amount12 = c.Decimal(precision: 18, scale: 2),
                        Amount13 = c.Decimal(precision: 18, scale: 2),
                        Amount14 = c.Decimal(precision: 18, scale: 2),
                        Amount15 = c.Decimal(precision: 18, scale: 2),
                        Amount16 = c.Decimal(precision: 18, scale: 2),
                        Amount17 = c.Decimal(precision: 18, scale: 2),
                        Amount18 = c.Decimal(precision: 18, scale: 2),
                        Amount19 = c.Decimal(precision: 18, scale: 2),
                        Amount20 = c.Decimal(precision: 18, scale: 2),
                        Amount21 = c.Decimal(precision: 18, scale: 2),
                        Amount22 = c.Decimal(precision: 18, scale: 2),
                        Amount23 = c.Decimal(precision: 18, scale: 2),
                        Amount24 = c.Decimal(precision: 18, scale: 2),
                        Amount25 = c.Decimal(precision: 18, scale: 2),
                        Amount26 = c.Decimal(precision: 18, scale: 2),
                        Amount27 = c.Decimal(precision: 18, scale: 2),
                        Amount28 = c.Decimal(precision: 18, scale: 2),
                        Amount29 = c.Decimal(precision: 18, scale: 2),
                        Amount30 = c.Decimal(precision: 18, scale: 2),
                        TypeOfCurrencyId1 = c.Int(),
                        TypeOfCurrencyId2 = c.Int(),
                        TypeOfCurrencyId3 = c.Int(),
                        TypeOfCurrencyId4 = c.Int(),
                        CurrencyRate1 = c.Double(),
                        CurrencyRate2 = c.Double(),
                        CurrencyRate3 = c.Double(),
                        CurrencyRate4 = c.Double(),
                        Date1 = c.DateTime(storeType: "smalldatetime"),
                        Date2 = c.DateTime(storeType: "smalldatetime"),
                        Date3 = c.DateTime(storeType: "smalldatetime"),
                        Date4 = c.DateTime(storeType: "smalldatetime"),
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
                    { "DynamicFilter_AccountTotalUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_AccountTotalUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.AccountTotalId)
                .ForeignKey("dbo.CAPS_Account", t => t.AccountId, cascadeDelete: true)
                .ForeignKey("dbo.CAPS_FiscalPeriod", t => t.FiscalPeriodId, cascadeDelete: true)
                .ForeignKey("dbo.CAPS_Job", t => t.JobId)
                .ForeignKey("dbo.CAPS_SubAccount", t => t.SubAccountId1)
                .ForeignKey("dbo.CAPS_SubAccount", t => t.SubAccountId10)
                .ForeignKey("dbo.CAPS_SubAccount", t => t.SubAccountId2)
                .ForeignKey("dbo.CAPS_SubAccount", t => t.SubAccountId3)
                .ForeignKey("dbo.CAPS_SubAccount", t => t.SubAccountId4)
                .ForeignKey("dbo.CAPS_SubAccount", t => t.SubAccountId5)
                .ForeignKey("dbo.CAPS_SubAccount", t => t.SubAccountId6)
                .ForeignKey("dbo.CAPS_SubAccount", t => t.SubAccountId7)
                .ForeignKey("dbo.CAPS_SubAccount", t => t.SubAccountId8)
                .ForeignKey("dbo.CAPS_SubAccount", t => t.SubAccountId9)
                .ForeignKey("dbo.CAPS_TypeOfCategory", t => t.TypeOfCategoryId, cascadeDelete: true)
                .Index(t => t.TypeOfCategoryId)
                .Index(t => t.FiscalPeriodId)
                .Index(t => t.AccountId)
                .Index(t => t.JobId)
                .Index(t => t.SubAccountId1)
                .Index(t => t.SubAccountId2)
                .Index(t => t.SubAccountId3)
                .Index(t => t.SubAccountId4)
                .Index(t => t.SubAccountId5)
                .Index(t => t.SubAccountId6)
                .Index(t => t.SubAccountId7)
                .Index(t => t.SubAccountId8)
                .Index(t => t.SubAccountId9)
                .Index(t => t.SubAccountId10);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_AccountTotal", "TypeOfCategoryId", "dbo.CAPS_TypeOfCategory");
            DropForeignKey("dbo.CAPS_AccountTotal", "SubAccountId9", "dbo.CAPS_SubAccount");
            DropForeignKey("dbo.CAPS_AccountTotal", "SubAccountId8", "dbo.CAPS_SubAccount");
            DropForeignKey("dbo.CAPS_AccountTotal", "SubAccountId7", "dbo.CAPS_SubAccount");
            DropForeignKey("dbo.CAPS_AccountTotal", "SubAccountId6", "dbo.CAPS_SubAccount");
            DropForeignKey("dbo.CAPS_AccountTotal", "SubAccountId5", "dbo.CAPS_SubAccount");
            DropForeignKey("dbo.CAPS_AccountTotal", "SubAccountId4", "dbo.CAPS_SubAccount");
            DropForeignKey("dbo.CAPS_AccountTotal", "SubAccountId3", "dbo.CAPS_SubAccount");
            DropForeignKey("dbo.CAPS_AccountTotal", "SubAccountId2", "dbo.CAPS_SubAccount");
            DropForeignKey("dbo.CAPS_AccountTotal", "SubAccountId10", "dbo.CAPS_SubAccount");
            DropForeignKey("dbo.CAPS_AccountTotal", "SubAccountId1", "dbo.CAPS_SubAccount");
            DropForeignKey("dbo.CAPS_AccountTotal", "JobId", "dbo.CAPS_Job");
            DropForeignKey("dbo.CAPS_AccountTotal", "FiscalPeriodId", "dbo.CAPS_FiscalPeriod");
            DropForeignKey("dbo.CAPS_AccountTotal", "AccountId", "dbo.CAPS_Account");
            DropIndex("dbo.CAPS_AccountTotal", new[] { "SubAccountId10" });
            DropIndex("dbo.CAPS_AccountTotal", new[] { "SubAccountId9" });
            DropIndex("dbo.CAPS_AccountTotal", new[] { "SubAccountId8" });
            DropIndex("dbo.CAPS_AccountTotal", new[] { "SubAccountId7" });
            DropIndex("dbo.CAPS_AccountTotal", new[] { "SubAccountId6" });
            DropIndex("dbo.CAPS_AccountTotal", new[] { "SubAccountId5" });
            DropIndex("dbo.CAPS_AccountTotal", new[] { "SubAccountId4" });
            DropIndex("dbo.CAPS_AccountTotal", new[] { "SubAccountId3" });
            DropIndex("dbo.CAPS_AccountTotal", new[] { "SubAccountId2" });
            DropIndex("dbo.CAPS_AccountTotal", new[] { "SubAccountId1" });
            DropIndex("dbo.CAPS_AccountTotal", new[] { "JobId" });
            DropIndex("dbo.CAPS_AccountTotal", new[] { "AccountId" });
            DropIndex("dbo.CAPS_AccountTotal", new[] { "FiscalPeriodId" });
            DropIndex("dbo.CAPS_AccountTotal", new[] { "TypeOfCategoryId" });
            DropTable("dbo.CAPS_AccountTotal",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_AccountTotalUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_AccountTotalUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
