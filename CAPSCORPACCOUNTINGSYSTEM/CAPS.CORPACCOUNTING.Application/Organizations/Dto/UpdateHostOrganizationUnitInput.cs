using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Organizations;
using CAPS.CORPACCOUNTING.Organization;

namespace CAPS.CORPACCOUNTING.Organizations.Dto
{
    [AutoMapFrom(typeof(OrganizationExtended))]
    public class UpdateHostOrganizationUnitInput : IInputDto
    {
        [Range(1, long.MaxValue)]
        public long Id { get; set; }
        public long? ParentId { get; set; }

        /// <summary>Gets or sets the Organization Name field. </summary>
        [Required(ErrorMessage = "Name fiels is required.")]
        [StringLength(OrganizationUnit.MaxDisplayNameLength)]
        public string DisplayName { get; set; }
      
    }
}