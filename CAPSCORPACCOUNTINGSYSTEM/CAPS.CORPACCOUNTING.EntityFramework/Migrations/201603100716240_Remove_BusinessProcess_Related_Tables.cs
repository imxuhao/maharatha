namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Remove_BusinessProcess_Related_Tables : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CAPS_BusinessProcessGroup", "TypeOfBusinessProcessGroupId", "dbo.CAPS_TypeOfBusinessProcessGroup");
            DropForeignKey("dbo.CAPS_BatchReportItem", "BusinessProcessGroupId", "dbo.CAPS_BusinessProcessGroup");
            DropForeignKey("dbo.CAPS_BusinessProcess", "RunBusinessProcessGroupId", "dbo.CAPS_BusinessProcessGroup");
            DropForeignKey("dbo.CAPS_BusinessRuleGroup", "BusinessRuleCategoryId", "dbo.CAPS_BusinessRuleCategory");
            DropForeignKey("dbo.CAPS_BusinessProcess", "BusinessRuleGroupId", "dbo.CAPS_BusinessRuleGroup");
            DropForeignKey("dbo.CAPS_BusinessProcess", "ServiceLevelAgreementId", "dbo.CAPS_ServiceLevelAgreement");
            DropForeignKey("dbo.CAPS_BusinessRule", "BusinessRuleCategoryId", "dbo.CAPS_BusinessRuleCategory");
            DropForeignKey("dbo.CAPS_RoleBusinessProcess", "BusinessProcessGroupId", "dbo.CAPS_BusinessProcessGroup");
            DropForeignKey("dbo.CAPS_UserRoleBusinessProcess", "BusinessProcessGroupId", "dbo.CAPS_BusinessProcessGroup");
            DropIndex("dbo.CAPS_BatchReportItem", new[] { "BusinessProcessGroupId" });
            DropIndex("dbo.CAPS_BusinessProcessGroup", new[] { "TypeOfBusinessProcessGroupId" });
            DropIndex("dbo.CAPS_BusinessProcess", new[] { "BusinessRuleGroupId" });
            DropIndex("dbo.CAPS_BusinessProcess", new[] { "RunBusinessProcessGroupId" });
            DropIndex("dbo.CAPS_BusinessProcess", new[] { "ServiceLevelAgreementId" });
            DropIndex("dbo.CAPS_BusinessRuleGroup", new[] { "BusinessRuleCategoryId" });
            DropIndex("dbo.CAPS_BusinessRule", new[] { "BusinessRuleCategoryId" });
            DropIndex("dbo.CAPS_RoleBusinessProcess", new[] { "BusinessProcessGroupId" });
            DropIndex("dbo.CAPS_UserRoleBusinessProcess", new[] { "BusinessProcessGroupId" });
            DropTable("dbo.CAPS_BusinessProcessGroup",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_BusinessProcessGroupUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_BusinessProcessGroupUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.CAPS_TypeOfBusinessProcessGroup");
            DropTable("dbo.CAPS_BusinessProcess",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_BusinessProcessUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_BusinessProcessUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.CAPS_BusinessRuleGroup",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_BusinessRuleGroupUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_BusinessRuleGroupUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.CAPS_BusinessRuleCategory");
            DropTable("dbo.CAPS_ServiceLevelAgreement",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_ServiceLevelAgreementUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_ServiceLevelAgreementUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.CAPS_BusinessRule",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_BusinessRuleUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_BusinessRuleUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.CAPS_RoleBusinessProcess",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_RoleBusinessProcessUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_RoleBusinessProcessUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.CAPS_TypeOfBusinessProcessControl");
            DropTable("dbo.CAPS_UserRoleBusinessProcess",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_UserRoleBusinessProcessUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_UserRoleBusinessProcessUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CAPS_UserRoleBusinessProcess",
                c => new
                    {
                        UserRoleId = c.Int(nullable: false, identity: true),
                        BusinessProcessGroupId = c.Int(nullable: false),
                        RerouteBusinessProcessGroupId = c.Int(),
                        DashBoardMenuSequence = c.Int(),
                        IsBusinessProcessDenied = c.Boolean(nullable: false),
                        IsOptionalProcessRequired = c.Boolean(nullable: false),
                        IsNotificationRequired = c.Boolean(nullable: false),
                        IsApprovalRequired = c.Boolean(nullable: false),
                        TimeOutPeriodBeforeRoleBroadCastDayHourMin = c.Int(),
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
                    { "DynamicFilter_UserRoleBusinessProcessUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_UserRoleBusinessProcessUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.UserRoleId);
            
            CreateTable(
                "dbo.CAPS_TypeOfBusinessProcessControl",
                c => new
                    {
                        TypeOfBusinessProcessControlId = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 100),
                        Caption = c.String(maxLength: 20),
                        DisplaySequence = c.Short(),
                        ControlValue = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.TypeOfBusinessProcessControlId);
            
            CreateTable(
                "dbo.CAPS_RoleBusinessProcess",
                c => new
                    {
                        RoleBusinessProcessId = c.Int(nullable: false, identity: true),
                        RoleId = c.Int(nullable: false),
                        BusinessProcessGroupId = c.Int(nullable: false),
                        RerouteBusinessProcessGroupId = c.Int(),
                        DashboardMenuSequence = c.Int(),
                        IsOptionalProcessRequired = c.Boolean(nullable: false),
                        IsNotificationRequired = c.Boolean(nullable: false),
                        TimeOutPeriodBeforeRoleBroadCastDayHourMin = c.Int(),
                        IsApprovalRequired = c.Boolean(nullable: false),
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
                    { "DynamicFilter_RoleBusinessProcessUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_RoleBusinessProcessUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.RoleBusinessProcessId);
            
            CreateTable(
                "dbo.CAPS_BusinessRule",
                c => new
                    {
                        BusinessRuleId = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 100),
                        Caption = c.String(maxLength: 20),
                        DisplaySequence = c.Short(),
                        Notes = c.String(),
                        BusinessRuleCategoryId = c.Short(nullable: false),
                        IsSchema = c.Boolean(nullable: false),
                        SchemaId = c.Int(),
                        IsPreference = c.Boolean(nullable: false),
                        DefaultPreferenceId = c.Int(),
                        IsPrivate = c.Boolean(nullable: false),
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
                    { "DynamicFilter_BusinessRuleUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_BusinessRuleUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.BusinessRuleId);
            
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
            
            CreateTable(
                "dbo.CAPS_BusinessRuleCategory",
                c => new
                    {
                        BusinessRuleCategoryId = c.Short(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 100),
                        Caption = c.String(maxLength: 20),
                        DisplaySequence = c.Short(),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.BusinessRuleCategoryId);
            
            CreateTable(
                "dbo.CAPS_BusinessRuleGroup",
                c => new
                    {
                        BusinessRuleGroupId = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 100),
                        Caption = c.String(maxLength: 20),
                        DisplaySequence = c.Short(),
                        Notes = c.String(),
                        BusinessRuleCategoryId = c.Short(nullable: false),
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
                    { "DynamicFilter_BusinessRuleGroupUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_BusinessRuleGroupUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.BusinessRuleGroupId);
            
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
                .PrimaryKey(t => t.BusinessProcessId);
            
            CreateTable(
                "dbo.CAPS_TypeOfBusinessProcessGroup",
                c => new
                    {
                        TypeOfBusinessProcessGroupId = c.Short(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 100),
                        Caption = c.String(maxLength: 20),
                        DisplaySequence = c.Short(),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.TypeOfBusinessProcessGroupId);
            
            CreateTable(
                "dbo.CAPS_BusinessProcessGroup",
                c => new
                    {
                        BusinessProcessGroupId = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 100),
                        Caption = c.String(maxLength: 20),
                        DisplaySequence = c.Short(),
                        Notes = c.String(),
                        TypeOfBusinessProcessGroupId = c.Short(nullable: false),
                        BusinessProcessGroupCategoryId = c.Int(nullable: false),
                        FormNameId = c.Int(),
                        IsProcessFlowRedirected = c.Boolean(nullable: false),
                        IsLogRequired = c.Boolean(nullable: false),
                        IsPrivate = c.Boolean(nullable: false),
                        IsOptional = c.Boolean(nullable: false),
                        IsNotificationAllowed = c.Boolean(nullable: false),
                        NotificationMessageId = c.Int(),
                        TimeOutPeriodBeforeRoleBroadCastDayHourMin = c.Short(),
                        TimeOutMessageId = c.Int(),
                        IsApprovalAllowed = c.Boolean(nullable: false),
                        ApprovalMessageId = c.Int(),
                        DashBoardGroupId = c.Int(),
                        DashBoardStepId = c.Int(),
                        DashBoardApplicationSequence = c.Short(),
                        IsStandardBrowserRequired = c.Boolean(nullable: false),
                        StandardIconId = c.Short(),
                        WarningIconId = c.Short(),
                        ErrorIconId = c.Short(),
                        IsUserInitiated = c.Boolean(nullable: false),
                        IsMultipleWorkFlowAllowed = c.Boolean(nullable: false),
                        IsWorkFlowNameAssignedByUser = c.Boolean(nullable: false),
                        TestedByUser = c.String(),
                        DateTested = c.DateTime(storeType: "smalldatetime"),
                        ApprovedByUser = c.String(),
                        DateApproved = c.DateTime(storeType: "smalldatetime"),
                        IsActive = c.Boolean(nullable: false),
                        IsPopUp = c.Boolean(nullable: false),
                        IsDashBoardFriendly = c.Boolean(nullable: false),
                        IsSmartPhoneFriendly = c.Boolean(nullable: false),
                        IsReportBpgid = c.Boolean(),
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
                    { "DynamicFilter_BusinessProcessGroupUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_BusinessProcessGroupUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.BusinessProcessGroupId);
            
            CreateIndex("dbo.CAPS_UserRoleBusinessProcess", "BusinessProcessGroupId");
            CreateIndex("dbo.CAPS_RoleBusinessProcess", "BusinessProcessGroupId");
            CreateIndex("dbo.CAPS_BusinessRule", "BusinessRuleCategoryId");
            CreateIndex("dbo.CAPS_BusinessRuleGroup", "BusinessRuleCategoryId");
            CreateIndex("dbo.CAPS_BusinessProcess", "ServiceLevelAgreementId");
            CreateIndex("dbo.CAPS_BusinessProcess", "RunBusinessProcessGroupId");
            CreateIndex("dbo.CAPS_BusinessProcess", "BusinessRuleGroupId");
            CreateIndex("dbo.CAPS_BusinessProcessGroup", "TypeOfBusinessProcessGroupId");
            CreateIndex("dbo.CAPS_BatchReportItem", "BusinessProcessGroupId");
            AddForeignKey("dbo.CAPS_UserRoleBusinessProcess", "BusinessProcessGroupId", "dbo.CAPS_BusinessProcessGroup", "BusinessProcessGroupId", cascadeDelete: true);
            AddForeignKey("dbo.CAPS_RoleBusinessProcess", "BusinessProcessGroupId", "dbo.CAPS_BusinessProcessGroup", "BusinessProcessGroupId", cascadeDelete: true);
            AddForeignKey("dbo.CAPS_BusinessRule", "BusinessRuleCategoryId", "dbo.CAPS_BusinessRuleCategory", "BusinessRuleCategoryId", cascadeDelete: true);
            AddForeignKey("dbo.CAPS_BusinessProcess", "ServiceLevelAgreementId", "dbo.CAPS_ServiceLevelAgreement", "ServiceLevelAgreementId");
            AddForeignKey("dbo.CAPS_BusinessProcess", "BusinessRuleGroupId", "dbo.CAPS_BusinessRuleGroup", "BusinessRuleGroupId");
            AddForeignKey("dbo.CAPS_BusinessRuleGroup", "BusinessRuleCategoryId", "dbo.CAPS_BusinessRuleCategory", "BusinessRuleCategoryId", cascadeDelete: true);
            AddForeignKey("dbo.CAPS_BusinessProcess", "RunBusinessProcessGroupId", "dbo.CAPS_BusinessProcessGroup", "BusinessProcessGroupId");
            AddForeignKey("dbo.CAPS_BatchReportItem", "BusinessProcessGroupId", "dbo.CAPS_BusinessProcessGroup", "BusinessProcessGroupId", cascadeDelete: true);
            AddForeignKey("dbo.CAPS_BusinessProcessGroup", "TypeOfBusinessProcessGroupId", "dbo.CAPS_TypeOfBusinessProcessGroup", "TypeOfBusinessProcessGroupId", cascadeDelete: true);
        }
    }
}
