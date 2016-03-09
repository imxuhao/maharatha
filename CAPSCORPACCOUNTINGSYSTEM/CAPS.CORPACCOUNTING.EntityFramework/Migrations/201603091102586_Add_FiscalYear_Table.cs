namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_FiscalYear_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_FiscalYear",
                c => new
                    {
                        FiscalYearId = c.Int(nullable: false, identity: true),
                        YearStartDate = c.DateTime(nullable: false, storeType: "smalldatetime"),
                        YearEndDate = c.DateTime(nullable: false, storeType: "smalldatetime"),
                        IsYearOpen = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsApproved = c.Boolean(nullable: false),
                        TypeOfInactiveStatusId = c.Int(),
                        IsCpaClosed = c.Boolean(),
                        DateCpaClosed = c.DateTime(storeType: "smalldatetime"),
                        CpaUserId = c.Int(),
                        IsDefaultReportingYear = c.Boolean(),
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
                    { "DynamicFilter_FiscalYearUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_FiscalYearUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.FiscalYearId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CAPS_FiscalYear",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_FiscalYearUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_FiscalYearUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
