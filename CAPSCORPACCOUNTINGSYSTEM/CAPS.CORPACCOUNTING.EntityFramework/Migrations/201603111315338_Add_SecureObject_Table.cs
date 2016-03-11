namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_SecureObject_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_SecureObject",
                c => new
                    {
                        SecureObjectId = c.Int(nullable: false, identity: true),
                        SecureAccessCategoryIdAssignedByUser = c.Int(nullable: false),
                        TypeOfSecureObjectId = c.Int(nullable: false),
                        TypeOfObjectId = c.Int(nullable: false),
                        ObjectId = c.Int(nullable: false),
                        ColumnIdProtectedControlId = c.Int(),
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
                    { "DynamicFilter_SecureObjectUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_SecureObjectUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.SecureObjectId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CAPS_SecureObject",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_SecureObjectUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_SecureObjectUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
