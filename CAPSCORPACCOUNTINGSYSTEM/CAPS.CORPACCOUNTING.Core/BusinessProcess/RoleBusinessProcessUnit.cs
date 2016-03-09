using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAPS.CORPACCOUNTING.BusinessProcess
{


    /// <summary>
    ///  RoleBusinessProcess is the table name in Lajit
    /// </summary>
    [Table("CAPS_RoleBusinessProcess")]
    public class RoleBusinessProcessUnit : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {
        /// <summary> Overriding the ID column with RoleBusinessProcessId field. </summary>
        [Column("RoleBusinessProcessId")]
        public override int Id { get; set; }

        /// <summary>Gets or sets the RoleId field. </summary>
        public virtual int RoleId { get; set; }
     
        /// <summary>Gets or sets the BusinessProcessGroupId field. </summary>
        public virtual int BusinessProcessGroupId { get; set; }
        [ForeignKey("BusinessProcessGroupId")]
        public virtual BusinessProcessGroupUnit BusinessProcessGroupUnit { get; set; }

        /// <summary>Gets or sets the RerouteBusinessProcessGroupId field. </summary>
        public virtual int? RerouteBusinessProcessGroupId { get; set; }

        /// <summary>Gets or sets the DashboardMenuSequence field. </summary>
        public virtual int? DashboardMenuSequence { get; set; }

        /// <summary>Gets or sets the IsOptionalProcessRequired field. </summary>
        public virtual bool IsOptionalProcessRequired { get; set; }

        /// <summary>Gets or sets the IsNotificationRequired field. </summary>
        public virtual bool IsNotificationRequired { get; set; }

        /// <summary>Gets or sets the TimeOutPeriodBeforeRoleBroadCastDayHourMin field. </summary>
        public virtual int? TimeOutPeriodBeforeRoleBroadCastDayHourMin { get; set; }

        /// <summary>Gets or sets the IsApprovalRequired field. </summary>
        public virtual bool IsApprovalRequired { get; set; }

        /// <summary>Gets or sets the IsActive field. </summary>
        public virtual bool IsActive { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }

    }
}
