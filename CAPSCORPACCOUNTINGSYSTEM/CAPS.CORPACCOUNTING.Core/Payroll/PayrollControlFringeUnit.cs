using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using CAPS.CORPACCOUNTING.Accounting;
using System;
using System.ComponentModel.DataAnnotations;

namespace CAPS.CORPACCOUNTING.Payroll
{
    /// <summary>
    /// Enum for TypeOfPayrollFringe
    /// </summary>
    public enum TypeOfPayrollFringe
    {
        [Display(Name = "COA Fringe Allocation")]
        COAFringeAllocation = 1,
        [Display(Name = "Earning")]
        Earning = 2,
        [Display(Name = "Employer Fringe")]
        EmployerFringe = 3,
        [Display(Name = "Payroll Fee")]
        PayrollFee = 4,
        [Display(Name = "Other")]
        Other = 5,
        [Display(Name = "Division Override")]
        DivisionOverride = 6
    }

    /// <summary>
    /// PayrollControlFringe is the table name in lajit
    /// </summary>
    [Table("CAPS_PayrollControlFringe")]
    public class PayrollControlFringeUnit : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {
        /// <summary>
        /// Max Length of Description
        /// </summary>
        public const int MaxLength = 100;

        #region Class Property Declarations

        /// <summary>Overriding the ID column with PayrollControlFringeId</summary>
        [Column("PayrollControlFringeId")]
        public override int Id { get; set; }

        /// <summary>Gets or sets the PayrollControlID field. </summary>
        public virtual int PayrollControlId { get; set; }

        [ForeignKey("PayrollControlId")]
        public virtual PayrollControlUnit PayrollControl { get; set; }

        /// <summary>Gets or sets the TypeOfPayrollFringeID field. </summary>
        public virtual TypeOfPayrollFringe TypeOfPayrollFringeId { get; set; }

        /// <summary>Gets or sets the StartingRange field. </summary>
        [Required]
        public virtual string StartingRange { get; set; }

        /// <summary>Gets or sets the EndingRange field. </summary>
        [Required]
        public virtual string EndingRange { get; set; } 

        /// <summary>Gets or sets the SelectStartRange field. </summary>
        public virtual string SelectStartRange { get; set; } 

        /// <summary>Gets or sets the SelectEndRange field. </summary>
        public virtual string SelectEndRange { get; set; }

        /// <summary>Gets or sets the FringeAccountMask field. </summary>
        public virtual string FringeAccountMask { get; set; } 

        /// <summary>Gets or sets the SubAccountID1 field. </summary>
        public virtual long? SubAccountId1 { get; set; }

        [ForeignKey("SubAccountId1")]
        public virtual SubAccountUnit SubAccount1 { get; set; }

        /// <summary>Gets or sets the SubAccountID2 field. </summary>
        public virtual long? SubAccountId2 { get; set; }

        [ForeignKey("SubAccountId2")]
        public virtual SubAccountUnit SubAccount2 { get; set; }

        /// <summary>Gets or sets the SubAccountID3 field. </summary>
        public virtual long? SubAccountId3 { get; set; }

        [ForeignKey("SubAccountId3")]
        public virtual SubAccountUnit SubAccount3 { get; set; }

        /// <summary>Gets or sets the SubAccountID4 field. </summary>
        public virtual long? SubAccountId4 { get; set; }

        [ForeignKey("SubAccountId4")]
        public virtual SubAccountUnit SubAccount4 { get; set; }

        /// <summary>Gets or sets the SubAccountID5 field. </summary>
        public virtual long? SubAccountId5 { get; set; }

        [ForeignKey("SubAccountId5")]
        public virtual SubAccountUnit SubAccount5 { get; set; }


        /// <summary>Gets or sets the SubAccountID6 field. </summary>
        public virtual long? SubAccountId6 { get; set; }

        [ForeignKey("SubAccountId6")]
        public virtual SubAccountUnit SubAccount6 { get; set; }


        /// <summary>Gets or sets the SubAccountID7 field. </summary>
        public virtual long? SubAccountId7 { get; set; }

        [ForeignKey("SubAccountId7")]
        public virtual SubAccountUnit SubAccount7 { get; set; }


        /// <summary>Gets or sets the SubAccountID8 field. </summary>
        public virtual long? SubAccountId8 { get; set; }

        [ForeignKey("SubAccountId8")]
        public virtual SubAccountUnit SubAccount8 { get; set; }


        /// <summary>Gets or sets the SubAccountID9 field. </summary>
        public virtual long? SubAccountId9 { get; set; }
        [ForeignKey("SubAccountId9")]
        public virtual SubAccountUnit SubAccount9 { get; set; }


        /// <summary>Gets or sets the SubAccountID10 field. </summary>
        public virtual long? SubAccountId10 { get; set; }

        [ForeignKey("SubAccountId10")]
        public virtual SubAccountUnit SubAccount10 { get; set; }

        /// <summary>Gets or sets the IsCorporateDivisonRequired field. </summary>
        public bool? IsCorporateDivisonRequired { get; set; } 

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }
        #endregion

    }
}
