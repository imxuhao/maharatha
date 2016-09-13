namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_ARStatementInfo_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_ARStatementInfo",
                c => new
                    {
                        ArStatementInfoId = c.Int(nullable: false, identity: true),
                        Description = c.String(maxLength: 50),
                        FooterArea1 = c.String(),
                        FooterArea2 = c.String(),
                        FooterArea3 = c.String(),
                        FooterArea4 = c.String(),
                        FooterArea5 = c.String(),
                        FooterArea6 = c.String(),
                        FooterArea7 = c.String(),
                        FooterArea8 = c.String(),
                        FooterArea9 = c.String(),
                        FooterArea10 = c.String(),
                        IsLogoPrinted = c.Boolean(nullable: false),
                        LogoCaption = c.String(),
                        IsDefault = c.Boolean(nullable: false),
                        EntityId = c.Int(nullable: false),
                        IsApproved = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        TypeOfInactiveStatusId = c.Int(),
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
                    { "DynamicFilter_ARStatementInfo_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_ARStatementInfo_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.ArStatementInfoId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CAPS_ARStatementInfo",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ARStatementInfo_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_ARStatementInfo_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
