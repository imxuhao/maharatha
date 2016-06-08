﻿using Abp.AutoMapper;
using CAPS.CORPACCOUNTING.Accounting.Dto;

namespace CAPS.CORPACCOUNTING.Payables.Dto
{
    [AutoMapTo(typeof(InvoiceEntryDocumentDetailUnit))]
    public class InvoiceEntryDocumentDetailUnitDto : AccountingItemUnitDto
    {
        /// <summary>Gets or sets the PurchaseOrderItemID field. </summary>   
        public virtual long? PurchaseOrderItemId { get; set; }

        /// <summary>Gets or sets the PoHistoryItemId field. </summary>   
        public virtual long? PoHistoryItemId { get; set; }

        /// <summary>Gets or sets the VendorId field. </summary>   
        public virtual int? VendorId { get; set; }
    }
}
