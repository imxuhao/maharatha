using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Organizations;
using CAPS.CORPACCOUNTING.Configuration.Host.Dto;
using CAPS.CORPACCOUNTING.Masters.Dto;
using System.Collections.Generic;

namespace CAPS.CORPACCOUNTING.Organizations.Dto
{
    [AutoMapFrom(typeof(OrganizationUnit))]
    public class OrganizationUnitDto : AuditedEntityDto<long>
    {
        public long? ParentId { get; set; }

        public string Code { get; set; }

        public string DisplayName { get; set; }

        public int MemberCount { get; set; }

        /// <summary>Gets or sets the Address of the Organization. </summary>
        public List<AddressUnitDto> Address { get; set; }

        public OrganizationManagementSettingsEditDto OrganizationSettings { get; set; }
    }
}