
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.GenericSearch.Dto;

namespace CAPS.CORPACCOUNTING.Banking.Dto
{
    public class GetBankAccoutPaymentRangeDto : SearchInputDto
    {
        /// <summary>Gets or Sets BankAccountPaymentRangeId. </summary>
        public int BankAccountId { get; set; }
    }
}
