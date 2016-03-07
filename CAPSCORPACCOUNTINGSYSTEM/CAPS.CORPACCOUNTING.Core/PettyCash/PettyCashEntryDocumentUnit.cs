using CAPS.CORPACCOUNTING.Accounting;
using CAPS.CORPACCOUNTING.Banking;
using CAPS.CORPACCOUNTING.Masters;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAPS.CORPACCOUNTING.PettyCash
{
    /// <summary>
    /// PettyCashEntryDocument is the table name in Lajit
    /// </summary>
    [Table("CAPS_PettyCashEntryDocument")]
    public class PettyCashEntryDocumentUnit  : AccountingHeaderTransactionsUnit
    {
        #region Declaration of Properties
        /// <summary>Gets or sets the BatchId field. </summary>   
        public virtual int? BatchId { get; set; }

        [ForeignKey("BatchId")]
        public virtual BatchUnit Batch { get; set; }
        
        /// <summary>Gets or sets the PettyCashAccountId field. </summary>   
        public virtual long PettyCashAccountId { get; set; }

        [ForeignKey("PettyCashAccountId")]
        public virtual PettyCashAccountUnit PettyCashAccounts { get; set; }

        /// <summary>Gets or sets the UploadDocumentLogId field. </summary>   
        public virtual long? UploadDocumentLogId { get; set; }

        [ForeignKey("UploadDocumentLogId")]
        public virtual UploadDocumentLogUnit UploadDocumentLog { get; set; }

        /// <summary>Gets or sets the ReimbursementAmount field. </summary>   
        public virtual decimal? ReimbursementAmount { get; set; }

        /// <summary>Gets or sets the BatchInfo field. </summary>   
        public virtual string BatchInfo { get; set; }

        /// <summary>Gets or sets the AdvanceAmount field. </summary>   
        public virtual decimal? AdvanceAmount { get; set; }

        /// <summary>Gets or sets the BankAccountId field. </summary>   
        public virtual long? BankAccountId { get; set; } 

        [ForeignKey("BankAccountId")]
        public virtual BankAccountUnit BankAccount { get; set; }

        /// <summary>Gets or sets the TypeOfPaymentMethodId field. </summary>   
        public virtual TypeofPaymentMethod? TypeOfPaymentMethodId { get; set; }
        #endregion
    }
}
