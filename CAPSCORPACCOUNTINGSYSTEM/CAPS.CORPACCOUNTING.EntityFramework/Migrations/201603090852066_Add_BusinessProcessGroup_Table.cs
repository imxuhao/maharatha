namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_BusinessProcessGroup_Table : DbMigration
    {
        public override void Up()
        {
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
                .PrimaryKey(t => t.BusinessProcessGroupId)
                .ForeignKey("dbo.CAPS_TypeOfBusinessProcessGroup", t => t.TypeOfBusinessProcessGroupId, cascadeDelete: true)
                .Index(t => t.TypeOfBusinessProcessGroupId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_BusinessProcessGroup", "TypeOfBusinessProcessGroupId", "dbo.CAPS_TypeOfBusinessProcessGroup");
            DropIndex("dbo.CAPS_BusinessProcessGroup", new[] { "TypeOfBusinessProcessGroupId" });
            DropTable("dbo.CAPS_BusinessProcessGroup",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_BusinessProcessGroupUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_BusinessProcessGroupUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
