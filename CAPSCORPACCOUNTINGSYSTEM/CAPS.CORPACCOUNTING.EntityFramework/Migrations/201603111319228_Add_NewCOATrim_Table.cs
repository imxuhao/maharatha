namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_NewCOATrim_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_NewCOATrim",
                c => new
                    {
                        CoaTrimId = c.Int(nullable: false, identity: true),
                        CoaDesc = c.String(maxLength: 50),
                        Coaid = c.Int(),
                        LineNumber = c.String(),
                        LineId = c.Int(),
                        Description = c.String(),
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
                    { "DynamicFilter_NewCOATrimUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_NewCOATrimUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.CoaTrimId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CAPS_NewCOATrim",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_NewCOATrimUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_NewCOATrimUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
