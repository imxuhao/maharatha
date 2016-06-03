using System;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using CAPS.CORPACCOUNTING.Accounting.Dto;

namespace CAPS.CORPACCOUNTING.Journals.dto
{
    [AutoMapTo(typeof(JournalEntryDocumentDetailUnit))]
    public class CreateJournalEntryDocDetailInputUnit :  CreateAccountingItemInputUnit
    {
        /// <summary>Gets or sets the VendorId field. </summary>   
        public int? VendorId { get; set; }

        /// <summary>Gets or sets the VendorId field. </summary>   
        public string VendorName { get; set; }

        /// <summary>Gets or sets the PurchaseOrderItemID field. </summary>   
        public long? PurchaseOrderItemId { get; set; }
    }
}
