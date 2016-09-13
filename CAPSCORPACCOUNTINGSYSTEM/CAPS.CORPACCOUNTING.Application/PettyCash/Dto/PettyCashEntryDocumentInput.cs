using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
using CAPS.CORPACCOUNTING.Accounting;
using CAPS.CORPACCOUNTING.Masters;
using CAPS.CORPACCOUNTING.Payables;
using System.ComponentModel.DataAnnotations;

namespace CAPS.CORPACCOUNTING.PettyCash.Dto
{
    /// <summary>
    /// PettyCashEntryDocument InputDto
    /// </summary>
    [AutoMapTo(typeof(ApHeaderTransactions))]
    public class PettyCashEntryDocumentInput : AccountingHeaderTransactionInputUnit
    {
        #region Declaration of Properties
        /// <summary>Gets or sets the BatchId field. </summary>   
        public int? BatchId { get; set; }

        /// <summary>Gets or sets the PettyCashAccountId field. </summary>   
        [Range(1,Int64.MaxValue)]
        public long PettyCashAccountId { get; set; }
      

        /// <summary>Gets or sets the UploadDocumentLogId field. </summary>   
        public long? UploadDocumentLogId { get; set; }

      
        /// <summary>Gets or sets the ReimbursementAmount field. </summary>   
        public decimal? ReimbursementAmount { get; set; }

        /// <summary>Gets or sets the BatchInfo field. </summary>   
        public string BatchInfo { get; set; }

        /// <summary>Gets or sets the AdvanceAmount field. </summary>   
        public decimal? AdvanceAmount { get; set; }

        /// <summary>Gets or sets the BankAccountId field. </summary>   
        public long? BankAccountId { get; set; }

        /// <summary>Gets or sets the TypeOfPaymentMethodId field. </summary>   
        public TypeofPaymentMethod? TypeOfPaymentMethodId { get; set; }

        ///<summary>Get Sets the PettyCashEntryDocumentDetailList field.</summary>
        public List<PettyCashEntryDocumentDetailInput> PettyCashEntryDocumentDetailList { get; set; }
        #endregion
    }
}
