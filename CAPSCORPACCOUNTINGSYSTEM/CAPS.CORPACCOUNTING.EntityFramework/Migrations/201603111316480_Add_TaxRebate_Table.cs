namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_TaxRebate_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_TaxRebate",
                c => new
                    {
                        TaxRebateId = c.Int(nullable: false, identity: true),
                        TaxCodeInfo = c.String(nullable: false, maxLength: 100),
                        TaxFormTypeOfCateGoryId = c.Int(nullable: false),
                        Description = c.String(),
                        Caption = c.String(maxLength: 20),
                        DisplaySequence = c.Short(),
                        IsActive = c.Boolean(nullable: false),
                        IsApproved = c.Boolean(nullable: false),
                        TypeOfInactiveStatusId = c.Int(),
                        EntityId = c.Int(nullable: false),
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
                    { "DynamicFilter_TaxRebateUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_TaxRebateUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.TaxRebateId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CAPS_TaxRebate",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_TaxRebateUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_TaxRebateUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
