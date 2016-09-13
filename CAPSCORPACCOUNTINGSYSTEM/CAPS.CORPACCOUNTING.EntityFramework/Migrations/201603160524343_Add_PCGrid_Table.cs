namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_PCGrid_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_PCGrid",
                c => new
                    {
                        PCGridId = c.Long(nullable: false, identity: true),
                        JobID = c.Long(),
                        AccountID = c.Long(),
                        VendorID = c.Long(),
                        AccountName = c.String(),
                        AccountNumber = c.String(),
                        JobName = c.String(),
                        JobNumber = c.String(),
                        ChartOfAccountID = c.Int(),
                        VendorName = c.String(),
                        JobDescription = c.String(),
                        AccountDescription = c.String(),
                        ProjectDescription = c.String(),
                        PCAccountID = c.Int(),
                        PCPettyCashAccountID = c.Int(),
                        Advances = c.Decimal(precision: 18, scale: 2),
                        AdvancesCount = c.Int(),
                        Expenses = c.Decimal(precision: 18, scale: 2),
                        ExpensesCount = c.Int(),
                        RETURNS = c.Decimal(precision: 18, scale: 2),
                        RETURNsCount = c.Int(),
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
                    { "DynamicFilter_PCGridUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_PCGridUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.PCGridId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CAPS_PCGrid",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_PCGridUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_PCGridUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
