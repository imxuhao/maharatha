using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace CAPS.CORPACCOUNTING.Authorization.Users.Dto
{
    public class CreateOrUpdateUserInput : IInputDto
    {
        [Required]
        public UserEditDto User { get; set; }

        [Required]
        public string[] AssignedRoleNames { get; set; }

        public bool SendActivationEmail { get; set; }

        public List<int> TenantList { get; set; }
    }
}