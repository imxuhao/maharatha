using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAPS.CORPACCOUNTING.Common
{

    public enum TypeOfWorkFlowStatus
    {
        [Display(Name ="New")]
        New=1,
        [Display(Name = "Processing")]
        Processing = 2,
        [Display(Name = "Resumeable")]
        Resumeable = 3,
        [Display(Name = "Suspended")]
        Suspended = 4,
        [Display(Name = "Failed")]
        Failed = 5,
        [Display(Name = "Completed")]
        Completed = 6,
        [Display(Name = "Waiting Approval")]
        WaitingApproval =7
    }

    public enum TypeOfWorkFlow
    {
        [Display(Name = "Background Process")]
        BackgroundProcess = 1,
        [Display(Name = "Process")]
        Processing = 2,
        [Display(Name = "Batch")]
        Batch = 3,
        [Display(Name = "Approval")]
        Approval = 4
    }

    /// <summary>
    ///  WorkFlow is the table name in Lajit
    /// </summary>
    [Table("CAPS_WorkFlow")]
    public class WorkFlowUnit : FullAuditedEntity<long>, IMustHaveTenant, IMayHaveOrganizationUnit
    {

        public const int MaxDescriptionLength = 400;

        /// <summary> Overriding the ID column with WorkFlowId field. </summary>
        [Column("WorkFlowId")]
        public override long Id { get; set; }

        /// <summary>Gets or sets the Description field. </summary>

        [StringLength(MaxDescriptionLength)]
        public virtual string Description { get; set; }

        /// <summary>Gets or sets the TypeOfWorkFlowId field. </summary>
        public virtual TypeOfWorkFlow TypeOfWorkFlowId { get; set; }

        /// <summary>Gets or sets the TypeOfWorkFlowStatusId field. </summary>
        public virtual TypeOfWorkFlowStatus TypeOfWorkFlowStatusId { get; set; }

        /// <summary>Gets or sets the WorkFlowDocument field. </summary>

        [Column(TypeName = "xml")]
        public virtual string WorkFlowDocument { get; set; }

        /// <summary>Gets or sets the CompletedByUserId field. </summary>
        public virtual int? CompletedByUserId { get; set; }

        /// <summary>Gets or sets the DateCompleted field. </summary>
        [Column(TypeName="smalldatetime")]
        public virtual DateTime? DateCompleted { get; set; }

        /// <summary>Gets or sets the RetryCount field. </summary>
        public virtual short? RetryCount { get; set; }

        /// <summary>Gets or sets the EntityId field. </summary>
        public virtual int EntityId { get; set; }

        /// <summary>Gets or sets the RoleId field. </summary>
        public virtual int RoleId { get; set; }

        /// <summary>Gets or sets the IsUrgent field. </summary>
        public virtual bool IsUrgent { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }

        public WorkFlowUnit()
        {
            IsUrgent = false;
        }



    }
}
