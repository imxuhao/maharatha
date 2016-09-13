using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAPS.CORPACCOUNTING.Accounting.Dto;
using Abp.AutoMapper;

namespace CAPS.CORPACCOUNTING.AccountReceivable.Dto
{
    /// <summary>
    /// 
    /// </summary>

    [AutoMapFrom(typeof(ArInvoiceEntryDocumentDetailUnit))]
    public class ArInvoiceEntryDetailUnitDto:AccountingItemUnitDto
    {

        /// <summary>Gets or sets the CustomerID field. </summary>   
        public  int? CustomerId { get; set; }

        /// <summary>Gets or sets the BillingTypeID field. </summary>   
        public  int? BillingTypeId { get; set; }

        /// <summary>Gets or sets the BillingTypeDescription field. </summary>   
        public string BillingTypeDesc { get; set; }

        /// <summary>Gets or sets the BillToCustomerJobID field. </summary>   
        public  int? BillToCustomerJobId { get; set; }

        /// <summary>Gets or sets the BillToCustomerJob field. </summary>   
        public string BillToCustomerJob { get; set; }

    }
}
