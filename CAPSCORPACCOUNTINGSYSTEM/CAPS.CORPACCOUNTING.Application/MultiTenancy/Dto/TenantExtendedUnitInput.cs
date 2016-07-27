using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using CAPS.CORPACCOUNTING.Authorization.Users.Profile.Dto;
using CAPS.CORPACCOUNTING.Configuration.Host.Dto;
using CAPS.CORPACCOUNTING.Masters.Dto;

namespace CAPS.CORPACCOUNTING.MultiTenancy.Dto
{
    [AutoMapTo(typeof(TenantExtendedUnit))]
    public class TenantExtendedUnitInput : IInputDto
    {
        /// <summary>Gets or sets the TenantExtendedUnitId field. </summary>
        public int TenantExtendedId { get; set; }

        /// <summary>Gets or sets the TransmitterContactName field. </summary>
        [MaxLength(TenantExtendedUnit.MaxLength)]
        public string TransmitterContactName { get; set; }

        /// <summary>Gets or sets the TransmitterEmailAddress field. </summary>
        [MaxLength(TenantExtendedUnit.MaxLength)]
        [EmailAddress]
        public string TransmitterEmailAddress { get; set; }

        /// <summary>Gets or sets the TransmitterCode field. </summary>
        [MaxLength(TenantExtendedUnit.MaxLength)]
        public string TransmitterCode { get; set; }

        /// <summary>Gets or sets the TransmitterControlCode field. </summary>
        [MaxLength(TenantExtendedUnit.MaxLength)]
        public string TransmitterControlCode { get; set; }

        /// <summary>Gets or sets the FederalTaxID field. </summary>
        [StringLength(TenantExtendedUnit.MaxLength)]
        public string FederalTaxId { get; set; }

        public Guid? CompanyLogoId { get; set; }

        /// <summary> Gets or sets Logo  </summary>
        public UpdateProfilePictureInput ComapanyLogo { get; set; }

        /// <summary> Gets or sets eAddress of the Organization. </summary>
        public UpdateAddressUnitInput Address { get; set; }

    }
}
