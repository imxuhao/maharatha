﻿using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System.Collections.ObjectModel;

namespace CAPS.CORPACCOUNTING.Masters.Dto
{
    [AutoMapFrom(typeof(CustomerUnit))]  
    public class CustomerUnitDto : IOutputDto
    {
        /// <summary>Gets or sets the CustomerId field.</summary>
        public int CustomerId { get; set; }
       
        /// <summary>Gets or sets the LastName field. </summary>
        public string LastName { get; set; }

        /// <summary>Gets or sets the FirstName field. </summary>
        public string FirstName { get; set; }

        /// <summary>Gets or sets the CustomerNumber field. </summary>
        public string CustomerNumber { get; set; }

        /// <summary>Gets or sets the CreditLimit field. </summary>
        public decimal? CreditLimit { get; set; }

        /// <summary> Gets or Sets TypeofPaymentMethodId field.</summary>
        public TypeofPaymentMethod? TypeofPaymentMethodId { get; set; }

        /// <summary>Gets or sets the Is CustomerPayTermsId field. </summary>
        public int? CustomerPayTermsId { get; set; }

        /// <summary>Gets or sets the Is SalesRepId field. </summary>
        public int? SalesRepId { get; set; }

        /// <summary>Gets or sets the Is IsApproved field. </summary>
        public bool IsApproved { get; set; }

        /// <summary>Gets or sets the Is IsActivet field. </summary>
        public bool IsActive { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public long? OrganizationUnitId { get; set; }

        /// <summary>
        /// Gets or Sets the Address of the Customer.
        /// </summary>
        public Collection<AddressUnitDto> Address { get; set; }

        /// <summary>Gets or sets the PaymentTermDescription field. </summary>
        public string PaymentTermDescription { get; set; }

        /// <summary>Gets or sets the SalesRepName field. </summary>
        public string SalesRepName { get; set; }

        /// <summary>Gets or sets the TotalOutstandingBalance field. </summary>
        public decimal? TotalOutstandingBalance { get; set; }

    }
}
