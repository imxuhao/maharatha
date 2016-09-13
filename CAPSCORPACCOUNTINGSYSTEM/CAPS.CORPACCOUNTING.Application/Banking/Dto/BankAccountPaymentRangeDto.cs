using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace CAPS.CORPACCOUNTING.Banking.Dto
{
    [AutoMapFrom(typeof(BankAccountPaymentRangeUnit))]
    public class BankAccountPaymentRangeDto : IOutputDto
    {
        public  int BankAccountPaymentRangeId { get; set; }

        /// <summary>Gets or sets the BankAccountId field. </summary>
        public long BankAccountId { get; set; }

        /// <summary>Gets or sets the StartingPaymentNumber field. </summary>
        
        public int StartingPaymentNumber { get; set; }

        /// <summary>Gets or sets the EndingPaymentNumber field. </summary>
      
        public int EndingPaymentNumber { get; set; }
      

        /// <summary>Gets or sets the CompanyId field. </summary>
        public long? OrganizationUnitId { get; set; }
    }
}
