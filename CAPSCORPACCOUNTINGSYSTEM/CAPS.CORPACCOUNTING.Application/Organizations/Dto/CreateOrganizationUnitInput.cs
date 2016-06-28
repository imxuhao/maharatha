using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.Organizations;
using System.Collections.Generic;
using CAPS.CORPACCOUNTING.Masters.Dto;
using CAPS.CORPACCOUNTING.Configuration.Host.Dto;

namespace CAPS.CORPACCOUNTING.Organizations.Dto
{
    public class CreateOrganizationUnitInput : IInputDto
    {
        public long? ParentId { get; set; }

        [Required]
        [StringLength(OrganizationUnit.MaxDisplayNameLength)]
        public string DisplayName { get; set; }

        /// <summary> Gets or sets Address of the Organization. </summary>
        public CreateAddressUnitInput Address { get; set; }

        public OrganizationManagementSettingsEditDto OrganizationSettings { get; set; }
    }
}