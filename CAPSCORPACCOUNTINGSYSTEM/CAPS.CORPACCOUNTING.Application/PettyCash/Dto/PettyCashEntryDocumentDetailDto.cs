using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
using CAPS.CORPACCOUNTING.Accounting.Dto;

namespace CAPS.CORPACCOUNTING.PettyCash.Dto
{
    /// <summary>
    /// PettycashDocumentDetail OutputDto
    /// </summary>
    [AutoMapFrom(typeof(PettyCashEntryDocumentDetailUnit))]
    public class PettyCashEntryDocumentDetailDto : AccountingItemUnitDto
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
