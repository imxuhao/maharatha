namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_UserReportUnit_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_UserReport",
                c => new
                    {
                        UserReportId = c.Long(nullable: false, identity: true),
                        Caption = c.String(maxLength: 100),
                        ReportParameters = c.String(),
                        SentFromUserId = c.Int(),
                        SentUserReportId = c.Int(),
                        IsApproved = c.Boolean(nullable: false),
                        IsActive = c.Boolean(),
                        IsDefault = c.Boolean(),
                        IsShared = c.Boolean(),
                        TypeOfInActiveStatusId = c.Int(),
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
                    { "DynamicFilter_UserReportUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_UserReportUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.UserReportId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CAPS_UserReport",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_UserReportUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_UserReportUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
