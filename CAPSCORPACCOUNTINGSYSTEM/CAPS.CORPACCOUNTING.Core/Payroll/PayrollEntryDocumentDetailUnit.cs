using CAPS.CORPACCOUNTING.Accounting;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAPS.CORPACCOUNTING.Payroll
{
    public enum TypeOfPayroll
    {
        [Display(Name = "Earning")]
        Earning = 1,
        [Display(Name = "Fringe")]
        Fringe = 2,
        [Display(Name = "Offset")]
        Offset = 3
    }

    /// <summary>
    /// CAPS_PayrollEntryDocumentDetail is the new table 
    /// </summary>
    [Table("CAPS_PayrollEntryDocumentDetail")]
    public class PayrollEntryDocumentDetailUnit : AccountingItemUnit
    {
        /// <summary>Gets or sets the ValueAddedTaxRecoveryID field. </summary>   
        public int? ValueAddedTaxRecoveryId { get; set; } // 

        /// <summary>Gets or sets the TaxGroupId field. </summary> 
        public int? TaxGroupId { get; set; } 

        /// <summary>Gets or sets the Quantity field. </summary> 
        public decimal? Quantity { get; set; }

        /// <summary>Gets or sets the UnitPrice field. </summary> 
        [Column(TypeName = "Money")]
        public decimal? UnitPrice { get; set; }

        /// <summary>Gets or sets the Weight field. </summary> 
        public decimal? Weight { get; set; }

        /// <summary>Gets or sets the VendorID field. </summary> 
        public int? VendorId { get; set; }

        /// <summary>Gets or sets the EmployeeID field. </summary> 
        public int? EmployeeId { get; set; }

        /// <summary>Gets or sets the EmployeeTaxNumber field. </summary> 
        public string EmployeeTaxNumber { get; set; }

        /// <summary>Gets or sets the TypeOfPayrollID field. </summary> 
        public short? TypeOfPayrollId { get; set; }

        /// <summary>Gets or sets the PayrollTrxInfo field. </summary> 
        public string PayrollTrxInfo { get; set; }

        /// <summary>Gets or sets the PayrollWorkHours field. </summary> 
        public decimal? PayrollWorkHours { get; set; }

        /// <summary>Gets or sets the PayrollDays field. </summary> 
        public decimal? PayrollDays { get; set; }

        /// <summary>Gets or sets the PayrollRate field. </summary> 
        [Column(TypeName = "Money")]
        public decimal? PayrollRate { get; set; }

        /// <summary>Gets or sets the PayrollCheckNumber field. </summary> 
        public string PayrollCheckNumber { get; set; }

        /// <summary>Gets or sets the PayrollCheckDate field. </summary> 

        [Column(TypeName = "smalldatetime")]
        public DateTime? PayrollCheckDate { get; set; }

        /// <summary>Gets or sets the PayrollWorkDate field. </summary> 
        [Column(TypeName = "smalldatetime")]
        public DateTime? PayrollWorkDate { get; set; }

        /// <summary>Gets or sets the PayrollOT field. </summary> 
        public decimal? PayrollOt { get; set; }

        /// <summary>Gets or sets the PayrollFICA field. </summary> 
        public decimal? PayrollFica { get; set; }

        /// <summary>Gets or sets the PayrollFUI field. </summary> 
        public decimal? PayrollFui { get; set; }

        /// <summary>Gets or sets the PayrollSUI field. </summary> 
        public decimal? PayrollSui { get; set; }

        /// <summary>Gets or sets the PayrollWC field. </summary> 
        public decimal? PayrollWc { get; set; }

        /// <summary>Gets or sets the PayrollHF field. </summary> 
        public decimal? PayrollHf { get; set; }
        /// <summary>Gets or sets the PayrollPHW field. </summary> 
        public decimal? PayrollPhw { get; set; }
        /// <summary>Gets or sets the PayrollJob field. </summary> 
        public string PayrollJob { get; set; }

        /// <summary>Gets or sets the PayrollAccount field. </summary> 
        public string PayrollAccount { get; set; }

        /// <summary>Gets or sets the PayrollWeekEnding field. </summary> 

        [Column(TypeName = "smalldatetime")]
        public DateTime? PayrollWeekEnding { get; set; } 

    }
}
