using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using System;
using CAPS.CORPACCOUNTING.Banking;

namespace CAPS.CORPACCOUNTING.Financials.Preferences
{
    /// <summary>
    /// FiscalPeriod is the table name in lajit
    /// </summary>
    [Table("CAPS_FiscalPeriod")]
    public class FiscalPeriodUnit : FullAuditedEntity, IMustHaveTenant, IMustHaveOrganizationUnit
    {
        #region Class Property Declarations

        /// <summary>Overriding the ID column with FiscalPeriodId</summary>
        [Column("FiscalPeriodId")]
        public override int Id { get; set; }
        /// <summary>
        /// Reference of Lajit IdentityColumn 
        /// </summary>
        public virtual int? LajitId { get; set; }

        /// <summary>Gets or sets the FiscalYearID field. </summary>
        public virtual int FiscalYearId { get; set; }

        [ForeignKey("FiscalYearId")]
        public virtual FiscalYearUnit FiscalYear { get; set; }

        /// <summary>Gets or sets the PeriodStartDate field. </summary>
        [Column(TypeName = "smalldatetime")]
        public virtual DateTime PeriodStartDate { get; set; }

        /// <summary>Gets or sets the PeriodEndDate field. </summary>
        [Column(TypeName = "smalldatetime")]
        public virtual DateTime PeriodEndDate { get; set; } 

        /// <summary>Gets or sets the IsPeriodOpen field. </summary>
        public virtual bool IsPeriodOpen { get; set; } 

        /// <summary>Gets or sets the IsActive field. </summary>
        public virtual bool IsActive { get; set; } 

        /// <summary>Gets or sets the TypeOfInactiveStatusID field. </summary>
        public virtual TypeOfInactiveStatus? TypeOfInactiveStatusId { get; set; } 

        /// <summary>Gets or sets the IsCPAClosed field. </summary>
        public virtual bool? IsCpaClosed { get; set; }

        /// <summary>Gets or sets the DateCPAClosed field. </summary>
        [Column(TypeName = "smalldatetime")]
        public virtual DateTime? DateCpaClosed { get; set; } 

        /// <summary>Gets or sets the CPAUserID field. </summary>
        public virtual int? CpaUserId { get; set; } 

        /// <summary>Gets or sets the IsYearEndAdjustmentsAllowed field. </summary>
        public virtual bool? IsYearEndAdjustmentsAllowed { get; set; } 

        /// <summary>Gets or sets the IsPreClose field. </summary>
        public virtual bool? IsPreClose { get; set; } 


        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long OrganizationUnitId { get; set; }

        /// <summary>Gets or sets the MonthYear field. </summary>
        public virtual string MonthYear { get; set; }

        #endregion
        public FiscalPeriodUnit()
        {
            IsPeriodOpen = false;
            IsActive = true;
        }
    }
}
