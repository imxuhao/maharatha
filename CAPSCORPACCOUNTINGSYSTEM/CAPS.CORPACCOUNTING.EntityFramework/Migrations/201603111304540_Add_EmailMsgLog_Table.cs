namespace CAPS.CORPACCOUNTING.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class Add_EmailMsgLog_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CAPS_EmailMsgLog",
                c => new
                    {
                        EmailMsgLogId = c.Long(nullable: false, identity: true),
                        TypeOfCategoryId = c.Short(),
                        MailSubject = c.String(),
                        MailBody = c.String(),
                        Priority = c.String(maxLength: 10),
                        MailSentDate = c.DateTime(),
                        MailSentStatus = c.String(maxLength: 10),
                        ErrorMessage = c.String(),
                        ObjectId = c.Long(),
                        TrxType = c.Int(),
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
                    { "DynamicFilter_EmailMsgLogUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_EmailMsgLogUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.EmailMsgLogId)
                .ForeignKey("dbo.CAPS_TypeOfCategory", t => t.TypeOfCategoryId)
                .Index(t => t.TypeOfCategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CAPS_EmailMsgLog", "TypeOfCategoryId", "dbo.CAPS_TypeOfCategory");
            DropIndex("dbo.CAPS_EmailMsgLog", new[] { "TypeOfCategoryId" });
            DropTable("dbo.CAPS_EmailMsgLog",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_EmailMsgLogUnit_MustHaveTenant", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                    { "DynamicFilter_EmailMsgLogUnit_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
