namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_ARInvoiceSchedule_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_ARInvoiceSchedule",
                c => new
                    {
                        ARInvoiceScheduleId = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 50),
                        Percentage1 = c.Int(nullable: false),
                        Percentage2 = c.Int(nullable: false),
                        Percentage3 = c.Int(),
                        Percentage4 = c.Int(),
                        IsActive = c.Boolean(nullable: false,defaultValue:true),
                        OrganizationUnitId = c.Long(),
                        TenantId = c.Int(nullable: false),
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
                    { "DynamicFilter_ARInvoiceScheduleUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_ARInvoiceScheduleUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.ARInvoiceScheduleId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CAPS_ARInvoiceSchedule",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ARInvoiceScheduleUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_ARInvoiceScheduleUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
