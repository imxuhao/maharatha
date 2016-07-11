using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAPS.CORPACCOUNTING.Accounting.Dto;
using Abp.AutoMapper;
using CAPS.CORPACCOUNTING.PurchaseOrders;

namespace CAPS.CORPACCOUNTING.PurchaseOrders.Dto
{
    /// <summary>
    /// 
    /// </summary>

    [AutoMapFrom(typeof(PurchaseOrderEntryDocumentDetailUnit))]
    public class PurchaseOrderDetailUnitDto : AccountingItemUnitDto
    {
        /// <summary>Gets or sets the IsPrePaid field. </summary>   
        public  bool IsPrePaid { get; set; }

        /// <summary>Gets or sets the IsPOPurchase field. </summary>   
        public  bool? IsPoPurchase { get; set; }

        /// <summary>Gets or sets the IsPORental field. </summary>   
        public  bool? IsPoRental { get; set; }

        /// <summary>Gets or sets the VendorId field. </summary>   
        public  int? VendorId { get; set; }

        /// <summary>Gets or sets the VendorName field. </summary>   
        public  string VendorName { get; set; }

        /// <summary>Gets or sets the RemainingAmount field. </summary>
        public virtual decimal? RemainingAmount { get; set; }

        /// <summary>Gets or sets the PendingAmount field. </summary>
        public virtual decimal? PendingAmount { get; set; }


    }
}
