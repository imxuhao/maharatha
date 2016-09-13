namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_FedExTranslation_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_FedExTranslation",
                c => new
                    {
                        FedExTranslationId = c.Int(nullable: false, identity: true),
                        VendorTypeId = c.Int(nullable: false),
                        JobNumberRangeStart = c.String(nullable: false, maxLength: 100),
                        JobNumberRangeEnd = c.String(maxLength: 100),
                        Linenumber = c.String(nullable: false, maxLength: 100),
                        IsActive = c.Boolean(),
                        InvoiceType = c.String(maxLength: 50),
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
                    { "DynamicFilter_FedExTranslationUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_FedExTranslationUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.FedExTranslationId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CAPS_FedExTranslation",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_FedExTranslationUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_FedExTranslationUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
