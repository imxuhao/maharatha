using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPS.CORPACCOUNTING.Authorization.Users.Dto
{
    public class TenantwithRoleDto : IDoubleWayDto
    {
        public int TenantId { get; set; }
        public string TenantName { get; set; }
        public string RoleDisplayName { get; set; }
        public string RoleName { get; set; }
        public int RoleId { get; set; }

        public bool IsRoleSelected { get; set; }

    }

}
