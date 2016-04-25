namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_CustomLanguageTexts_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_CustomLanguageTexts",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        RegularExpression = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        IsMandatory = c.Boolean(nullable: false),
                        OrganizationUnitId = c.Long(),
                        Key = c.String(),
                        TenantId = c.Int(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_CustomLanguageTextsUnit_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CAPS_CustomLanguageTexts",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_CustomLanguageTextsUnit_MayHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
