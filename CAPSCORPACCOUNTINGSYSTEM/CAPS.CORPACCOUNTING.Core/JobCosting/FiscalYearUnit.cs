using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using System;
using CAPS.CORPACCOUNTING.Banking;

namespace CAPS.CORPACCOUNTING.JobCosting
{
    /// <summary>
    /// FiscalYear is the table name in lajit
    /// </summary>
    [Table("CAPS_FiscalYear")]
    public class FiscalYearUnit : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {
        #region Class Property Declarations

        /// <summary>Overriding the ID column with FiscalYearId</summary>
        [Column("FiscalYearId")]
        public override int Id { get; set; }

        /// <summary>Gets or sets the YearStartDate field. </summary>
        [Column(TypeName = "smalldatetime")]
        public virtual DateTime YearStartDate { get; set; }

        /// <summary>Gets or sets the YearEndDate field. </summary>
        [Column(TypeName = "smalldatetime")]
        public virtual DateTime YearEndDate { get; set; }

        /// <summary>Gets or sets the IsYearOpen field. </summary>
        public virtual bool IsYearOpen { get; set; }       

        /// <summary>Gets or sets the IsActive field. </summary>
        public virtual bool IsActive { get; set; } 

        /// <summary>Gets or sets the IsApproved field. </summary>
        public virtual bool IsApproved { get; set; }

        /// <summary>Gets or sets the TypeOfInactiveStatusID field. </summary>
        public virtual TypeOfInactiveStatus? TypeOfInactiveStatusId { get; set; }

        /// <summary>Gets or sets the IsCPAClosed field. </summary>
        public virtual bool? IsCpaClosed { get; set; }

        /// <summary>Gets or sets the DateCPAClosed field. </summary>
        [Column(TypeName = "smalldatetime")]
        public virtual DateTime? DateCpaClosed { get; set; } 

        /// <summary>Gets or sets the CPAUserID field. </summary>
        public virtual int? CpaUserId { get; set; } 

        /// <summary>Gets or sets the IsDefaultReportingYear field. </summary>
        public virtual bool? IsDefaultReportingYear { get; set; } 

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }

        #endregion
        public FiscalYearUnit()
        {
            IsYearOpen = false;
            IsActive = true;
            IsApproved = false;            
        }

    }
}
