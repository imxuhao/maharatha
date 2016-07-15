using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Organizations;
using CAPS.CORPACCOUNTING.Configuration.Host.Dto;
using CAPS.CORPACCOUNTING.Masters.Dto;
using System.Collections.Generic;

namespace CAPS.CORPACCOUNTING.Organizations.Dto
{
    [AutoMapFrom(typeof(OrganizationUnit))]
    public class HostOrganizationUnitDto : AuditedEntityDto<long>
    {
        public long? ParentId { get; set; }

        public string Code { get; set; }

        public string DisplayName { get; set; }

        public int MemberCount { get; set; }

        public int ConnectionStringId { get; set; }

        public string ConnectionStringName { get; set; }


    }
}