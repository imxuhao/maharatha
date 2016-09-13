namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_BusinessProcess_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_BusinessProcess",
                c => new
                    {
                        BusinessProcessId = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 100),
                        Caption = c.String(maxLength: 20),
                        DisplaySequence = c.Short(),
                        Notes = c.String(),
                        TypeOfBusinessProcessId = c.Int(nullable: false),
                        BusinessProcessCategoryId = c.Int(nullable: false),
                        BusinessRuleGroupId = c.Int(),
                        RunBusinessProcessGroupId = c.Int(),
                        ServiceLevelAgreementId = c.Int(),
                        IsLogRequired = c.Boolean(nullable: false),
                        TestedByUser = c.String(),
                        DateTested = c.DateTime(storeType: "smalldatetime"),
                        ApprovedByUser = c.String(),
                        DateApproved = c.DateTime(storeType: "smalldatetime"),
                        IsActive = c.Boolean(nullable: false),
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
                    { "DynamicFilter_BusinessProcessUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_BusinessProcessUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.BusinessProcessId)
                .ForeignKey("dbo.CAPS_BusinessProcessGroup", t => t.RunBusinessProcessGroupId)
                .ForeignKey("dbo.CAPS_BusinessRuleGroup", t => t.BusinessRuleGroupId)
                .ForeignKey("dbo.CAPS_ServiceLevelAgreement", t => t.ServiceLevelAgreementId)
                .Index(t => t.BusinessRuleGroupId)
                .Index(t => t.RunBusinessProcessGroupId)
                .Index(t => t.ServiceLevelAgreementId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_BusinessProcess", "ServiceLevelAgreementId", "dbo.CAPS_ServiceLevelAgreement");
            DropForeignKey("dbo.CAPS_BusinessProcess", "BusinessRuleGroupId", "dbo.CAPS_BusinessRuleGroup");
            DropForeignKey("dbo.CAPS_BusinessProcess", "RunBusinessProcessGroupId", "dbo.CAPS_BusinessProcessGroup");
            DropIndex("dbo.CAPS_BusinessProcess", new[] { "ServiceLevelAgreementId" });
            DropIndex("dbo.CAPS_BusinessProcess", new[] { "RunBusinessProcessGroupId" });
            DropIndex("dbo.CAPS_BusinessProcess", new[] { "BusinessRuleGroupId" });
            DropTable("dbo.CAPS_BusinessProcess",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_BusinessProcessUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_BusinessProcessUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
