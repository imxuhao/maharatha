using CAPS.CORPACCOUNTING.Accounting;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAPS.CORPACCOUNTING.Payroll
{  

    /// <summary>
    /// CAPS_PayrollEntryDocumentDetail is the new table 
    /// </summary>
    [Table("CAPS_PayrollEntryDocumentDetail")]
    public class PayrollEntryDocumentDetailUnit : AccountingItemUnit
    {
        /// <summary>Gets or sets the ValueAddedTaxRecoveryID field. </summary>   
        public virtual int? ValueAddedTaxRecoveryId { get; set; } // 

        /// <summary>Gets or sets the TaxGroupId field. </summary> 
        public virtual int? TaxGroupId { get; set; } 

        /// <summary>Gets or sets the Quantity field. </summary> 
        public virtual decimal? Quantity { get; set; }

        /// <summary>Gets or sets the UnitPrice field. </summary> 
        [Column(TypeName = "Money")]
        public virtual decimal? UnitPrice { get; set; }

        /// <summary>Gets or sets the Weight field. </summary> 
        public virtual decimal? Weight { get; set; }

        /// <summary>Gets or sets the VendorID field. </summary> 
        public virtual int? VendorId { get; set; }

        /// <summary>Gets or sets the EmployeeID field. </summary> 
        public virtual int? EmployeeId { get; set; }

        /// <summary>Gets or sets the EmployeeTaxNumber field. </summary> 
        public virtual string EmployeeTaxNumber { get; set; }

        /// <summary>Gets or sets the TypeOfPayrollID field. </summary> 
        public virtual short? TypeOfPayrollId { get; set; }

        [ForeignKey("TypeOfPayrollId")]
        public virtual TypeOfPayrollUnit TypeOfPayroll { get; set; }

        /// <summary>Gets or sets the PayrollTrxInfo field. </summary> 
        public virtual string PayrollTrxInfo { get; set; }

        /// <summary>Gets or sets the PayrollWorkHours field. </summary> 
        public virtual decimal? PayrollWorkHours { get; set; }

        /// <summary>Gets or sets the PayrollDays field. </summary> 
        public virtual decimal? PayrollDays { get; set; }

        /// <summary>Gets or sets the PayrollRate field. </summary> 
        [Column(TypeName = "Money")]
        public virtual decimal? PayrollRate { get; set; }

        /// <summary>Gets or sets the PayrollCheckNumber field. </summary> 
        public virtual string PayrollCheckNumber { get; set; }

        /// <summary>Gets or sets the PayrollCheckDate field. </summary> 

        [Column(TypeName = "smalldatetime")]
        public virtual DateTime? PayrollCheckDate { get; set; }

        /// <summary>Gets or sets the PayrollWorkDate field. </summary> 
        [Column(TypeName = "smalldatetime")]
        public virtual DateTime? PayrollWorkDate { get; set; }

        /// <summary>Gets or sets the PayrollOT field. </summary> 
        public virtual decimal? PayrollOt { get; set; }

        /// <summary>Gets or sets the PayrollFICA field. </summary> 
        public virtual decimal? PayrollFica { get; set; }

        /// <summary>Gets or sets the PayrollFUI field. </summary> 
        public virtual decimal? PayrollFui { get; set; }

        /// <summary>Gets or sets the PayrollSUI field. </summary> 
        public virtual decimal? PayrollSui { get; set; }

        /// <summary>Gets or sets the PayrollWC field. </summary> 
        public virtual decimal? PayrollWc { get; set; }

        /// <summary>Gets or sets the PayrollHF field. </summary> 
        public virtual decimal? PayrollHf { get; set; }
        /// <summary>Gets or sets the PayrollPHW field. </summary> 
        public virtual decimal? PayrollPhw { get; set; }
        /// <summary>Gets or sets the PayrollJob field. </summary> 
        public virtual string PayrollJob { get; set; }

        /// <summary>Gets or sets the PayrollAccount field. </summary> 
        public virtual string PayrollAccount { get; set; }

        /// <summary>Gets or sets the PayrollWeekEnding field. </summary> 

        [Column(TypeName = "smalldatetime")]
        public virtual DateTime? PayrollWeekEnding { get; set; } 

    }
}
