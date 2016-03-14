namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_ApprovedSox_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_ApprovedSOX",
                c => new
                    {
                        ApprovedSOXID = c.Int(nullable: false, identity: true),
                        TypeOfObjectId = c.Int(nullable: false),
                        ObjectId = c.Long(nullable: false),
                        DateApproved = c.DateTime(nullable: false, storeType: "smalldatetime"),
                        ApprovedByUserId = c.Int(nullable: false),
                        IsUnApproved = c.Boolean(nullable: false),
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
                    { "DynamicFilter_ApprovedSoxUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_ApprovedSoxUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.ApprovedSOXID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CAPS_ApprovedSOX",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ApprovedSoxUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_ApprovedSoxUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
