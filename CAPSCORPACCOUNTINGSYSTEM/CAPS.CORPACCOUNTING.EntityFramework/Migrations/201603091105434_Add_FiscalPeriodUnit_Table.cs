namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_FiscalPeriodUnit_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_FiscalPeriod",
                c => new
                    {
                        FiscalPeriodId = c.Int(nullable: false, identity: true),
                        FiscalYearId = c.Int(nullable: false),
                        PeriodStartDate = c.DateTime(nullable: false, storeType: "smalldatetime"),
                        PeriodEndDate = c.DateTime(nullable: false, storeType: "smalldatetime"),
                        IsPeriodOpen = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        TypeOfInactiveStatusId = c.Int(),
                        IsCpaClosed = c.Boolean(),
                        DateCpaClosed = c.DateTime(storeType: "smalldatetime"),
                        CpaUserId = c.Int(),
                        IsYearEndAdjustmentsAllowed = c.Boolean(),
                        IsPreClose = c.Boolean(),
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
                    { "DynamicFilter_FiscalPeriodUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_FiscalPeriodUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.FiscalPeriodId)
                .ForeignKey("dbo.CAPS_FiscalYear", t => t.FiscalYearId, cascadeDelete: true)
                .Index(t => t.FiscalYearId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_FiscalPeriod", "FiscalYearId", "dbo.CAPS_FiscalYear");
            DropIndex("dbo.CAPS_FiscalPeriod", new[] { "FiscalYearId" });
            DropTable("dbo.CAPS_FiscalPeriod",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_FiscalPeriodUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_FiscalPeriodUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
