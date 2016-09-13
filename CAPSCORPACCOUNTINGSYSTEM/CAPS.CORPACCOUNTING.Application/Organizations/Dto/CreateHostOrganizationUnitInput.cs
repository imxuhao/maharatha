using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Organizations;
using CAPS.CORPACCOUNTING.Organization;

namespace CAPS.CORPACCOUNTING.Organizations.Dto
{
    [AutoMapFrom(typeof(OrganizationExtended))]
    public class CreateHostOrganizationUnitInput : IInputDto
    {
        public long? ParentId { get; set; }

        /// <summary>Gets or sets the Organization Name field. </summary>
        [Required(ErrorMessage = "Name field is required.")]
        [StringLength(OrganizationUnit.MaxDisplayNameLength)]
        public string DisplayName { get; set; }

        /// <summary>Gets or sets the ConnectionStringId Name field. </summary>


        [Required(ErrorMessage = "Please select database.")]
        public int? ConnectionStringId { get; set; }
    }
}