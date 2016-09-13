namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_ServiceLevelAgreement_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_ServiceLevelAgreement",
                c => new
                    {
                        ServiceLevelAgreementId = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false),
                        Caption = c.String(maxLength: 20),
                        DisplaySequence = c.Short(),
                        Notes = c.String(),
                        TypeOfServiceLevelAgreementId = c.Int(nullable: false),
                        TransactionVolume = c.Short(),
                        DayHourMinSecResponse = c.Short(),
                        StatusMessageId = c.Int(),
                        WarningMessageId = c.Int(),
                        ErrorMessageId = c.Int(),
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
                    { "DynamicFilter_ServiceLevelAgreementUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_ServiceLevelAgreementUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.ServiceLevelAgreementId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CAPS_ServiceLevelAgreement",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ServiceLevelAgreementUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_ServiceLevelAgreementUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
