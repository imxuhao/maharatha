namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_AttachedObject_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_AttachedObject",
                c => new
                    {
                        AttachedObjectId = c.Int(nullable: false, identity: true),
                        TypeOfAttachedObjectId = c.Int(nullable: false),
                        TypeOfObjectId = c.Int(nullable: false),
                        ObjectId = c.Long(nullable: false),
                        Description = c.String(),
                        FileName = c.String(),
                        AttachedDate = c.DateTime(nullable: false),
                        FileSize = c.Int(),
                        FileExtension = c.String(maxLength: 20),
                        UserAttachmentFilesId = c.Guid(),
                        IsSystemGenerated = c.Boolean(),
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
                    { "DynamicFilter_AttachedObjectUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_AttachedObjectUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.AttachedObjectId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CAPS_AttachedObject",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_AttachedObjectUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_AttachedObjectUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
