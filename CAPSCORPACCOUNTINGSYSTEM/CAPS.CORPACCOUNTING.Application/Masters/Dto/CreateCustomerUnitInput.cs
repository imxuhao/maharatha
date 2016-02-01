using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace CAPS.CORPACCOUNTING.Masters.Dto
{
    public class CreateCustomerUnitInput : IInputDto
    {
        [Required]
        [StringLength(CustomerUnit.MaxName)]
        public string LastName { get; set; }

        /// <summary>Gets or sets the FirstName field. </summary>
        [StringLength(CustomerUnit.MaxName)]
        public string FirstName { get; set; }

        /// <summary>Gets or sets the [CustomerNumber] field. </summary>
        [StringLength(CustomerUnit.MaxNumberLength)]
        public string CustomerNumber { get; set; }
        /// <summary>Gets or sets the CustomerNumber field. </summary>
        public decimal? CreditLimit { get; set; }

        /// <summary>
        /// Gets or Sets TypeofPaymentMethodId
        /// </summary>
        public TypeofPaymentMethod TypeofPaymentMethodId { get; set; }

        /// <summary>Gets or sets the Is CustomerPayTermsId field. </summary>
        public int? CustomerPayTermsId { get; set; }

        /// <summary>Gets or sets the Is SalesRepId field. </summary>
        public int? SalesRepId { get; set; }

        /// <summary>Gets or sets the Is IsApproved field. </summary>
        public bool IsApproved { get; set; } = true;

        /// <summary>Gets or sets the Is IsActivet field. </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>Gets or sets the CompanyId field. </summary>
        public long? OrganizationUnitId { get; set; }
        /// <summary>
        ///Gets or Sets the Address
        /// </summary>
        public CreateAddressUnitInput InputAddress { get; set; }
    }
}
