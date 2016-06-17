using CAPS.CORPACCOUNTING.Accounting.Dto;
using Abp.AutoMapper;
using CAPS.CORPACCOUNTING.PurchaseOrders;

namespace CAPS.CORPACCOUNTING.PurchaseOrders.Dto
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapTo(typeof(PurchaseOrderEntryDocumentDetailUnit))]
    public class PurchaseOrderDetailInputUnit : AccountingItemInputUnit
    {
        /// <summary>Gets or sets the IsPrePaid field. </summary>   
        public  bool IsPrePaid { get; set; }

        /// <summary>Gets or sets the IsPOPurchase field. </summary>   
        public  bool? IsPoPurchase { get; set; }

        /// <summary>Gets or sets the IsPORental field. </summary>   
        public  bool? IsPoRental { get; set; }

        /// <summary>Gets or sets the VendorId field. </summary>   
        public  int? VendorId { get; set; }
    }
}
