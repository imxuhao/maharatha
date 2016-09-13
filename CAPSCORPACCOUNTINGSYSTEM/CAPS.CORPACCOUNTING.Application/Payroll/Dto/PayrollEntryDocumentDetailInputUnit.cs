using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using CAPS.CORPACCOUNTING.Accounting.Dto;

namespace CAPS.CORPACCOUNTING.Payroll.Dto
{
    /// <summary>
    /// InputDto for PayrollEntryDocumentDetailUnit
    /// </summary>
    [AutoMapTo(typeof(PayrollEntryDocumentDetailUnit))]
    public class PayrollEntryDocumentDetailInputUnit : AccountingItemInputUnit
    {
        /// <summary>Gets or sets the ValueAddedTaxRecoveryID field. </summary>   
        public  int? ValueAddedTaxRecoveryId { get; set; } // 

        /// <summary>Gets or sets the TaxGroupId field. </summary> 
        public  int? TaxGroupId { get; set; }

        /// <summary>Gets or sets the Quantity field. </summary> 
        public  decimal? Quantity { get; set; }

        /// <summary>Gets or sets the UnitPrice field. </summary> 
        public  decimal? UnitPrice { get; set; }

        /// <summary>Gets or sets the Weight field. </summary> 
        public  decimal? Weight { get; set; }

        /// <summary>Gets or sets the VendorID field. </summary> 
        public  int? VendorId { get; set; }

        /// <summary>Gets or sets the EmployeeID field. </summary> 
        public  int? EmployeeId { get; set; }

        /// <summary>Gets or sets the EmployeeTaxNumber field. </summary> 
        public  string EmployeeTaxNumber { get; set; }

        /// <summary>Gets or sets the TypeOfPayrollID field. </summary> 
        public  short? TypeOfPayrollId { get; set; }

        /// <summary>Gets or sets the PayrollTrxInfo field. </summary> 
        public  string PayrollTrxInfo { get; set; }

        /// <summary>Gets or sets the PayrollWorkHours field. </summary> 
        public  decimal? PayrollWorkHours { get; set; }

        /// <summary>Gets or sets the PayrollDays field. </summary> 
        public  decimal? PayrollDays { get; set; }

     
        public  decimal? PayrollRate { get; set; }

        /// <summary>Gets or sets the PayrollCheckNumber field. </summary> 
        public  string PayrollCheckNumber { get; set; }

        /// <summary>Gets or sets the PayrollCheckDate field. </summary> 

       
        public  DateTime? PayrollCheckDate { get; set; }

        /// <summary>Gets or sets the PayrollWorkDate field. </summary> 
      
        public  DateTime? PayrollWorkDate { get; set; }

        /// <summary>Gets or sets the PayrollOT field. </summary> 
        public  decimal? PayrollOt { get; set; }

        /// <summary>Gets or sets the PayrollFICA field. </summary> 
        public  decimal? PayrollFica { get; set; }

        /// <summary>Gets or sets the PayrollFUI field. </summary> 
        public  decimal? PayrollFui { get; set; }

        /// <summary>Gets or sets the PayrollSUI field. </summary> 
        public  decimal? PayrollSui { get; set; }

        /// <summary>Gets or sets the PayrollWC field. </summary> 
        public  decimal? PayrollWc { get; set; }

        /// <summary>Gets or sets the PayrollHF field. </summary> 
        public  decimal? PayrollHf { get; set; }
        /// <summary>Gets or sets the PayrollPHW field. </summary> 
        public  decimal? PayrollPhw { get; set; }
        /// <summary>Gets or sets the PayrollJob field. </summary> 
        public  string PayrollJob { get; set; }

        /// <summary>Gets or sets the PayrollAccount field. </summary> 
        public  string PayrollAccount { get; set; }

        /// <summary>Gets or sets the PayrollWeekEnding field. </summary> 
       
        public  DateTime? PayrollWeekEnding { get; set; }

       

    }
}
