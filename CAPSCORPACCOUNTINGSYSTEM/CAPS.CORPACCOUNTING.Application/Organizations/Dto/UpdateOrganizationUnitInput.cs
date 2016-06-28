using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.Organizations;
using System.Collections.Generic;
using CAPS.CORPACCOUNTING.Masters.Dto;
using CAPS.CORPACCOUNTING.Configuration.Host.Dto;

namespace CAPS.CORPACCOUNTING.Organizations.Dto
{
    public class UpdateOrganizationUnitInput : IInputDto
    {
        [Range(1, long.MaxValue)]
        public long Id { get; set; }

        [Required]
        [StringLength(OrganizationUnit.MaxDisplayNameLength)]
        public string DisplayName { get; set; }

        /// <summary> Gets or sets eAddress of the Organization. </summary>
        public UpdateAddressUnitInput Address { get; set; }

        public OrganizationManagementSettingsEditDto OrganizationSettings { get; set; }
    }
}