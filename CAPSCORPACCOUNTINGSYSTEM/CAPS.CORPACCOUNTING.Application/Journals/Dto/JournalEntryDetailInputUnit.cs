using Abp.AutoMapper;
using CAPS.CORPACCOUNTING.Accounting.Dto;

namespace CAPS.CORPACCOUNTING.Journals.dto
{

    /// <summary>
    /// 
    /// </summary>
    [AutoMapTo(typeof(JournalEntryDocumentDetailUnit))]
    public class JournalEntryDetailInputUnit : AccountingItemInputUnit
    {
        /// <summary>Gets or sets the VendorId field. </summary>   
        public int? VendorId { get; set; }

        /// <summary>Gets or sets the PurchaseOrderItemID field. </summary>   
        public long? PurchaseOrderItemId { get; set; }

        /// <summary>Gets or sets the DebitCreditGroup field. </summary>   
        public string DebitCreditGroup { get; set; }
    }
}
