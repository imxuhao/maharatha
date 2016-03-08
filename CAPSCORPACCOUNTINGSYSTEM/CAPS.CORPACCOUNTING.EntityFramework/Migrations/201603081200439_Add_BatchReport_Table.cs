namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_BatchReport_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_BatchReport",
                c => new
                    {
                        BatchReportId = c.Int(nullable: false, identity: true),
                        BatchName = c.String(nullable: false, maxLength: 200),
                        BatchSDate = c.DateTime(storeType: "smalldatetime"),
                        BatchEDate = c.DateTime(storeType: "smalldatetime"),
                        IsActive = c.Boolean(nullable: false),
                        IsrptConsol = c.Boolean(),
                        IsPublic = c.Boolean(),
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
                    { "DynamicFilter_BatchReportUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_BatchReportUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.BatchReportId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CAPS_BatchReport",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_BatchReportUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_BatchReportUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
