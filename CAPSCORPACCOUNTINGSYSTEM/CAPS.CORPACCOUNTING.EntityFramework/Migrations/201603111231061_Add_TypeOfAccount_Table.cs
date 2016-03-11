namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_TypeOfAccount_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_TypeOfAccount",
                c => new
                    {
                        TypeOfAccountId = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 100),
                        Caption = c.String(maxLength: 20),
                        DisplaySequence = c.Short(),
                        Notes = c.String(),
                        TypeOfAccountClassificationId = c.Short(),
                        IsCurrencyCodeRequired = c.Boolean(nullable: false),
                        IsPaymentType = c.Boolean(nullable: false),
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
                    { "DynamicFilter_TypeOfAccountUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_TypeOfAccountUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.TypeOfAccountId)
                .ForeignKey("dbo.CAPS_TypeOfAccountClassification", t => t.TypeOfAccountClassificationId)
                .Index(t => t.TypeOfAccountClassificationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_TypeOfAccount", "TypeOfAccountClassificationId", "dbo.CAPS_TypeOfAccountClassification");
            DropIndex("dbo.CAPS_TypeOfAccount", new[] { "TypeOfAccountClassificationId" });
            DropTable("dbo.CAPS_TypeOfAccount",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_TypeOfAccountUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_TypeOfAccountUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
