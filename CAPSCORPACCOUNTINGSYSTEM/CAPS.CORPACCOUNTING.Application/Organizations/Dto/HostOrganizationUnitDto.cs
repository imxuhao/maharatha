using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Organizations;

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


        /// <summary>
        /// Gets or Sets ConnectionStringName of ConnectionString
        /// </summary>
        public string ConnectionStringName { get; set; }

        /// <summary>
        /// Gets or Sets ServerName of ConnectionString
        /// </summary>
        public string ServerName { get; set; }

        /// <summary>
        /// Gets or Sets DatabaseName of ConnectionString
        /// </summary>
        public string DatabaseName { get; set; }


    }
}