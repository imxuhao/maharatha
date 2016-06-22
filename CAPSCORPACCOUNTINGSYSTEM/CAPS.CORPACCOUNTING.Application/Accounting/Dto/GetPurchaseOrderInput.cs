using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace CAPS.CORPACCOUNTING.Accounting.Dto
{
    /// <summary>
    /// InputDto for Getting PurchaseOrders with Details by VendorId or PurchaseOrderReferences
    /// </summary>
    public class GetPurchaseOrderInput : IInputDto
    {
        /// <summary>
        /// This is requird when we want to search PurchaseOrders by PurchaseOrderReferences
        /// </summary>
        public string PurchaseOrderReferences { get; set; }

        /// <summary>
        /// This is requird when we want to search PurchaseOrders by VendorID
        /// </summary>
        public int VendorId { get; set; }
    }
}
