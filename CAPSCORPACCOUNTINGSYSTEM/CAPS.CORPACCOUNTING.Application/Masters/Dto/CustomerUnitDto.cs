using Abp.Application.Services.Dto;
using  Abp.AutoMapper;

namespace CAPS.CORPACCOUNTING.Masters.Dto
{
    [AutoMapFrom(typeof(CustomerUnit))]  
    public class CustomerUnitDto : AuditedEntityDto
    {
        /// <summary>Gets or sets the CustomerId</summary>
        public int CustomerId { get; set; }
        /// <summary>Gets or sets the LastName field. </summary>
        public string LastName { get; set; }

        /// <summary>Gets or sets the FirstName field. </summary>
        public string FirstName { get; set; }

        /// <summary>Gets or sets the [CustomerNumber] field. </summary>
        public string CustomerNumber { get; set; }
        /// <summary>Gets or sets the [CustomerNumber] field. </summary>
        public decimal? CreditLimit { get; set; }

        /// <summary>
        /// Gets or Sets CustomerPaymentTerm
        /// </summary>
        public TypeofPaymentMethod TypeofPaymentMethodId { get; set; }

        /// <summary>Gets or sets the Is CustomerPayTermsId field. </summary>
        public int? CustomerPayTermsId { get; set; }

        /// <summary>Gets or sets the Is SalesRepId field. </summary>
        public int? SalesRepId { get; set; }
        
        public bool IsApproved { get; set; }

        /// <summary>Gets or sets the Is IsActivet field. </summary>
        public bool IsActive { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public long? OrganizationUnitId { get; set; }
    }
}
