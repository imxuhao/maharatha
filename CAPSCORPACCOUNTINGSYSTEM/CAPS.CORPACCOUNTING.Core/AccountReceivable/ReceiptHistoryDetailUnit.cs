using CAPS.CORPACCOUNTING.Accounting;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAPS.CORPACCOUNTING.AccountReceivable
{
    /// <summary>
    /// CAPS_ReceiptHistoryDetail is the new table
    /// </summary>
    [Table("CAPS_ReceiptHistoryDetail")]
    public class ReceiptHistoryDetailUnit : AccountingItemUnit
    {
        /// <summary>Gets or sets the CustomerInvoiceId field. </summary>   
        public int? CustomerInvoiceId { get; set; }
    }
}
