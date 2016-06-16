using CAPS.CORPACCOUNTING.Accounting.Dto;
using Abp.AutoMapper;

namespace CAPS.CORPACCOUNTING.AccountReceivable.Dto
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapTo(typeof(ArInvoiceEntryDocumentDetailUnit))]
    public class ArInvoiceEntryDetailInputUnit:AccountingItemInputUnit
    {
        /// <summary>Gets or sets the CustomerID field. </summary>   
        public  int? CustomerId { get; set; }

        /// <summary>Gets or sets the BillingTypeID field. </summary>   
        public  int? BillingTypeId { get; set; }

        /// <summary>Gets or sets the BillToCustomerJobID field. </summary>   
        public  int? BillToCustomerJobId { get; set; }
    }
}
