using Abp.AutoMapper;
using CAPS.CORPACCOUNTING.Accounting.Dto;

namespace CAPS.CORPACCOUNTING.PettyCash.Dto
{
    /// <summary>
    /// PettycashDocumentDetail InputDto
    /// </summary>
    [AutoMapTo(typeof(PettyCashEntryDocumentDetailUnit))]
    public class PettyCashEntryDocumentDetailInput : AccountingItemInputUnit
    {
        /// <summary>Gets or sets the PettyCashID field. </summary>   
        public int? PettyCashId { get; set; }

        /// <summary>Gets or sets the PurchaseOrderItemID field. </summary>   
        public long? PurchaseOrderItemId { get; set; }

        /// <summary>Gets or sets the PoHistoryItemId field. </summary>   
        public long? PoHistoryItemId { get; set; }

        /// <summary>Gets or sets the VendorId field. </summary>   
        public int? VendorId { get; set; }
    }
}
