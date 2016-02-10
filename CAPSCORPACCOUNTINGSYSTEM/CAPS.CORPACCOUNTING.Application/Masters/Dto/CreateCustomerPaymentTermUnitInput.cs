using Abp.Application.Services.Dto;
using System;
using System.ComponentModel.DataAnnotations;

namespace CAPS.CORPACCOUNTING.Masters.Dto
{
    public class CreateCustomerPaymentTermUnitInput : IInputDto
    {
        /// <summary>Gets or sets the Description field. </summary>
        [Required]
        [StringLength(CustomerPaymentTermUnit.MaxDesc)]
        public string Description { get; set; }

        /// <summary>Gets or sets the PaymentInstruction field. </summary>
        [StringLength(CustomerPaymentTermUnit.MaxStringLength)]
        public string PaymentInstruction { get; set; }

        /// <summary>Gets or sets the DueDays field. </summary>
        [Range(1, Int32.MaxValue, ErrorMessage = "Please enter valid DueDays")]
        public int DueDays { get; set; }

        /// <summary>Gets or sets the DiscountPercent field. </summary>
        public decimal? DiscountPercent { get; set; }

        /// <summary>Gets or sets the DiscountDays field. </summary>
        public int? DiscountDays { get; set; }

        /// <summary>Gets or sets the OvernightInstructions field. </summary>
        [StringLength(CustomerPaymentTermUnit.MaxStringLength)]
        public string OvernightInstructions { get; set; }

        /// <summary>Gets or sets the WiringInstructions field. </summary>
        [StringLength(CustomerPaymentTermUnit.MaxStringLength)]
        public string WiringInstructions { get; set; }

        /// <summary>Gets or sets the FooterMessage field. </summary>
        [StringLength(CustomerPaymentTermUnit.MaxStringLength)]
        public string FooterMessage { get; set; }

        /// <summary>Gets or sets the LogoCaption field. </summary>
        [StringLength(CustomerPaymentTermUnit.MaxStringLength)]
        public string LogoCaption { get; set; }

        /// <summary>Gets or sets the IsDefault field. </summary>
        public bool IsDefault { get; set; } = true;

        /// <summary>Gets or sets the CompanyId field. </summary>
        public long? OrganizationUnitId { get; set; }
    }
}
