using Abp.Application.Services.Dto;
using  Abp.AutoMapper;

namespace CAPS.CORPACCOUNTING.Masters.Dto
{
    [AutoMapFrom(typeof(CustomerPaymentTermUnit))]  
    public class CustomerPaymentTermUnitDto : IOutputDto
    {
        /// <summary>Gets or sets the CustomerPaymentTermId</summary>
        public int CustomerPaymentTermId { get; set; }

        /// <summary>Gets or sets the Description field. </summary>
        public string Description { get; set; }

        /// <summary>Gets or sets the PaymentInstruction Type. </summary>
        public string PaymentInstruction { get; set; }

        /// <summary>Gets or sets the DueDays. </summary>
        public int DueDays { get; set; }

        /// <summary>Gets or sets the DiscountPercent. </summary>
        public decimal? DiscountPercent { get; set; }

        /// <summary>Gets or sets the DiscountDays. </summary>
        public int? DiscountDays { get; set; }

        /// <summary>Gets or sets the OvernightInstructions Type. </summary>
        public string OvernightInstructions { get; set; }

        /// <summary>Gets or sets the WiringInstructions Type. </summary>
        public string WiringInstructions { get; set; }

        /// <summary>Gets or sets the FooterMessage Type. </summary>
        public string FooterMessage { get; set; }

        /// <summary>Gets or sets the LogoCaption Type. </summary>
        public string LogoCaption { get; set; }

        /// <summary>Gets or sets the IsDefault field. </summary>
        public bool IsDefault { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public long? OrganizationUnitId { get; set; }
    }
}
