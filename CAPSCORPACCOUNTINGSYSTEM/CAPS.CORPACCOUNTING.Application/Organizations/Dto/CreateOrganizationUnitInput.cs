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
        
    }
}