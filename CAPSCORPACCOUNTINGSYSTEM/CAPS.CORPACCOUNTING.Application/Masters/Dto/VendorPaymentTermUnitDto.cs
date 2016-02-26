using Abp.Application.Services.Dto;
using  Abp.AutoMapper;

namespace CAPS.CORPACCOUNTING.Masters.Dto
{
    [AutoMapFrom(typeof(VendorPaymentTermUnit))]  
    public class VendorPaymentTermUnitDto : AuditedEntityDto
    {
        /// <summary>Gets or sets the VendorPaymentTermId field.</summary>
        public int VendorPaymentTermId { get; set; }

        /// <summary>Gets or sets the Description field.</summary>
        public string Description { get; set; }
        /// <summary>Gets or sets the DueDays field. </summary>
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
