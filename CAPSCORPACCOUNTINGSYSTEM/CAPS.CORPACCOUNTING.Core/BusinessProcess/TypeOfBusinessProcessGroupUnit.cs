using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CAPS.CORPACCOUNTING.BusinessProcess
{

    public enum BusinessProcessCategory
    {
        [Display(Name = "General Ledger")]
        GeneralLedger = 1,
        [Display(Name = "Accounts Payable")]
        AccountsPayable = 2,
        [Display(Name = "Purchase Order")]
        PurchaseOrder = 3,
        [Display(Name = "Accounts Receivable")]
        AccountsReceivable = 4,
        [Display(Name = "Banking")]
        Banking = 5,
        [Display(Name = "Credit Card")]
        CreditCard =6,
        [Display(Name = "Shipping")]
        Shipping = 7,
        [Display(Name = "Global System")]
        GlobalSystem = 8,
        [Display(Name = "Receivables")]
        Receivables = 9,
        [Display(Name = "Petty Cash")]
        PettyCash = 10
    }


    /// <summary>
    /// TypeOfBusinessProcessGroup is the table name in Lajit
    /// </summary>

    [Table("CAPS_TypeOfBusinessProcessGroup")]
  public class TypeOfBusinessProcessGroupUnit
    {

        private const int MaxDescriptionLength = 100;
        private const int MaxCaptionLength = 20;

        /// <summary> virtual the ID column with TypeOfBusinessProcessGroupId field. </summary>
        [Column("TypeOfBusinessProcessGroupId")]
        public virtual short Id { get; set; }

        /// <summary>Gets or sets the Description field. </summary>
        [Required]
        [StringLength(MaxDescriptionLength)]
        public string Description { get; set; }

        /// <summary>Gets or sets the Caption field. </summary>
        [StringLength(MaxCaptionLength)]
        public string Caption { get; set; }

        /// <summary>Gets or sets the DisplaySequence field. </summary>
        public short? DisplaySequence { get; set; }

        /// <summary>Gets or sets the Notes field. </summary>
        public string Notes { get; set; } 

    }
}
