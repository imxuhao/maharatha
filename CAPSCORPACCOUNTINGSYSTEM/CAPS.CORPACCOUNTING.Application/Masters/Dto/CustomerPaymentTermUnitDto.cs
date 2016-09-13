using Abp.Application.Services.Dto;
using  Abp.AutoMapper;

namespace CAPS.CORPACCOUNTING.Masters.Dto
{
    [AutoMapFrom(typeof(CustomerPaymentTermUnit))]  
    public class CustomerPaymentTermUnitDto : IOutputDto
    {
        /// <summary>Gets or sets the CustomerPaymentTermId field.</summary>
        public int CustomerPaymentTermId { get; set; }

        /// <summary>Gets or sets the Description field. </summary>
        public string Description { get; set; }

        /// <summary>Gets or sets the PaymentInstruction Type. </summary>
        public string PaymentInstruction { get; set; }

        /// <summary>Gets or sets the DueDays field. </summary>
        public int DueDays { get; set; }

        /// <summary>Gets or sets the DiscountPercent field. </summary>
        public decimal? DiscountPercent { get; set; }

        /// <summary>Gets or sets the DiscountDays field. </summary>
        public int? DiscountDays { get; set; }

        /// <summary>Gets or sets the OvernightInstructions field. </summary>
        public string OvernightInstructions { get; set; }

        /// <summary>Gets or sets the WiringInstructions field. </summary>
        public string WiringInstructions { get; set; }

        /// <summary>Gets or sets the FooterMessage field. </summary>
        public string FooterMessage { get; set; }

        /// <summary>Gets or sets the LogoCaption field. </summary>
        public string LogoCaption { get; set; }

        /// <summary>Gets or sets the IsDefault field. </summary>
        public bool IsDefault { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public long? OrganizationUnitId { get; set; }
    }
}
