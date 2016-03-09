using CAPS.CORPACCOUNTING.Accounting;
using CAPS.CORPACCOUNTING.Masters;
using System.ComponentModel.DataAnnotations.Schema;

namespace CAPS.CORPACCOUNTING.AccountReceivable
{
    /// <summary>
    /// CAPS_ArInvoiceEntryDocumentDetail is the new table
    /// </summary>
    [Table("CAPS_ArInvoiceEntryDocumentDetail")]
    public class ArInvoiceEntryDocumentDetailUnit : AccountingItemUnit
    {
        /// <summary>Gets or sets the CustomerID field. </summary>   
        public int? CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public CustomerUnit Customer { get; set; }

        /// <summary>Gets or sets the BillingTypeID field. </summary>   
        public int? BillingTypeId { get; set; }

        /// <summary>Gets or sets the BillToCustomerJobID field. </summary>   
        public int? BillToCustomerJobId { get; set; }
    }
}
