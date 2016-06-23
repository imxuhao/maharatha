using System;
using Abp.AutoMapper;
using CAPS.CORPACCOUNTING.Accounting.Dto;

namespace CAPS.CORPACCOUNTING.Journals.dto
{
    [AutoMapFrom(typeof(JournalEntryDocumentDetailUnit))]
    public class JournalEntryDetailUnitDto :  AccountingItemUnitDto
    {
        /// <summary>Gets or sets the VendorId field. </summary>   
        public int? VendorId { get; set; }

        /// <summary>Gets or sets the Vendor field. </summary>   
        public string VendorName { get; set; }

        /// <summary>Gets or sets the PurchaseOrderItemID field. </summary>   
        public long? PurchaseOrderItemId { get; set; }

        /// <summary>Gets or sets the PurchaseOrderItem field. </summary>   
        public string PurchaseOrderItem { get; set; }

        /// <summary>Gets or sets the DebitAccountingItemId field. </summary>   
        public virtual long? DebitAccountingItemId { get; set; }
    }
}
