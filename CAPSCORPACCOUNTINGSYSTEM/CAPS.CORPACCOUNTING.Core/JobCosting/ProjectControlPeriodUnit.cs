using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using System;
using CAPS.CORPACCOUNTING.Banking;

namespace CAPS.CORPACCOUNTING.JobCosting
{
    /// <summary>
    /// ProjectControlPeriod is the table name in lajit
    /// </summary>
    [Table("CAPS_ProjectControlPeriod")]
    public class ProjectControlPeriodUnit : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {
        #region Class Property Declarations

        /// <summary>Overriding the ID column with ProjectControlPeriodId</summary>
        [Column("ProjectControlPeriodId")]
        public override int Id { get; set; }

        /// <summary>Gets or sets the JobID field. </summary>
        public virtual int JobId { get; set; }
        
        [ForeignKey("JobId")]
        public virtual JobUnit Job { get; set; } 

        /// <summary>Gets or sets the ControlPeriodNumber field. </summary>
        public virtual int? ControlPeriodNumber { get; set; }

        /// <summary>Gets or sets the ControlPeriodDate field. </summary>
        [Column(TypeName = "smalldatetime")]
        public virtual DateTime? ControlPeriodDate { get; set; }

        /// <summary>Gets or sets the PostCutoffDate field. </summary>
        [Column(TypeName = "smalldatetime")]
        public virtual DateTime? PostCutoffDate { get; set; }

        /// <summary>Gets or sets the DateClosed field. </summary>
        [Column(TypeName = "smalldatetime")]
        public virtual DateTime? DateClosed { get; set; }

        /// <summary>Gets or sets the ClosedByUserID field. </summary>
        public virtual int? ClosedByUserId { get; set; }

        /// <summary>Gets or sets the DateReOpened field. </summary>
        [Column(TypeName = "smalldatetime")]
        public virtual DateTime? DateReOpened { get; set; } 

        /// <summary>Gets or sets the ReOpenedByUserID field. </summary>
        public virtual int? ReOpenedByUserId { get; set; } 

        /// <summary>Gets or sets the Description field. </summary>
        public virtual string Description { get; set; } 

        /// <summary>Gets or sets the IsApproved field. </summary>
        public virtual bool IsApproved { get; set; }

        /// <summary>Gets or sets the IsActive field. </summary>
        public virtual bool IsActive { get; set; } 

        /// <summary>Gets or sets the TypeofInactiveStatusID field. </summary>
        public virtual TypeOfInactiveStatus? TypeofInactiveStatusId { get; set; } 

        /// <summary>Gets or sets the FiscalPeriodID field. </summary>
        public virtual int? FiscalPeriodId { get; set; } 

        [ForeignKey("FiscalPeriodId")]
        public FiscalPeriodUnit FiscalPeriod { get; set; }

        /// <summary>Gets or sets the ClosedStatusTypeOfCategoryID field. </summary>
        public virtual int? ClosedStatusTypeOfCategoryId { get; set; } 

        /// <summary>Gets or sets the IsClosed field. </summary>
        public virtual bool? IsClosed { get; set; }

        /// <summary>Gets or sets the PeriodStartDate field. </summary>
        [Column(TypeName = "smalldatetime")]
        public virtual DateTime? PeriodStartDate { get; set; }

        /// <summary>Gets or sets the PeriodEndDate field. </summary>
        [Column(TypeName = "smalldatetime")]
        public virtual DateTime? PeriodEndDate { get; set; } 

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }

        #endregion

        public ProjectControlPeriodUnit()
        {
            IsApproved = false;
            IsActive = true;
        }
    }
}
