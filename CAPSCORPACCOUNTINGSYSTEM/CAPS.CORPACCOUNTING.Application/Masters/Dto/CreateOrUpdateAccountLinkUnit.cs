using Abp.AutoMapper;

namespace CAPS.CORPACCOUNTING.Masters.Dto
{
    [AutoMapTo(typeof(AccountLinks))]
    public class CreateOrUpdateAccountLinkUnit
    {
        /// <summary>
        /// Gets or sets AccountLinkId field
        /// </summary>
        public long AccountLinkId { get; set; }

        /// <summary>
        /// Gets or sets HomeAccountId field
        /// 
        /// </summary>
        public long? HomeAccountId { get; set; }

        /// <summary>
        /// Gets or sets MapAccountId field
        /// 
        /// </summary>
        public long? MapAccountId { get; set; }
    }
}
