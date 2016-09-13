namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Email_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_Email",
                c => new
                    {
                        EmailId = c.Int(nullable: false, identity: true),
                        TypeOfEmailId = c.Int(nullable: false),
                        TypeOfObjectId = c.Int(nullable: false),
                        ObjectId = c.Int(nullable: false),
                        ContactInfo = c.String(),
                        EmailAddress = c.String(),
                        IsPrimary = c.Boolean(nullable: false),
                        OrganizationUnitId = c.Long(),
                        TenantId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                        TypeOfCategoryId_Id = c.Short(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_EmailUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_EmailUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.EmailId)
                .ForeignKey("dbo.CAPS_TypeOfCategory", t => t.TypeOfCategoryId_Id)
                .Index(t => t.TypeOfCategoryId_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_Email", "TypeOfCategoryId_Id", "dbo.CAPS_TypeOfCategory");
            DropIndex("dbo.CAPS_Email", new[] { "TypeOfCategoryId_Id" });
            DropTable("dbo.CAPS_Email",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_EmailUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_EmailUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
