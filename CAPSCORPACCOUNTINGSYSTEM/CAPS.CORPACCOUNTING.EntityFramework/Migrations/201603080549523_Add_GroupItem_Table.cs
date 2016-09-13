namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_GroupItem_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_GroupItem",
                c => new
                    {
                        GroupItemId = c.Int(nullable: false, identity: true),
                        GroupTotalId = c.Int(nullable: false),
                        Description = c.String(nullable: false),
                        Caption = c.String(maxLength: 20),
                        Sequence = c.Short(nullable: false),
                        IncludeInSequenceNumber = c.Short(),
                        DisplaySequence = c.Short(),
                        IsSubTotal = c.Boolean(nullable: false),
                        LinkCaption = c.String(maxLength: 50),
                        GroupLayoutStyle = c.String(maxLength: 50),
                        MaintenanceBpgid = c.Int(),
                        IsNegative = c.Boolean(nullable: false),
                        FontName = c.String(maxLength: 100),
                        FontSize = c.String(maxLength: 100),
                        RowColor = c.String(maxLength: 100),
                        BorderLineStyle = c.String(maxLength: 50),
                        BorderWeight = c.String(maxLength: 50),
                        BorderSide = c.String(maxLength: 50),
                        BorderColor = c.String(maxLength: 50),
                        IsItalicized = c.Boolean(),
                        IsBold = c.Boolean(),
                        CaptionIndent = c.Int(),
                        LineGap = c.Int(),
                        XFormula = c.String(maxLength: 500),
                        XFormat = c.String(maxLength: 50),
                        IsRowMandatory = c.Boolean(),
                        BorderLineStyleId = c.Int(),
                        BorderWeightId = c.Int(),
                        BorderSideId = c.Int(),
                        IsJobBreakdown = c.Boolean(),
                        IsSignReversed = c.Boolean(),
                        IsLineConstant = c.Boolean(),
                        IsSubTotalHidden = c.Boolean(),
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
                    { "DynamicFilter_GroupItemUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_GroupItemUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.GroupItemId)
                .ForeignKey("dbo.CAPS_GroupTotal", t => t.GroupTotalId, cascadeDelete: true)
                .Index(t => t.GroupTotalId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_GroupItem", "GroupTotalId", "dbo.CAPS_GroupTotal");
            DropIndex("dbo.CAPS_GroupItem", new[] { "GroupTotalId" });
            DropTable("dbo.CAPS_GroupItem",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_GroupItemUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_GroupItemUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
