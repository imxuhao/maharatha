namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_ProjectControlPeriod_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_ProjectControlPeriod",
                c => new
                    {
                        ProjectControlPeriodId = c.Int(nullable: false, identity: true),
                        JobId = c.Int(nullable: false),
                        ControlPeriodNumber = c.Int(),
                        ControlPeriodDate = c.DateTime(storeType: "smalldatetime"),
                        PostCutoffDate = c.DateTime(storeType: "smalldatetime"),
                        DateClosed = c.DateTime(storeType: "smalldatetime"),
                        ClosedByUserId = c.Int(),
                        DateReOpened = c.DateTime(storeType: "smalldatetime"),
                        ReOpenedByUserId = c.Int(),
                        Description = c.String(),
                        IsApproved = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        TypeofInactiveStatusId = c.Int(),
                        FiscalPeriodId = c.Int(),
                        ClosedStatusTypeOfCategoryId = c.Int(),
                        IsClosed = c.Boolean(),
                        PeriodStartDate = c.DateTime(storeType: "smalldatetime"),
                        PeriodEndDate = c.DateTime(storeType: "smalldatetime"),
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
                    { "DynamicFilter_ProjectControlPeriodUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_ProjectControlPeriodUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.ProjectControlPeriodId)
                .ForeignKey("dbo.CAPS_FiscalPeriod", t => t.FiscalPeriodId)
                .ForeignKey("dbo.CAPS_Job", t => t.JobId, cascadeDelete: true)
                .Index(t => t.JobId)
                .Index(t => t.FiscalPeriodId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_ProjectControlPeriod", "JobId", "dbo.CAPS_Job");
            DropForeignKey("dbo.CAPS_ProjectControlPeriod", "FiscalPeriodId", "dbo.CAPS_FiscalPeriod");
            DropIndex("dbo.CAPS_ProjectControlPeriod", new[] { "FiscalPeriodId" });
            DropIndex("dbo.CAPS_ProjectControlPeriod", new[] { "JobId" });
            DropTable("dbo.CAPS_ProjectControlPeriod",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ProjectControlPeriodUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_ProjectControlPeriodUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
