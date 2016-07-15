using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;

namespace CAPS.CORPACCOUNTING.Authorization.Users.Dto
{
    public class RoleTenantInput :IInputDto
    {
        public int? TenantId { get; set; }

        [Range(1,Int32.MaxValue,ErrorMessage = "Role is required")]
        public int RoleId { get; set; }
    }
}
