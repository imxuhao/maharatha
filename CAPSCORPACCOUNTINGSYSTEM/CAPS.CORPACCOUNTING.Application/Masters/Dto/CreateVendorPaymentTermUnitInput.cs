using Abp.Application.Services.Dto;
using System;
using System.ComponentModel.DataAnnotations;

namespace CAPS.CORPACCOUNTING.Masters.Dto
{
    public class CreateVendorPaymentTermUnitInput : IInputDto
    {
        /// <summary>Gets or sets the Description field.</summary>
        [Required]
        [StringLength(VendorPaymentTermUnit.MaxDesc)]
        public string Description { get; set; }

        /// <summary>Gets or sets the DueDays field. </summary>
        [Range(1, Int32.MaxValue, ErrorMessage = "Please enter valid DueDays")]
        public int DueDays { get; set; }

        /// <summary>Gets or sets the DiscountDays field. </summary>
        public int? DiscountDays { get; set; }

        /// <summary>Gets or sets the IsActive field. </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>Gets or sets the CompanyId field. </summary>
        public long? OrganizationUnitId { get; set; }
    }
}
