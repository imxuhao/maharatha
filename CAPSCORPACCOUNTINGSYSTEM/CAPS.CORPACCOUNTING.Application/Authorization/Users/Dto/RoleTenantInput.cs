using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;

namespace CAPS.CORPACCOUNTING.Authorization.Users.Dto
{
    public class RoleTenantInput :IInputDto
    {
        public int TenantId { get; set; }
        public int RoleId { get; set; }
    }
}
