using Abp.Application.Services.Dto;
using Abp.AutoMapper;


namespace CAPS.CORPACCOUNTING.Banking.Dto
{
    [AutoMapFrom(typeof(BankAccountPaymentRangeUnit))]
    public class BankAccountPaymentRangDto : IOutputDto
    {

        /// <summary>Gets or Sets BankAccountPaymentRangeId. </summary>
       
        public int BankAccountPaymentRangeId { get; set; }

        /// <summary>Gets or Sets StartingPaymentNumber. </summary>
      
        public int StartingPaymentNumber { get; set; }
        /// <summary>Gets or Sets EndingPaymentNumber. </summary>
       
        public int EndingPaymentNumber { get; set; }

        /// <summary>Gets or sets the BankAccountId field. </summary>
        public virtual long BankAccountId { get; set; }
        /// <summary>Gets or sets the CompanyId field. </summary>
        public long? OrganizationUnitId { get; set; }


    }
}
