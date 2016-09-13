using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAPS.CORPACCOUNTING.PurchaseOrders.Dto;

namespace CAPS.CORPACCOUNTING.Accounting.Dto
{
    /// <summary>
    /// OutPutDto of PurchaseOrders with Details
    /// </summary>
    public  class PurchaseOrderEntyDocumnetwithDetailOutputDto :PurchaseOrderDetailUnitDto
    {
        /// <summary>Gets or sets Description</summary>
        public virtual string Description { get; set; }

        /// <summary>Gets or sets DocumentReference</summary>

        public virtual string DocumentReference { get; set; }

        /// <summary>Gets or sets DocumentDate</summary>

        public virtual DateTime? DocumentDate { get; set; }
    }
}
