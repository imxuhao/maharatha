using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.MultiTenancy.Dto;

namespace CAPS.CORPACCOUNTING.Authorization.Users.Dto
{
    public class TenanltListWithRolesInput : IInputDto
    {
        public int TenantId { get; set; }
        public string TenantName { get; set; }

        public int[] RoleIds { get; set; }
    }
}