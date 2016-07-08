using Abp.AutoMapper;
using CAPS.CORPACCOUNTING.Accounting.Dto;

namespace CAPS.CORPACCOUNTING.Payables.Dto
{
    /// <summary>
    /// InvoiceEntryDocumentDetailUnit OutputDto
    /// </summary>
    [AutoMapFrom(typeof(InvoiceEntryDocumentDetailUnit))]
    public class InvoiceEntryDocumentDetailUnitDto : AccountingItemUnitDto
    {
        /// <summary>Gets or sets the PurchaseOrderItemID field. </summary>   
        public virtual long? PurchaseOrderItemId { get; set; }

        /// <summary>Gets or sets the PoHistoryItemId field. </summary>   
        public virtual long? PoHistoryItemId { get; set; }

        /// <summary>Gets or sets the VendorId field. </summary>   
        public virtual int? VendorId { get; set; }

        /// <summary>Gets or sets the PurchaseOrderReference field. </summary>   
        public string PurchaseOrderReference { get; set; }

        /// <summary>Gets or sets the ActualAmount field. </summary>   
        public decimal ActualAmount { get; set; }
    }
}
