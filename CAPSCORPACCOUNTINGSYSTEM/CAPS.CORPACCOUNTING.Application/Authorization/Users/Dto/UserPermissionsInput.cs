using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Authorization.Roles.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPS.CORPACCOUNTING.Authorization.Users.Dto
{
    /// <summary>
    /// UserPermissionsInput
    /// </summary>
    public class UserPermissionsInput : IInputDto
    {
        //public CreateOrUpdateUserInput UserInput { get; set; }
        /// <summary>
        /// UserId
        /// </summary>
        public long? UserId { get; set; }

        /// <summary>
        /// Role 
        /// </summary>
        [Required]
        public RoleEditDto Role { get; set; }

        // <summary>
        /// GrantedPermissionNames
        /// </summary>
        [Required]
        public List<string> GrantedPermissionNames { get; set; }

    }
}
