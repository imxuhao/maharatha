namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_EmailMsgAddress_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_EmailMsgAddress",
                c => new
                    {
                        EmailMsgAddressId = c.Long(nullable: false, identity: true),
                        EmailMsgLogId = c.Long(),
                        EmailAddressId = c.Long(),
                        IsToEmail = c.Boolean(),
                        IsCcEmail = c.Boolean(),
                        IsBccEmail = c.Boolean(),
                        IsFromEmail = c.Boolean(),
                        IsMailDelivered = c.Boolean(),
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
                    { "DynamicFilter_EmailMsgAddressUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_EmailMsgAddressUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.EmailMsgAddressId)
                .ForeignKey("dbo.CAPS_EmailAddress", t => t.EmailAddressId)
                .ForeignKey("dbo.CAPS_EmailMsgLog", t => t.EmailMsgLogId)
                .Index(t => t.EmailMsgLogId)
                .Index(t => t.EmailAddressId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_EmailMsgAddress", "EmailMsgLogId", "dbo.CAPS_EmailMsgLog");
            DropForeignKey("dbo.CAPS_EmailMsgAddress", "EmailAddressId", "dbo.CAPS_EmailAddress");
            DropIndex("dbo.CAPS_EmailMsgAddress", new[] { "EmailAddressId" });
            DropIndex("dbo.CAPS_EmailMsgAddress", new[] { "EmailMsgLogId" });
            DropTable("dbo.CAPS_EmailMsgAddress",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_EmailMsgAddressUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_EmailMsgAddressUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
