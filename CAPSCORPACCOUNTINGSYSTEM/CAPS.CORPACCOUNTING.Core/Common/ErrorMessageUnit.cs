using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;

namespace CAPS.CORPACCOUNTING.Common
{
    public enum TypeOfError
    {
        [Display(Name = "Critical System Error")]
        CriticalSystemError=1,
        [Display(Name = "Hardware Error")]
        HardwareError = 2,
        [Display(Name = "Application Exception")]
        ApplicationException = 3,
        [Display(Name = "Data Entry")]
        DataEntry = 4
    }

    public enum ErrorCategory
    {
        [Display(Name = "Business Process Engine")]
        BusinessProcessEngine = 1,
        [Display(Name = "Accounts Payable")]
        AccountsPayable = 2,
        [Display(Name = "General Ledger")]
        GeneralLedger = 3,
        [Display(Name = "Generic Errors")]
        GenericErrors = 4
    }

    /// <summary>
    /// ErrorMessage is the table name in Lajit
    /// </summary>
    [Table("CAPS_ErrorMessage")]
    public class ErrorMessageUnit : CreationAuditedEntity
    {
        /// <summary>
        /// MaxLength of the Caption
        /// </summary>
        public const int MaxCaptionLength = 20;

        #region Class Property Declarations

        /// <summary>Overriding the Id column with ErrorMessageId</summary>
        [Column("ErrorMessageId")]
        public override int Id { get; set; }

        /// <summary>
        /// Reference of Lajit IdentityColumn 
        /// </summary>
        public virtual int? LajitId { get; set; }

        /// <summary>Gets or sets the Description field. </summary>
        [Required]
        public virtual string Description { get; set; }

        /// <summary>Gets or sets the Caption field. </summary>

        [StringLength(MaxCaptionLength)]
        public virtual string Caption { get; set; }

        /// <summary>Gets or sets the TypeOfErrorId field. </summary>
        public virtual TypeOfError TypeOfErrorId { get; set; } 

        /// <summary>Gets or sets the ErrorCategoryID field. </summary>
        public virtual ErrorCategory? ErrorCategoryId { get; set; } 
       
        #endregion

    }
}
