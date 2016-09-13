namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_PayrollControl_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_PayrollControl",
                c => new
                    {
                        PayrollControlId = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        TypeOfInactiveStatusId = c.Int(),
                        ChartOfAccountId = c.Int(),
                        OffsetAccountNumber = c.String(),
                        OffsetSubAccountNumber = c.String(),
                        OffsetJobId = c.Int(),
                        IsGlobalPreference = c.Boolean(),
                        IsPayrollFringeAutoAdjusted = c.Boolean(),
                        IsApBuildActive = c.Boolean(),
                        IsApCheckGenerated = c.Boolean(),
                        EntityId = c.Int(),
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
                    { "DynamicFilter_PayrollControlUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_PayrollControlUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.PayrollControlId)
                .ForeignKey("dbo.CAPS_ChartOfAccount", t => t.ChartOfAccountId)
                .Index(t => t.ChartOfAccountId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_PayrollControl", "ChartOfAccountId", "dbo.CAPS_ChartOfAccount");
            DropIndex("dbo.CAPS_PayrollControl", new[] { "ChartOfAccountId" });
            DropTable("dbo.CAPS_PayrollControl",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_PayrollControlUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_PayrollControlUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
