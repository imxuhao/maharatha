using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Organizations;
using CAPS.CORPACCOUNTING.Masters;
using System.ComponentModel.DataAnnotations;

namespace CAPS.CORPACCOUNTING.JobCosting
{
    /// <summary>
    /// JobWrapPayrollLog is the table name in lajit
    /// </summary>
    [Table("CAPS_JobWrapPayrollLog")]
    public class JobWrapPayrollLogUnit : FullAuditedEntity, IMustHaveTenant, IMayHaveOrganizationUnit
    {

        public const int MaxDescLength = 200;        

        #region Class Property Declarations

        /// <summary>Overriding the ID column with JobWrapPayrollLogId</summary>
        [Column("JobWrapPayrollLogId")]
        public override int Id { get; set; }
        
        /// <summary>
        /// Reference of Lajit IdentityColumn 
        /// </summary>
        public virtual int? LajitId { get; set; }


        /// <summary>Gets or sets the JobWrapDocumentLogID field. </summary>
        public int? JobWrapDocumentLogId { get; set; }  

        /// <summary>Gets or sets the JobBudgetID field. </summary>
        public int JobBudgetId { get; set; }

        [ForeignKey("JobBudgetId")]
        public virtual JobBudgetUnit JobBudget { get; set; }

        /// <summary>Gets or sets the AccountID field. </summary>
        public long? AccountId { get; set; }
        [ForeignKey("AccountId")]
        public virtual AccountUnit Account { get; set; }

        /// <summary>Gets or sets the LineInfo field. </summary>
        public string LineInfo { get; set; } 

        /// <summary>Gets or sets the Payee field. </summary>
        public string Payee { get; set; } 

        /// <summary>Gets or sets the PONumber field. </summary>
        public string PoNumber { get; set; }

        /// <summary>Gets or sets the TaxRate field. </summary>
        [Column(TypeName = "Money")]
        public decimal? TaxRate { get; set; }

        /// <summary>Gets or sets the OTbase field. </summary>
        [Column(TypeName = "Money")]
        public decimal? OTbase { get; set; }

        /// <summary>Gets or sets the Days field. </summary>
        [Column(TypeName = "Money")]
        public decimal? Days { get; set; }

        /// <summary>Gets or sets the Rate field. </summary>
        [Column(TypeName = "Money")]
        public decimal? Rate { get; set; }

        /// <summary>Gets or sets the OT1Hours field. </summary>

        [Column(TypeName = "Money")]
        public decimal? Ot1Hours { get; set; }

        /// <summary>Gets or sets the OT2Hours field. </summary>
        [Column(TypeName = "Money")]
        public decimal? Ot2Hours { get; set; }

        /// <summary>Gets or sets the OT3Hours field. </summary>
        [Column(TypeName = "Money")]
        public decimal? Ot3Hours { get; set; } 

        /// <summary>Gets or sets the OtherTaxable field. </summary>
        public decimal? OtherTaxable { get; set; }

        /// <summary>Gets or sets the OtherNonTaxable field. </summary>
        public decimal? OtherNonTaxable { get; set; }

        /// <summary>Gets or sets the TotalST field. </summary>
        public decimal? TotalSt { get; set; } 

        /// <summary>Gets or sets the TotalOT field. </summary>
        public decimal? TotalOt { get; set; }

        /// <summary>Gets or sets the TotalPay field. </summary>
        public decimal? TotalPay { get; set; }

        /// <summary>Gets or sets the Description field. </summary>
        [StringLength(MaxDescLength)]
        public string Description { get; set; } 

        /// <summary>Gets or sets the TotalTax field. </summary>
        public decimal? TotalTax { get; set; } 

        /// <summary>Gets or sets the TenantId field. </summary>
        public virtual int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public virtual long? OrganizationUnitId { get; set; }
        #endregion


    }
}
