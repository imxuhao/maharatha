using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.Organizations;
using System.Collections.Generic;
using CAPS.CORPACCOUNTING.Masters.Dto;
using CAPS.CORPACCOUNTING.Configuration.Host.Dto;
using CAPS.CORPACCOUNTING.Organization;
using CAPS.CORPACCOUNTING.Authorization.Users.Profile.Dto;

namespace CAPS.CORPACCOUNTING.Organizations.Dto
{
    public class CreateOrganizationUnitInput : IInputDto
    {
        public long? ParentId { get; set; }

        /// <summary>Gets or sets the Organization Name field. </summary>
        [Required]
        [StringLength(OrganizationUnit.MaxDisplayNameLength)]
        public string DisplayName { get; set; }


        /// <summary>Gets or sets the TransmitterContactName field. </summary>
        [MaxLength(OrganizationExtended.MaxLength)]
        public string TransmitterContactName { get; set; }

        /// <summary>Gets or sets the TransmitterEmailAddress field. </summary>
        [MaxLength(OrganizationExtended.MaxLength)]
        [EmailAddress]
        public string TransmitterEmailAddress { get; set; }

        /// <summary>Gets or sets the TransmitterCode field. </summary>
        [MaxLength(OrganizationExtended.MaxLength)]
        public string TransmitterCode { get; set; }

        /// <summary>Gets or sets the TransmitterControlCode field. </summary>
        [MaxLength(OrganizationExtended.MaxLength)]
        public string TransmitterControlCode { get; set; }

        /// <summary>Gets or sets the FederalTaxID field. </summary>
        [StringLength(OrganizationExtended.MaxLength)]
        public string FederalTaxId { get; set; }

        /// <summary> Gets or sets Address of the Organization. </summary>
        public CreateAddressUnitInput Address { get; set; }

        /// <summary> Gets or sets Address of the OrganizationSettings. </summary>
        public OrganizationManagementSettingsEditDto OrganizationSettings { get; set; }

        /// <summary> Gets or sets Logo  </summary>
        public UpdateProfilePictureInput Logo { get; set; }
    }
}