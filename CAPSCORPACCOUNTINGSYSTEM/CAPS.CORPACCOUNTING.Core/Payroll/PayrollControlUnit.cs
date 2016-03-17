using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using CAPS.CORPACCOUNTING.Banking;
using CAPS.CORPACCOUNTING.Masters;

namespace CAPS.CORPACCOUNTING.Payroll
{
    /// <summary>
    /// PayrollControl is the table name in Lajit
    /// </summary>
    [Table("CAPS_PayrollControl")]
    public class PayrollControlUnit : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {
        /// <summary>
        /// Max Length of Description
        /// </summary>
        public const int MaxLength = 100;

        #region Class Property Declarations

        /// <summary>Overriding the ID column with PayrollControlId</summary>
        [Column("PayrollControlId")]
        public override int Id { get; set; }

        /// <summary>
        /// Reference of Lajit IdentityColumn 
        /// </summary>
        public virtual int? LajitId { get; set; }

        /// <summary>Gets or sets the Description field. </summary>
        public string Description { get; set; } 

        /// <summary>Gets or sets the IsActive field. </summary>
        public bool IsActive { get; set; }

        /// <summary>Gets or sets the TypeOfInactiveStatusID field. </summary>
        public TypeOfInactiveStatus? TypeOfInactiveStatusId { get; set; } 

        /// <summary>Gets or sets the ChartOfAccountID field. </summary>
        public int? ChartOfAccountId { get; set; } 
        [ForeignKey("ChartOfAccountId")]
        public CoaUnit Coa { get; set; }

        /// <summary>Gets or sets the OffsetAccountNumber field. </summary>
        public string OffsetAccountNumber { get; set; } 

        /// <summary>Gets or sets the OffsetSubAccountNumber field. </summary>
        public string OffsetSubAccountNumber { get; set; } 

        /// <summary>Gets or sets the OffsetJobID field. </summary>
        public int? OffsetJobId { get; set; }

        /// <summary>Gets or sets the IsGlobalPreference field. </summary>
        public bool? IsGlobalPreference { get; set; }

        /// <summary>Gets or sets the IsPayrollFringeAutoAdjusted field. </summary>
        public bool? IsPayrollFringeAutoAdjusted { get; set; } 

        /// <summary>Gets or sets the IsAPBuildActive field. </summary>
        public bool? IsApBuildActive { get; set; } 

        /// <summary>Gets or sets the IsAPCheckGenerated field. </summary>
        public bool? IsApCheckGenerated { get; set; } 

        /// <summary>Gets or sets the EntityID field. </summary>
        public int? EntityId { get; set; } 

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }
        #endregion

        public PayrollControlUnit()
        {
            IsActive = true;
            IsGlobalPreference = false;
            IsPayrollFringeAutoAdjusted = true;
            IsApBuildActive = true;           
        }
    }
}
