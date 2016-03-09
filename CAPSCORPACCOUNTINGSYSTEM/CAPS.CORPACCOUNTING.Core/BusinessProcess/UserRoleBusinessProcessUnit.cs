using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CAPS.CORPACCOUNTING.BusinessProcess
{
    /// <summary>
    ///  UserRoleBusinessProcess is the table name in Lajit
    /// </summary>
    [Table("CAPS_UserRoleBusinessProcess")]
    public class UserRoleBusinessProcessUnit : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {

        /// <summary> Overriding the ID column with UserRoleId field. </summary>
        [Column("UserRoleId")]
        public override int Id { get; set; }

        /// <summary>Gets or sets the BusinessProcessGroupId field. </summary>
        public virtual int BusinessProcessGroupId { get; set; }
        [ForeignKey("BusinessProcessGroupId")]
        public virtual BusinessProcessGroupUnit BusinessProcessGroup { get; set; }

        /// <summary>Gets or sets the RerouteBusinessProcessGroupId field. </summary>
        public virtual int? RerouteBusinessProcessGroupId { get; set; }

        /// <summary>Gets or sets the DashBoardMenuSequence field. </summary>
        public virtual int? DashBoardMenuSequence { get; set; }

        /// <summary>Gets or sets the IsBusinessProcessDenied field. </summary>
        public virtual bool IsBusinessProcessDenied { get; set; }

        /// <summary>Gets or sets the IsOptionalProcessRequired field. </summary>
        public virtual bool IsOptionalProcessRequired { get; set; }

        /// <summary>Gets or sets the IsNotificationRequired field. </summary>
        public virtual bool IsNotificationRequired { get; set; }

        /// <summary>Gets or sets the IsApprovalRequired field. </summary>
        public virtual bool IsApprovalRequired { get; set; }

        /// <summary>Gets or sets the TimeOutPeriodBeforeRoleBroadCastDayHourMin field. </summary>
        public virtual int? TimeOutPeriodBeforeRoleBroadCastDayHourMin { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }


    }
}
