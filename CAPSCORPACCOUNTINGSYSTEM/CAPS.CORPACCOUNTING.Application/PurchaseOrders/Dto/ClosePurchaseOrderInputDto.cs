using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPS.CORPACCOUNTING.PurchaseOrders.Dto
{
    public class ClosePurchaseOrderInputDto : IInputDto
    {
        /// <summary>Gets or sets the AccountDocumentList field. </summary> 
        public List<long> AccountDocumentList { get; set; }

        /// <summary>Gets or sets the CloseDate field. </summary> 
        public DateTime CloseDate { get; set; }
    
    }
}
