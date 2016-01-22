using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using System;

namespace CAPS.CORPACCOUNTING.Masters.Dto
{
    public class UpdateVendorPaymentTermUnitInput : IInputDto
    {
        /// <summary>Gets or sets the VenorPaymentTermId</summary>
        [Range(1, Int32.MaxValue, ErrorMessage = "Please enter valid VenorPaymentTermId")]
        public int VendorPaymentTermId { get; set; }
        /// <summary>Gets or sets the Description</summary>
        [Required]
        public string Description { get; set; }
        /// <summary>Gets or sets the DueDays. </summary>
        [Range(1, Int32.MaxValue, ErrorMessage = "Please enter valid DueDays")]
        public int DueDays { get; set; }

        /// <summary>Gets or sets the DiscountDays field. </summary>
        public int? DiscountDays { get; set; }

        /// <summary>Gets or sets the IsActive field. </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>Gets or sets the TenantId field. </summary>
        public int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public long? OrganizationUnitId { get; set; }
    }
}
